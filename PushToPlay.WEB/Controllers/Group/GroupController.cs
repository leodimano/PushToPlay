using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;

using PushToPlay.Model;
using PushToPlay.WEB.Models;
using PushToPlay.Helpers;

namespace PushToPlay.WEB.Controllers
{
    public class GroupController : Controller
    {

        #region Create

        [HttpGet]
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public ActionResult Create(GroupCreateViewModel _model, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                var _user = WebHelper.GetLoggedUser();

                Model.Group _group = new Model.Group();
                _group.Name = _model.Name;
                _group.Description = _model.Description;
                _group.GroupType = (int)Model.GroupTypeEnum.GROUP;
                _group.Register();

                if (file != null)
                {
                    NetHelper.SaveFile(file.InputStream, NetHelper.GetIdFileName(_group.Id), ConfigurationManager.AppSettings[Constants.CONFIG_KEY_GROUP_SAVE_IMAGE_PATH]);
                }

                return RedirectToRoute(Constants.ROUTE_GROUP_HOME, new { id = _group.Id, name = _group.Name });
            }
            else
            {
                return View(_model);
            }
        }

        #endregion

        #region Home

        public ActionResult Home(string id, string name, string page)
        {
            int _id;
            Int32.TryParse(id, out _id);

            int _page;
            Int32.TryParse(page, out _page);

            var _groupFound = Group.GetGroupById(_id);

            if (_groupFound != null)
            {
                var _groupHomeViewModel = new GroupHomeViewModel(_groupFound);
                _groupHomeViewModel.InitializeMessages(_page);
                return View(_groupHomeViewModel);
            }
            else
            {
                return RedirectToRoute(Constants.ROUTE_HOME);
            }
        }

        [Authorize]
        [HttpGet]
        public ActionResult HomeJoin(string id)
        {
            int _groupId;
            Int32.TryParse(id, out _groupId);

            var _loggedUser = WebHelper.GetLoggedUser();
            var _joinedGroup = _loggedUser.JoinGroup(_groupId);

            return RedirectToRoute(Constants.ROUTE_GROUP_HOME,
                                   new
                                   {
                                       id = _joinedGroup.Id,
                                       name = WebHelper.ProcessUrlEnconding(_joinedGroup.Name)
                                   });
        }

        [Authorize]
        [HttpGet]
        public ActionResult HomeLeave(string id)
        {
            int _groupId;
            Int32.TryParse(id, out _groupId);

            var _loggedUser = WebHelper.GetLoggedUser();
            var _joinedGroup = _loggedUser.LeaveGroup(_groupId);

            return RedirectToRoute(Constants.ROUTE_GROUP_HOME,
                                   new
                                   {
                                       id = _joinedGroup.Id,
                                       name = WebHelper.ProcessUrlEnconding(_joinedGroup.Name)
                                   });
        }


        [Authorize]
        [HttpGet]
        public ActionResult CompactJoin(string id)
        {
            int _groupId;
            Int32.TryParse(id, out _groupId);

            var _loggedUser = WebHelper.GetLoggedUser();
            var _joinedGroup = _loggedUser.JoinGroup(_groupId);

            GroupCompactViewModel _groupCompactViewModel = new GroupCompactViewModel(_joinedGroup);

            return PartialView("GroupCompactView", _groupCompactViewModel);
        }

        [Authorize]
        [HttpGet]
        public ActionResult CompactLeave(string id)
        {
            int _groupId;
            Int32.TryParse(id, out _groupId);

            var _loggedUser = WebHelper.GetLoggedUser();
            var _joinedGroup = _loggedUser.LeaveGroup(_groupId);

            GroupCompactViewModel _groupCompactViewModel = new GroupCompactViewModel(_joinedGroup);

            return PartialView("GroupCompactView", _groupCompactViewModel);
        }



        #endregion

        #region Search

        [HttpGet]
        public ActionResult Search(string name, string page)
        {
            return View(SearchGroup(name, page));
        }

        private GroupSearchViewModel SearchGroup(string groupName_, string page_)
        {
            GroupSearchViewModel _groupSearch = new GroupSearchViewModel();

            int _pageNumber;

            if (string.IsNullOrWhiteSpace(groupName_))
            {
                groupName_ = string.Empty;
            }

            Int32.TryParse(page_, out _pageNumber);

            var _listGroup = Group.GetGroupByName(groupName_, GroupTypeEnum.GROUP, _pageNumber);

            if (_listGroup != null)
            {
                _groupSearch.SetGroupList(_listGroup.Objects);
                _groupSearch.ObjectCount = _listGroup.TotalObject;
                _groupSearch.CurrentPage = _pageNumber;
                _groupSearch.TotalPage = _listGroup.TotalPages;
            }

            return _groupSearch;
        }

        #endregion
    }
}
