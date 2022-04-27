
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

    let treeConfig = {
        ParentDiv: "svgWrapper",
        nodeClick: nodeClicked
    }


    let tree = new treeViewer(treeConfig);
    tree.LoadTree(window.SelectedAdventure.StartStep);

    $("#btnZoomIn").click(function () { tree.zoomIn() });
    $("#btnZoomOut").click(function () { tree.zoomOut() });
    $("#btnResetZoom").click(function () { tree.resetZoom() });
    $("#btnPanLeft").click(function () { tree.panLeft() });
    $("#btnPanRight").click(function () { tree.panRight() });
    $("#btnCenter").click(function () { tree.center() });

    $("#svgWrapper").on("tree_editor:data_updated", function (d) {
        //console.log(d.treeData);
        SelectedAdventure = d.treeData;
    });
}


function nodeClicked(d, callBack) {
    console.log(d);
    callBack(d);
}