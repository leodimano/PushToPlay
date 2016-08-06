using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using PushToPlay.Model;

namespace PushToPlay.WEB.Models
{
    public class GroupHomeViewModel : PushToPlayBaseViewModel
    {
        public int GroupId { get; set; }
        public string GroupName { get; set; }
        public string GroupDescription { get; set; }
        public int UserCount { get; set; }
        public int GameCount { get; set; }
        public int EventCount { get; set; }
        public bool UserHas { get; set; }
        public MessengerViewModel MessengerModel { get; set; }

        public GroupHomeViewModel(Group groupModel_)
        {
            this.GroupId = groupModel_.Id;
            this.GroupName = groupModel_.Name;
            this.GroupDescription = groupModel_.Description;

            if (base.LoggedUser != null)
            {
                this.UserHas = base.LoggedUser.IsAssociatedToGroup(this.GroupId);
            }
        }

        public void InitializeMessages(int pageNumber_)
        {
            this.MessengerModel = new MessengerViewModel();
            this.MessengerModel.BaseId = WebHelper.GetLoggedUserId();
            this.MessengerModel.BaseType = MessageSourceTypeEnum.User;
            this.MessengerModel.TargetId = this.GroupId;
            this.MessengerModel.TargetType = MessageSourceTypeEnum.Group;
            this.MessengerModel.MessageList = ActivityMessage.GetGroupMessages(this.GroupId, pageNumber_);
        }

    }
}