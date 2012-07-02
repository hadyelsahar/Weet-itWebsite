/// <reference path="jquery-1.4.1-vsdoc.js" />
/// <reference path="../jquery-1.4.1.js" />


var DATACELLSNUMBER=0;
var entityCounter = 0;

function settableviewvariables() {
    DATACELLSNUMBER = $(".datarow").children(".datacell").length / $(".datarow").length;
    entityCounter = DATACELLSNUMBER;
    var rowwidth = DATACELLSNUMBER * 300;
    $(".datarow").css("width", rowwidth.toString() + "px");
    $(".comparisonbox").fadeIn();
}

function moveright() {
    if (entityCounter > 2) {
        $(".datarow").animate({ right: "+=300px" }, 500)
        entityCounter--;
    }
}

function moveleft() {
    if (entityCounter < DATACELLSNUMBER) {
        $(".datarow").animate({ right: "-=300px" }, 500)
        entityCounter++;
    }
}


///event attachments


//$(document).ready();

$(document).ready(function () {


    settableviewvariables();

    if (DATACELLSNUMBER > 2) {

        $(".answerbox").append("<div class='GalleryButtons'><a href='#'  class='leftbutton' onclick='moveleft()'></a><a href='#' class='rightbutton' onclick='moveright()'></a></div>");
    }


    $(window).keyup(function (event) {

        if (event.which == 37) {
            moveleft();
        }
        else if (event.which == 39) {
            moveright();
        }

    })

});


