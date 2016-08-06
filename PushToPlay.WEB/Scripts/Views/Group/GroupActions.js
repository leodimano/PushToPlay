/// <reference path="../../_references.js" />

$(document).ready(function () {

    $(document).on("click", "[data-group-action='leave']", function () {

        var _groupId = $(this).attr('data-group-id');
        var _targetId = $(this).attr('data-group-container');

        $.get("/Group/CompactLeave/" + _groupId,
               null,
               function (data) {
                   $('#' + _targetId).html(data);
               });
    });

    $(document).on("click", "[data-group-action='join']", function () {

        var _groupId = $(this).attr('data-group-id');
        var _targetId = $(this).attr('data-group-container');

        $.get("/Group/CompactJoin/" + _groupId,
       null,
       function (data) {
           $('#' + _targetId).html(data);
       });

    });


});