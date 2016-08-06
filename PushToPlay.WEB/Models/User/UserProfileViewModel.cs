using System;
using System.Linq;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PushToPlay.WEB.Models
{
    public class UserProfileViewModel : PushToPlayBaseViewModel
    {
        public UserProfileViewModel()
        {
            base.CurrentUser = base.LoggedUser;

            this.Name = base.LoggedUser.Name;
            this.Description = base.LoggedUser.Description;
            //this.SteamAppId = base.LoggedUser.SteamAppId
            this.PsnId = base.LoggedUser.PsnId;
            this.GamerTag = base.LoggedUser.GamerTag;
            this.OriginId = base.LoggedUser.OriginId;
            this.SkynerdProfile = base.LoggedUser.SkynerdId;
        }

        [Display(Name = "Nome")]
        public string Name { get; set; }

        [AllowHtml]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Descrição")]
        public string Description { get; set; }

        [Display(Name = "Steam ID")]
        public string SteamAppId { get; set; }

        [Display(Name = "PSN ID")]
        public string PsnId { get; set; }

        [Display(Name = "GamerTag")]
        public string GamerTag { get; set; }

        [Display(Name = "Origin ID")]
        public string OriginId { get; set; }

        [Display(Name = "Perfil Skynerd")]
        public string SkynerdProfile { get; set; }

        [Display(Name = "Foto")]
        public string Image { get ; set ; }
    }
}