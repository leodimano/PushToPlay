using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace PushToPlay.WEB.Helper
{
    public enum Action
    {
        NumberActive,
        Number,
        Next,
        Back,
        First,
        Last
    }

    public class Pager
    {

        public static string Draw(string formName_, int currentPage_, int totalPage_, string buttonName_ = "")
        {
            string _rawHtml = string.Empty;

            if (totalPage_ > 1)
            {
                HtmlGenericControl _div = new HtmlGenericControl("div");
                HtmlGenericControl _hidden = new HtmlGenericControl("input");
                HtmlGenericControl _paging = CreateHeaderDiv();
                HtmlGenericControl _ulPaging = CreateULHeader();

                _div.Attributes.Add("class", "textCenter");
                _ulPaging.Controls.Add(CreateLIControl(Action.First, currentPage_, totalPage_));
                _ulPaging.Controls.Add(CreateLIControl(Action.Back, currentPage_, totalPage_));

                if (totalPage_ > 9)
                {
                    int maxBack = 4;
                    int maxForward = 8;
                    int drawButtonCount = 0;

                    if ((totalPage_ - currentPage_) <= 4)
                    {
                        maxBack = maxForward - (totalPage_ - currentPage_);
                    }

                    /* Draw back-pages button */
                    for (int i = currentPage_ - maxBack; i < currentPage_; i++)
                    {
                        if (i > 0)
                        {
                            if (drawButtonCount == maxBack)
                                break;

                            _ulPaging.Controls.Add(CreateLIPage(Action.Number, i));
                            drawButtonCount++;
                        }
                    }

                    _ulPaging.Controls.Add(CreateLIPage(Action.NumberActive, currentPage_));

                    /* Draw next-pages button */
                    for (int i = (currentPage_ + 1); i <= totalPage_; i++)
                    {
                        if (drawButtonCount == maxForward)
                            break;

                        _ulPaging.Controls.Add(CreateLIPage(Action.Number, i));
                        drawButtonCount++;
                    }
                }
                else
                {
                    for (int i = 1; i <= totalPage_; i++)
                    {
                        if (i == currentPage_)
                        {
                            _ulPaging.Controls.Add(CreateLIPage(Action.NumberActive, i));
                        }
                        else
                        {
                            _ulPaging.Controls.Add(CreateLIPage(Action.Number, i));
                        }
                    }
                }

                _ulPaging.Controls.Add(CreateLIControl(Action.Next, currentPage_, totalPage_));
                _ulPaging.Controls.Add(CreateLIControl(Action.Last, currentPage_, totalPage_));

                _paging.Controls.Add(_ulPaging);

                _div.Controls.Add(_paging);

                using (System.IO.StringWriter _swriter = new System.IO.StringWriter())
                {
                    HtmlTextWriter _writer = new HtmlTextWriter(_swriter);
                    _div.RenderControl(_writer);
                    _rawHtml = _swriter.ToString();
                }

            }

            _rawHtml = string.Concat(_rawHtml, " ", GenerateJS(formName_, buttonName_));

            return _rawHtml;
        }

        private static string GenerateJS(string formName_, string buttonName_)
        {
            string _command = string.Empty;
            string _object = string.Empty;

            if (!string.IsNullOrWhiteSpace(buttonName_))
            {
                _command = "click()";
                _object = buttonName_;
            }
            else
            {
                _command = "submit()";
                _object = formName_;
            }

            return string.Concat(@"
<script type='text/javascript'>
function submitform(pageNumber) {
    document.getElementById('pageHidden').value = pageNumber;
    var _htmlForm = document.getElementById('", _object, @"');
    _htmlForm.", _command, @";
}

function clearPage(pageNumber) {
    document.getElementById('pageHidden').value = '';
}

</script>");
        }

        private static HtmlGenericControl CreateHeaderDiv()
        {
            HtmlGenericControl _div = new HtmlGenericControl("div");
            _div.Attributes.Add("class", "pagination");
            return _div;
        }

        private static HtmlGenericControl CreateULHeader()
        {
            return new HtmlGenericControl("ul");
        }

        private static HtmlGenericControl CreateLIControl(Action action_, int currentPage_, int totalPage_)
        {
            string _cssClassLi = string.Empty;
            string _cssClassA = string.Empty;
            string _page = string.Empty;
            HtmlGenericControl _li = new HtmlGenericControl("li");
            HtmlGenericControl _a = new HtmlGenericControl("a");

            _cssClassA = "paginationFieldText";

            switch (action_)
            {
                case Action.Next:

                    _cssClassLi = "next";

                    if (currentPage_ >= totalPage_)
                    {
                        _page = currentPage_.ToString();
                    }
                    else
                    {
                        _page = (++currentPage_).ToString();
                    }

                    break;
                case Action.Back:

                    _cssClassLi = "prev";

                    if (currentPage_ <= 1)
                    {
                        _page = currentPage_.ToString();
                    }
                    else
                    {
                        _page = (--currentPage_).ToString();
                    }

                    break;
                case Action.First:

                    _cssClassLi = "first";

                    _page = "1";

                    break;
                case Action.Last:

                    _cssClassLi = "last";

                    _page = totalPage_.ToString();

                    break;
            }

            _li.Attributes.Add("class", _cssClassLi);

            _a.Attributes.Add("class", _cssClassA);
            _a.Attributes.Add("onclick", string.Format("javascript:submitform({0})", _page));
            _a.Attributes.Add("href", "javascript:{}");

            _li.Controls.Add(_a);

            return _li;
        }

        private static HtmlGenericControl CreateLIPage(Action action_, int pageNumber_)
        {
            string _cssClassLi = string.Empty;
            string _cssClassA = string.Empty;
            HtmlGenericControl _li = new HtmlGenericControl("li");
            HtmlGenericControl _a = new HtmlGenericControl("a");

            switch (action_)
            {
                case Action.NumberActive:
                    _cssClassLi = "active";
                    _cssClassA = "paginationFieldTextSelected";
                    break;
                case Action.Number:
                    _cssClassA = "paginationFieldText";
                    break;
            }

            _li.Attributes.Add("class", _cssClassLi);

            _a.Attributes.Add("class", _cssClassA);
            _a.Attributes.Add("onclick", string.Format("javascript:submitform({0})", pageNumber_));
            _a.Attributes.Add("href", "javascript:{}");
            _a.InnerText = pageNumber_.ToString();

            _li.Controls.Add(_a);

            return _li;
        }
    }
}