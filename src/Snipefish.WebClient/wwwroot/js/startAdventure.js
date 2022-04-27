
$(document).ready(function () {
    loadTree();
    bindUi();
});


function bindUi() {
    $("#btnFinishAdventure").click(finishAdventure);
}


function finishAdventure() {
    window.SelectedAdventure.IsFinished = true;
    window.Snipfish.CommonFunctions.cHiNLoader(true);

    window.Snipfish.CommonFunctions.AjaxPut(Snipfish.Configurations.snipefishApiUrl + "UserAdventures", window.SelectedAdventure)
        .then(function (data) {
            window.Snipfish.CommonFunctions.cHiNLoader(false);
            $.growl.notice({ title: "Adventure saved successfully", message: window.SelectedAdventure.Name + "Adventure successfully saved." });
            setTimeout(function () { redirectToViewPage(); }, 1000);
        }.bind(this));
}

function redirectToViewPage() {
    let viewUrl = window.location.href.replace("StartAdventure", "ViewAdventure");
    window.location.href = viewUrl;
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
        SelectedAdventure.StartStep = d.treeData;
        console.log(SelectedAdventure);
    });
}


function nodeClicked(d, callBack) {
    console.log(d);
    callBack(d);
}