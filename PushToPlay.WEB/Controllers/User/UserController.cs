using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Configuration;

using PushToPlay.Model;
using PushToPlay.WEB.Models;
using PushToPlay.Helpers;

namespace PushToPlay.WEB.Controllers
{
    public class UserController : Controller
    {
        #region Login

        [HttpGet]
        public ActionResult Login()
        {
            ViewData[Constants.VIEWDATA_DRAW_LOGIN_BAR] = false;
            return View();
        }

        [HttpPost]
        public ActionResult Login(WEB.Models.UserLoginViewModel userLogin_)
        {
            ViewData[Constants.VIEWDATA_DRAW_LOGIN_BAR] = false;
            if (ModelState.IsValid)
            {
                ViewData[Constants.VIEWDATA_DO_NOT_DISPLAY_LOGIN_PARTIAL] = null;

                Model.User _loggedUser = new User();
                Model.User _user = new User()
                {
                    UserName = userLogin_.UserName,
                    Password = userLogin_.Password
                };

                _user = _user.CheckUserCredentials();

                if (_user != null)                
                {
                    FormsAuthentication.SetAuthCookie(_user.UserName, userLogin_.RememberMe);
                    Session[Constants.SESSION_LOGGED_USER] = _user;

                    ViewData[Constants.VIEWDATA_DRAW_LOGIN_BAR] = null;

                    if (WebHelper.IsActionNameInSession())
                    {
                        string _action = WebHelper.GetActionNameFromSession();
                        string _controller = WebHelper.GetControllerNameFromSession();

                        return RedirectToAction(_action, _controller);
                    }
                    else
                    {
                        return RedirectToAction("home", "user", new { id = _user.UserName.ToLower() });
                    }
                }
                else
                {
                    userLogin_.ModelErrorMsg = "Usuário e/ou senha inválidos.";
                    ViewData[Constants.VIEWDATA_DO_NOT_DISPLAY_LOGIN_PARTIAL] = true;
                }
            }
            else
            {
                userLogin_.ModelErrorMsg = "Usuário e/ou senha inválidos.";
                ViewData[Constants.VIEWDATA_DO_NOT_DISPLAY_LOGIN_PARTIAL] = true;
            }

            return View(userLogin_);
        }

        [HttpPost]
        public ActionResult FastLogin(WEB.Models.UserLoginViewModel userLogin_)
        {
            if (ModelState.IsValid)
            {
                Model.User _user = new User()
                {
                    UserName = userLogin_.UserName,
                    Password = userLogin_.Password
                };

                _user = _user.CheckUserCredentials();

                if (_user != null)
                {
                    FormsAuthentication.SetAuthCookie(_user.UserName, userLogin_.RememberMe);
                    Session[Constants.SESSION_LOGGED_USER] = _user;

                    return Json(new { Success = "1" });
                }
                else
                {
                    userLogin_.ModelErrorMsg = "Usuário e/ou senha inválidos.";
                }
            }
            else
            {
                userLogin_.ModelErrorMsg = "Usuário e/ou senha inválidos.";
            }

            return PartialView("LoginPartial", userLogin_);
        }

        #endregion

        #region Register

        [HttpGet]
        public ActionResult Register()
        {
            ViewData[Constants.VIEWDATA_DRAW_LOGIN_BAR] = false;
            return View();
        }

        [HttpPost]
        public ActionResult Register(WEB.Models.UserRegisterViewModel userRegister_)
        {
            ViewData[Constants.VIEWDATA_DRAW_LOGIN_BAR] = false;
            if (ModelState.IsValid)
            {
                Model.User _user = new User()
                {
                    UserName = userRegister_.UserName,
                    Password = userRegister_.Password,
                    Email = userRegister_.Email
                };


                if (_user.Register())
                {
                    ViewData[Constants.VIEWDATA_DRAW_LOGIN_BAR] = null;
                    FormsAuthentication.SetAuthCookie(_user.UserName, userRegister_.RememberMe);
                    return RedirectToAction("index", "home");
                }
                else
                {
                    userRegister_.ModelErrorMsg = _user.ModelErrorMsg;
                }
            }

            return View(userRegister_);
        }

        #endregion

        #region Logout

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("index", "home");
        }

        #endregion

        #region Profile

        [HttpGet]
        [Authorize]
        public ActionResult Profile()
        {
            UserProfileViewModel _userProfile = new UserProfileViewModel();
            return View(_userProfile);
        }

        [HttpPost]
        public ActionResult Profile(UserProfileViewModel userProfile_, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                var _user = WebHelper.GetLoggedUser();

                if (_user != null)
                {
                    _user.Name = userProfile_.Name;
                    _user.Description = userProfile_.Description.ToString();
                    //_user.SteamId = userProfile_.SteamId;
                    _user.PsnId = userProfile_.PsnId;
                    _user.GamerTag = userProfile_.GamerTag;
                    _user.OriginId = userProfile_.OriginId;
                    _user.SkynerdId = userProfile_.SkynerdProfile;
                    _user.Update();

                    if (file != null)
                    {
                        NetHelper.SaveFile(file.InputStream, NetHelper.GetIdFileName(_user.Id), ConfigurationManager.AppSettings[Constants.CONFIG_KEY_USER_SAVE_IMAGE_PATH]);
                    }

                    Session[Constants.SESSION_LOGGED_USER] = _user;
                    return RedirectToAction("home", "user", new { id = _user.UserName.ToLower() });
                }
                else
                {

                }
            }

            return View();
        }

        #endregion

        #region Home

        public ActionResult Home(string id, string page)
        {
            int _page;
            Int32.TryParse(page, out _page);
            if (_page <= 0)
                _page = 1;

            var _homeViewModel = new Models.UserHomeViewModel(id, _page);
            return View(_homeViewModel);
        }

        #endregion

        #region GameList

        public ActionResult Game(string id, string page)
        {
            if (!string.IsNullOrWhiteSpace(id))
            {
                int _pageNumber;
                Int32.TryParse(page, out _pageNumber);

                var _model = new UserGameListViewModel(id, _pageNumber);

                return View(_model);
            }

            return RedirectToAction("index", "home");
        }

        #endregion

        #region GroupList

        public ActionResult Group(string id, string page)
        {
            if (!string.IsNullOrWhiteSpace(id))
            {
                int _pageNumber;
                Int32.TryParse(page, out _pageNumber);

                var _model = new UserGroupListViewModel(id, _pageNumber);

                return View(_model);
            }

            return RedirectToRoute(Constants.ROUTE_HOME);
        }

        #endregion

        #region User Search

        [HttpGet]
        public ActionResult Search(string search, string page)
        {
            return View(SearchUsers(search, page));
        }

        private UserSearchViewModel SearchUsers(string search_, string page_)
        {
            UserSearchViewModel _model = new UserSearchViewModel();

            int _pageNumber;

            if (string.IsNullOrWhiteSpace(search_))
            {
                search_ = string.Empty;
            }

            Int32.TryParse(page_, out _pageNumber);

            using (User _user = new User())
            {
                var _listUser = _user.GetUserByUserNameOrName(search_, _pageNumber);

                if (_listUser != null)
                {
                    _model.SetUserList(_listUser.Objects);
                    _model.ObjectCount = _listUser.TotalObject;
                    _model.CurrentPage = _pageNumber;
                    _model.TotalPage = _listUser.TotalPages;

                    if (_model.ObjectCount > 1)
                        _model.ResultText = string.Format("{0} jogadores foram encontrados", _model.ObjectCount);
                    else
                        _model.ResultText = "Jogador(a) encontrado";
                }
                else
                {
                    if (!string.IsNullOrWhiteSpace(_model.UserSearchString))
                    {
                        _model.ResultText = string.Format("Nenhum jogador foi encontrado para '{0}'", search_);
                    }
                    else
                    {
                        _model.ResultText = string.Format("Não foi encontrado nenhum jogador com os filtros informados");
                    }
                }
            }

            return _model;
        }

        #endregion

        #region Friend List

        public ActionResult Friends(string id, string page)
        {
            if (!string.IsNullOrWhiteSpace(id))
            {
                var _friendViewModel = new FriendViewModel(id, Convert.ToInt32(page));
                _friendViewModel.InitializeRelationWith();
                return View(_friendViewModel);
            }

            return RedirectToAction("index", "home");
        }

        [HttpPost]
        [Authorize]
        public ActionResult RelationAction(string id, string param)
        {
            var _loggedUser = WebHelper.GetLoggedUser();
            var _currentUser = Model.User.GetUserById(Convert.ToInt32(id));

            bool _deny = false;
            if (!string.IsNullOrWhiteSpace(param))
                _deny = param.Equals("deny", StringComparison.InvariantCultureIgnoreCase);

            RelationFriend.RelationAction(_loggedUser.Id, _currentUser.Id, _deny);

            var _viewModel = new UserCompactViewModel(_currentUser);
            _viewModel.InitializeRelationWith();

            return PartialView("UserCompactView", _viewModel);
        }

        [Authorize]
        public ActionResult RelationActionUrl(string id, string param)
        {
            var _loggedUser = WebHelper.GetLoggedUser();
            var _currentUser = Model.User.GetUserById(Convert.ToInt32(id));

            bool _deny = false;
            if (!string.IsNullOrWhiteSpace(param))
                _deny = param.Equals("deny", StringComparison.InvariantCultureIgnoreCase);

            RelationFriend.RelationAction(_loggedUser.Id, _currentUser.Id, _deny);

            return Redirect(Request.UrlReferrer.AbsolutePath);
        }

        #endregion
    }
}
