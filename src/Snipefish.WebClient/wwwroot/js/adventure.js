
$(document).ready(function () {

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

    function nodeClicked(d) {
        console.log(d);
        return true;
    }

    function addClicked(d) {
        console.log(d);

        //window.Snipfish.cHiNLoader(true);
        /*
        window.Snipfish.CommonFunctions.AjaxPost(Snipfish.Configurations.snipefishApiUrl + "todo", { "Name": "chin", "Description": "chin 12345" })
            .then(function (data) {
                //window.CommonFunctions.cHiNLoader(false);
            }.bind(this));
        */

        return true;
    }

    function removeClicked(d) {
        console.log(d);
        return true;
    }

    function editClicked(d) {
        console.log(d);
        return true;
    }
});


