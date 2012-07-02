﻿using System;
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
                List<KeyValuePair<questionAnswer,List<Profile>>> QNDiffProfiles = QuestionAnswerer.getAnswersInProfiles(question);
                foreach (KeyValuePair<questionAnswer,List<Profile>> QNProfiles in QNDiffProfiles)
                {
                    List<Profile> profiles = QNProfiles.Value;
                    if (QNProfiles.Key.questiontype == utilquestionTypes.countAnswer)
                        HTML += "<div>Number of results=" + profiles.Count;
                    foreach (Profile profile in profiles)
                    {
                        if (profile is FullProfile)
                        {
                            FullProfile fullProfile = (FullProfile)profile;
                            HTML+=showFullProfile(fullProfile);
                        }
                            
                        else if (profile is MiniProfile)
                        {
                            MiniProfile miniProfile = (MiniProfile)profile;
                            HTML+= showMiniProfie(miniProfile);
                            
                        }
                        else if (profile is MicroProfile)
                        {
                            MicroProfile microProfile = (MicroProfile)profile;
                            HTML+= showMicroProfile(microProfile);
                        }
                        else if (profile is LiteralProfile)
                        {
                            LiteralProfile LP = (LiteralProfile)profile;
                            HTML += showLiteralProfile(LP);
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
                            + "<td class=\"value\">";
                    foreach (Entity en in key.Value)
                    {
                        details += "<div class=\"line\">";
                        if (en.URI != null)
                            details += "<a href=\"answer.aspx?uri=" + en.URI + "\">" + en.Label + "</a>";
                        else if(en.URI==null &&Uri.IsWellFormedUriString(en.Label,UriKind.Absolute))
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
                                +"<div class=\"line\">" 
                                    + "<img src=\"http://maps.googleapis.com/maps/api/staticmap?center=" + fullProfile.Location.Latitude + "," + fullProfile.Location.Longitude + "&zoom=11&size=500x200&sensor=false\" title=\"" + "Latitude= " + fullProfile.Location.Latitude + ", Longitude= " + fullProfile.Location.Longitude + "\">" 
                                + "</div>";
            details += "</td>"
                    + "</tr>";
            foreach (Entity rel in fullProfile.Related.ToList())
                relatedImg += "<td>"
                                + "<div class=\"imgcontainer\">"
                                    +"<a href=\"answer.aspx?uri="+rel.URI+"\">"+ "<img src=\"" + rel.Picture + "\"/>"+"</a>"
                                + "</div>"
                            + "</td>";
            foreach (Entity rel in fullProfile.Related.ToList())
                relatedName += "<td class=\"title\">"
                                +"<a href=\"answer.aspx?uri="+rel.URI+"\">"+ rel.Label+"</a>"
                            + "</td>";
            HTML += "<div class=\"fullprofile\">"
                    + "<div class=\"abstractcontainer\">"
                        + "<div class=\"profilepic\">"
                            + "<img src=\"" + fullProfile.Picture + "\" />"
                        + "</div>"
                        + "<div class=\"abstract\">"
                            + "<a href=\"answer.aspx?uri="+fullProfile.URI+"\" class=\"title\">" + fullProfile.Label + "</a>"
                            + "<p class=\"abstracttext\">"
                                + fullProfile.Abstract
                            + "</p>"
                        + "</div>"
                        + "<div class=\"clearfix\">"
                        + "</div>"
                    + "</div>"
                    + "<div class=\"relatedresults\">"
                        + "<div class=\"subtitle\">"
                            + "Related Results</div>"
                        +"<table class=\"relateTable\" id=\"headerTable\" runat=\"server\">"
                            +"<tr>"
                                + relatedImg
                            +"</tr>"
                            +"<tr>"
                                +relatedName
                            +"</tr>"
                        +"</table>"
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
                    {
                        details += "<div class=\"line\">";
                        if (en.URI != null)
                            details += "<a href=\"answer.aspx?uri=" + en.URI + "\">" + en.Label + "</a>";
                        else if (en.URI == null && Uri.IsWellFormedUriString(en.Label, UriKind.Absolute))
                            details += "<a href=\"" + en.Label + "\">" + en.Label + "</a>";
                        else
                            details += en.Label;
                        details+=                "</div>";
                    }
                    details += "</td>"
                            + "</tr>";
                }
            }
            HTML += "<div class=\"miniprofile\">"
                        + "<div class=\"minileft\">"
                            + "<a href=\"answer.aspx?uri="+miniProfile.URI+"\"><img src=\"" + miniProfile.Picture + "\" />"+"</a>"
                        + "</div>"
                        + "<div class=\"miniright\">"
                            + "<div>"
                                + "<a class=\"title\" href=\"answer.aspx?uri="+miniProfile.URI+"\">" + miniProfile.Label + "</a>"
                            + "</div>"
                        + "<div class=\"abstract\">"
                            + miniProfile.Abstract+"<a href=\"answer.aspx?uri="+miniProfile.URI+"\">more</a>"
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
                                        + "<a href=\"answer.aspx?uri=" + microProfile.URI + "\"><img src=\"" + microProfile.Picture + "\" />" + "</a>"
                                    + "</div>"
                                    + "<div class=\"right\">"
                                        + "<div>"
                                            + "<a class=\"title\" href=\"answer.aspx?uri="+microProfile.URI+"\">" + microProfile.Label + "</a>"
                                        + "</div>"
                                        + "<div class=\"abstract\">"
                                            + microProfile.Abstract+"<a href=\"answer.aspx?uri="+microProfile.URI+"\">more</a>"
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