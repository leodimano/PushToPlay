using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using PushToPlay.Model;

namespace PushToPlay.WEB.Models
{
    public class GroupCompactViewModel : PushToPlayBaseViewModel
    {
        private string _groupDescription;

        public int GroupId { get; set; }
        public string GroupName { get; set; }
        public string GroupImage { get; set; }

        public bool UserHas { get; set; }

        public GroupCompactViewModel(Group group_)
        {
            this.GroupId = group_.Id;
            this.GroupName = group_.Name;

            if (base.LoggedUser != null)
                this.UserHas = UserGroups.IsAssociated(base.LoggedUser.Id, this.GroupId);

            this.GroupImage = WebHelper.GetGroupImagePath(GroupId, true);
            this.AjaxDivContent = string.Concat("GRCV", this.GroupId);
        }
    }
}