/// <reference path="../jquery-1.4.1-vsdoc.js" />
/// <reference path="../arborjs/arbor.js" />
/// <reference path="Interaction_Driver.js" />
/// <reference path="renderer.js" />

var ArborDriver = function () {

    var Variable = {

        sys: {}
,
        InteractionDriver: {}
,
        initialize: function (canvasView) {

            this.sys = arbor.ParticleSystem({ 'repulsion': 10, 'stiffness': 280, 'friction': 0.5 });
            this.sys.parameters({ gravity: true });

            this.InteractionDriver = InteractionDriver(this.sys);
            this.sys.renderer = Renderer(canvasView, this.InteractionDriver);
        }
,
        addNode: function (name, data) {

            this.sys.addNode(name, data)
        }
        ,
        addNodes: function (data) {

            this.sys.graft(data);
        }
        ,
        addEdge: function (source, target, data) {

            this.sys.addEdge(source, target, data)
        }
,
        getSelected: function () {
            return (this.InteractionHandler.getSelectedNodes());
        }
,
        getSelectedURIs: function () {

            return (this.InteractionHandler.getSelectedNodesURIs());
        }

    }

    return Variable;
}