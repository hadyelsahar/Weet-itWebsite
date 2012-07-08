using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using weetit_website.ServiceReference1;
namespace weetit_website
{
    public partial class compare : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //if there's no query, return the original page (this case should never happen)
            try
            {
                if (Request.QueryString["q"] == null)
                {
                   // answerbox.InnerHtml = "<h3> No parameters to compare between... </h3>";
                    return;
                }
                else
                {


                    //parsing the parameters
                    List<string> parsedStringsList = parseString(Request.QueryString["q"].ToString());

                    //the outputURIs
                    List<string> outputURIs = new List<string>();

                    //using the keyword search to get the URIs
                    keywordSearchServiceInterfaceClient kwClient = new keywordSearchServiceInterfaceClient();
                    foreach (string item in parsedStringsList)
                    {
                        outputURIs.Add(kwClient.geturi_bestMatch(item));

                    }


                    //comparing between the translated uris
                    List<ResourceInformation> comparisonOutput = new List<ResourceInformation>();
                    ComparisonServiceInterfaceClient comparisonClient = new ComparisonServiceInterfaceClient();
                    comparisonOutput = comparisonClient.Compare(outputURIs.ToArray()).ToList();


                    // comparisonOutput = preprocessingComparisonObjects(comparisonOutput);

                    //getting the micro profiles
                    ProfileConstructorInterfaceClient constructProfile = new ProfileConstructorInterfaceClient();
                    List<MicroProfile> MicroProfilesList = new List<MicroProfile>();

                    foreach (string item in outputURIs)
                    {
                        MicroProfilesList.Add((MicroProfile)constructProfile.ConstructProfile(item, MergedServicechoiceProfile.micro, 10));

                    }


                    //output to the divs
                    //First the microProfile
                    answerbox.InnerHtml = "";
                    answerbox.InnerHtml +=
                        "<div class=\"comparisonbox tableheader\">" +
                        "<div class=\"predicate\">" +
                            "<p></p>" +
                        "</div>" +
                        "<div class=\"window\">" +
                            "<div class=\"datarow\">";

                    //this was added to handle the maps if available


                    foreach (MicroProfile item in MicroProfilesList)
                    {
                        answerbox.InnerHtml += "<div class=\"datacell\">" +
                                    "<a href=\"answer.aspx?uri=" + item.URI + " \"><img src=\"" + item.Picture + "\" />  </a>" +
                                    "<div class=\"title\"> <a href=\"answer.aspx?uri=" + item.URI + "\">" + item.Label + "</a></div>" +
                            //"<div class=\"Abstract\">" + item.Abstract + "</div>" +
                                "</div>";

                    }

                    answerbox.InnerHtml += "</div>" +
                        "</div>" +
                        "<div class=\"clearfix\">" +
                        "</div>" +
                    "</div>";

                    //right till now
                    //will crash if no comparison........


                    //now for the data for each entity
                    int numberOfPredicates = comparisonOutput[0].FinalComparisonObject.ToList().Count;
                    int numberOfObjects = comparisonOutput.Count;
                    //Second the table values

                    ////////////////THE MAPS PART///////////
                    if (canViewMap(comparisonOutput))
                    {
                        answerbox.InnerHtml += "<div class=\"comparisonbox\">" +
                        "<div class=\"predicate\">" +
                            //this returns the predicate of all the tables
                        "Map" +
                        "</div>";

                        answerbox.InnerHtml += "<div class=\"window\">" +
                            "<div class=\"datarow\">";
                        List<string> mapstrings = viewMap(comparisonOutput);
                        foreach (string item in mapstrings)
                        {
                            answerbox.InnerHtml += "<div class=\"datacell expandableContent\">";
                            answerbox.InnerHtml += item;
                            answerbox.InnerHtml += "</div>";

                        }
                        answerbox.InnerHtml += "</div>" +
                            "</div>" +
                            "</div>";
                    }
                    ////////////////////////////////////////////////////////


                    //comparisonOutput = removeUnwantedPredicates(comparisonOutput.ToList()).ToList();

                    for (int i = 0; i < numberOfPredicates; i++)
                    {
                        //check if the predicate is of the ones that should be excluded 
                        if (isExcludedURI(comparisonOutput[0].FinalComparisonObject[i].Key.Key))
                            continue;



                        answerbox.InnerHtml += "<div class=\"comparisonbox\">" +
                        "<div class=\"predicate\">" +
                            //this returns the predicate of all the tables
                        comparisonOutput[0].FinalComparisonObject[i].Key.Value +
                        "</div>";

                        answerbox.InnerHtml += "<div class=\"window\">" +
                            "<div class=\"datarow\">";


                        for (int j = 0; j < numberOfObjects; j++)
                        {
                            answerbox.InnerHtml += "<div class=\"datacell expandableContent\">";
                            //should be in a href key and value
                            for (int k = 0; k < comparisonOutput[j].FinalComparisonObject[i].Value.ToList().Count; k++)
                            {
                                //we check if it's a uri or not
                                string uri = comparisonOutput[j].FinalComparisonObject[i].Value[k].Key;
                                string value = HttpUtility.HtmlDecode(comparisonOutput[j].FinalComparisonObject[i].Value[k].Value);
                                if (Uri.IsWellFormedUriString(uri, UriKind.Absolute))
                                {
                                    if (Uri.IsWellFormedUriString(value, UriKind.Absolute))
                                    {
                                        answerbox.InnerHtml += "<span><a href=\"" + uri + "\">" + value + "</a><br/></span>";
                                    }
                                    else
                                    {
                                        answerbox.InnerHtml += "<span><a href=\"answer.aspx?uri=" + uri + "\">" + value + "</a><br/></span>";
                                    }

                                }

                                else
                                {
                                    answerbox.InnerHtml += "<span>" + value + "<br/></span>";
                                }

                            }
                            answerbox.InnerHtml += "</div>";
                        }
                        answerbox.InnerHtml += "</div>" +
                            "</div>" +
                            "</div>";

                    }

                    answerbox.InnerHtml += "</div>";

                }
            }
            catch (Exception c)
            {

                //logging should go here
                Util.log("Comparison>> Message: " + c.Message + " Data: " + c.Data);
                Response.Redirect("error.aspx", false);

            }
        }


        
        List<string> parseString(string input)
        {
            List<string> parsedStringsList = new List<string>();
            parsedStringsList = input.Split(',').ToList();

            for (int i = 0; i < parsedStringsList.Count; i++)
            {
                parsedStringsList[i] = HttpUtility.HtmlDecode(parsedStringsList[i]);
            }            
            return parsedStringsList;
        }


        /// <summary>
        /// returns whether or not these comparison has maps included
        /// </summary>
        /// <param name="input">the list of resource information to see</param>
        /// <returns>true or false</returns>
        bool canViewMap(List<ResourceInformation> input)
        {
            bool longAvailable=false;
            bool latAvailable=false;
            foreach (ResourceInformation item in input)
            {
                foreach (var item2 in item.FinalComparisonObject)
                {
                    if (item2.Key.Key.Equals("http://www.w3.org/2003/01/geo/wgs84_pos#lat"))
                        latAvailable = true;
                    if (item2.Key.Key.Equals("http://www.w3.org/2003/01/geo/wgs84_pos#long"))
                        longAvailable = true;
                }
            }

            //if both are available
            if (longAvailable && latAvailable)
                return true;
            else
                return false;
        }

        /// <summary>
        /// to return the view string of the map
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        List<string> viewMap(List<ResourceInformation> input)
        {
            List<string> toreturn = new List<string>();
            //<img src="http://maps.googleapis.com/maps/api/staticmap?center=-15.800513,-47.91378&zoom=11&size=200x200&sensor=false">
            
            //fetching the string to draw the map from every resource information
            foreach (ResourceInformation item in input)
            {
                string temp = "<img src=\"http://maps.googleapis.com/maps/api/staticmap?center=";
                string lat = item.FinalComparisonObject.ToList().Find(e => e.Key.Key.Equals("http://www.w3.org/2003/01/geo/wgs84_pos#lat")).Value[0].Value.ToString();
                temp += lat;
                //temp+=item.FinalComparisonObject.First(e=>e.Key.Key.
                temp += ",";
                string longt = item.FinalComparisonObject.ToList().Find(e => e.Key.Key.Equals("http://www.w3.org/2003/01/geo/wgs84_pos#long")).Value[0].Value.ToString();
                temp += longt;
                temp += "&zoom=10&size=400x400&sensor=false\" title=\""+"longtitude="+longt+"\nlatitude="+lat+"\">";
                toreturn.Add(temp);
            }
            return toreturn;

            
 
        }



        bool isExcludedURI(string input)
        {
            List<string> excludedURIs = new List<string>();
            excludedURIs.Add("http://www.w3.org/2003/01/geo/wgs84_pos#long");
            excludedURIs.Add("http://www.w3.org/2003/01/geo/wgs84_pos#lat");
            excludedURIs.Add("http://www.w3.org/2003/01/geo/wgs84_pos#point");
            excludedURIs.Add("http://xmlns.com/foaf/0.1/name");
            excludedURIs.Add("http://www.w3.org/2000/01/rdf-schema#label");

            return excludedURIs.Contains(input);
 
        }
    }
}