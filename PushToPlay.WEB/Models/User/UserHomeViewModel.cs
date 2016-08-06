using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using PushToPlay.Model;

namespace PushToPlay.WEB.Models
{

    public class UserHomeViewModel : PushToPlayBaseViewModel
    {
        public List<UserCompactViewModel> FriendList { get; set; }

        public List<GameCompactViewModel> GameList { get; set; }

        public MessengerViewModel MessengerModel { get; set; }

        public UserHomeViewModel(string currentUserName_, int pageNumber_)
            : base()
        {
            this.FriendList = new List<UserCompactViewModel>();
            this.GameList = new List<GameCompactViewModel>();
            this.MessengerModel = new MessengerViewModel();

            base.CurrentUser = Model.User.GetUserByUserName(currentUserName_);

            var _friendList = base.CurrentUser.GetFriends(1, Model.RelationFriendEnum.Friend);

            if (_friendList != null && _friendList.TotalObject > 0)
            {
                for (int i = 0; i < _friendList.TotalObject && i <= 11; i++)
                {
                    this.FriendList.Add(new UserCompactViewModel(_friendList.Objects[i]));
                }
            }

            var _gameList = Model.GameDetail.GetGameDetailByUser(base.CurrentUser.Id, 1);

            if (_gameList != null && _gameList.TotalObject > 0)
            {
                for (int i = 0; i < _gameList.TotalObject && i <= 11; i++)
                {
                    this.GameList.Add(new GameCompactViewModel(_gameList.Objects[i], false));
                }
            }

            MessengerModel.BaseId = WebHelper.GetLoggedUserId();
            MessengerModel.BaseType = MessageSourceTypeEnum.User;
            MessengerModel.TargetId = base.CurrentUser.Id;
            MessengerModel.TargetType = MessageSourceTypeEnum.User;
            MessengerModel.MessageList = ActivityMessage.GetUserMessages(base.CurrentUser.Id, pageNumber_);
            
            base.InitializeRelationWith();
        }
    }

}