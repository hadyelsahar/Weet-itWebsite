using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using weetit_website.ServiceReference1;

namespace weetit_website
{
    public partial class relate : System.Web.UI.Page
    {
        private List<string> parseString(string input)
        {


            List<string> parsedStringsList = new List<string>();
            parsedStringsList = input.Split(',').ToList();

            for (int i = 0; i < parsedStringsList.Count; i++)
            {
                parsedStringsList[i] = HttpUtility.HtmlDecode(parsedStringsList[i]);
            }
            return parsedStringsList;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                if (Request.QueryString["q"] != null)
                {
                    List<string> inputKeyWords = parseString(Request.QueryString["q"].ToString());
                    List<string> inputKeyWordsURI = new List<string>();
                    List<string> microHtmlPic = new List<string>();
                    List<string> microHtmlLabel = new List<string>();
                    string HTML;
                    keywordSearchServiceInterfaceClient keyWordsClient = new keywordSearchServiceInterfaceClient();

                    foreach (string keyword in inputKeyWords)
                    {
                        inputKeyWordsURI.Add(keyWordsClient.geturi_bestMatch(keyword));
                    }

                    ProfileConstructorInterfaceClient profileClient = new ProfileConstructorInterfaceClient();
                    List<MicroProfile> profiles = new List<MicroProfile>();
                    foreach (string keyword in inputKeyWordsURI)
                    {
                        profiles.Add((MicroProfile)profileClient.ConstructProfile(keyword, MergedServicechoiceProfile.micro, 1));
                    }

                    List<string> htmlTable = new List<string>();//<tr><td><div class="imgcontainer"><img src="testimg/220px-WikiBex.jpg" /></div></td></tr><tr><td class="title">hany</td></tr>
                    HTML = "<table class=\"relateTable\"><tr>";
                    foreach (MicroProfile microProfile in profiles)
                    {
                        HTML += "<td><div class=\"imgcontainer\"><a href=\"answer.aspx?uri=" + microProfile.URI + "\"><img src=\"" + microProfile.Picture + "\" /></a></div></td>";
                    }
                    HTML += "</tr><tr>";
                    foreach (MicroProfile microProfile in profiles)
                    {
                        HTML += "<td class=\"title\"><a href=\"answer.aspx?uri=" + microProfile.URI + "\">" + microProfile.Label + "</a></td>";
                    }
                    HTML += "</tr></table>";
                    HTML += "<script>var mainNodes = \"" + string.Join(",", inputKeyWordsURI.ToArray()) + "\"; </script>";
                    headerTable.InnerHtml = HTML;
                }
                //if nothing is sent in q parameter
                else
                {
                    //headerTable.InnerHtml = "<h2>There were no parameters to relate between</h2>"; 
                    answerbox.InnerHtml = "<h2>There were no parameters to relate between.....</h2>";

                }
            }
            catch (Exception c)
            {
                Util.log("RELATION ERROR" + c.Message);
                Response.Redirect("error.aspx");
                
            }
        }

        [WebMethod(EnableSession = false)]
        public static string getRelation(string URI, string distance)
        {
            int limit = 10;
            RelationGeneratorServiceInterfaceClient myclient = new RelationGeneratorServiceInterfaceClient();
            string[] uris = URI.Split(',');
            Relation[] relations = myclient.getRelationWithLabels(uris, Int32.Parse(distance), limit);

            char qoute = '"';
            string json = "[";
            foreach (Relation rel in relations)
            {

                json += "[";
                foreach (RelationEntity entity in rel.entities)
                {
                    json += "{" + qoute + "name" + qoute + ":" + qoute + entity.label + qoute + "," + qoute + "uri" + qoute + ":" + qoute + entity.uri + qoute + "},"; //update when available

                }
                json = json.Remove(json.Length - 2);
                json += "}],";

            }

            json = json.Remove(json.Length - 2) + "]]";

            return json;
        }

    }
}
