/// <reference path="../../_references.js" />

$(document).ready(function () {

    $(document).on("click", "[data-related]", function () {

        var _relatedId = $(this).attr("data-related");

        var _relatedObjectVisibility = $("#" + _relatedId).is(":visible");

        if (!_relatedObjectVisibility) {
            FadeIn(_relatedId);
        } else {
            FadeOut(_relatedId);
        }
    });

    $(document).on("click", "[data-delete-id]", function () {

        var _messageId = $(this).attr("data-delete-id");

        if (_messageId != null) {

            $.post("/messenger/postmessage",
                   JSON.stringify({ MessageId: _messageId, Delete: true }),
                   function () {
                       location.reload(false);
                   });
        }

    });

    $(document).on("click", "button[data-reply-buttom='true']", function () {

        var _isLogged = $(this).attr("data-uid");

        if (_isLogged != "-1") {

            var _messageId = $(this).attr("id");
            var _baseType = $(this).attr("data-type");
            var _targetID = $(this).attr("data-target-id");
            var _targetType = $(this).attr("data-target-type");

            var _textMessage = $("#area" + _messageId).first().val();

            if (_textMessage != null && _textMessage != "") {

                $.post("/messenger/PostMessage",
                       JSON.stringify({
                           text: _textMessage,
                           MessageId: _messageId,
                           uId: _isLogged,
                           Delete: false,
                           BaseType: "1",
                           BaseId: _isLogged,
                           TargetType: _targetType,
                           TargetId: _targetID
                       }),
                        function () {
                            ClearTextArea();
                            location.reload(false);
                        });
            }

        }
        else {
            RequestLoginPopUp();
        }

    });

    $(document).on("click", "#CommentButton", function () {

        var _isLogged = $(this).attr("data-uid");

        if (_isLogged != "-1") {

            $("#CommentButton").attr("disabled", true);

            var _baseType = $(this).attr("data-type");
            var _targetID = $(this).attr("data-target-id");
            var _targetType = $(this).attr("data-target-type");

            var _textMessage = $("#AreaComment").first().val();

            if (_textMessage != null && _textMessage != "") {

                $.post("/messenger/PostMessage",
                       JSON.stringify({
                           text: _textMessage,
                           uId: _isLogged,
                           Delete: false,
                           BaseType: "1",
                           BaseId: _isLogged,
                           TargetType: _targetType,
                           TargetId: _targetID
                       }),
                        function () {
                            ClearTextArea();
                            $("#CommentButton").removeAttr("disabled");
                            location.reload(false);
                        });
            }
            else {
                $(this).removeAttr("disabled");
            }

        }
        else {
            RequestLoginPopUp();
        }

    });

});

function ClearTextArea() {

    $("textarea").each(function () {

        $(this).val("");

    });

}