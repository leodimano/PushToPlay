/// <reference path="../../_references.js" />

$(document).ready(function () {
    
    var _getLocation = null;

    $("#txtSearch").focus();
    $("#txtSearch").keypress(function (eventObject) {

        if (eventObject.which == 13) {
            $("#btnSearch").click();
        }
    });

});