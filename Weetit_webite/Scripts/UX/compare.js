/// <reference path="jquery-1.4.1-vsdoc.js" />


var DATACELLSNUMBER=0;
var counter = 0;

function settableviewvariables() {
    DATACELLSNUMBER = $(".datarow").children(".datacell").length / $(".datarow").length;
    counter = DATACELLSNUMBER;
    var rowwidth = DATACELLSNUMBER * 300;
    $(".datarow").css("width", rowwidth.toString() + "px");
    $(".comparisonbox").fadeIn();
}

function moveright() {
    if (counter > 2) {
        $(".datarow").animate({ right: "+=300px" }, 500)
        counter--;
    }
}

function moveleft() {
    if (counter < DATACELLSNUMBER) {
        $(".datarow").animate({ right: "-=300px" }, 500)
        counter++;
    }
}


///event attachments


$(document).ready(settableviewvariables);

$(document).ready(function () {


    $(window).keyup(function (event) {

        if (event.which == 37) {
            moveleft();
        }
        else if (event.which == 39) {
            moveright();
        }

    })

});


