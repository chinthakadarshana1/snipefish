
// Set the dimensions and margins of the diagram
var margin = { top: 20, right: 90, bottom: 30, left: 90 },
    width = 600 - margin.left - margin.right,
    height = 400 - margin.top - margin.bottom;

var treeData =
{
    "name": "Top Level",
    "children": [
        {
            "name": "Level 2: A",
            "children": [
                { "name": "Son of A" },
                {
                    "name": "Daughter of A"
                    , "children": [
                        { "name": "Son of A" },
                        { "name": "Son of A" },
                        { "name": "Daughter of A" }
                    ]
                }
            ]
        },
        { "name": "Level 2: B" }
    ]
};



let zoom = d3.zoom()
    .scaleExtent([0.25, 10])
    .on('zoom', handleZoom);

function initZoom() {
    d3.select('svg')
        .call(zoom);
}

function handleZoom() {
    d3.select('svg g')
        .attr('transform', d3.event.transform);
}

function zoomIn() {
    d3.select('svg')
        .transition()
        .call(zoom.scaleBy, 2);
}

function zoomOut() {
    d3.select('svg')
        .transition()
        .call(zoom.scaleBy, 0.5);
}

function resetZoom() {
    d3.select('svg')
        .transition()
        .call(zoom.scaleTo, 1);
}

function center() {
    d3.select('svg')
        .transition()
        .call(zoom.translateTo, 0.5 * width, 0.5 * height);
}

function panLeft() {
    d3.select('svg')
        .transition()
        .call(zoom.translateBy, -50, 0);
}

function panRight() {
    d3.select('svg')
        .transition()
        .call(zoom.translateBy, 50, 0);
}


// append the svg object to the body of the page
// appends a 'group' element to 'svg'
// moves the 'group' element to the top left margin
var svg = d3.select("svg g")
    .attr("transform", "translate("
        + margin.left + "," + margin.top + ")");

var i = 0,
    duration = 750,
    root;

// declares a tree layout and assigns the size
var treemap = d3.tree().size([height, width]);

// Assigns parent, children, height, depth
root = d3.hierarchy(treeData, function (d) { return d.children; });
root.x0 = height / 2;
root.y0 = 0;

// Collapse after the second level
root.children.forEach(collapse);


initZoom();
update(root);

// Collapse the node and all it's children
function collapse(d) {
    if (d.children) {
        d._children = d.children
        d._children.forEach(collapse)
        d.children = null
    }
}

function update(source) {

    // Assigns the x and y position for the nodes
    var treeData = treemap(root);

    // Compute the new tree layout.
    var nodes = treeData.descendants(),
        links = treeData.descendants().slice(1);

    // Normalize for fixed-depth.
    nodes.forEach(function (d) { d.y = d.depth * 220 });

    // ****************** Nodes section ***************************

    // Update the nodes...
    var node = svg.selectAll('g.node')
        .data(nodes, function (d) { return d.id || (d.id = ++i); });

    // Enter any new modes at the parent's previous position.
    var nodeEnter = node.enter().append('g')
        .attr('class', 'node')
        .attr("transform", function (d) {
            return "translate(" + source.y0 + "," + source.x0 + ")";
        })

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
        .on('click', nodeClick);


    // Add new button for the nodes
    nodeEnter.append('rect')
        .attr('class', 'node-action')
        .attr('width', 20)
        .attr('height', 20)
        .attr('y', -40)
        .attr('cursor', 'pointer')
        .on('click', addClick);


    // Add new button labels for the nodes
    nodeEnter.append('text')
        .attr("dy", ".35em")
        .attr("x", function (d) {
            return 12;
        })
        .attr("y", function (d) {
            return -30;
        })
        .attr("text-anchor", function (d) {
            //return d.children || d._children ? "end" : "start";
            return "end";
        })
        .text(function (d) { return "+"; })
        .attr('cursor', 'pointer')
        .on('click', addClick);


    // remove button for the nodes
    nodeEnter.append('rect')
        .attr('class', 'node-action')
        .attr('width', 20)
        .attr('height', 20)
        .attr('y', -40)
        .attr('x', 20)
        .attr('cursor', 'pointer')
        .on('click', removeClick);


    // remove button labels for the nodes
    nodeEnter.append('text')
        .attr("dy", ".35em")
        .attr("x", function (d) {
            return 32;
        })
        .attr("y", function (d) {
            return -30;
        })
        .attr("text-anchor", function (d) {
            //return d.children || d._children ? "end" : "start";
            return "end";
        })
        .text(function (d) { return "-"; })
        .attr('cursor', 'pointer')
        .on('click', removeClick);


    // edit button for the nodes
    nodeEnter.append('rect')
        .attr('class', 'node-action')
        .attr('width', 20)
        .attr('height', 20)
        .attr('y', -40)
        .attr('x', 40)
        .attr('cursor', 'pointer')
        .on('click', editClick);


    // edit button labels for the nodes
    nodeEnter.append('text')
        .attr("dy", ".35em")
        .attr("x", function (d) {
            return 52;
        })
        .attr("y", function (d) {
            return -30;
        })
        .attr("text-anchor", function (d) {
            //return d.children || d._children ? "end" : "start";
            return "end";
        })
        .text(function (d) { return "E"; })
        .attr('cursor', 'pointer')
        .on('click', editClick);


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
        .on('click', nodeClick);

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
            var o = { x: source.x0, y: source.y0 }
            return diagonal(o, o)
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
            var o = { x: source.x, y: source.y }
            return diagonal(o, o)
        })
        .remove();

    // Store the old positions for transition.
    nodes.forEach(function (d) {
        d.x0 = d.x;
        d.y0 = d.y;
    });

    // Creates a curved (diagonal) path from parent to the child nodes
    function diagonal(s, d) {
        path = `M ${s.y} ${s.x}
                    C ${(s.y + d.y) / 2} ${s.x},
                      ${(s.y + d.y) / 2} ${d.x},
                      ${d.y} ${d.x}`
        return path
    }

    // Toggle children on nodeClick.
    function nodeClick(d) {
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
    function addClick(d) {
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
            nodeClick(d);
        } else {
            if (!d.children) {
                d.children = [];
            }
            d.children.push(newNode);
            update(d);
        }
    }

    // Remove node
    function removeClick(d) {
        var index = d.parent.data.children.length;
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
            nodeClick(d.parent);
        } else {
            update(d.parent);
        }
    }

    //edit node
    function editClick(d) {
        //todo -popup
    }

}

