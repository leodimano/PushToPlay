using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace PushToPlay.WEB.Models
{
    public class GameCompactViewModel : PushToPlayBaseViewModel
    {
        public GameCompactViewModel(PushToPlay.Model.GameDetail gameDetail_, bool checkAssociation_)
            : base()
        {
            this.Name = gameDetail_.Game.Name;
            this.GameId = gameDetail_.Game.Id;

            this.GameDetailId = gameDetail_.Id;
            this.GameImage = gameDetail_.GameImage;
            this.GameImagePath = WebHelper.GetGameImagePath(this.GameImage);
            this.PlatformId = gameDetail_.PlatformId;
            this.PlatformName = gameDetail_.Platform.Name;
            this.SteamAppId = gameDetail_.Game.SteamAppId;
            this.SteamAppUrl = gameDetail_.Game.SteamAppUrl;
                        
            if (checkAssociation_)
                this.UserHas = Model.UserGame.UserGameAssociated(WebHelper.GetLoggedUserId(), gameDetail_.Id);

            base.AjaxDivContent = string.Concat("GCV", gameDetail_.Id);
        }

        public int GameId { get; set; }

        public int GameDetailId { get; set; }

        public string GameImage { get; set; }

        public string Name { get; set; }

        public int PlatformId { get; set; }

        public string PlatformName { get; set; }

        public int? SteamAppId { get; set; }

        public string SteamAppUrl { get; set; }

        public bool UserHas { get; set; }

        public string GameImagePath { get; set; }
    }
}