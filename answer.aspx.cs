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
            try
            {
                String question = Request.QueryString["q"];
                String uri = Request.QueryString["uri"];
                String HTML = "";

                if (question != null && uri == null)
                {
                    List<KeyValuePair<questionAnswer, List<Profile>>> QNDiffProfiles = QuestionAnswerer.getAnswersInProfiles(question);
                    if (QNDiffProfiles.Count == 0)
                        HTML += "<div> No results found </div>";
                    if (QNDiffProfiles.Count > 1)
                    {
                        HTML += "<ul id=\"menu\" class=\"menu\">";
                        for (int x = 0; x < QNDiffProfiles.Count; x++)
                        {
                            questionAnswer answer = QNDiffProfiles[x].Key;
                            if (x == 0)
                                HTML += "<li class=\"active\"><a href=\"#tab" + x + "\">" + answer.predicateLabelList.Keys.ToList()[0] + " of " + answer.subjectLabelList.Keys.ToList()[0] + "</a></li>";
                            else
                                HTML += "<li><a href=\"#tab" + x + "\">" + answer.predicateLabelList.Keys.ToList()[0] + " of " + answer.subjectLabelList.Keys.ToList()[0] + "</a></li>";
                        }
                        HTML += "</ul>";
                    }
                    
                    foreach (KeyValuePair<questionAnswer, List<Profile>> QNProfiles in QNDiffProfiles)
                    {
                        questionAnswer answer = QNDiffProfiles[0].Key;
                        List<Profile> profiles = QNProfiles.Value;
                        bool isCountAnswer = false;
                        if (QNProfiles.Key.questiontype == utilquestionTypes.countAnswer)
                            isCountAnswer = true;
                        if (QNDiffProfiles.Count > 1)
                            HTML += "<div id=\"tab" + QNDiffProfiles.IndexOf(QNProfiles) + "\">";
                        else
                            HTML += "<div>";
                        foreach (Profile profile in profiles)
                        {
                            if (profile is FullProfile)
                            {
                                FullProfile fullProfile = (FullProfile)profile;
                                HTML += showFullProfile(fullProfile, isCountAnswer, profiles.Count, answer.predicateLabelList.Keys + " of " + answer.subjectLabelList.Keys);
                            }

                            else if (profile is MiniProfile)
                            {
                                MiniProfile miniProfile = (MiniProfile)profile;
                                HTML += showMiniProfie(miniProfile, isCountAnswer, profiles.Count, answer.predicateLabelList.Keys + " of " + answer.subjectLabelList.Keys);

                            }
                            else if (profile is MicroProfile)
                            {
                                MicroProfile microProfile = (MicroProfile)profile;
                                HTML += showMicroProfile(microProfile, isCountAnswer, profiles.Count, answer.predicateLabelList.Keys + " of " + answer.subjectLabelList.Keys);
                            }
                            else if (profile is LiteralProfile)
                            {
                                LiteralProfile LP = (LiteralProfile)profile;
                                HTML += showLiteralProfile(LP);
                            }
                        }
                        HTML += "</div>";
                    }
                }
                else if (uri != null && question == null)
                {
                    ProfileConstructorInterfaceClient PCIClient = new ProfileConstructorInterfaceClient();
                    FullProfile fullProfile = (FullProfile)PCIClient.ConstructProfile(uri, MergedServicechoiceProfile.full, 50);
                    HTML = showFullProfile(fullProfile, false, 0, "");
                }

                else if (uri == null && question == null)
                { }
                else
                    Response.Redirect("error.aspx");
                answerbox.InnerHtml = HTML;
            }
            catch (Exception exep)
            {
                Util.log(exep.Message);
                Response.Redirect("error.aspx");
            }
        }

        private String showFullProfile(FullProfile fullProfile, bool isCountAnswer, int counter, String HTMLClass)
        {

            String HTML = "";
            String relatedImg = "";
            String relatedName = "";
            String details = "";
            foreach (KeyValuePair<String, Entity[]> key in fullProfile.Details.ToList())
            {
                if (key.Value.Length != 0)
                {
                    details += "<tr>"
                            + "<td class=\"property\">"
                                + key.Key
                            + "</td>"
                            + "<td class=\"value expandableContent\">";
                    foreach (Entity en in key.Value)
                    {
                        details += "<div class=\"line\">";
                        if (en.URI != null)
                            details += "<a href=\"answer.aspx?uri=" + en.URI + "\">" + en.Label + "</a>";
                        else if (en.URI == null && Uri.IsWellFormedUriString(en.Label, UriKind.Absolute))
                            details += "<a href=\"" + en.Label + "\">" + en.Label + "</a>";
                        else
                            details += en.Label;
                        details += "</div>";
                    }
                }
            }
            if (fullProfile.Location.Latitude != null & fullProfile.Location.Longitude != null)
                details += "<tr>"
                            + "<td class=\"property\">"
                                + "map"
                            + "</td>"
                            + "<td class=\"value\">"
                                + "<div class=\"line\">"
                                    + "<img src=\"http://maps.googleapis.com/maps/api/staticmap?center=" + fullProfile.Location.Latitude + "," + fullProfile.Location.Longitude + "&zoom=11&size=500x200&sensor=false\" title=\"" + "Latitude= " + fullProfile.Location.Latitude + ", Longitude= " + fullProfile.Location.Longitude + "\">"
                                + "</div>";
            details += "</td>"
                    + "</tr>";
            foreach (Entity rel in fullProfile.Related.ToList())
                relatedImg += "<td>"
                                + "<div class=\"imgcontainer\">"
                                    + "<a href=\"answer.aspx?uri=" + rel.URI + "\">" + "<img src=\"" + rel.Picture + "\"/>" + "</a>"
                                + "</div>"
                            + "</td>";
            foreach (Entity rel in fullProfile.Related.ToList())
                relatedName += "<td class=\"title\">"
                                + "<a href=\"answer.aspx?uri=" + rel.URI + "\">" + rel.Label + "</a>"
                            + "</td>";
            HTML += "<div class=\"fullprofile\">"
                    + "<div class=\"abstractcontainer\">";
            if (isCountAnswer)
                HTML += "<div>Number of results=" + counter + "</div>";
            HTML += "<div class=\"profilepic\">"
                  + "<img src=\"" + fullProfile.Picture + "\" />"
              + "</div>"
              + "<div class=\"abstract\">"
                  + "<a href=\"answer.aspx?uri=" + fullProfile.URI + "\" class=\"title\">" + fullProfile.Label + "</a>"
                  + "<p class=\"abstracttext expandableContent\">"
                      + fullProfile.Abstract
                  + "</p>"
              + "</div>"
              + "<div class=\"clearfix\">"
              + "</div>"
          + "</div>"
          + "<div class=\"relatedresults\">"
              + "<div class=\"subtitle\">"
                  + "Related Results</div>"
              + "<table class=\"relateTable\" id=\"headerTable\" runat=\"server\">"
                  + "<tr>"
                      + relatedImg
                  + "</tr>"
                  + "<tr>"
                      + relatedName
                  + "</tr>"
              + "</table>"
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

        private String showMiniProfie(MiniProfile miniProfile, bool isCountAnswer, int counter, String HTMLClass)
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
                            + "<td class=\"value expandableContent\">";
                    foreach (Entity en in key.Value)
                    {
                        details += "<div class=\"line\">";
                        if (en.URI != null)
                            details += "<a href=\"answer.aspx?uri=" + en.URI + "\">" + en.Label + "</a>";
                        else if (en.URI == null && Uri.IsWellFormedUriString(en.Label, UriKind.Absolute))
                            details += "<a href=\"" + en.Label + "\">" + en.Label + "</a>";
                        else
                            details += en.Label;
                        details += "</div>";
                    }
                    details += "</td>"
                            + "</tr>";
                }
            }
            HTML += "<div class=\"miniprofile\">";
            if (isCountAnswer)
                HTML += "<div>Number of results=" + counter + "</div>";
            HTML += "<div class=\"minileft\">"
                            + "<a href=\"answer.aspx?uri=" + miniProfile.URI + "\"><img src=\"" + miniProfile.Picture + "\" />" + "</a>"
                        + "</div>"
                        + "<div class=\"miniright\">"
                            + "<div>"
                                + "<a class=\"title\" href=\"answer.aspx?uri=" + miniProfile.URI + "\">" + miniProfile.Label + "</a>"
                            + "</div>"
                        + "<div class=\"abstract expandableContent\">"
                            + miniProfile.Abstract;
            if (miniProfile.IsShortAbstract)
                HTML += "<a href=\"answer.aspx?uri=" + miniProfile.URI + "\">more</a>";
            HTML += "</div>"
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

        private String showMicroProfile(MicroProfile microProfile, bool isCountAnswer, int counter, String HTMLClass)
        {
            String HTML = "";
            //HTML += "<div class=\"microprofile\">"
            //+ "<div class=\"left\">";
            HTML += "<div class=\"microprofile\">";
            if (isCountAnswer)
                HTML += "<div>Number of results=" + counter + "</div>";
            HTML += "<div class=\"left\">"
                + "<a href=\"answer.aspx?uri=" + microProfile.URI + "\"><img src=\"" + microProfile.Picture + "\" />" + "</a>"
            + "</div>"
            + "<div class=\"right\">"
                + "<div>"
                    + "<a class=\"title\" href=\"answer.aspx?uri=" + microProfile.URI + "\">" + microProfile.Label + "</a>"
                + "</div>"
                + "<div class=\"abstract expandableContent\">"
                    + microProfile.Abstract;
            if (microProfile.IsShortAbstract)
                HTML += "<a href=\"answer.aspx?uri=" + microProfile.URI + "\">more</a>";
            HTML += "</div>"
        + "</div>"
        + "<div class=\"clearfix\">"
        + "</div>"
    + "</div>";
            return HTML;
        }

        private String showLiteralProfile(LiteralProfile literalProfile)
        {
            String HTML = "";
            HTML += "<div class=\"miniprofile literalanswer\">"
                                            + "<div class=\"minileft\">"
                                                + "<a href=\"answer.aspx?uri=" + literalProfile.subjectURI + "\">" + "<img src=\"" + literalProfile.imageURI + "\"/></>"
                                            + " </div>"
                                            + "<div class=\"miniright\">"
                                                + " <div>"
                                                    + "<a class=\"title\" href=\"answer.aspx?uri=" + literalProfile.subjectURI + "\">" + literalProfile.subjectLabel + "</a>"
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