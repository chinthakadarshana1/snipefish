


function treeEditor(configurations) {
    "use strict";

    let PublicProps = {}
    let margin = { top: 10, right: 10, bottom: 10, left: 10 }, svgWidth = 0, svgHeight = 0;
    let i = 0, duration = 750, root;

    let parentDivId = initTreeEditor();
    let zoom = setupZoom();
    setupTree();

    function initTreeEditor() {
        if (!d3)
            throw "D3 library not found";

        if (!configurations)
            throw "Invalid configurations";

        if (!configurations.ParentDiv)
            throw "Invalid configurations";

        let parentDivId = '#' + configurations.ParentDiv;
        let parentDiv = $(parentDivId);

        svgWidth = parentDiv.width() - margin.left - margin.right;
        svgHeight = parentDiv.height() - margin.top - margin.bottom;

        let svgEditor = parentDiv.find(".svg-editor")[0];

        if (!svgEditor)
            throw ".svg-editor SVG element not found";

        $(svgEditor).attr("width", svgWidth)
            .attr("height", svgHeight);

        return parentDivId;
    }

    function setupZoom() {
        PublicProps.initZoom = function () {
            d3.select(parentDivId).select('svg')
                .call(zoom);
        }

        PublicProps.handleZoom = function () {
            d3.select(parentDivId).select('svg g')
                .attr('transform', d3.event.transform);
        }

        PublicProps.zoomIn = function () {
            d3.select(parentDivId).select('svg')
                .transition()
                .call(zoom.scaleBy, 2);
        }

        PublicProps.zoomOut = function () {
            d3.select(parentDivId).select('svg')
                .transition()
                .call(zoom.scaleBy, 0.5);
        }

        PublicProps.resetZoom = function () {
            d3.select(parentDivId).select('svg')
                .transition()
                .call(zoom.scaleTo, 1);
        }

        PublicProps.center = function () {
            d3.select(parentDivId).select('svg')
                .transition()
                .call(zoom.translateTo, 0.5 * svgWidth, 0.5 * svgHeight);
        }

        PublicProps.panLeft = function () {
            d3.select(parentDivId).select('svg')
                .transition()
                .call(zoom.translateBy, -50, 0);
        }

        PublicProps.panRight = function () {
            d3.select(parentDivId).select('svg')
                .transition()
                .call(zoom.translateBy, 50, 0);
        }

        return d3.zoom()
            .scaleExtent([0.25, 10])
            .on('zoom', PublicProps.handleZoom);

    }

    function setupTree() {
        let svg = d3.select(parentDivId).select("svg g")
            .attr("transform", "translate("
                + margin.left + "," + margin.top + ")");

        d3.select(parentDivId).select('svg').call(zoom);

        var treemap = d3.tree().size([svgHeight, svgWidth]);


        // Collapse the node and all it's children
        function collapse(d) {
            if (d.children) {
                d._children = d.children;
                d._children.forEach(collapse);
                d.children = null;
            }
        }

        function update(source) {
            // Assigns the x and y position for the nodes
            let treeData = treemap(root);

            // Compute the new tree layout.
            let nodes = treeData.descendants(),
                links = treeData.descendants().slice(1);

            // Normalize for fixed-depth.
            nodes.forEach(function (d) { d.y = d.depth * 220 });

            // ****************** Nodes section ***************************

            // Update the nodes...
            let node = svg.selectAll('g.node')
                .data(nodes, function (d) { return d.id || (d.id = ++i); });

            // Enter any new modes at the parent's previous position.
            let nodeEnter = node.enter().append('g')
                .attr('class', 'node')
                .attr("transform",
                    function (d) {
                        return "translate(" + source.y0 + "," + source.x0 + ")";
                    });

            // Add Rect for the nodes
            nodeEnter.append('rect')
                .attr('class', 'node')
                .attr('width', 100)
                .attr('height', 60)
                .attr('y', -30)
                .attr('cursor', 'pointer')
                .style("fill", function (d) {
                    return d._children ? "lightsteelblue" : "#d6e2ec";
                })
                .on('click', function (d) { configurations.nodeClick(d, nodeClickInternal); });


            //add new button
            loadActionBtn(nodeEnter, 0, -40, function (d) { configurations.addClick(d, addClickInternal); }, "+");

            //remove button
            loadActionBtn(nodeEnter, 20, -40, function (d) { configurations.removeClick(d, removeClickInternal) }, "-");

            //edit button
            loadActionBtn(nodeEnter, 40, -40, function (d) { configurations.editClick(d, editClickInternal) }, "E");


            // Add labels for the nodes
            nodeEnter.append('text')
                .attr("dy", ".35em")
                .attr("x", function (d) {
                    return 10;
                })
                .attr("text-anchor", function (d) {
                    //return d.children || d._children ? "end" : "start";
                    return "start";
                })
                .text(function (d) { return d.data.name; })
                .attr('cursor', 'pointer')
                .on('click', function (d) { configurations.nodeClick(d, nodeClickInternal); });

            // UPDATE
            var nodeUpdate = nodeEnter.merge(node);

            // Transition to the proper position for the node
            nodeUpdate.transition()
                .duration(duration)
                .attr("transform", function (d) {
                    return "translate(" + d.y + "," + d.x + ")";
                });


            // Remove any exiting nodes
            var nodeExit = node.exit().transition()
                .duration(duration)
                .attr("transform", function (d) {
                    return "translate(" + source.y + "," + source.x + ")";
                })
                .remove();

            // On exit reduce the node circles size to 0
            nodeExit.select('rect')
                .attr('width', 0)
                .attr('heigth', 0);

            // On exit reduce the opacity of text labels
            nodeExit.select('text')
                .style('fill-opacity', 1e-6);

            // ****************** links section ***************************

            // Update the links...
            var link = svg.selectAll('path.link')
                .data(links, function (d) { return d.id; });

            // Enter any new links at the parent's previous position.
            var linkEnter = link.enter().insert('path', "g")
                .attr("class", function (d) { return d._children ? "link active" : "link"; })
                .attr('d', function (d) {
                    var o = { x: source.x0, y: source.y0 };
                    return diagonal(o, o);
                });

            // UPDATE
            var linkUpdate = linkEnter.merge(link);

            // Transition back to the parent element position
            linkUpdate.transition()
                .duration(duration)
                .attr('d', function (d) { return diagonal(d, d.parent) });

            // Remove any exiting links
            var linkExit = link.exit().transition()
                .duration(duration)
                .attr('d', function (d) {
                    var o = { x: source.x, y: source.y };
                    return diagonal(o, o);
                })
                .remove();

            // Store the old positions for transition.
            nodes.forEach(function (d) {
                d.x0 = d.x;
                d.y0 = d.y;
            });

            // Creates a curved (diagonal) path from parent to the child nodes
            function diagonal(s, d) {
                let path = `M ${s.y} ${s.x}
                        C ${(s.y + d.y) / 2} ${s.x},
                        ${(s.y + d.y) / 2} ${d.x},
                        ${d.y} ${d.x}`;
                return path;
            }
        }


        // Toggle children on nodeClick.
        function nodeClickInternal(d) {
            if (d.children) {
                d._children = d.children;
                d.children = null;
            } else {
                d.children = d._children;
                d._children = null;
            }

            update(d);
        }

        // Add new Node
        function addClickInternal(d) {
            let newChild = { "name": "chin" };

            if (!d.data.children) {
                d.data.children = [];
            }
            d.data.children.push(newChild);

            var newNode = d3.hierarchy(newChild);
            newNode.depth = d.depth + 1;
            newNode.height = d.height - 1;
            newNode.parent = d;

            if (!d.children) {
                if (!d._children) {
                    d._children = [];
                }
                d._children.push(newNode);
                update(d);
                nodeClickInternal(d);
            } else {
                if (!d.children) {
                    d.children = [];
                }
                d.children.push(newNode);
                update(d);
            }
        }

        // Remove node
        function removeClickInternal(d) {
            let index = d.parent.data.children.length;
            while (index--) {
                if (d.parent.data.children[index].name === d.data.name) {
                    if (d.parent._children) {
                        d.parent._children.splice(index, 1);
                    }

                    if (d.parent.children) {
                        d.parent.children.splice(index, 1);
                    }

                    d.parent.data.children.splice(index, 1);
                    break;
                }
            }

            if (d.parent.data.children.length == 0) {
                nodeClickInternal(d.parent);
            } else {
                update(d.parent);
            }
        }

        //edit node
        function editClickInternal(d) {
            //todo
        }


        function loadActionBtn(node, x, y, onClick, text) {
            node.append('rect')
                .attr('class', 'node-action')
                .attr('width', 20)
                .attr('height', 20)
                .attr('y', y)
                .attr('x', x)
                .attr('cursor', 'pointer')
                .on('click', onClick);

            node.append('text')
                .attr("dy", ".35em")
                .attr("x", function (d) {
                    return x + 12;
                })
                .attr("y", function (d) {
                    return y + 10;
                })
                .attr("text-anchor", function (d) {
                    return "end";
                })
                .text(function (d) { return text; })
                .attr('cursor', 'pointer')
                .on('click', onClick);
        }


        PublicProps.LoadTree = function (data) {
            root = d3.hierarchy(data, function (d) { return d.children; });
            root.x0 = svgHeight / 2;
            root.y0 = 0;

            // Collapse after the second level //todo
            root.children.forEach(collapse);
            update(root);
        }

    }

    return PublicProps;
}

