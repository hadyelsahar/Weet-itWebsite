/// <reference path="../jquery-1.4.1-vsdoc.js" />

function expand(obj) {
    var html = $(obj).children(".hidden").html();
    $(obj).empty();
    $(obj).append(html);
    $(obj).append("<a class=\"expand\" href=\"#\" onclick=\"contract($(this).parent()); return false;\">[-]</a></span>");
}

function contract(obj) {


    if ($(obj).children("span").length > 7) {
        var links = $(obj).children("span");
        var hidden = "<div class='hidden' style='display:none;'>" + $(obj).html() + "</div>";

        $(obj).empty();
        $(obj).append(links.slice(0, 6));
        $(obj).append("<a class=\"expand\" href=\"#\" onclick=\"expand($(this).parent()) ;return false;\">[+]</a></span>");
        $(obj).append(hidden);
    }
    else if ($(obj).children("span").length == 1 && $(obj).children("span").text().length > 300) {
        debugger;
        var text = $(obj).children("span").text();
        var hidden = "<div class='hidden' style='display:none;'>" + text + "</div>";
        $(obj).empty();
        $(obj).append("<span>" + getFirstWords(300, text) + "<a class=\"expand\" href=\"#\" onclick=\"expand($(this).parent()) ;return false;\">[+]</a></span>");
        $(obj).append(hidden);
    }



}

function getFirstWords(charCount, Text) {

    var counter = 0;
    var toReturn = "";
    var words = Text.split(" ");
    for (i in words) {
        counter += words[i].length;
        if (counter < charCount) {
            toReturn = toReturn + " " + words[i];
        }
        else {
            break;
        }

    }
    return toReturn;
}

$(document).ready(function () {

    //cells in the compare page
    $(".datacell:.expandableContent").each(function () {

        contract(this);
    });
    //cells in the answer page 
    $(".:.expandableContent").each(function () {

        contract(this);
    });


});