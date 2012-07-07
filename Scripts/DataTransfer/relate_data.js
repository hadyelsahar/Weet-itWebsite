/// <reference path="../jquery-1.4.1-vsdoc.js" />
var globalRelations = new Array();
var ajaxSuccessCounter = 2;
var maxDistance = 4;
var relation = 0;
var play =1;
var counter = 0;
function drawDistance(distance,relation) {

    var id = setInterval(function () {
        if (play == 1) {
            drawnode(globalRelations[distance - 2][relation]);
            relation++;
        }
        if (relation >= globalRelations[distance - 2].length) {
            clearInterval(id);
        }

    }, 5000);
}

function playandpause() {
    if (play == 1) {
        play = 0;
        $(".playButton").css("background-image", "url('../img/playIcon.png')");
        $(".loading").stop();
        $(".loading").fadeOut();

    }

    else {
        play = 1;
        $(".playButton").css("background-image", "url('../img/pauseIcon.png')");
        $(".loading").stop();
        $(".loading").fadeIn();

    }
        
    
}


function drawnode(x) {
    var json;
    var shape;
    var color;
    var id;
    
    var previd;
    json = "{nodes: {";


    for (i2 in x) {
        if (i2 == 0)
        {id = x[i2].name;  shape = "roundedrectangle"; color = "#372b2b"; }
        else if (i2 == x.length - 1)
        { id = x[i2].name; shape = "roundedrectangle"; color = "#372b2b"; driver.addEdge(previd, id); }
        else if (i2 % 2 == 0)
        { id = x[i2].name + counter++; shape = "roundedrectangle"; color = "#4C6191"; driver.addEdge(previd, id); }
        else
        { id = x[i2].name + counter++; shape = "rectangle"; color = "#9E9E9E"; driver.addEdge(previd, id); }
       //debugger;
        driver.addNode(id, { "uri": x[i2].uri, "shape": shape, "color": color, "label": x[i2].name, "fixed": true });
       
        previd = id;
    }

    //4C6191
//    for (i2 = 0; i2 < x.length - 1; i2++) {
//        driver.addEdge(x[i2].name, x[i2 + 1].name);
  //  }

}



function ajaxGetRelation(distance) {

    if (play == 1 && ajaxSuccessCounter <= maxDistance) {
        $.ajax({
            type: "POST",
            url: "relate.aspx/getRelation",
            data: "{'URI':'" + mainNodes + "','distance':'" + distance + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (msg) {
                eval("var x = " + msg.d);
                console.log(x);
                globalRelations.push(x);
                    drawDistance(ajaxSuccessCounter, 0);
                    ajaxSuccessCounter++;
                    ajaxGetRelation(ajaxSuccessCounter);

            }
        });
    }
}

$("document").ready(function () {

    ajaxGetRelation(ajaxSuccessCounter);
});




 