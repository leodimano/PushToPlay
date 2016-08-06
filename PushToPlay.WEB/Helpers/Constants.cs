using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PushToPlay.WEB
{
    public class Constants
    {
        public const string SESSION_LOGGED_USER = "LoggedUser";

        public const string SESSION_MUST_BE_LOGGED_MSG = "MustBeLogged";

        public const string SESSION_TRANSFER_TO_ACTION = "TransferToAction";
        public const string SESSION_TRANSFER_TO_CONTROLLER = "TransferToController";
        public const string SESSION_TRANSFER_TO_MODEL = "TransferToModel";
        public const string SESSION_PREVIOUS_SEARCH_NAME = "PreviousSearchName";
        public const string SESSION_PREVIOUS_SEARCH_OBJECT = "PreviousSearchObject";
        
        public const string VIEWDATA_CONTEXT_GAMEDETAIL = "ContextGameDetail";
        public const string VIEWDATA_DRAW_LOGIN_BAR = "DrawLoginBar";
        public const string VIEWDATA_DO_NOT_DISPLAY_LOGIN_PARTIAL = "DoNotDisplayLoginPartial";
        public const string VIEWDATA_LAYOUT_PATH = "LayoutPath";
        
        public const string CONFIG_KEY_USER_SAVE_IMAGE_PATH = "UserSaveImagePath";
        public const string CONFIG_KEY_GROUP_SAVE_IMAGE_PATH = "GroupSaveImagePath";

        public const string CONFIG_KEY_GAME_DISPLAY_IMAGE_PATH = "GameDisplayImagePath";
        public const string CONFIG_KEY_USER_DISPLAY_IMAGE_PATH = "UserDisplayImagePath";
        public const string CONFIG_KEY_GROUP_DISPLAY_IMAGE_PATH = "GroupDisplayImagePath";

        public const string CONFIG_KEY_RSS_FEED_GAME = "UrlFeed";

        #region Route Keys 

        public const string ROUTE_HOME = "Home";

        public const string ROUTE_GAME_HOME = "GameHome";
        public const string ROUTE_GAME_HOME_ADD = "GameHomeAdd";
        public const string ROUTE_GAME_HOME_DEL = "GameHomeDel";
        public const string ROUTE_GAME_SEARCH = "GameSearch";        
        public const string ROUTE_GAME_HOME_STREAMS = "GameHome_Streams";
        public const string ROUTE_GAME_HOME_STREAMS_WATCH = "GameHome_Streams_Watch";

        public const string ROUTE_USER_HOME = "UserHome";
        public const string ROUTE_USER_SEARCH = "UserSearch";
        public const string ROUTE_USER_HOME_GAME = "UserHome_Games";
        public const string ROUTE_USER_HOME_FRIEND = "UserHome_Friends";
        public const string ROUTE_USER_HOME_GROUP = "UserHome_Group";

        public const string ROUTE_GROUP_HOME = "GroupHome";
        public const string ROUTE_GROUP_SEARCH = "GroupSearch";
        public const string ROUTE_GROUP_CREATE = "GroupCreate";
        public const string ROUTE_GROUP_JOIN = "GroupJoin";
        public const string ROUTE_GROUP_LEAVE = "GroupLeave";

        #endregion


    }
}