using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using PushToPlay.WEB.Models;

namespace PushToPlay.WEB.Controllers.Home
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            HomeViewModel _homeViewModel = new HomeViewModel();
            _homeViewModel.LoadHomeFeeds();
            return View(_homeViewModel);
        }
    }
}
