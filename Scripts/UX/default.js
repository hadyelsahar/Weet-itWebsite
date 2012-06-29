/// <reference path="../jquery-1.4.1-vsdoc.js" />

var sentence;
var sentncetype;
var max = 5;
var entityCounter;
function stopadd() {
    if ($(".maincontainer .searcharea .addedEntitiesBox .addedEntity").length < max) {
        return false; 
    }
    else {
        return true;
    }
}

function addedNum() {

    entityCounter = max - $(".maincontainer .searcharea .addedEntitiesBox .addedEntity .text").length;
    $(".maincontainer .searcharea .addedEntitiesBox .leftNum").html("Number of Left entities : " + entityCounter);

}


function enable() {
    ///<summary> 
    ///check if the main textbox is empty : (empty , full of spaces) or not
    ///if empty return false esle return true 
    /// <para></para>
    ///
    ///</summary>
    if ($(".maincontainer .searcharea .searchbox .searchinput").val() == "" || $(".maincontainer .searcharea .searchbox .searchinput").val().match(/^\s+$/gi) != null)
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
        sentence.push($(".maincontainer .searcharea .searchbox .searchinput").val());
        sentncetype = "question";
    }
    if ($("input[name='option']:checked").val() != 'QA') {
        jQuery.each($(".maincontainer .searcharea .addedEntitiesBox .addedEntity .text"), function (i, val) {
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
        var objectName = $(".maincontainer .searcharea .searchbox .searchinput").val();
        $(".maincontainer .searcharea .searchbox .searchinput").val("");
        var x = $(".maincontainer .searcharea .addedEntitiesBox");
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
            $('.maincontainer .searcharea .searchbox .searchinput').focus();
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
    $('.maincontainer .searcharea .searchbox .searchinput').focus();
    $("input[name='option']").change(function () {
        $('.maincontainer .searcharea .searchbox .searchinput').focus();
        if ($("input[name='option']:checked").val() == 'QA') {
            $(".maincontainer .searcharea .searchbox .addbutton").fadeOut(200);
            $(".maincontainer .searcharea .addedEntitiesBox ").fadeOut(200, function () {
                $(".maincontainer .searcharea .addedEntitiesBox .title").text("");
            });
        }
        else if ($("input[name='option']:checked").val() == 'compare') {
            if ($(".maincontainer .searcharea .addedEntitiesBox .title").html() != "compare between") {
                $(".maincontainer .searcharea .searchbox .addbutton").fadeIn(200);

                $(".maincontainer .searcharea .addedEntitiesBox .title").fadeOut(200, function () {
                    $(".maincontainer .searcharea .addedEntitiesBox .title").text("Compare between");
                    $(".maincontainer .searcharea .addedEntitiesBox").fadeIn(200);
                    $(".maincontainer .searcharea .addedEntitiesBox .title").fadeIn(200);
                });
            }
        }
        else if ($("input[name='option']:checked").val() == 'relate') {
            if ($(".maincontainer .searcharea .addedEntitiesBox .title").html() != "Relate between") {
                $(".maincontainer .searcharea .searchbox .addbutton").fadeIn();
                $(".maincontainer .searcharea .addedEntitiesBox").fadeIn();
                $(".maincontainer .searcharea .addedEntitiesBox .title").fadeOut(200, function () {
                    $(".maincontainer .searcharea .addedEntitiesBox .title").text("Relate between");
                    $(".maincontainer .searcharea .addedEntitiesBox .title").fadeIn(200);
                });
            }
        }
    });

    ///<summary> 
    ///changes the opacity if the add button when the main text bow is empty or not
    ///</summary>

    $(".maincontainer .searcharea .searchbox .addbutton").css("opacity", ".3");
    $(".maincontainer .searcharea .searchbox .searchinput").keyup(function () {
        if (!enable() || stopadd()) {
            $(".maincontainer .searcharea .searchbox .addbutton").css("opacity", ".3");
        }
        else {
            $(".maincontainer .searcharea .searchbox .addbutton").css("opacity", "1");
        }
    });


    $(".maincontainer").change(addedNum());


    $(".maincontainer .searcharea .searchbox .addbutton").click(function () {
        addObject();
        $(".maincontainer .searcharea .searchbox .addbutton").css("opacity", ".3");
        $('.maincontainer .searcharea .searchbox .searchinput').focus();
        return false;
    });

    ///<summary> 
    ///starts the search process when the user clicks on the search button
    ///if empty return false esle return true 
    /// 
    ///
    ///</summary>

    $(".maincontainer .searcharea .searchbox .searchbutton").click(function () {

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



    $('.maincontainer .searcharea .searchbox .searchinput').bind('keypress', function (e) {                // Enter pressed... do anything here...
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

