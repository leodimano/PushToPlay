using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

using System.Data.Entity;
using PushToPlay.WEB.Engines;
using PushToPlay.Core.PlatformFactory.LoL;

namespace PushToPlay.WEB
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            
            /* Inicializa o banco de dados e evita erros de migracao */
            System.Data.Entity.Database.SetInitializer<PushToPlay.Model.PushToPlayContext>(null);

            /* Inicializa a engine custom de views */
            ViewEngines.Engines.Add(new ViewLocationEngine());

            /* Inicialize a fabrica da plataforma de League of Legends */
            LoLFactory.Instance.InitializeLoLFactory("br");
        }

        private static void RegisterCustomViewLocations()
        {

        }
    }
}