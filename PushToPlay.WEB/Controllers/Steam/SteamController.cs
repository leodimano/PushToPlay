using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using PushToPlay.Core.PlatformFactory.Steam;

namespace PushToPlay.WEB.Controllers.Steam
{
    public class SteamController : Controller
    {
        public ActionResult RequestAuthentication()
        {
            using (SteamFactory _steamFactory = new SteamFactory())
            {
                _steamFactory.Authenticate(this.HttpContext);
            }
            
            return View();
        }

        public ActionResult HandleAuthentication()
        {
            string _steamAppId = new SteamFactory().HandleSteamOpenIDReturn(this.HttpContext);
            Session["SteamAppId"] = _steamAppId;
            return RedirectToAction("home", "user");
        }

        public ActionResult GetPlayerSummary(string id)
        {
            return new ContentResult() { Content = string.Empty };
        }
    }
}
