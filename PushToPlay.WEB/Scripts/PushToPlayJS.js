/// <reference path="_references.js" />

$(document).ready(function () {

    $("div > .pagerButton").click(function () {
        _getLocation = GetUrlWithPageNumber($(this).attr("data-pager-selected"));
        window.location = _getLocation;
    });

});

function RequestLoginPopUp() {
    $('#myModal').reveal({
        animationspeed: 300,                       //how fast animtions 
        animation: 'fade',                   //fade, fadeAndPop, noneare
        closeonbackgroundclick: true,              //if you click background will modal close?
        dismissmodalclass: 'close-reveal-modal'
    });
}

function HandleLogin(data, formName) {
    if (data.Success == "1") {
        var _loginFormName = document.getElementById('loginformname');
        if (_loginFormName != null) {
            document.getElementById(_loginFormName.value).submit();
        }
        else if (formName != null) {
            document.getElementById(formName).submit();
        } else {
            location.reload();
        }
    }
}

// Visual Scripts
function FadeIn(elementId) {
    $('#' + elementId).fadeIn("slow", function () {
        // FadeIn Complete
    });
}

function FadeOut(elementId) {
    $('#' + elementId).fadeOut("slow", function () {
        // FadeOut Complete
    });
}

// Functions
function ToggleCheckBox(checkBoxSelector_) {
    var _checked = $(checkBoxSelector_).is(":checked");
    $(checkBoxSelector_).prop("checked", !_checked);
}

function GetUrlWithPageNumber(pageNumber_) {

    var _location = window.location.toString();
    var _queryString = "";

    var _urlAndQueryString = _location.split("?");

    if (_urlAndQueryString.length > 1) {

        var _query = _urlAndQueryString[1];
        var _queryItems = _query.split("&");

        for (var i = 0; i <= _queryItems.length - 1; i++) {
            if (_queryItems[i].indexOf("page") <= -1) {
                if (_queryString == "") {
                    _queryString = "?" + _queryItems[i];
                }
                else {
                    _queryString = _queryString + "&" + _queryItems[i];
                }
            }
        }

        if (_queryString == "") {
            _queryString = "?page=" + pageNumber_;
        }
        else {
            _queryString = _queryString + "&page=" + pageNumber_;
        }

    }
    else {
        _queryString = "?page=" + pageNumber_;
    }

    return window.location.pathname + _queryString;
}