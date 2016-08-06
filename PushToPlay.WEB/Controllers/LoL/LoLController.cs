using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using PushToPlay.Core.PlatformFactory.LoL;

namespace PushToPlay.WEB.Controllers.LoL
{
    public class LoLController : Controller
    {
        //
        // GET: /LoL/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Champion(string id)
        {
            var _champion = LoLFactory.Instance.GetChampionList("br", true).Find(c => c.name.Equals(id, StringComparison.InvariantCultureIgnoreCase));
            return View(_champion);
        }
    }
}
