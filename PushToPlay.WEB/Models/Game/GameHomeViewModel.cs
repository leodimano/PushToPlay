using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using PushToPlay.Model;

namespace PushToPlay.WEB.Models
{
    public class GameHomeViewModel : PushToPlayBaseViewModel
    {
        public int GameDetailId { get; set; }

        public string GameName { get; set; }

        public string GameImage { get; set; }

        public int PlatformID { get; set; }

        [Display(Name = "Plataforma")]
        public string PlatformName { get; set; }

        [Display(Name = "Publisher")]
        public string Publisher { get; set; }

        [Display(Name = "Developer")]
        public string Developer { get; set; }

        [Display(Name = "Lançamento")]
        public DateTime? ReleaseDate { get; set; }

        public int? SteamAppId { get; set; }

        public string SteamAppUrl { get; set; }

        public int UserCount { get; set; }

        public int ClanCount { get; set; }

        public int GroupCount { get; set; }

        public int EventCount { get; set; }

        public bool UserHas { get; set; }

        public MessengerViewModel MessengerModel { get; set; }

        public GameHomeViewModel(PushToPlay.Model.GameDetail gameDetail_)
        {
            this.GameDetailId = gameDetail_.Id;
            this.GameName = gameDetail_.Game.Name;
            this.GameImage = gameDetail_.GameImage;
            this.PlatformID = gameDetail_.PlatformId;
            this.PlatformName = gameDetail_.Platform.Name;
            this.Publisher = gameDetail_.Game.Publisher;
            this.Developer = gameDetail_.Game.Developer;
            this.ReleaseDate = gameDetail_.ReleaseDate;
            this.SteamAppId = gameDetail_.Game.SteamAppId;
            this.SteamAppUrl = gameDetail_.Game.SteamAppUrl;

            this.UserHas = Model.UserGame.UserGameAssociated(WebHelper.GetLoggedUserId(), gameDetail_.Id);

            this.UserCount = Model.UserGame.GetUsersByGameDetail(gameDetail_.Id);            
        }

        public void InitilizeMessages(int pageNumber_)
        {
            this.MessengerModel = new MessengerViewModel();
            this.MessengerModel.BaseId = WebHelper.GetLoggedUserId();
            this.MessengerModel.BaseType = MessageSourceTypeEnum.User;
            this.MessengerModel.TargetId = this.GameDetailId;
            this.MessengerModel.TargetType = MessageSourceTypeEnum.Game;                
            this.MessengerModel.MessageList = ActivityMessage.GetGameMessages(this.GameDetailId, pageNumber_);
            this.MessengerModel.PlatformId = this.PlatformID;
        }

    }
}