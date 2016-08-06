using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

using PushToPlay.Model;

namespace PushToPlay.WEB.Models
{
    public class UserSearchViewModel : PushToPlayBaseViewModel
    {
        public UserSearchViewModel()
        {
            CurrentPage = 1;
            TotalPage = 0;
            ResultText = string.Empty;
            UserSearchString = string.Empty;
        }

        [Display(Name = "buscar por ")]
        public string UserSearchString { get; set; }

        public string ResultText { get; set; }

        public List<UserCompactViewModel> ListUserCompactViewModel { get; set; }
        
        public void SetUserList(List<User> listUser_)
        {
            if (listUser_ != null && listUser_.Count > 0)
            {
                this.ListUserCompactViewModel = new List<UserCompactViewModel>();

                foreach (var _user in listUser_)
                {
                    var _userCompactViewModel = new UserCompactViewModel(_user);
                    _userCompactViewModel.InitializeRelationWith();
                    this.ListUserCompactViewModel.Add(_userCompactViewModel);
                }
            }
        }
    }
}