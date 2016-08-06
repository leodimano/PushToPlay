/// <reference path="../../_references.js" />

$(document).ready(function () {

    var _getLocation = null;

    $("#btnSearch").click(function () {

        var _location = null;

        if (_getLocation == null) {

            var _queryString = "";
            var _searchTerm = $("#txtSearch").val();

            _location = window.location.pathname;

            if (_searchTerm != "") {
                _queryString = "?name=" + _searchTerm;
            }

            if (_queryString != "") {
                _location = _location + _queryString;
            }            
        }
        else {
            _location = _getLocation;
            _getLocation = null;
        }

        window.location.replace(_location);

    });

});