<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="relate.aspx.cs" Inherits="weetit_website.relate" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Relate</title>
    <link href="Styles/Defaultpage.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="Scripts/DataTransfer/default_data.js" type="text/javascript"></script>
    <script src="Scripts/UX/defaultpage.js" type="text/javascript"></script>
    <script src="Scripts/UX/Relate.js" type="text/javascript"></script>
    <script src="Scripts/DataTransfer/relate_data.js" type="text/javascript"></script>
    <link href="Styles/RelatePage.css" rel="stylesheet" type="text/css" />
    <!-- arborJs Library -->
    <script src="Scripts/ArborJS/arbor.js" type="text/javascript"></script>
    <script src="Scripts/ArborJS/arbor-tween.js" type="text/javascript"></script>
    <script src="Scripts/ArborJS/graphics.js" type="text/javascript"></script>
    <script src="Scripts/ArborJS/Interaction_Driver.js" type="text/javascript"></script>
    <script src="Scripts/ArborJS/renderer.js" type="text/javascript"></script>
    <script src="Scripts/ArborJS/ArborJSDriver.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="header">
        <div class="topmenu">
            <ul>
                <li class="first">About</li>
                <li>Developers</li>
                <li>Team</li>
                <li>Contact</li>
            </ul>
        </div>
        <div class="logo">
        </div>
        <div class="searcharea">
            <div class="options">
                <span>
                    <input type="radio" name="option" value="QA" id="QA" checked /><label for="QA">Q&A</label></span>
                <span>
                    <input type="radio" name="option" value="compare" id="compare" /><label for="compare">Compare</label></span>
                <span>
                    <input type="radio" name="option" value="relate" id="relate" /><label for="relate">Relate</label></span>
            </div>
            <div class="searchbox">
                <input type="text" class="searchinput" />
                <a href="#" class="button addbutton"></a><a href="#" class="button searchbutton">
                </a>
            </div>
            <div class="clearfix">
            </div>
            <div class="addedEntitiesBox">
                <span class="title">Relate between:</span> <span class="leftNum">left Number:</span>
            </div>
        </div>
        <div class="clearfix">
        </div>
    </div>
    <div class="contentwrapper">
        <div class="leftcol">
            <p>
            </p>
        </div>
        <div class="answerbox" runat="server" id="answerbox">
            <div id="headerTable" runat="server">
                <table class="relateTable">
                    <tr>
                        <td>
                            <div class="imgcontainer">
                                <img src="testimg/220px-WikiBex.jpg" />
                            </div>
                        </td>
                        <td>
                            <div class="imgcontainer">
                                <img src="testimg/10%20Best%20Footballers%20in%20the%20FIFA%20World%20Cup%202010_01.jpg" />
                            </div>
                        </td>
                        <td>
                            <div class="imgcontainer">
                                <img src="testimg/10%20Best%20Footballers%20in%20the%20FIFA%20World%20Cup%202010_01.jpg" />
                            </div>
                        </td>
                        <td>
                            <div class="imgcontainer">
                                <img src="testimg/10%20Best%20Footballers%20in%20the%20FIFA%20World%20Cup%202010_01.jpg" />
                            </div>
                        </td>
                        <td>
                            <div class="imgcontainer">
                                <img src="testimg/10%20Best%20Footballers%20in%20the%20FIFA%20World%20Cup%202010_01.jpg" />
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td class="title">
                            hany
                        </td>
                        <td class="title">
                            hany
                        </td>
                        <td class="title">
                            hany
                        </td>
                        <td class="title">
                            islam
                        </td>
                        <td class="title">
                            islam
                        </td>
                    </tr>
                </table>
            </div>
            <div class="RelationDiagram">
                <span class="subtitle">relation Diagram</span>
                <div class="canvascontainer">
                    <canvas id="viewport" width="740px" height="600"></canvas>
                    <a href="#" class="playButton" onclick="playandpause(); return false;"></a>
                    <img  class="loading" src="img/ajaxloader.gif"  />
                </div>
            </div>
        </div>
        <div class="rightcol">
            <p>
            </p>
        </div>
    </div>
    <div class="microProfilesBox">
    </div>
    </form>
</body>
</html>
