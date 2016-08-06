using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PushToPlay.Model;

namespace PushToPlay.WEB.Models
{
    public class PushToPlayBaseViewModel
    {
        private int _currentPage;

        public string ModelErrorMsg { get; set; }

        public int CurrentPage
        {
            get
            {
                if (this._currentPage <= 0)
                    this._currentPage = 1;

                return this._currentPage;
            }
            set
            {
                if (value <= 0)
                    this._currentPage = 1;
                else
                    this._currentPage = value;
            }
        }

        public int TotalPage { get; set; }

        public int ObjectCount { get; set; }

        public User LoggedUser { get; set; }
        public User CurrentUser { get; set; }

        public string AjaxDivContent { get; set; }

        public RelationFriendEnum RelationWith { get; set; }

        public PushToPlayBaseViewModel()
        {
            this.LoggedUser = WebHelper.GetLoggedUser();
            this.RelationWith = RelationFriendEnum.Undefined;
        }

        public void InitializeRelationWith()
        {
            if (this.LoggedUser != null && this.CurrentUser != null)
            {
                var _relation = RelationFriend.GetRelation(this.LoggedUser.Id, this.CurrentUser.Id);
                if (_relation != null)
                {
                    this.RelationWith = (RelationFriendEnum)_relation.Status;
                }
                else
                    this.RelationWith = RelationFriendEnum.NotFriend;
            }            
        }
    }
}
