var globalRelations = new Array();
var ajaxcounter = 2;
var relation = 0;

function drawDistance(distance) {
    
    var id = setInterval(function () {


        //alert("draw distance");
        drawnode(globalRelations[distance-2][relation]);
        relation++;
        if (relation >= globalRelations[distance-2].length) {
            clearInterval(id);
        }

    }, 2000);
}

function drawnode(x) {
    var json;
    var shape;
    var color;
        json = "{nodes: {";

        for (i2 in x) {
            if (i2 % 2 == 0)
            { shape = "roundedrectangle"; color = "#372b2b"; }
            else
            { shape = "rectangle"; color = "#9E9E9E"; }

            // json += "\"" + x[i2].name + "\"" + " : { \"uri\": \" " + x[i2].uri + " \", \"shape\": \" " + shape + " \", \"color\": \" " + color+ " \", \"label\": \" " + x[i2].name + " \" },";
            driver.addNode(x[i2].name, { "uri": x[i2].uri, "shape": shape, "color": color, "label": x[i2].name }); 
        }
        //json= json.substring(0, json.length - 2);
        //json += "}},edges: {"


        for (i2 = 0; i2 < x.length - 1; i2++) {
            //  json += "\"" + x[i2].name + "\": { \"" + x[i2+1].name + " \": {} },";
            driver.addEdge(x[i2].name, x[i2 + 1].name);
        }
        // alert(json.length);
        //json = json.substring(0, json.length - 1);
        //alert("ay betengaaan");
        //json += "}}";
   // debugger;
        //console.log(json);
       // debugger;
   // eval("json =" +json);
   // driver.addNodes(json);
}



function ajaxGetRelation(distance) {
   
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
            if (ajaxcounter <= 4) {
                //debugger;
                drawDistance(ajaxcounter);
                ajaxcounter++;
                ajaxGetRelation(ajaxcounter);

            }
        }
    });
}




 