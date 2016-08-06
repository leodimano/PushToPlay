using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PushToPlay.WEB.Engines
{
    public class ViewLocationEngine : RazorViewEngine
    {
        private static string[] MyViewEngine = new string[]{
            "~/Views/Shared/Feed/{0}.cshtml",
            "~/Views/Shared/Game/{0}.cshtml",
            "~/Views/Shared/Group/{0}.cshtml",
            "~/Views/Shared/Home/{0}.cshtml",
            "~/Views/Shared/Login/{0}.cshtml",
            "~/Views/Shared/Messenger/{0}.cshtml",
            "~/Views/Shared/Stream/{0}.cshtml",
            "~/Views/Shared/User/{0}.cshtml"
        };

        public ViewLocationEngine()
        {
            base.PartialViewLocationFormats = base.PartialViewLocationFormats.Union(MyViewEngine).ToArray();
        }

    }
}