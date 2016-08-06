/// <reference path="../../_references.js" />

$(document).ready(function () {

    var _getLocation = null;

    $("#txtSearch").focus();
    $("#txtSearch").keypress(function (e) {
        if (e.which == "13") {
            $("#btnSearch").click();
        }
    });

    $(".labelCheckBox").click(function () {
        ToggleCheckBox($(this).attr("data-checkbox-name"));
    });

    $("#btnSearch").click(function () {

        if (_getLocation == null) {
            var _location = window.location.pathname;
            var _queryString = "";
            var _platformQueryString = "";

            var _searchTerm = $("#txtSearch").val();
            var _checkedPC = $("#chkPC").is(":checked");
            var _checkedPS4 = $("#chkPS4").is(":checked");
            var _checkedPS3 = $("#chkPS3").is(":checked");
            var _checkedXONE = $("#chkXONE").is(":checked");
            var _checkedX360 = $("#chkX360").is(":checked");
            var _checkedWiiU = $("#chkWiiU").is(":checked");
            var _checkedWii = $("#chkWii").is(":checked");
            var _checkedMAC = $("#chkMAC").is(":checked");
            var _checkedLinux = $("#chkLinux").is(":checked");

            if (_searchTerm != "") {
                _queryString = "?name=" + _searchTerm;
            }

            if (!(_checkedPC &&
                 _checkedPS4 &&
                 _checkedPS3 &&
                 _checkedXONE &&
                 _checkedX360 &&
                 _checkedWiiU &&
                 _checkedWii &&
                 _checkedMAC &&
                 _checkedLinux)) {

                if ($("#chkPC").is(":checked")) {
                    _platformQueryString = AddPlatformQueryString(_platformQueryString, "PC");
                }

                if ($("#chkPS4").is(":checked")) {
                    _platformQueryString = AddPlatformQueryString(_platformQueryString, "PS4");
                }

                if ($("#chkPS3").is(":checked")) {
                    _platformQueryString = AddPlatformQueryString(_platformQueryString, "PS3");
                }

                if ($("#chkXONE").is(":checked")) {
                    _platformQueryString = AddPlatformQueryString(_platformQueryString, "XONE");
                }

                if ($("#chkX360").is(":checked")) {
                    _platformQueryString = AddPlatformQueryString(_platformQueryString, "X360");
                }

                if ($("#chkWiiU").is(":checked")) {
                    _platformQueryString = AddPlatformQueryString(_platformQueryString, "WIIU");
                }

                if ($("#chkWii").is(":checked")) {
                    _platformQueryString = AddPlatformQueryString(_platformQueryString, "WII");
                }

                if ($("#chkMAC").is(":checked")) {
                    _platformQueryString = AddPlatformQueryString(_platformQueryString, "MAC");
                }

                if ($("#chkLinux").is(":checked")) {
                    _platformQueryString = AddPlatformQueryString(_platformQueryString, "LINUX");
                }
            }

            if (_platformQueryString != "") {

                if (_queryString != "") {
                    _queryString = _queryString + "&" + _platformQueryString;
                }
                else {
                    _queryString = "?" + _platformQueryString;
                }
            }

            _location = _location + _queryString;
        }
        else {
            _location = _getLocation;
            _getLocation = null;
        }

        window.location.replace(_location);
    });

});

function AddPlatformQueryString(queryString, param) {

    if (queryString == "") {
        return "platform=" + param;
    }
    else
        return queryString + "," + param;
}


