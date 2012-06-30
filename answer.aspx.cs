using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using weetit_website.ServiceReference1;
using weetit_website.classes;

namespace weetit_website
{
    public partial class answer : System.Web.UI.Page
    {
        public answer()
        {



        }

        protected void Page_Load(object sender, EventArgs e)
        {
            String question = Request.QueryString["q"];
            String HTML = "";
            if (question != null)
            {
                List<List<Profile>> diffProfiles = QuestionAnswerer.getAnswersInProfiles(question);
                foreach (List<Profile> profiles in diffProfiles)
                {
                    foreach (Profile profile in profiles)
                    {
                        if (profile is FullProfile)
                        {
                            FullProfile fullProfile = (FullProfile)profile;
                            HTML=showFullProfile(fullProfile);
                        }
                        else if (profile is MiniProfile)
                        {
                            MiniProfile miniProfile = (MiniProfile)profile;
                            HTML = showMiniProfie(miniProfile);
                            
                        }
                        else if (profile is MicroProfile)
                        {
                            MicroProfile microProfile = (MicroProfile)profile;
                            HTML = showMicroProfile(microProfile);
                        }
                        else if (profile is LiteralProfile)
                        {
                            LiteralProfile LP = (LiteralProfile)profile;
                            HTML = showLiteralProfile(LP);
                        }
                    }
                }
            }
            String uri = Request.QueryString["uri"];
            if (uri != null)
            {
                ProfileConstructorInterfaceClient PCIClient = new ProfileConstructorInterfaceClient();
                FullProfile fullProfile=(FullProfile)PCIClient.ConstructProfile(uri, MergedServicechoiceProfile.full, 50);
                HTML = showFullProfile(fullProfile);
            }
            answerbox.InnerHtml = HTML;
        }

        private String showFullProfile(FullProfile fullProfile)
        {
            String HTML = "";
            String relatedImg = "";
            String details = "";
            foreach (KeyValuePair<String, Entity[]> key in fullProfile.Details.ToList())
            {
                if (key.Value.Length != 0)
                {
                    details += "<tr>"
                            + "<td class=\"property\">"
                                + key.Key
                            + "</td>"
                            + "<td class=\"value\">";
                    foreach (Entity en in key.Value)
                        details += "<div class=\"line\">"
                                    + en.Label
                                + "</div>";
                    details += "</td>"
                            + "</tr>";
                }
            }
            foreach (Entity rel in fullProfile.Related.ToList())
                relatedImg += "<img src=\"" + rel.Picture + "\"/>";
            HTML += "<div class=\"fullprofile\">"
                    + "<div class=\"abstractcontainer\">"
                        + "<div class=\"profilepic\">"
                            + "<img src=\"" + fullProfile.Picture + "\" />"
                        + "</div>"
                        + "<div class=\"abstract\">"
                            + "<a href=\"#\" class=\"title\">" + fullProfile.Label + "</a>"
                            + "<p class=\"abstracttext\">"
                                + fullProfile.Abstract
                            + "</p>"
                        + "</div>"
                        + "<div class=\"clearfix\">"
                        + "</div>"
                    + "</div>"
                    + "<div class=\"relatedresults\">"
                        + "<div class=\"subtitle\">"
                            + "Related results</div>"
                        + relatedImg
                    + "</div>"
                    + "<div class=\"profiledetails\">"
                        + "<div class=\"subtitle\">"
                            + "Profile details</div>"
                        + "<table class=\"detailstable\">"
                            + details
                        + "</table>"
                    + "</div>"
                + "</div>";
            return HTML;
        }

        private String showMiniProfie(MiniProfile miniProfile)
        {
            String HTML = "";
            String details = "";
            foreach (KeyValuePair<String, Entity[]> key in miniProfile.Details.ToList())
            {
                if (key.Value.Length != 0)
                {
                    details += "<tr>"
                            + "<td class=\"property\">"
                                + key.Key
                            + "</td>"
                            + "<td class=\"value\">";
                    foreach (Entity en in key.Value)
                        details += "<div class=\"line\">"
                                    + en.Label
                                + "</div>";
                    details += "</td>"
                            + "</tr>";
                }
            }
            HTML += "<div class=\"miniprofile\">"
                        + "<div class=\"minileft\">"
                            + "<img src=\"" + miniProfile.Picture + "\" />"
                        + "</div>"
                        + "<div class=\"miniright\">"
                            + "<div>"
                                + "<a class=\"title\" href=\"#\">" + miniProfile.Label + "</a>"
                            + "</div>"
                        + "<div class=\"abstract\">"
                            + miniProfile.Abstract
                        + "</div>"
                        + "<div class=\"minitable\">"
                            + "<table class=\"detailstable\">"
                                + details
                            + "</table>"
                        + "</div>"
                    + "</div>"
                    + "<div class=\"clearfix\">"
                    + "</div>"
                + "</div>";
            return HTML;
        }

        private String showMicroProfile(MicroProfile microProfile)
        {
            String HTML = "";
            HTML += "<div class=\"microprofile\">"
                                    + "<div class=\"left\">"
                                        + "<img src=\"" + microProfile.Picture + "\" />"
                                    + "</div>"
                                    + "<div class=\"right\">"
                                        + "<div>"
                                            + "<a class=\"title\" href=\"#\">" + microProfile.Label + "</a>"
                                        + "</div>"
                                        + "<div class=\"abstract\">"
                                            + microProfile.Abstract
                                        + "</div>"
                                    + "</div>"
                                    + "<div class=\"clearfix\">"
                                    + "</div>"
                                + "</div>";
            return HTML;
        }

        private String showLiteralProfile(LiteralProfile literalProfile)
        {
            String HTML="";
            HTML += "<div class=\"miniprofile literalanswer\">"
                                            + "<div class=\"minileft\">"
                                                + "<img src=" + literalProfile.imageURI + "/>"
                                            + " </div>"
                                            + "<div class=\"miniright\">"
                                                + " <div>"
                                                    + "<a class=\"title\" href=\"#\">" + literalProfile.subjectLabel + "</a>"
                                                + "</div>"
                                                + "<div class=\"minitable\">"
                                                    + "<table class=\"detailstable\">"
                                                        + "<tr>"
                                                            + "<td class=\"property\">"
                                                                + literalProfile.PredicateLabel
                                                            + "</td>"
                                                            + "<td class=\"value\">"
                                                                + literalProfile.objectValue + " " + literalProfile.objectUnit
                                                            + "</td>"
                                                        + "</tr>"
                                                    + "</table>"
                                                + "</div>"
                                            + "</div>"
                                            + "<div class =\"clearfix\">"
                                            + "</div>"
                                        + "</div>";
            return HTML;
        }
    }
}