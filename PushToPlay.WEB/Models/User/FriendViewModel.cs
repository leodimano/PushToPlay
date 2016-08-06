using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

using PushToPlay.Model;

namespace PushToPlay.WEB.Models
{
    public class FriendViewModel : PushToPlayBaseViewModel
    {
        public List<UserCompactViewModel> ListUserCompactViewModel;

        public FriendViewModel(string user_, int pageNumber_)
        {
            base.CurrentUser = Model.User.GetUserByUserName(user_);

            ListObject<User> _relationList = null;

            if (base.LoggedUser != null && (base.CurrentUser.Id == base.LoggedUser.Id))
                _relationList = base.CurrentUser.GetFriends(pageNumber_, RelationFriendEnum.Undefined);
            else
                _relationList = base.CurrentUser.GetFriends(pageNumber_, RelationFriendEnum.Friend);

            if (_relationList != null && _relationList.TotalObject > 0)
            {
                this.CurrentPage = pageNumber_;
                this.TotalPage = _relationList.TotalPages;
                this.ObjectCount = _relationList.TotalObject;

                this.ListUserCompactViewModel = new List<UserCompactViewModel>();

                foreach (var _user in _relationList.Objects)
                {
                    var _userCompactViewModel = new UserCompactViewModel(_user);
                    _userCompactViewModel.InitializeRelationWith();
                    this.ListUserCompactViewModel.Add(_userCompactViewModel);
                }
            }
        }
    }
}