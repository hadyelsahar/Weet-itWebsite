/// <reference path="../jquery-1.4.1-vsdoc.js" />

function expand(obj) {
    //get the datacells
    debugger;
    var datacells = $(obj).children(".window").children(".datarow").children(".datacell");

    datacells.each(function () {

        if ($(this).children(".hidden").html() != null) {
            var html = $(this).children(".hidden").html();
            $(this).empty();
            $(this).append(html);
        }
    });

    if ($(obj).hasClass("contracted")) {
        debugger;
        $(obj).children(".predicate").children(".expand").remove();
        $(obj).children(".predicate").append("<a class=\"expand\" href=\"#\" onclick=\"contract($(this).parent().parent()); return false;\">[-]</a>");
        $(obj).removeClass("contracted");

    }
}

function contract(obj) {

    //get the datacells
    var datacells = $(obj).children(".window").children(".datarow").children(".datacell");

    datacells.each(function () {
        debugger;
        if ($(this).children("span").length > 7) {

            var links = $(this).children("span");
            var hidden = "<div class='hidden' style='display:none;'>" + $(this).html() + "</div>";

            $(this).empty();
            $(this).append(links.slice(0, 7));
            $(this).append(hidden);

            if (!$(obj).hasClass("contracted")) {
                $(obj).children(".predicate").children(".expand").remove();
                $(obj).children(".predicate").append("<a class=\"expand\" href=\"#\" onclick=\"expand($(this).parent().parent()) ;return false;\">[+]</a>");
                $(obj).addClass("contracted");
            }

        }

        else if ($(this).children("span").length == 1 && $(this).children("span").text().length > 300) {

            var text = $(this).children("span").text();
            var hidden = "<div class='hidden' style='display:none;'><span>" + text + "</span></div>";
            $(this).empty();

            $(this).append("<span>" + getFirstWords(300, text) + "</span>")

            //adding class contracted to the comparison box check that it's not added before
            if (!$(obj).hasClass("contracted")) {
                $(obj).children(".predicate").children(".expand").remove();
                $(obj).children(".predicate").append("<a class=\"expand\" href=\"#\" onclick=\"expand($(this).parent().parent()) ;return false;\">[+]</a>");
                $(obj).addClass("contracted");
            }

            $(this).append(hidden);
        }

    });
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
    $(".expandableContent").each(function () {
        contract(this);
    });

});