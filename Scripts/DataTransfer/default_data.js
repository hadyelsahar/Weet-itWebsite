/// <reference path="../jquery-1.4.1-vsdoc.js" />
/// <reference path="../json2.js" />

function submit(obj) {
    //<summary>
    //a function takes an object of the query and takes it's type 
    // if type is q&a pass it to the Question classifier method to get it's type 
    // redirect compare to compare page 
    // redirect relate to relate page 
    //<para>object contains the Question Type 
    //if the question is Q&A it will contain the question string 
    //other wise ti will contain array of things to compare between or relate between
    //</para>
    //</summary>

    if (obj.type.toLowerCase() == "q&a") {

        $.ajax({
            type: "POST",
            url: "Default.aspx/getQuestionType",
            data: "{'data':'" + obj.data + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (msg) {
                console.log(msg);
                eval("msg =" + msg.d);

                switch (msg.type) {
                    case "question":
                        window.location = "answer.aspx";
                        break;
                    case "comparison":
                        window.location = "compare.aspx";
                        break;
                    case "relate":
                        window.location = "relate.aspx";
                        break;

                }

            }
        });


    }
    else if (obj.type.toLowerCase() == "compare") {

        window.location = "compare.aspx";

    }
    else if (obj.type.toLowerCase() == "relate") {

        window.location = "relate.aspx";

    }





}


$(document).ready(function () {


});