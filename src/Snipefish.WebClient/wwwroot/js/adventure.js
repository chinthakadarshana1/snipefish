
var newAdventure = {
    "UserId": Snipfish.UserId,
    "StartStep": {},
    "Name": ""
};

$(document).ready(function () {
    
    loadTree();
    bindUi();
});


function bindUi() {
    $("#btnAddAdventure").click(saveAdventure);
}


function saveAdventure() {
    let advName = $("#txtAdventureName").val();

    if (advName) {
        newAdventure.Name = advName;
        window.Snipfish.CommonFunctions.cHiNLoader(true);

        window.Snipfish.CommonFunctions.AjaxPost(Snipfish.Configurations.snipefishApiUrl + "UserAdventures", newAdventure)
            .then(function (data) {
                window.Snipfish.CommonFunctions.cHiNLoader(false);
                $.growl.notice({ title: "Adventure saved successfully", message: newAdventure.Name + "Adventure successfully added." });
                setTimeout(function () { location.reload(); }, 1000);
            }.bind(this));

    }
}

function loadTree() {
    var treeData =
    {
        "StepId": uuidv4(),
        "Name": "Start",
        "IsSelected": true,
        "SubSteps": [
        ]
    };

    let treeConfig = {
        ParentDiv: "svgWrapper",
        addClick: addClicked,
        removeClick: removeClicked,
        editClick: editClicked,
        nodeClick: nodeClicked
    }


    let tree = new treeEditor(treeConfig);
    tree.LoadTree(treeData);

    $("#btnZoomIn").click(function () { tree.zoomIn() });
    $("#btnZoomOut").click(function () { tree.zoomOut() });
    $("#btnResetZoom").click(function () { tree.resetZoom() });
    $("#btnPanLeft").click(function () { tree.panLeft() });
    $("#btnPanRight").click(function () { tree.panRight() });
    $("#btnCenter").click(function () { tree.center() });

    $("#svgWrapper").on("tree_editor:data_updated", function (d) {
        //console.log(d.treeData);
        newAdventure.StartStep = d.treeData;
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
}


function addNewStep(d, callBack) {
    let newStepName = $("#txtStepName").val();
    if (newStepName) {
        let newStep = {
            "StepId": uuidv4(),
            "Name": newStepName,
            "IsSelected": false,
            "SubSteps": [
            ]
        };

        $('#modalAddNode').modal('hide');
        callBack(d, newStep);
    }
}


function removeClicked(d, callBack) {
    callBack(d);
}

function editClicked(d, callBack) {
    $("#txtStepName").val(d.data.Name);
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
        d.data.Name = modifiedName;
        $('#modalAddNode').modal('hide');
        callBack(d);
    }
}