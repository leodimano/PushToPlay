using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using System.Web;
using System.Web.Mvc;

namespace PushToPlay.Model
{
    public enum MessageActionEnum
    {
        Message = -1,

        FriendAdded = 0,
        FriendAccepted = 1,

        GameAdded = 2,
        GameDeleted = 3,

        GroupAdded = 4,
        GroupDeleted = 5
    }

    public enum MessageTypeEnum
    {
        Message = 0,
        Activity = 1
    }

    public enum MessageSourceTypeEnum
    {
        Undefined = 0,
        User = 1,
        Game = 2,
        Group = 3,
        Clan = 4,
        Event = 5
    }

    /// <summary>
    /// Classe parcial para manter as regras de negocio do modelo
    /// </summary>
    public partial class ActivityMessage : ModelBase, IDisposable
    {
        #region Constants

        public const string BaseIDParam = "{#BaseID#}";

        public const string TargetIDParam = "{#TargetID#}";

        public const string MessageParam = "{#Message#}";
        public const string MessageDateParam = "{#MessageDate#}";

        private const string FriendAddedMessage = "<a class ='{4}' href='{0}'>{1}</a> aceitou <a class='{4}' href='{2}'>{3}</a> como amigo(a).";
        private const string FriendAcceptedMessage = "<a class ='{4}' href='{0}'>{1}</a> adicionou <a class='{4}' href='{2}'>{3}</a> a sua lista de amigos.";

        private const string GameAddedMessage = "<a class='{5}' href='{0}'>{1}</a> adicionou o jogo <a class='{2}' href='{3}'>{4}</a> a sua lista de jogos.";
        private const string GameDeletedMessage = "<a class='{5}' href='{0}'>{1}</a> removeu o jogo <a class='{2}' href='{3}'>{4}</a> da sua lista de jogos.";

        private const string GroupAddedMessage = "<a class='{4}' href='{0}'>{1}</a> entrou para o grupo <a class='{4}' href='{2}'>{3}</a>.";
        private const string GroupDeletedMessage = "<a class='{4}' href='{0}'>{1}</a> saiu do grupo <a class='{4}' href='{2}'>{3}</a>.";


        #endregion

        public static string GetMessage(ActivityMessageProcessed _activityMessage, int? platformId_)
        {
            int _platformId = 5;

            if (platformId_.HasValue)
            {
                _platformId = platformId_.Value;
            }

            string _messageAux = string.Empty;

            UrlHelper _urlHelper = new UrlHelper(HttpContext.Current.Request.RequestContext);

            if (_activityMessage.MessageType == (int)MessageTypeEnum.Activity)
            {
                /* If the message doenst exists, create it */
                if (string.IsNullOrWhiteSpace(_activityMessage.Message))
                {
                    switch (_activityMessage.MessageAction)
                    {
                        case (int)MessageActionEnum.FriendAccepted:

                            _messageAux = string.Format(FriendAcceptedMessage,
                                                        _urlHelper.Action("home", "user", new { id = _activityMessage.BaseUserName.ToLower() }),
                                                        string.IsNullOrWhiteSpace(_activityMessage.BaseName) ? _activityMessage.BaseUserName : _activityMessage.BaseName,
                                                        _urlHelper.Action("home", "user", new { id = _activityMessage.TargetUserName.ToLower() }),
                                                        string.IsNullOrWhiteSpace(_activityMessage.TargetName) ? _activityMessage.TargetUserName : _activityMessage.TargetName,
                                                        Helpers.NetHelper.GetCSSLinkGame(_platformId));

                            break;
                        case (int)MessageActionEnum.FriendAdded:

                            _messageAux = string.Format(FriendAddedMessage,
                                                        _urlHelper.Action("home", "user", new { id = _activityMessage.BaseUserName.ToLower() }),
                                                        string.IsNullOrWhiteSpace(_activityMessage.BaseName) ? _activityMessage.BaseUserName : _activityMessage.BaseName,
                                                        _urlHelper.Action("home", "user", new { id = _activityMessage.TargetUserName.ToLower() }),
                                                        string.IsNullOrWhiteSpace(_activityMessage.TargetName) ? _activityMessage.TargetUserName : _activityMessage.TargetName,
                                                        Helpers.NetHelper.GetCSSLinkGame(_platformId));
                            break;
                        case (int)MessageActionEnum.GameAdded:

                            _messageAux = string.Format(GameAddedMessage,
                                                        _urlHelper.Action("home", "user", new { id = _activityMessage.BaseUserName.ToLower() }),
                                                        string.IsNullOrWhiteSpace(_activityMessage.BaseName) ? _activityMessage.BaseUserName : _activityMessage.BaseName,
                                                        Helpers.NetHelper.GetCSSLinkGame(_activityMessage.TargetPlatformId.Value),
                                                        _urlHelper.Action("home", "game", new { id = _activityMessage.TargetID, gamename = Helpers.NetHelper.ProcessGameUrlName(_activityMessage.TargetGameName) }),
                                                        _activityMessage.TargetGameName,
                                                        Helpers.NetHelper.GetCSSLinkGame(_platformId));

                            break;
                        case (int)MessageActionEnum.GameDeleted:

                            _messageAux = string.Format(GameDeletedMessage,
                                                        _urlHelper.Action("home", "user", new { id = _activityMessage.BaseUserName.ToLower() }),
                                                        string.IsNullOrWhiteSpace(_activityMessage.BaseName) ? _activityMessage.BaseUserName : _activityMessage.BaseName,
                                                        Helpers.NetHelper.GetCSSLinkGame(_activityMessage.TargetPlatformId.Value),
                                                        _urlHelper.Action("home", "game", new { id = _activityMessage.TargetID, gamename = Helpers.NetHelper.ProcessGameUrlName(_activityMessage.TargetGameName) }),
                                                        _activityMessage.TargetGameName,
                                                        Helpers.NetHelper.GetCSSLinkGame(_platformId));

                            break;
                        case (int)MessageActionEnum.GroupAdded:

                            _messageAux = string.Format(GroupAddedMessage,
                                                        _urlHelper.Action("home", "user", new { id = _activityMessage.BaseUserName.ToLower() }),
                                                        string.IsNullOrWhiteSpace(_activityMessage.BaseName) ? _activityMessage.BaseUserName : _activityMessage.BaseName,
                                                        _urlHelper.Action("home", "group", new { id = _activityMessage.TargetID }),
                                                        _activityMessage.TargetGroupName,
                                                        Helpers.NetHelper.GetCSSLinkGame(_platformId));

                            break;
                        case (int)MessageActionEnum.GroupDeleted:


                            _messageAux = string.Format(GroupDeletedMessage,
                                                        _urlHelper.Action("home", "user", new { id = _activityMessage.BaseUserName.ToLower() }),
                                                        string.IsNullOrWhiteSpace(_activityMessage.BaseName) ? _activityMessage.BaseUserName : _activityMessage.BaseName,
                                                        _urlHelper.Action("home", "group", new { id = _activityMessage.TargetID }),
                                                        _activityMessage.TargetGroupName,
                                                        Helpers.NetHelper.GetCSSLinkGame(_platformId));

                            break;

                        case (int)MessageActionEnum.Message: break;
                    }
                }
            }

            if (!string.IsNullOrWhiteSpace(_messageAux))
            {
                _messageAux = _messageAux.Replace(BaseIDParam, _activityMessage.BaseId.ToString())
                                         .Replace(TargetIDParam, _activityMessage.TargetID.ToString())
                                         .Replace(MessageParam, _activityMessage.Message)
                                         .Replace(MessageDateParam, _activityMessage.CreationDate.ToString());
                return _messageAux;
            }
            else
            {
                if (string.IsNullOrWhiteSpace(_activityMessage.Message))
                    _activityMessage.Message = string.Empty;
                else
                {
                    _activityMessage.Message = _activityMessage.Message.Replace(BaseIDParam, _activityMessage.BaseId.ToString())
                                               .Replace(TargetIDParam, _activityMessage.TargetID.ToString())
                                               .Replace(MessageParam, _activityMessage.Message)
                                               .Replace(MessageDateParam, _activityMessage.CreationDate.ToString());
                }
                return _activityMessage.Message;
            }
        }

        public ActivityMessage()
        {
        }

        public void Dispose()
        {
        }

        #region Persistence

        public void Add()
        {
            using (PushToPlayContext _context = new PushToPlayContext())
            {
                this.CreationDate = DateTime.Now;
                _context.ActivityMessages.Add(this);
                _context.SaveChanges();
            }
        }

        public void Delete()
        {
            using (PushToPlayContext _context = new PushToPlayContext())
            {
                _context.Entry<ActivityMessage>(this).State = System.Data.EntityState.Deleted;
                _context.SaveChanges();
            }
        }

        #endregion

        #region Statics

        private static string GetSQLComplement
        {
            get
            {
                return @"(SELECT USERNAME FROM USERS WHERE USERS.ID = BASEID AND BASETYPE = 1) BaseUserName,
                         (SELECT NAME FROM USERS WHERE USERS.ID = BASEID AND BASETYPE = 1) BaseName,
                         (SELECT USERNAME FROM USERS WHERE USERS.ID = TARGETID AND TARGETTYPE = 1) TargetUserName,
                         (SELECT NAME FROM USERS WHERE USERS.ID = TARGETID AND TARGETTYPE = 1) TargetName,
                         (SELECT GAMES.NAME  FROM GAMEDETAILS, GAMES WHERE BASETYPE = 2 AND GAMEDETAILS.ID = BASEID AND GAMEDETAILS.GAMEID = GAMES.ID) BaseGameName,
                         (SELECT GAMES.NAME  FROM GAMEDETAILS, GAMES WHERE TARGETTYPE = 2 AND GAMEDETAILS.ID = TARGETID AND GAMEDETAILS.GAMEID = GAMES.ID) TargetGameName,
                         (SELECT GAMEDETAILS.PLATFORMID FROM GAMEDETAILS WHERE BASETYPE = 2 AND GAMEDETAILS.ID = BASEID) BasePlatformId,
                         (SELECT GAMEDETAILS.PLATFORMID FROM GAMEDETAILS WHERE TARGETTYPE = 2 AND GAMEDETAILS.ID = TARGETID) TargetPlatformId,
                         (SELECT NAME FROM GROUPS WHERE BASETYPE = 3 AND GROUPS.ID = BASEID) BaseGroupName,
                         (SELECT NAME FROM GROUPS WHERE TARGETTYPE = 3 AND GROUPS.ID = TARGETID) TargetGroupName,";
            }
        }

        private static string GetUserMessageSQL
        {
            get
            {
                return @"SELECT *,
                               {3}
                               ROW_NUMBER() OVER ( ORDER BY FatherID DESC, CreationDate ) AS RowNum 
                        FROM (
                        Select *, Id as FatherID from (
                            SELECT AM.*
                                FROM ActivityMessages as AM
                                WHERE (AM.BASETYPE = {0} AND AM.BASEID = {1} AND AM.ReplyToMessage = {2}) OR
                                      (AM.TARGETTYPE = {0} AND AM.TARGETID = {1} AND AM.MESSAGETYPE = {2} AND AM.ReplyToMessage = 0)              
                                UNION
                            SELECT AM.*
                                FROM RELATIONFRIEND RF,
                                     ActivityMessages as AM
                                WHERE RF.USERBASEID = {1}
                                AND RF.STATUS     = 3
                                AND AM.BASEID     = RF.USERTARGETID
                                AND AM.TARGETID  <> {1}
                                AND AM.ReplyToMessage = 0) as UserMessages
                        UNION
	                        SELECT AM.*, AM.ReplyToMessage as FatherID
	                          FROM ActivityMessages AM,
		                          (
			                        SELECT AM.*
		                              FROM ActivityMessages as AM
			                         WHERE (AM.BASETYPE = {0} AND AM.BASEID   = {1} AND AM.ReplyToMessage = {2}) OR
				                            (AM.TARGETTYPE = {0} AND AM.TARGETID = {1} AND AM.MESSAGETYPE    = {2} AND AM.ReplyToMessage = 0)              
			                         UNION
		                            SELECT AM.*
			                          FROM RELATIONFRIEND RF,
			                               ActivityMessages as AM
			                         WHERE RF.USERBASEID = {1}
			                           AND RF.STATUS     = 3
			                           AND AM.BASEID     = RF.USERTARGETID
			                           AND AM.TARGETID  <> {1}
			                           AND AM.ReplyToMessage = 0) as UserMessages
	                         WHERE AM.ReplyToMessage = UserMessages.Id) FinalMessage";
            }
        }

        private static string GetGameSQL
        {
            get
            {
                return @"SELECT *,
                                {2}
                                ROW_NUMBER() OVER ( ORDER BY FatherID DESC, CreationDate ) AS RowNum 
                           FROM (SELECT * FROM (
                                 SELECT AM.*
	                                    , AM.Id FatherID
                                   FROM ActivityMessages AM
                                  WHERE AM.TargetType = {0}
                                    AND AM.TargetID = {1}
                                    AND AM.ReplyToMessage = 0
                                 Union
                                 SELECT AM.*,
	                                    AM.ReplyToMessage FatherID
                                   FROM ActivityMessages AM,
	                                    (SELECT Id
		                                  FROM ActivityMessages AM
		                                 WHERE AM.TargetType = {0}
		                                   AND AM.TargetID = {1}
		                                   AND AM.ReplyToMessage = 0) GameMessages
                                  WHERE AM.ReplyToMessage = GameMessages.Id
                                 ) AS FinalMessage) AS T";
            }
        }

        private static string GetGroupSQL
        {
            get
            {
                return @"SELECT *,
                                {2}
                                ROW_NUMBER() OVER ( ORDER BY FatherID DESC, CreationDate ) AS RowNum 
                          FROM (SELECT * 
                                  FROM (
                                        SELECT AM.*,
                                               AM.Id FatherID
                                          FROM ActivityMessages AM
                                         WHERE AM.TargetType = {0}
                                           AND AM.TargetID = {1}
                                           AND AM.ReplyToMessage = 0
                                        Union
                                        SELECT AM.*,
                                               AM.ReplyToMessage FatherID
                                          FROM ActivityMessages AM,
                                               (SELECT Id
                                                  FROM ActivityMessages AM
                                                 WHERE AM.TargetType = {0}
                                                   AND AM.TargetID = {1}
                                                   AND AM.ReplyToMessage = 0) GroupMessages
                                         WHERE AM.ReplyToMessage = GroupMessages.Id
                                       ) AS FinalMessage) AS T";
            }
        }

        
        public static void CreateActivity(int BaseId_,
                                          MessageSourceTypeEnum BaseType_,
                                          int TargetId_,
                                          MessageSourceTypeEnum TargetType_,
                                          string Message_,
                                          MessageTypeEnum MessageType_,
                                          MessageActionEnum MessageAction_,
                                          int ReplyToMessage_)
        {
            ActivityMessage _aMessage = new ActivityMessage();

            _aMessage.BaseId = BaseId_;
            _aMessage.BaseType = (int)BaseType_;

            _aMessage.TargetID = TargetId_;
            _aMessage.TargetType = (int)TargetType_;

            _aMessage.Message = Message_;
            _aMessage.MessageType = (int)MessageType_;
            _aMessage.MessageAction = (int)MessageAction_;

            _aMessage.ReplyToMessage = ReplyToMessage_;

            _aMessage.Add();
        }

        public static ListObject<ActivityMessageProcessed> GetUserMessages(int userId, int pageNumber = 1)
        {
            ListObject<ActivityMessageProcessed> _listReturn = new ListObject<ActivityMessageProcessed>(pageNumber);

            using (PushToPlayContext _context = new PushToPlayContext())
            {

                string _queryCount = @"SELECT COUNT(1) FROM ({0}) AS T ";

                string _queryString = string.Format(GetUserMessageSQL,
                                                    (int)MessageSourceTypeEnum.User,
                                                    userId,
                                                    (int)MessageTypeEnum.Message,
                                                    string.Empty);

                _queryCount = string.Format(_queryCount, _queryString);

                _listReturn.TotalObject = _context.Database.SqlQuery<int>(_queryCount).First();

                if (_listReturn.TotalObject > 0)
                {
                    _queryString = string.Format(@"SELECT * FROM ({0}) AS T WHERE RowNum >= {1} and RowNum <= {2}",
                                                 string.Format(GetUserMessageSQL,
                                                               (int)MessageSourceTypeEnum.User,
                                                               userId,
                                                               (int)MessageTypeEnum.Message,
                                                               GetSQLComplement),
                                                 _listReturn.GetMinRowNum,
                                                 _listReturn.GetMaxRowNum);

                    var _query = _context.Database.SqlQuery<ActivityMessageProcessed>(_queryString);

                    _listReturn.Objects = _query.ToList();
                }
            }

            return _listReturn;
        }

        public static ListObject<ActivityMessageProcessed> GetGameMessages(int gameDetailId, int pageNumber = 1)
        {
            ListObject<ActivityMessageProcessed> _listReturn = new ListObject<ActivityMessageProcessed>(pageNumber);

            using (PushToPlayContext _context = new PushToPlayContext())
            {
                string _queryCount = @"SELECT COUNT(1) FROM ({0}) AS CONTA ";

                string _queryString = string.Format(GetGameSQL, 
                                                    (int)MessageSourceTypeEnum.Game, 
                                                    gameDetailId, 
                                                    string.Empty);

                _queryCount = string.Format(_queryCount, _queryString);

                _listReturn.TotalObject = _context.Database.SqlQuery<int>(_queryCount).First();

                if (_listReturn.TotalObject > 0)
                {
                    _queryString = string.Format(@"SELECT * FROM ({0}) AS T WHERE RowNum >= {1} and RowNum <= {2}", 
                                                 string.Format(GetGameSQL, 
                                                               (int)MessageSourceTypeEnum.Game, 
                                                               gameDetailId, 
                                                               GetSQLComplement),
                                                 _listReturn.GetMinRowNum, 
                                                 _listReturn.GetMaxRowNum);

                    var _query = _context.Database.SqlQuery<ActivityMessageProcessed>(_queryString);

                    _listReturn.Objects = _query.ToList();
                }
            }

            return _listReturn;
        }

        public static ListObject<ActivityMessageProcessed> GetGroupMessages(int groupId_, int pageNumber = 1)
        {
            ListObject<ActivityMessageProcessed> _listReturn = new ListObject<ActivityMessageProcessed>(pageNumber);

            using (PushToPlayContext _context = new PushToPlayContext())
            {
                string _queryCount = @"SELECT COUNT(1) FROM ({0}) AS CONTA ";

                string _queryString = string.Format(GetGroupSQL, 
                                                    (int)MessageSourceTypeEnum.Group, 
                                                    groupId_, 
                                                    string.Empty);

                _queryCount = string.Format(_queryCount, _queryString);

                _listReturn.TotalObject = _context.Database.SqlQuery<int>(_queryCount).First();

                if (_listReturn.TotalObject > 0)
                {
                    _queryString = string.Format(@"SELECT * FROM ({0}) AS T WHERE RowNum >= {1} and RowNum <= {2}", 
                                                 string.Format(GetGroupSQL, 
                                                               (int)MessageSourceTypeEnum.Group, 
                                                               groupId_,
                                                               GetSQLComplement), 
                                                 _listReturn.GetMinRowNum, 
                                                 _listReturn.GetMaxRowNum);

                    var _query = _context.Database.SqlQuery<ActivityMessageProcessed>(_queryString);

                    _listReturn.Objects = _query.ToList();

                }
            }

            return _listReturn;
        }

        public static void DeleteMessage(int messageId)
        {
            List<ActivityMessage> _listReturn = null;

            using (PushToPlayContext _context = new PushToPlayContext())
            {
                var _query = _context.ActivityMessages.Where(am => am.Id == messageId || am.ReplyToMessage == messageId);

                if (_query != null && _query.Count() > 0)
                {
                    _listReturn = _query.ToList();
                }
            }

            if (_listReturn != null)
            {
                foreach (ActivityMessage _am in _listReturn)
                {
                    _am.Delete();
                }
            }
        }

        public static ActivityMessage GetMessageById(int messageId)
        {
            using (PushToPlayContext _context = new PushToPlayContext())
            {
                var _query = _context.ActivityMessages.Where(am => am.Id == messageId);

                if (_query.Count() > 0)
                    return _query.First();
                else
                    return null;
            }
        }

        #endregion
    }
}
