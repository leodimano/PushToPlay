using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace PushToPlay.WEB
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            #region Group Routes

            routes.MapRoute(
                name: Constants.ROUTE_GROUP_SEARCH,
                url: "grupos",
                defaults: new { controller = "Group", action = "Search" }
            );

            routes.MapRoute(
                name: Constants.ROUTE_GROUP_CREATE,
                url: "grupos/criar",
                defaults: new { controller = "Group", action = "Create" }
            );

            routes.MapRoute(
                name: Constants.ROUTE_GROUP_JOIN,
                url: "grupos/entrar/{id}",
                defaults: new { controller = "Group", action = "HomeJoin" },
                constraints: new { id = @"\d+" }
            );

            routes.MapRoute(
                name: Constants.ROUTE_GROUP_LEAVE,
                url: "grupos/sair/{id}",
                defaults: new { controller = "Group", action = "HomeLeave" },
                constraints: new { id = @"\d+" }
            );

            routes.MapRoute(
                name: Constants.ROUTE_GROUP_HOME,
                url: "grupos/{id}/{name}",
                defaults: new { controller = "Group", action = "Home" },
                constraints: new { id = @"\d+" }
            );

            #endregion

            #region Game Routes

            routes.MapRoute(
                name: Constants.ROUTE_GAME_SEARCH,
                url: "jogos",
                defaults: new { controller = "Game", action = "Search" }
            );

            routes.MapRoute(
                name: Constants.ROUTE_GAME_HOME_ADD,
                url: "jogos/{id}/add",
                defaults: new { controller = "Game", action = "HomeAdd" },
                constraints: new { id = @"\d+" }
            );

            routes.MapRoute(
                name: Constants.ROUTE_GAME_HOME_DEL,
                url: "jogos/{id}/remove",
                defaults: new { controller = "Game", action = "HomeRemove" },
                constraints: new { id = @"\d+" }
            );

            routes.MapRoute(
                name: Constants.ROUTE_GAME_HOME,
                url: "{id}/{gamename}",
                defaults: new { controller = "Game", action = "Home" },
                constraints: new { id = @"\d+" }
            );

            routes.MapRoute(
                name: Constants.ROUTE_GAME_HOME_STREAMS,
                url: "{id}/{gamename}/streams",
                defaults: new { controller = "Game", action = "Streams" },
                constraints: new { id = @"\d+" }
            );

            routes.MapRoute(
                name: Constants.ROUTE_GAME_HOME_STREAMS_WATCH,
                url: "{id}/{gamename}/streams/{streamid}",
                defaults: new { controller = "Game", action = "Watch" },
                constraints: new { id = @"\d+" }
            );

            #endregion

            #region User Routes

            routes.MapRoute(
                name: Constants.ROUTE_USER_SEARCH,
                url: "jogadores",
                defaults: new { controller = "User", action = "Search" }
            );

            routes.MapRoute(
                name: Constants.ROUTE_USER_HOME,
                url: "{id}",
                defaults: new { controller = "User", action = "Home" }
            );

            routes.MapRoute(
                name: Constants.ROUTE_USER_HOME_GAME,
                url: "{id}/jogos",
                defaults: new { controller = "User", action = "Game" }
            );

            routes.MapRoute(
                name: Constants.ROUTE_USER_HOME_FRIEND,
                url: "{id}/amigos",
                defaults: new { controller = "User", action = "Friends" }
            );

            routes.MapRoute(
                name: Constants.ROUTE_USER_HOME_GROUP,
                url: "{id}/grupos",
                defaults: new { controller = "User", action = "Group" }
            );

            #endregion

            routes.MapRoute(
                name: Constants.ROUTE_HOME,
                url: "",
                defaults: new { controller = "Home", action = "Index" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

        }
    }
}