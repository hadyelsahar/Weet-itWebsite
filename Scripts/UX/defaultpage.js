/// <reference path="../jquery-1.4.1-vsdoc.js" />

var sentence;
var sentncetype;
var max = 5;
var counter;
function stopadd() {
    if ($(".header .searcharea .addedEntitiesBox .addedEntity").length < max) {
        return false;
    }
    else {
        return true;
    }
}

function addedNum() {

    counter = max - $(".header .searcharea .addedEntitiesBox .addedEntity .text").length;
    $(".header .searcharea .addedEntitiesBox .leftNum").html("Number of Left entities : " + counter);

}


function addEntitiesBox() {
    $('.header .searcharea .searchbox .searchinput').focus();
    if ($("input[name='option']:checked").val() == 'QA') {
        $(".header .searcharea .searchbox .addbutton").fadeOut(200);
        $(".header .searcharea .addedEntitiesBox ").fadeOut(200, function () {
            $(".header .searcharea .addedEntitiesBox .title").text("");
        });
    }
    else if ($("input[name='option']:checked").val() == 'compare') {
        if ($(".header .searcharea .addedEntitiesBox .title").html() != "compare between") {
            $(".header .searcharea .searchbox .addbutton").fadeIn(200);

            $(".header .searcharea .addedEntitiesBox .title").fadeOut(200, function () {
                $(".header .searcharea .addedEntitiesBox .title").text("Compare between");
                $(".header .searcharea .addedEntitiesBox").fadeIn(200);
                $(".header .searcharea .addedEntitiesBox .title").fadeIn(200);
            });
        }
    }
    else if ($("input[name='option']:checked").val() == 'relate') {
        if ($(".header .searcharea .addedEntitiesBox .title").html() != "Relate between") {
            $(".header .searcharea .searchbox .addbutton").fadeIn();
            $(".header .searcharea .addedEntitiesBox").fadeIn();
            $(".header .searcharea .addedEntitiesBox .title").fadeOut(200, function () {
                $(".header .searcharea .addedEntitiesBox .title").text("Relate between");
                $(".header .searcharea .addedEntitiesBox .title").fadeIn(200);
            });
        }
    }
}











function enable() {
    ///<summary> 
    ///check if the main textbox is empty : (empty , full of spaces) or not
    ///if empty return false esle return true 
    /// <para></para>
    ///
    ///</summary>
    if ($(".header .searcharea .searchbox .searchinput").val() == "" || $(".header .searcharea .searchbox .searchinput").val().match(/^\s+$/gi) != null)
        return false;
    return true;
}


function searchBegin() {
    ///<summary> 
    ///get the input from the main textbox and gets the type of the sentence based on the value of the radio buttons and returns the tyb of the sentence and array of search objects
    /// <para></para>
    ///
    ///</summary>

    sentence = new Array();
    if ($("input[name='option']:checked").val() == 'QA') {
        sentence.push($(".header .searcharea .searchbox .searchinput").val());
        sentncetype = "question";
    }
    if ($("input[name='option']:checked").val() != 'QA') {
        jQuery.each($(".header .searcharea .addedEntitiesBox .addedEntity .text"), function (i, val) {
            sentence[i] = val.innerHTML;
        });
        if ($("input[name='option']:checked").val() == 'compare') {
            sentncetype = "compare";
        }
        else if ($("input[name='option']:checked").val() == 'relate') {
            sentncetype = "relate";
        }

    }

    var obj = { "type": sentncetype, "data": sentence };
    debugger;
    submit(obj);

}

function addObject() {
    ///<summary> 
    ///gets the sentence from the main textbox and add it to the added entities box after checking the validation of the sentence an, it shows , hides the add button and removes the objects from addedEntitiesBox
    ///if empty return false esle return true 
    /// <para></para>
    ///
    ///</summary>
    if (enable() == true && !stopadd()) {
        var objectName = $(".header .searcharea .searchbox .searchinput").val();
        $(".header .searcharea .searchbox .searchinput").val("");
        var x = $(".header .searcharea .addedEntitiesBox");
        x.append("<span class='addedEntity'><span class='text'>" + objectName + "</span><a href='#' class='closeIcon'><img src='img/closeIcon.png'/></a><a href='#' class='closeIconHover'><img src='img/closeIconHover.png'/></a></span>");
        addedNum(); //read the number of the left entities
        $("a.closeIconHover").hide();
        $(".addedEntity").hover(
  			function () {
  			    $(this).children('a.closeIcon').hide();
  			    $(this).children('a.closeIconHover').show();
  			},
  			function () {
  			    $(this).children('a.closeIconHover').hide();
  			    $(this).children('a.closeIcon').show();
  			});
    };




    $("a.closeIconHover").click(function () {
        $(this).parent().fadeOut(250, function () {
            $(this).remove();
            $('.header .searcharea .searchbox .searchinput').focus();
            $(".header .searcharea .searchbox .addbutton").css("opacity", "1");
            addedNum();
        });

    });


}

//////////////////////////////////////////////////////////////

$(document).ready(function () {
    ///<summary> 
    ///to focus on the main text box when the user presses enter or presses the add button and when the page is ready 
    ///if empty return false esle return true 
    /// <para></para>
    ///
    ///</summary>
    addedNum();
    addEntitiesBox();
    $('.header .searcharea .searchbox .searchinput').focus();

    $("input[name='option']").change(addEntitiesBox);

    ///<summary> 
    ///changes the opacity if the add button when the main text bow is empty or not
    ///</summary>

    $(".header .searcharea .searchbox .addbutton").css("opacity", ".3");
    $(".header .searcharea .searchbox .searchinput").keyup(function () {
        if (!enable() || stopadd()) {
            $(".header .searcharea .searchbox .addbutton").css("opacity", ".3");
        }
        else {
            $(".header .searcharea .searchbox .addbutton").css("opacity", "1");
        }
    });




    $(".header").change(addedNum); //change the co




    $(".header .searcharea .searchbox .addbutton").click(function () {
        addObject();
        $(".header .searcharea .searchbox .addbutton").css("opacity", ".3");
        $('.header .searcharea .searchbox .searchinput').focus();
        return false;
    });

    ///<summary> 
    ///starts the search process when the user clicks on the search button
    ///if empty return false esle return true 
    /// 
    ///
    ///</summary>

    $(".header .searcharea .searchbox .searchbutton").click(function () {

        if (enable() && $("input[name='option']:checked").val() == 'QA')
        { searchBegin(); }
        else if ($("input[name='option']:checked").val() != 'QA') {
            searchBegin();
        }

        return false;
    });

    ///<summary> 
    ///to start the search when the user presses enter and adds new objects to addedEntitiesBox when the user presses enter
    ///if empty return false esle return true 
    /// 
    ///
    ///</summary>



    $('.header .searcharea .searchbox .searchinput').bind('keypress', function (e) {                // Enter pressed... do anything here...
        var code = (e.keyCode ? e.keyCode : e.which);
        if (code == 13) {
            if ($("input[name='option']:checked").val() != 'QA') {
                addObject();
            }
            if ($("input[name='option']:checked").val() == 'QA') {
                if (enable())
                { searchBegin(); }
            }

            return false;
        }
    });
});

