<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="compare.aspx.cs" Inherits="weetit_website.compare" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>compare</title>
    <link href="Styles/Defaultpage.css" rel="stylesheet" type="text/css" />
    <link href="Styles/comparisonpage.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="Scripts/UX/compare.js" type="text/javascript"></script>
    <script src="Scripts/DataTransfer/default_data.js" type="text/javascript"></script>
    <script src="Scripts/UX/defaultpage.js" type="text/javascript"></script>
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
            <div class="clearfix">
            </div>
            <div class="addedEntitiesBox">
                <span class="title"></span><span class="leftNum"></span>
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
        <div class="answerbox" id="answerbox" runat="server">
            <div class="comparisonbox tableheader">
                <div class="predicate">
                    <p>
                    </p>
                </div>
                <div class="window">
                    <div class="datarow">
                        <div class="datacell">
                            <img src="img/852221_f496.jpg" />
                            <div class="title">
                                drogba</div>
                        </div>
                        <div class="datacell">
                            <img src="img/_46571386_ac_0003130_d_0.jpg" />
                            <div class="title">
                                angelina joulie</div>
                        </div>
                        <div class="datacell">
                            <img src="img/_46571386_ac_0003130_d_0.jpg" />
                            <div class="title">
                                angelina joulie</div>
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
                            Argentine
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
