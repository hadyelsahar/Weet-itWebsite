/// <reference path="../jquery-1.4.1-vsdoc.js" />

function expand(obj) {
    //get the datacells
    //debugger;

        var html = $(obj).children(".hidden").html();
        if (html != null) {
            $(obj).empty();
            $(obj).append(html);
        }

    if ($(obj).hasClass("contracted")) {

        $(obj).children(".expand").remove();
        $(obj).append("<a class=\"expand\" href=\"#\" onclick=\"contract($(this).parent()); return false;\">[-]</a>");
        $(obj).removeClass("contracted");
    }
}

function contract(obj) {

        if ($(obj).children(".line").length > 10) {

            var links = $(obj).children(".line");
            var hidden = "<div class='hidden' style='display:none;'>" + $(obj).html() + "</div>";

            $(obj).empty();
            $(obj).append(links.slice(0, 10));
            $(obj).append(hidden);

            if (!$(obj).hasClass("contracted")) {
                $(obj).children(".expand").remove();
                $(obj).append("<a class=\"expand\" href=\"#\" onclick=\"expand($(this).parent()) ;return false;\">[+]</a>");
                $(obj).addClass("contracted");
            }
        }

        else if ($(this).children(".line").length == 1 && $(this).children(".line").text().length > 250) {

            var text = $(this).children("span").text();
            var hidden = "<div class='hidden' style='display:none;'><span>" + text + "</span></div>";
            $(this).empty();

            $(this).append("<span>" + getFirstWords(300, text) + "</span>")

            //adding class contracted to the comparison box check that it's not added before
            if (!$(obj).hasClass("contacted")) {
                $(obj).children(".predicate").append("<a class=\"expand\" href=\"#\" onclick=\"expand($(this).parent().parent()) ;return false;\">[+]</a>");
                $(obj).addClass("contacted");
            }

            $(this).append(hidden);
        }
}

function expand_abstract(obj) {

    var html = $(obj).children(".hidden").html();
    if (html != null) {
        $(obj).empty();
        $(obj).append(html);
    }

    if ($(obj).hasClass("contracted")) {

        $(obj).children(".more").remove();
        $(obj).append("<a class=\"more\" href=\"#\" onclick=\"contract_abstract($(this).parent()); return false;\">..less</a>");
        $(obj).removeClass("contracted");
    }


}

function contract_abstract(obj) {

    if ($(obj).text().length > 400) {

        $("obj").children(".more").remove();
        var text = $(obj).html();
        var hidden = "<div class='hidden' style='display:none;'>" + text + "</div>";
        $(obj).empty();
        $(obj).append(getFirstWords(400, text))

        //adding class contracted to the comparison box check that it's not added before
        if (!$(obj).hasClass("contracted")) {
            $(obj).append("<a class=\"more\" href=\"#\" onclick=\"expand_abstract($(this).parent()) ;return false;\">..more</a>");
            $(obj).addClass("contracted");
        }

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
    $(".expandableContent").each(function () {
        contract(this);
    });

    $(".abstracttext:.expandableContent").each(function () {
        contract_abstract(this);
    });

});