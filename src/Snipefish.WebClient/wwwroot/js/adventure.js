
$(document).ready(function () {
    loadTree();
});


function loadTree() {
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

    let treeConfig = {
        ParentDiv: "svgWrapper",
        addClick: addClicked,
        removeClick: removeClicked,
        editClick: editClicked,
        nodeClick: nodeClicked
    }


    let tree = treeEditor(treeConfig);
    tree.LoadTree(treeData);

    $("#btnZoomIn").click(function () { tree.zoomIn() });
    $("#btnZoomOut").click(function () { tree.zoomOut() });
    $("#btnResetZoom").click(function () { tree.resetZoom() });
    $("#btnPanLeft").click(function () { tree.panLeft() });
    $("#btnPanRight").click(function () { tree.panRight() });
    $("#btnCenter").click(function () { tree.center() });

    $("#svgWrapper").on("tree_editor:data_updated", function (d) {
        console.log(d.treeData);
    });
}


function nodeClicked(d, callBack) {
    console.log(d);
    callBack(d);
}

function addClicked(d, callBack) {
    $("#txtStepName").val("");
    $("#modalAddNodeLabel").html("Add Step");
    $('#modalAddNode').modal('show');
    $('#btnAddStep').off("click");
    $('#btnAddStep').click(function () {
        addNewStep(d, callBack)
    });

    //window.Snipfish.cHiNLoader(true);
    /*
    window.Snipfish.CommonFunctions.AjaxPost(Snipfish.Configurations.snipefishApiUrl + "todo", { "Name": "chin", "Description": "chin 12345" })
        .then(function (data) {
            //window.CommonFunctions.cHiNLoader(false);
        }.bind(this));
    */
    //callBack(d);
}


function addNewStep(d, callBack) {
    let newStepName = $("#txtStepName").val();
    if (newStepName) {
        let newStep = { name: newStepName };
        $('#modalAddNode').modal('hide');
        callBack(d, newStep);
    }
}


function removeClicked(d, callBack) {
    callBack(d);
}

function editClicked(d, callBack) {
    $("#txtStepName").val(d.data.name);
    $("#modalAddNodeLabel").html("Edit Step");
    $('#modalAddNode').modal('show');
    $('#btnAddStep').off("click");
    $('#btnAddStep').click(function () {
        editStep(d, callBack)
    });
}

function editStep(d, callBack) {
    let modifiedName = $("#txtStepName").val();
    if (modifiedName) {
        d.data.name = modifiedName;
        $('#modalAddNode').modal('hide');
        callBack(d);
    }
}