// renderer takes interaction Driver to handle the click , hover effects on the node
/// <reference path="arbor.js" />
/// <reference path="Interaction_Driver.js" />
/// <reference path="graphics.js" />
/// <reference path="../jquery-1.4.1-vsdoc.js" />

function roundRect(ctx, x, y, width, height, radius, fill, stroke) {
/**
 * Draws a rounded rectangle using the current state of the canvas. 
 * If you omit the last three params, it will draw a rectangle 
 * outline with a 5 pixel border radius 
 * @param {CanvasRenderingContext2D} ctx
 * @param {Number} x The top left x coordinate
 * @param {Number} y The top left y coordinate 
 * @param {Number} width The width of the rectangle 
 * @param {Number} height The height of the rectangle
 * @param {Number} radius The corner radius. Defaults to 5;
 * @param {Boolean} fill Whether to fill the rectangle. Defaults to false.
 * @param {Boolean} stroke Whether to stroke the rectangle. Defaults to true.
 */
    if (typeof stroke == "undefined") {
        stroke = true;
    }
    if (typeof radius === "undefined") {
        radius = 5;
    }
    ctx.beginPath();
    ctx.moveTo(x + radius, y);
    ctx.lineTo(x + width - radius, y);
    ctx.quadraticCurveTo(x + width, y, x + width, y + radius);
    ctx.lineTo(x + width, y + height - radius);
    ctx.quadraticCurveTo(x + width, y + height, x + width - radius, y + height);
    ctx.lineTo(x + radius, y + height);
    ctx.quadraticCurveTo(x, y + height, x, y + height - radius);
    ctx.lineTo(x, y + radius);
    ctx.quadraticCurveTo(x, y, x + radius, y);
    ctx.closePath();
    if (stroke) {
        ctx.stroke();
    }
    if (fill) {
        ctx.fill();
    }
}

(function () {

    ///the main 

    Renderer = function (canvas, interactionDriver) {
        var canvas = $(canvas).get(0);
        var ctx = canvas.getContext("2d");
        var gfx = arbor.Graphics(canvas)
        var myInteractionDriver = interactionDriver;
        var particleSystem = null

        var that = {
           interactionDriver: myInteractionDriver,

           init: function (system) {
                particleSystem = system
                particleSystem.screenSize(canvas.width, canvas.height)
                particleSystem.screenPadding(40)
                that.initMouseHandling()
            },

           redraw: function () {
                if (!particleSystem) return

                gfx.clear() // convenience ƒ: clears the whole canvas rect

                // draw the nodes & save their bounds for edge drawing
                var nodeBoxes = {}
                particleSystem.eachNode(function (node, pt) {
                    // node: {mass:#, p:{x,y}, name:"", data:{}}
                    // pt:   {x:#, y:#}  node position in screen coords

                    // determine the box size and round off the coords if we'll be 
                    // drawing a text label (awful alignment jitter otherwise...)

                    var label = node.data.label || ""
                    var w = ctx.measureText("" + label).width + 10
                    if (!("" + label).match(/^[ \t]*$/)) {
                        pt.x = Math.floor(pt.x)
                        pt.y = Math.floor(pt.y)
                    } else {
                        label = null
                    }

                    // draw a rectangle centered at pt
                    if (node.data.color) ctx.fillStyle = node.data.color
                    else ctx.fillStyle = "rgba(0,0,0,.2)"
                    if (node.data.color == 'none') ctx.fillStyle = "white"

                    if (node.data.shape.toLowerCase() == 'dot' || node.data.shape.toLowerCase() == 'circle') {
                        gfx.oval(pt.x - w / 2, pt.y - w / 2, w, w, { fill: ctx.fillStyle })
                        nodeBoxes[node.name] = [pt.x - w / 2, pt.y - w / 2, w, w]
                    } 
                    else if (node.data.shape.toLowerCase() == 'roundedrectangle') {
                    
                    roundRect(ctx,pt.x -w /2 ,pt.y- 15 ,w,30,0,true,true);
                    nodeBoxes[node.name] = [pt.x - w / 2, pt.y - 15, w, 32]
                    
                    } 
                    else {
                        gfx.rect(pt.x - w / 2, pt.y - 10, w, 20, 4, { fill: ctx.fillStyle })
                        nodeBoxes[node.name] = [pt.x - w / 2, pt.y - 10, w,22]
                    }

                    // draw the text
                    if (label) {
                        ctx.font = "12px Helvetica"
                        ctx.textAlign = "center"
                        ctx.fillStyle = "white"
                        if (node.data.color == 'none') ctx.fillStyle = '#333333'
                        ctx.fillText(label || "", pt.x, pt.y + 4)
                        ctx.fillText(label || "", pt.x, pt.y + 4)
                    }
                })


                // draw the edges
                particleSystem.eachEdge(function (edge, pt1, pt2) {
                    // edge: {source:Node, target:Node, length:#, data:{}}
                    // pt1:  {x:#, y:#}  source position in screen coords
                    // pt2:  {x:#, y:#}  target position in screen coords

                    var weight = edge.data.weight
                    var color = edge.data.color

                    if (!color || ("" + color).match(/^[ \t]*$/)) color = null

                    // find the start point
                    var tail = intersect_line_box(pt1, pt2, nodeBoxes[edge.source.name])
                    var head = intersect_line_box(tail, pt2, nodeBoxes[edge.target.name])

                    ctx.save()
                    ctx.beginPath()
                    ctx.lineWidth = (!isNaN(weight)) ? parseFloat(weight) : 1
                    ctx.strokeStyle = (color) ? color : "#607797"
                    ctx.fillStyle = null

                    ctx.moveTo(tail.x, tail.y)
                    ctx.lineTo(head.x, head.y)
                    ctx.stroke()
                    ctx.restore()

                    // draw an arrowhead if this is a -> style edge
                    if (edge.data.directed) {
                        ctx.save()
                        // move to the head position of the edge we just drew
                        var wt = !isNaN(weight) ? parseFloat(weight) : 1
                        var arrowLength = 6 + wt
                        var arrowWidth = 2 + wt
                        ctx.fillStyle = (color) ? color : "#cccccc"
                        ctx.translate(head.x, head.y);
                        ctx.rotate(Math.atan2(head.y - tail.y, head.x - tail.x));

                        // delete some of the edge that's already there (so the point isn't hidden)
                        ctx.clearRect(-arrowLength / 2, -wt / 2, arrowLength / 2, wt)

                        // draw the chevron
                        ctx.beginPath();
                        ctx.moveTo(-arrowLength, arrowWidth);
                        ctx.lineTo(0, 0);
                        ctx.lineTo(-arrowLength, -arrowWidth);
                        ctx.lineTo(-arrowLength * 0.8, -0);
                        ctx.closePath();
                        ctx.fill();
                        ctx.restore()
                    }
                })
            },

           initMouseHandling: function () {
                // no-nonsense drag and drop (thanks springy.js)

                var selected = null;
                var nearest = null;
                var dragged = null;
                var oldmass = 1;
                var _mouseP = null;

                // set up a handler object that will initially listen for mousedowns then
                // for moves and mouseups while dragging

                var handler = {
                    doubleClicked: function (e) {

                        var pos = $(canvas).offset();
                        _mouseP = arbor.Point(e.pageX - pos.left, e.pageY - pos.top)
                        selected = nearest = particleSystem.nearest(_mouseP);

                        
                        myInteractionDriver.nodeDoubleClickHandler(selected.node);
                        selected = null;
                        _mouseP = null;

                        return false;

                    },

                    clicked: function (e) {
                        var pos = $(canvas).offset();
                        _mouseP = arbor.Point(e.pageX - pos.left, e.pageY - pos.top)
                        selected = nearest = particleSystem.nearest(_mouseP);

                        $(canvas).bind('mousemove', handler.dragged);
                        $(window).bind('mouseup', handler.dropped);

                        return false;
                    },

                    dragged: function (e) {

                        var old_nearest = nearest && nearest.node._id

                        //select the new position of the mouse S 
                        var pos = $(canvas).offset();
                        var s = arbor.Point(e.pageX - pos.left, e.pageY - pos.top)

                        //check if the dragged value is too large  > dragged 
                        if (Math.sqrt(Math.pow(_mouseP.x - s.x, 2) + Math.pow(_mouseP.y - s.y, 2)) >= 50) {
                            dragged = selected;
                        }

                        //if the value is small  not dragged only clicked
                        else {

                        }

                        if (!nearest) return;

                        if (dragged !== null && dragged.node !== null) {
                            var p = particleSystem.fromScreen(s);
                            dragged.node.p = p;
                        }

                        return false;
                    },

                    dropped: function (e) {
                        if (dragged === null) {

                            //Interaction Handling: regard it as clicked as it's not dragged.
                            myInteractionDriver.nodeClickedHandler(selected.node);

                            //Fixing the node.
                            selected.node.fixed = (selected.node.fixed) ? false : true;
                        }

                        else if (dragged.node !== null) {
                            //todo : interactionDriver Node dragged handler
                            dragged.node.tempMass = 1000;
                            dragged = null;
                            myInteractionDriver.nodeDraggedHandler(selected.node);

                        }

                        selected = null;
                        _mouseP = null;

                        $(canvas).unbind('mousemove', handler.dragged);
                        $(window).unbind('mouseup', handler.dropped);

                        return false;
                    }
                }

                $(canvas).mousedown(handler.clicked);
                $(canvas).dblclick(handler.doubleClicked);

            }

        }

        // helpers for figuring out where to draw arrows (thanks springy.js)
        var intersect_line_line = function (p1, p2, p3, p4) {
            var denom = ((p4.y - p3.y) * (p2.x - p1.x) - (p4.x - p3.x) * (p2.y - p1.y));
            if (denom === 0) return false // lines are parallel
            var ua = ((p4.x - p3.x) * (p1.y - p3.y) - (p4.y - p3.y) * (p1.x - p3.x)) / denom;
            var ub = ((p2.x - p1.x) * (p1.y - p3.y) - (p2.y - p1.y) * (p1.x - p3.x)) / denom;

            if (ua < 0 || ua > 1 || ub < 0 || ub > 1) return false
            return arbor.Point(p1.x + ua * (p2.x - p1.x), p1.y + ua * (p2.y - p1.y));
        }

        var intersect_line_box = function (p1, p2, boxTuple) {
            var p3 = { x: boxTuple[0], y: boxTuple[1] },
          w = boxTuple[2],
          h = boxTuple[3]

            var tl = { x: p3.x, y: p3.y };
            var tr = { x: p3.x + w, y: p3.y };
            var bl = { x: p3.x, y: p3.y + h };
            var br = { x: p3.x + w, y: p3.y + h };

            return intersect_line_line(p1, p2, tl, tr) ||
            intersect_line_line(p1, p2, tr, br) ||
            intersect_line_line(p1, p2, br, bl) ||
            intersect_line_line(p1, p2, bl, tl) ||
            false
        }

        return that
    }

})()