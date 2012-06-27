<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="answer.aspx.cs" Inherits="weetit_website.answer" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>answer</title>
    <link href="Styles/Defaultpage.css" rel="stylesheet" type="text/css" />
    <link href="Styles/answerpage.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="Scripts/UX/compare.js" type="text/javascript"></script>
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
            <div class="clearfix"></div>
            <div class="addedEntitiesBox">
                <span class="title">Relate between:</span>
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
            <div class="fullprofile">
                <div class="abstractcontainer">
                    <div class="profilepic">
                        <img src="testimg/220px-WikiBex.jpg" />
                    </div>
                    <div class="abstract">
                        <a href="#" class="title">David Beckham </a>
                        <p class="abstracttext">
                            David Robert Joseph Beckham, (born 2 May 1975) is an English footballer who plays
                            midfield for Los Angeles Galaxy in Major League Soccer, having previously played,
                            having missed the 2010 World Cup through injury and not featuring in England manager
                            Fabio Capello's post-World Cup plans. Beckham has twice been runner-up for FIFA
                            World Player of the Year and in 2004 was the world's highest-paid footballer when
                            taking into account salary and advertising deals. Beckham was the first British
                            footballer to play 100 Champions League matches. He is third in the Premier League's
                            all time time assist provider chart, with 152 assists in 265 appearances. He was
                            since 1999; they have four children.</p>
                    </div>
                    <div class="clearfix">
                    </div>
                </div>
                <div class="relatedresults">
                    <div class="subtitle">
                        Related results</div>
                    <img src="testimg/10%20Best%20Footballers%20in%20the%20FIFA%20World%20Cup%202010_01.jpg" />
                    <img src="testimg/10%20Best%20Footballers%20in%20the%20FIFA%20World%20Cup%202010_01.jpg" />
                    <img src="testimg/10%20Best%20Footballers%20in%20the%20FIFA%20World%20Cup%202010_01.jpg" />
                    <img src="testimg/10%20Best%20Footballers%20in%20the%20FIFA%20World%20Cup%202010_01.jpg" />
                </div>
                <div class="profiledetails">
                    <div class="subtitle">
                        Profile details</div>
                    <table class="detailstable">
                        <tr>
                            <td class="property">
                                Full Name
                            </td>
                            <td class="value">
                                David Robert Joseph Beckham
                            </td>
                        </tr>
                        <tr>
                            <td class="property">
                                Date of birth
                            </td>
                            <td class="value">
                                1975-05-02
                            </td>
                        </tr>
                        <tr>
                            <td class="property">
                                Place of birth
                            </td>
                            <td class="value">
                                Leytonstone, London, England
                            </td>
                        </tr>
                        <tr>
                            <td class="property">
                                Height
                            </td>
                            <td class="value">
                                1.828800 m
                            </td>
                        </tr>
                        <tr>
                            <td class="property">
                                Position
                            </td>
                            <td class="value">
                                Midfielder
                            </td>
                        </tr>
                        <tr>
                            <td class="property">
                                Quote
                            </td>
                            <td class="value">
                                I'm coming there not to be a superstar. I'm coming there to be part of the team,
                                to work hard and to hopefully win things. With me, it's about football. I'm coming
                                there to make a difference. I'm coming there to play football ... I'm not saying
                                me coming over to the States is going to make soccer the biggest sport in America.
                                That would be difficult to achieve. Baseball, basketball, American football, they've
                                been around. But I wouldn't be doing this if I didn't think I could make a difference.
                            </td>
                        </tr>
                        <tr>
                            <td class="property">
                                Full Name
                            </td>
                            <td class="value">
                                David Robert Joseph Beckham
                            </td>
                        </tr>
                        <tr>
                            <td class="property">
                                Full Name
                            </td>
                            <td class="value">
                                David Robert Joseph Beckham
                            </td>
                        </tr>
                        <tr>
                            <td class="property">
                                Full Name
                            </td>
                            <td class="value">
                                David Robert Joseph Beckham
                            </td>
                        </tr>
                        <tr>
                            <td class="property">
                                Full Name
                            </td>
                            <td class="value">
                                David Robert Joseph Beckham
                            </td>
                        </tr>
                        <tr>
                            <td class="property">
                                Full Name
                            </td>
                            <td class="value">
                                David Robert Joseph Beckham
                            </td>
                        </tr>
                        <tr>
                            <td class="property">
                                Full Name
                            </td>
                            <td class="value">
                                David Robert Joseph Beckham
                            </td>
                        </tr>
                        <tr>
                            <td class="property">
                                Full Name
                            </td>
                            <td class="value">
                                David Robert Joseph Beckham
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            <div class="miniprofile">
                <div class="minileft">
                    <img src="852221_f496.jpg" />
                </div>
                <div class="miniright">
                    <div>
                        <a class="title" href="#">Tom clancy's The Ghost Recon </a>
                    </div>
                    <div class="abstract">
                        I'm coming there not to be a superstar. I'm coming there to be part of the team,
                        to work hard and to hopefully win things. With me, it's about football. I'm coming
                        there to make a difference. I'm coming there to play football
                    </div>
                    <div class="minitable">
                        <table class="detailstable">
                            <tr>
                                <td class="property">
                                    Full Name
                                </td>
                                <td class="value">
                                    David Robert Joseph Beckham
                                </td>
                            </tr>
                            <tr>
                                <td class="property">
                                    Date of birth
                                </td>
                                <td class="value">
                                    1975-05-02
                                </td>
                            </tr>
                            <tr>
                                <td class="property">
                                    Place of birth
                                </td>
                                <td class="value">
                                    Leytonstone, London, England
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
                <div class="clearfix">
                </div>
            </div>
            <div class="microprofile">
                <div class="left">
                    <img src="852221_f496.jpg" />
                </div>
                <div class="right">
                    <div>
                        <a class="title" href="#">Madonna </a>
                    </div>
                    <div class="abstract">
                        I'm coming there not to be a superstar. I'm coming there to be part of the team,
                        to work hard and to hopefully win things. With me, it's about football. I'm coming
                        there to make a difference. I'm coming there to play football
                    </div>
                </div>
                <div class="clearfix">
                </div>
            </div>
        </div>
        <div class="rightcol">
            <p>
            </p>
        </div>
    </div>
    </form>
</body>
</html>
