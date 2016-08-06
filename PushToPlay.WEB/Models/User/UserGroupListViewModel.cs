using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PushToPlay.WEB.Models
{
    public class UserGroupListViewModel : PushToPlayBaseViewModel
    {
        public List<GroupCompactViewModel> ListGroupCompactViewModel { get; set; }

        public UserGroupListViewModel(string userName_, int pageNumber_)
        {
            base.CurrentUser = Model.User.GetUserByUserName(userName_);

            var _groupList = CurrentUser.GetGroups(pageNumber_);

            if (_groupList.TotalObject > 0)
            {
                ListGroupCompactViewModel = new List<GroupCompactViewModel>();

                base.CurrentPage = pageNumber_;
                base.ObjectCount = _groupList.TotalObject;
                base.TotalPage = _groupList.TotalPages;
                
                foreach (var _group in _groupList.Objects)
                {
                    ListGroupCompactViewModel.Add(new GroupCompactViewModel(_group));
                }
            }
        }
    }
}