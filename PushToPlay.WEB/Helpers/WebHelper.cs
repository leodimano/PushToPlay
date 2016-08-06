using System;
using System.Security.Principal;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

using PushToPlay.Model;
using System.Configuration;
using System.Web.UI.HtmlControls;
using System.Web.UI;

namespace PushToPlay.WEB
{
    public class WebHelper
    {
        #region Users helpers

        public static User GetLoggedUser()
        {
            IPrincipal _principalUser = HttpContext.Current.User;
            if (_principalUser != null)
            {
                if (_principalUser.Identity.IsAuthenticated)
                {
                    if (HttpContext.Current.Session[Constants.SESSION_LOGGED_USER] != null)
                    {
                        var _user = (Model.User)HttpContext.Current.Session[Constants.SESSION_LOGGED_USER];
                        if (string.Equals(_principalUser.Identity.Name, _user.UserName, StringComparison.CurrentCultureIgnoreCase))
                            return _user;
                        else
                            return null;
                    }
                    else
                    {
                        var _user = User.GetUserByUserName(_principalUser.Identity.Name);
                        HttpContext.Current.Session[Constants.SESSION_LOGGED_USER] = _user;
                        return _user;
                    }
                }
                else
                    return null;
            }
            else
                return null;
        }

        public static int GetLoggedUserId()
        {
            Model.User _user = WebHelper.GetLoggedUser();

            if (_user != null)
                return _user.Id;
            else
                return -1;
        }

        #endregion

        #region Image helpers

        public static string GetUserImagePath(int id_, bool generateRandom_)
        {
            string random = string.Empty;

            if (generateRandom_)
                random = string.Concat("?", DateTime.Now.Ticks.ToString());

            return System.IO.Path.Combine("~/", ConfigurationManager.AppSettings[Constants.CONFIG_KEY_USER_DISPLAY_IMAGE_PATH], string.Concat(id_, ".jpg", random));
        }

        public static string GetGameImagePath(string imageName_)
        {
            if (imageName_ != null)
                return System.IO.Path.Combine("~/", ConfigurationManager.AppSettings[Constants.CONFIG_KEY_GAME_DISPLAY_IMAGE_PATH], imageName_);
            else
                return string.Empty;
        }

        public static string GetGroupImagePath(int id_, bool generateRandom_)
        {
            string random = string.Empty;

            if (generateRandom_)
                random = string.Concat("?", DateTime.Now.Ticks.ToString());

            return System.IO.Path.Combine("~/", ConfigurationManager.AppSettings[Constants.CONFIG_KEY_GROUP_DISPLAY_IMAGE_PATH], string.Concat(id_, ".jpg", random));
        }

        #endregion

        #region Style helpers

        public static string GetCSSBackGroundGame(int platformId_)
        {
/*            switch (platformId_)
            {
                case 1: // MAC
                case 5: // PC
                case 9: // LINUX                
                        backGroundClass = string.Concat(backGroundClass, "darken");
                    break;
                case 2:
                    // XBOX 360
                    backGroundClass = string.Concat(backGroundClass, "green");
                    break;
                case 3:
                    // PLAYSTATION 3
                    backGroundClass = string.Concat(backGroundClass, "blue");
                    break;
                case 4:
                    // WII
                    backGroundClass = string.Concat(backGroundClass, "white");
                    break;
                case 6:
                    // Wii U
                    backGroundClass = string.Concat(backGroundClass, "white");
                    break;
                case 7:
                    // XBOX ONE
                    backGroundClass = string.Concat(backGroundClass, "greenLight");
                    break;
                case 8:
                    // PLAYSTATION 4
                    backGroundClass = string.Concat(backGroundClass, "blueDark");
                    break;
                default:
                    break;

            }
            return backGroundClass;*/

            return "displayAreaColor";
        }

        public static string GetCSSNormalBorderGame(int platformId_)
        {
            switch (platformId_)
            {
                case 1: // MAC
                case 9: // LINUX
                case 5: // PC
                    return "borderColor";
                case 2: // XBOX 360                    
                case 7: // XBOX ONE
                    return "xboxBorderColor";
                case 3: // PLAYSTATION 3
                case 8: // PLAYSTATION 4                    
                    return "psnBorderColor";
                case 4: // WII
                case 6: // WII U
                    return "borderColorLight";
                default:
                    return "borderColor";
            }
        }

        #endregion

        #region MVC helpers

        public static void SetReturnData(string action_ = "", string controller_ = "", object model_ = null)
        {
            SetActionNameInSession(action_);
            SetControllerNameInSession(controller_);
            SetModelInSession(model_);
        }

        public static bool IsActionNameInSession()
        {
            return HttpContext.Current.Session[Constants.SESSION_TRANSFER_TO_ACTION] != null;
        }

        public static bool IsControllerNameInSession()
        {
            return HttpContext.Current.Session[Constants.SESSION_TRANSFER_TO_CONTROLLER] != null;
        }

        public static bool IsModelInSession()
        {
            return HttpContext.Current.Session[Constants.SESSION_TRANSFER_TO_MODEL] != null;
        }

        public static void SetActionNameInSession(string actionName_)
        {
            HttpContext.Current.Session[Constants.SESSION_TRANSFER_TO_ACTION] = !string.IsNullOrWhiteSpace(actionName_) ? actionName_ : null;

        }

        public static void SetControllerNameInSession(string controllerName_)
        {
            HttpContext.Current.Session[Constants.SESSION_TRANSFER_TO_CONTROLLER] = !string.IsNullOrWhiteSpace(controllerName_) ? controllerName_ : null;
        }

        public static void SetModelInSession(object model_)
        {
            HttpContext.Current.Session[Constants.SESSION_TRANSFER_TO_MODEL] = model_ != null ? model_ : null;
        }

        public static string GetActionNameFromSession()
        {
            string _action = (string)HttpContext.Current.Session[Constants.SESSION_TRANSFER_TO_ACTION];
            HttpContext.Current.Session[Constants.SESSION_TRANSFER_TO_ACTION] = null;
            return _action;
        }

        public static string GetControllerNameFromSession()
        {
            string _controller = (string)HttpContext.Current.Session[Constants.SESSION_TRANSFER_TO_CONTROLLER];
            HttpContext.Current.Session[Constants.SESSION_TRANSFER_TO_CONTROLLER] = null;
            return _controller;
        }

        public static object GetModelFromSession()
        {
            object _model = (string)HttpContext.Current.Session[Constants.SESSION_TRANSFER_TO_MODEL];
            HttpContext.Current.Session[Constants.SESSION_TRANSFER_TO_MODEL] = null;
            return _model;
        }

        public static void GetActionButton(HtmlHelper helper_,
                                                 bool toAdd_,
                                               string buttonId_,
                                               string buttonText_)
        {
            string _javaScriptLogin = "javascript:RequestLoginPopUp();return false;";
            string _buttonIcon = string.Empty;
            string _buttonClass = string.Empty;

            if (toAdd_)
            {
                _buttonIcon = "icon-minus";
                _buttonClass = "buttonNOkNoMargin";
            }
            else
            {
                _buttonIcon = "icon-plus";
                _buttonClass = "buttonOkNoMargin";
            }

            HtmlGenericControl _button = new HtmlGenericControl("button");
            HtmlGenericControl _icon = new HtmlGenericControl("i");
            HtmlGenericControl _span = new HtmlGenericControl("span");

            _button.ID = buttonId_;
            _button.Attributes.Add("type", "submit");
            _button.Attributes.Add("class", string.Concat(_buttonClass, " no-padding no-margin"));

            if (GetLoggedUser() == null)
                _button.Attributes.Add("onclick", _javaScriptLogin);

            _icon.Attributes.Add("class", _buttonIcon);
            _span.InnerText = buttonText_;

            _button.Controls.Add(_icon);
            _button.Controls.Add(_span);

            string _rawHtml = string.Empty;

            using (System.IO.StringWriter _swriter = new System.IO.StringWriter())
            {
                HtmlTextWriter _hwriter = new HtmlTextWriter(_swriter);
                _button.RenderControl(_hwriter);

                _rawHtml = _swriter.ToString();
            }

            helper_.ViewContext.Writer.Write(_rawHtml.ToString());
        }

        public static void SetLayoutPath(ViewDataDictionary viewData_, string layoutPath_)
        {
            viewData_[Constants.VIEWDATA_LAYOUT_PATH] = layoutPath_;
        }

        public static string GetLayoutPath(ViewDataDictionary viewData_, string layoutPath_)
        {
            return (string)viewData_[Constants.VIEWDATA_LAYOUT_PATH];
        }

        #endregion

        #region Navigation

        public static ActionResult RequestAuth(string backToActionName_, string backToControllerName_, bool showMustBeLoggedMsg, object backToModel = null)
        {
            if (!string.IsNullOrWhiteSpace(backToActionName_) &&
               !string.IsNullOrWhiteSpace(backToControllerName_))
            {
                WebHelper.SetReturnData(backToActionName_, backToControllerName_, backToModel);
            }

            if (showMustBeLoggedMsg)
                HttpContext.Current.Session[Constants.SESSION_MUST_BE_LOGGED_MSG] = true;

            return new RedirectToRouteResult(new RouteValueDictionary(new { action = "login", controller = "user" }));
        }

        public static string ProcessUrlEnconding(string url_)
        {
            if (!string.IsNullOrWhiteSpace(url_))
            {
                return url_.Replace(".", string.Empty).
                            Replace(":", string.Empty).
                            Replace("/", string.Empty).
                            Replace("'", string.Empty).
                            Replace("&", string.Empty).
                            Replace("*", string.Empty).
                            Replace("¿", string.Empty).
                            Replace("?", string.Empty).
                            Replace("-", string.Empty).
                            Replace("@", string.Empty).
                            Replace("(", string.Empty).
                            Replace(")", string.Empty).
                            Replace(" ", "-").
                            Replace("--", "-").TrimStart('-').Trim('_').ToLower();
            }
            else
            {
                return string.Empty;
            }
        }

        #endregion

        #region DateTime Format

        public static string GetTimeSince(DateTime dateTime_)
        {
            int Months = 0;
            int Years = 0;

            TimeSpan _timeSpan = DateTime.Now - dateTime_;

            if (_timeSpan.Days > 0)
                Months = _timeSpan.Days / 30;

            if (Months > 12)
                Years = Months / 12;

            if (Years >= 1)
                return string.Format("há {0} ano{1}", Years, Years > 1 ? "s" : string.Empty);
            else if (Months >= 1)
                return string.Format("há {0} mes{1}", Months, Months > 1 ? "es" : string.Empty);
            else if (_timeSpan.Days >= 1)
                return string.Format("há {0} dia{1}", _timeSpan.Days, _timeSpan.Days > 1 ? "s" : string.Empty);
            else if (_timeSpan.Hours >= 1)
                return string.Format("há {0} hora{1}", _timeSpan.Hours, _timeSpan.Hours > 1 ? "s" : string.Empty);
            else if (_timeSpan.Minutes >= 1)
                return string.Format("há {0} minuto{1}", _timeSpan.Minutes, _timeSpan.Minutes > 1 ? "s" : string.Empty);
            else
                return "há menos de 1 minuto";
        }

        #endregion
    }
}