using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PushToPlay.WEB.Models
{
    public class UserCompactViewModel : PushToPlayBaseViewModel
    {
        private string _name;

        public UserCompactViewModel()
        {
        }

        public UserCompactViewModel(Model.User user_)
        {
            this.UserId = user_.Id;
            this.UserName = user_.UserName;
            this._name = user_.Name;

            //this.FriendCount = Model.RelationFriend.GetRelationCount(user_.Id, Model.RelationFriendEnum.Friend);
            //this.GameCount = Model.UserGame.GetGamesbyUserId(this.UserId);
            this.GroupCount = 0;
            this.ClanCount = 0;
            this.EventCount = 0;

            this.AjaxDivContent = string.Concat("CUC", this.UserId);

            this.UserImagePath = WebHelper.GetUserImagePath(this.UserId, true);
        }

        public int UserId { get; set; }

        public string UserName { get; set; }

        public string Name
        {
            get
            {
                if (string.IsNullOrWhiteSpace(this._name))
                    return this.UserName;
                else
                    return this._name;
            }
            set
            {
                this._name = value;
            }
        }

        public int FriendCount { get; set; }

        public int GameCount { get; set; }

        public int GroupCount { get; set; }

        public int ClanCount { get; set; }

        public int EventCount { get; set; }

        public bool IsFriend { get; set; }

        public Model.RelationFriendEnum RelationStatus { get; set; }

        public string UserImagePath { get; set; }


        public void InitializeRelationWith()
        {
            var _relation = Model.RelationFriend.GetRelation(WebHelper.GetLoggedUserId(), this.UserId);

            if (_relation != null)
            {
                this.RelationStatus = (Model.RelationFriendEnum)_relation.Status;
            }
            else
                this.RelationStatus = Model.RelationFriendEnum.NotFriend;
        }

    }
}