$(document).ready(function () {

    $(document).tooltip(
        {
            track: true,
            position: {my: "right-15 center", at:"left center" },
            content: function () {
                var _title = $(this).attr('data-feed-title');
                var _percent = $(this).attr('data-feed-percent');
                var _price = $(this).attr('data-feed-price');
                var _discountPrice = $(this).attr('data-feed-discount-price');
                var _platform = $(this).attr('data-feed-platform');

                var _imageCSS = null;
                var _titleCSS = "";

                var _return = "";
                
                // SteamPlatform
                if (_platform == '1')
                {
                    _imageCSS = "divFeedSteamPlatformImage";
                }

                // NuuvemPlatform
                if (_platform == '2')
                {
                    _imageCSS = "divFeedNuuvemPlatformImage";
                }

                if (_title.length < 10) {
                    _titleCSS = "divFeedGameTitleMargin"
                }

                if (_imageCSS != null) {
                    _return = "<div class='divFeedGamePromo contentColor'><div class='displayAreaColor divFeedGameTitle fieldText'><div class='" + _imageCSS + "'></div><span class='" + _titleCSS + "'>" + _title + "</span></div><div class='divFeedGameColumn1' ><div class='displayAreaColor divFeedGamePercent fieldText'>" + _percent + "</div></div><div class='divFeedGameColumn2' ><div class='divFeedOriginalPrice'><strike>" + _price + "</strike></div><div class='divFeedDiscountPrice'>" + _discountPrice + "</div></div><div class='clearBoth' /></div>";
                }

                return _return;
            }
        });
});