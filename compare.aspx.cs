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
            if (Request.QueryString["q"] == null)
                return;

            List<string> parsedStringsList = parseString(Request.QueryString["q"].ToString());

            //hardconding for testing 
            //List<string> parsedStringsList = new List<string>();
            //parsedStringsList.Add("Egypt");
            //parsedStringsList.Add("Syria");
            //parsedStringsList.Add("");
            //parsedStringsList.Add("ATI_Technologies");


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
            foreach (MicroProfile item in MicroProfilesList)
            {
                answerbox.InnerHtml += "<div class=\"datacell\">" +
                            "<img src=\"" + item.Picture + "\" />" +
                            "<div class=\"title\">" + item.Label + "</div>" +
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
            for (int i = 0; i < numberOfPredicates; i++)
            {
                answerbox.InnerHtml += "<div class=\"comparisonbox\">" +
                "<div class=\"predicate\">" +
                    //this returns the predicate of all the tables
                comparisonOutput[0].FinalComparisonObject[i].Key.Value +
                "</div>";

                answerbox.InnerHtml += "<div class=\"window\">" +
                    "<div class=\"datarow\">";

                for (int j = 0; j < numberOfObjects; j++)
                {
                    answerbox.InnerHtml += "<div class=\"datacell\">";
                    //should be in a href key and value
                    for (int k = 0; k < comparisonOutput[j].FinalComparisonObject[i].Value.ToList().Count; k++)
                    {
                        //we check if it's a uri or not
                        string uri = comparisonOutput[j].FinalComparisonObject[i].Value[k].Key;
                        //if (uri.Contains("http://"))
                        //    answerbox.InnerHtml += "<a href=\"" + uri + ">" + comparisonOutput[j].FinalComparisonObject[i].Value[k].Value + "</a>\n";
                        //else
                        answerbox.InnerHtml += comparisonOutput[j].FinalComparisonObject[i].Value[k].Value + "<br/>";

                    }
                    answerbox.InnerHtml += "</div>";
                }
                answerbox.InnerHtml += "</div>" +
                    "</div>" +
                    "</div>";

            }
            //answerbox.InnerHtml += "</div>" +
            //    "<div class=\"clearfix\">" +
            //    "</div>" +
            //"</div>";

            answerbox.InnerHtml += "</div>";

        }


        List<string> parseString(string input)
        {
            List<string> parsedStringsList = new List<string>();
            //parsedStringsList.Add("the dark knight");
            parsedStringsList.Add("google");
            //parsedStringsList.Add("apple inc");
            parsedStringsList.Add("DELL");
            parsedStringsList.Add("samsung");


            //            return input.Split(' ').ToList();
            return parsedStringsList;
        }


        List<ResourceInformation> preprocessingComparisonObjects(List<ResourceInformation> input)
        {
            //remove label
            //remove duplicates
            //remove duplicates of ontology and property 
            //fix bugs..

            List<ResourceInformation> toReturn = new List<ResourceInformation>();
            ResourceInformation temp = new ResourceInformation();



            int numberofPredicates = input[0].FinalComparisonObject.ToList().Count;
            for (int i = 0; i < input.Count; i++)
            {
                for (int j = 0; j < input[i].FinalComparisonObject.ToList().Count; j++)
                {

                    for (int k = j + 1; k < input[i].FinalComparisonObject.ToList().Count; k++)
                    {
                        if (input[i].FinalComparisonObject.ToList()[j].Key.Value.Equals(input[i].FinalComparisonObject.ToList()[k].Key.Value))
                        {
                            input[i].FinalComparisonObject.ToList().RemoveAt(j);
                        }

                        //    if (input[i].FinalComparisonObject.ToList()[j].Key.Value.Equals(input[k].FinalComparisonObject.ToList()[j].Key.Value))
                        //    {
                        //        input[i].FinalComparisonObject.ToList()

                        //    }
                    }

                }
            }



            return input;
        }
    }
}