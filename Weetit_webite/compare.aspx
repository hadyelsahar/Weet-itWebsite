<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="compare.aspx.cs" Inherits="Weetit_webite.compare" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>answer</title>
    <link href="Styles/Defaultpage.css" rel="stylesheet" type="text/css" />
    <link href="Styles/comparisonpage.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="Scripts/UX/compare.js" type="text/javascript"></script>
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
                <input type="text" class="searchinput" /><a href="#" class="button addbutton"></a><a
                    href="#" class="button searchbutton"></a>
            </div>
            <div class="clearfix"></div>
            <div class="addedEntitiesBox">
                <span class="title">Relate between:</span> <span class="addedEntity">Batman begins<img
                    src="img/closeIconHover.png" /></span> <span class="addedEntity">how i met your mother
                        <img src="img/closeIcon.png" />
                    </span><span class="addedEntity">assasins creed<img src="img/closeIcon.png" /></span>
                <span class="addedEntity">Hello<img src="img/closeIcon.png" /></span> <span class="addedEntity">
                    anitvirus<img src="img/closeIcon.png" /></span>
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
        <div class="answerbox">
            <div class="testdiv">
                <button type="button" class="leftbutton" onclick="moveleft()">
                    Left</button>
                <button type="button" class="rightbutton" onclick="moveright()">
                    Right</button>
            </div>
            <div class="comparisonbox">
                <div class="predicate tableheader">
                    <p>
                    </p>
                </div>
                <div class="window">
                    <div class="datarow">
                        <div class="datacell  tableheader">
                            <img src="img/852221_f496.jpg" />
                            <p>
                                Drogba</p>
                        </div>
                        <div class="datacell  tableheader">
                            <img src="img/852221_f496.jpg" />
                            <p>
                                Messi</p>
                        </div>
                        <div class="datacell  tableheader">
                            <img src="img/852221_f496.jpg" />
                            <p>
                                Beckham</p>
                        </div>
                        <div class="datacell  tableheader">
                            <img src="img/852221_f496.jpg" />
                            <p>
                                Hamada</p>
                        </div>
                        <div class="clearfix">
                        </div>
                    </div>
                </div>
                <div class="clearfix">
                </div>
            </div>
            <div class="comparisonbox">
                <div class="predicate">
                    Place of birth
                </div>
                <div class="window">
                    <div class="datarow">
                        <div class="datacell">
                            Cote d'ivoire
                        </div>
                        <div class="datacell">
                            Argentine
                        </div>
                        <div class="datacell">
                            England
                        </div>
                        <div class="datacell">
                            Egypt
                        </div>
                        <div class="clearfix">
                        </div>
                    </div>
                </div>
                <div class="clearfix">
                </div>
            </div>
            <div class="comparisonbox">
                <div class="predicate">
                    Date of birth
                </div>
                <div class="window">
                    <div class="datarow">
                        <div class="datacell">
                            11-5-19884
                        </div>
                        <div class="datacell">
                            11-5-19884
                        </div>
                        <div class="datacell">
                            11-5-19884
                        </div>
                        <div class="datacell">
                            11-5-19884
                        </div>
                        <div class="clearfix">
                        </div>
                    </div>
                </div>
                <div class="clearfix">
                </div>
            </div>
            <div class="comparisonbox">
                <div class="predicate">
                    Position
                </div>
                <div class="window">
                    <div class="datarow">
                        <div class="datacell">
                            Striker
                        </div>
                        <div class="datacell">
                            Midfielder/Winger
                        </div>
                        <div class="datacell">
                            Midfielder
                        </div>
                        <div class="datacell">
                            Goalkeeper
                        </div>
                        <div class="clearfix">
                        </div>
                    </div>
                </div>
                <div class="clearfix">
                </div>
            </div>
        </div>
        <div class="rightcol">
        </div>
        <div class="clearfix">
        </div>
    </div>
    </form>
</body>
</html>
