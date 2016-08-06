using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;

using PushToPlay.WEB;

namespace PushToPlay.HtmlHelpers
{
    public static class HtmlHelpers
    {
        #region Pager

        public enum Action
        {
            Number,
            Next,
            Back,
            First,
            Last
        }

        public static MvcHtmlString PushToPlayPager(this HtmlHelper html,
            int TotalPage, int CurrentPage)
        {
            if (TotalPage > 1)
            {
                string _htmlString = string.Empty;

                _htmlString = string.Concat(_htmlString, Pager_CreateButton(Action.First, 0, false, CurrentPage, TotalPage));
                _htmlString = string.Concat(_htmlString, Pager_CreateButton(Action.Back, 0, false, CurrentPage, TotalPage));

                if (TotalPage > 9)
                {
                    int maxBack = 4;
                    int maxForward = 8;
                    int drawButtonCount = 0;

                    if ((TotalPage - CurrentPage) <= 4)
                    {
                        maxBack = maxForward - (TotalPage - CurrentPage);
                    }

                    /* Draw back-pages button */
                    for (int i = CurrentPage - maxBack; i < CurrentPage; i++)
                    {
                        if (i > 0)
                        {
                            if (drawButtonCount == maxBack)
                                break;

                            _htmlString = string.Concat(_htmlString, Pager_CreateButton(Action.Number, i, false, CurrentPage, TotalPage));
                            drawButtonCount++;
                        }
                    }

                    _htmlString = string.Concat(_htmlString, Pager_CreateButton(Action.Number, CurrentPage, true, CurrentPage, TotalPage));

                    /* Draw next-pages button */
                    for (int i = (CurrentPage + 1); i <= TotalPage; i++)
                    {
                        if (drawButtonCount == maxForward)
                            break;

                        _htmlString = string.Concat(_htmlString, Pager_CreateButton(Action.Number, i, false, CurrentPage, TotalPage));
                        drawButtonCount++;
                    }
                }
                else
                {
                    for (int i = 1; i <= TotalPage; i++)
                    {
                        _htmlString = string.Concat(_htmlString, Pager_CreateButton(Action.Number, i, i == CurrentPage, CurrentPage, TotalPage));
                    }
                }

                _htmlString = string.Concat(_htmlString, Pager_CreateButton(Action.Next, 0, false, CurrentPage, TotalPage));
                _htmlString = string.Concat(_htmlString, Pager_CreateButton(Action.Last, 0, false, CurrentPage, TotalPage));

                return new MvcHtmlString(_htmlString);
            }
            else
            {
                return new MvcHtmlString(string.Empty);
            }
        }

        private static string Pager_CreateButton(Action action_, int pageNumber_, bool isCurrent_, int currentPage_, int totalPage_)
        {
            var _pagerButton = new TagBuilder("div");

            if (isCurrent_)
            {
                _pagerButton.AddCssClass("leftItem pagerButtonActive");
            }
            else
            {
                _pagerButton.AddCssClass("leftItem pagerButton");
            }
            

            switch (action_)
            {
                case Action.First:
                    _pagerButton.MergeAttribute("data-pager-selected", "1");
                    _pagerButton.InnerHtml = "<<";
                    break;
                case Action.Back:                   
                    if (currentPage_ > 1)
                        _pagerButton.MergeAttribute("data-pager-selected", (currentPage_ - 1).ToString());
                    else
                        _pagerButton.MergeAttribute("data-pager-selected", "1");

                    _pagerButton.InnerHtml = "<";
                    break;
                case Action.Number:
                    _pagerButton.MergeAttribute("data-pager-selected", pageNumber_.ToString());
                    _pagerButton.InnerHtml = pageNumber_.ToString();
                    break;
                case Action.Next:
                    if ((currentPage_ + 1) > totalPage_)
                    {
                        _pagerButton.MergeAttribute("data-pager-selected", totalPage_.ToString());
                    }
                    else
                    {
                        _pagerButton.MergeAttribute("data-pager-selected", (currentPage_ + 1).ToString());
                    }
                    
                    _pagerButton.InnerHtml = ">";
                    break;
                case Action.Last:
                    _pagerButton.MergeAttribute("data-pager-selected", totalPage_.ToString());
                    _pagerButton.InnerHtml = ">>";
                    break;
            }

            return _pagerButton.ToString(TagRenderMode.Normal);
        }

        #endregion

        #region Game Extensions

        public static MvcHtmlString PushToPlayActionGameHome(this HtmlHelper html,
            int GameDetaiId, string GameName)
        {
            UrlHelper _helper = new UrlHelper(html.ViewContext.RequestContext);
            return new MvcHtmlString(_helper.RouteUrl(Constants.ROUTE_GAME_HOME, new { id = GameDetaiId, gamename = WebHelper.ProcessUrlEnconding(GameName) }));
        }

        #endregion

        #region AjaxHelper Extensions

        public static MvcHtmlString RawActionLink(this AjaxHelper ajaxHelper, string linkText, string actionName, string controllerName, object routeValues, AjaxOptions ajaxOptions, object htmlAttributes)
        {
            var repID = Guid.NewGuid().ToString();
            var lnk = ajaxHelper.ActionLink(repID, actionName, controllerName, routeValues, ajaxOptions, htmlAttributes);
            return MvcHtmlString.Create(lnk.ToString().Replace(repID, linkText));
        }

        #endregion
    }
}