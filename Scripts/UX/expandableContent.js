/// <reference path="../jquery-1.4.1-vsdoc.js" />


$(document).ready(funciton(){

//cells in the compare page
    $(".datacell:.expandablecontent").each(function (){
    
    if ($(this).children("span")> 6 )
    {
    var hidden = "<div class='hidden' style='display:none;'>"+$(this).html()+"</div>";

        $(this).empty();
        $(this).append($(this).children("span").slice(0,6);
        $(this).append(hidden);

    }
    
   
    });





});