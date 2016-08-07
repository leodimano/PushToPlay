using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PushToPlay.Model
{
    /// <summary>
    /// Classe parcial para manter as regras de negocio do modelo
    /// </summary>
    public partial class User : ModelBase, IDisposable
    {
        public void Dispose()
        {
        }

        #region Auth

        public User CheckUserCredentials(string userName_, string password_)
        {
            this.UserName = userName_;
            this.Password = password_;
            return this.CheckUserCredentials();
        }

        public User CheckUserCredentials()
        {
            using (PushToPlayContext _context = new PushToPlayContext())
            {
                string encryptPassword = Helpers.SHA1.Encode(this.Password);

                var _user = from user in _context.Users
                            where user.UserName.ToUpper().Equals(this.UserName.ToUpper()) &&
                                  user.Password == encryptPassword
                            select user;

                if (_user.Count() > 0)
                    return _user.First();
                else
                    return null;
            }
        }

        #endregion

        #region Search Methods

        public static User GetUserByUserName(string userName_)
        {
            using (PushToPlayContext _context = new PushToPlayContext())
            {
                var _user = from user in _context.Users
                            where user.UserName.Equals(userName_.ToUpper())
                            select user;

                if (_user.Count() > 0)
                    return _user.First();
                else
                    return null;
            }
        }

        public static User GetUserById(int userId_)
        {
            using (PushToPlayContext _context = new PushToPlayContext())
            {
                var _user = _context.Users.Where(u => u.Id == userId_);

                if (_user.Count() > 0)
                    return _user.First();
                else
                    return null;
            }
        }

        public User GetUserByName()
        {
            return GetUserByUserName(this.UserName);
        }

        public User GetUserByEmail()
        {
            using (PushToPlayContext _context = new PushToPlayContext())
            {
                var _user = _context.Users.Where(u => u.Email.ToUpper() == this.Email.ToUpper());

                if (_user.Count() > 0)
                    return _user.First();
                else
                    return null;
            }
        }

        public ListObject<User> GetUserByUserNameOrName(string searchString_, int pageNumber_ = 1)
        {
            ListObject<User> _listReturn = new ListObject<User>(pageNumber_);

            using (PushToPlayContext _context = new PushToPlayContext())
            {
                string _where = string.Empty;
                string _queryCount = @"SELECT COUNT(1) ";

                string _queryString =
                    @"SELECT ROW_NUMBER() OVER ( ORDER BY U.NAME ) AS ROWNUM,
                             U.* ";
                _where =
                    @" FROM DBO.USERS U ";

                if (!string.IsNullOrWhiteSpace(searchString_))
                {
                    _where = string.Concat(_where, string.Format(" WHERE U.USERNAME LIKE '{0}%' OR U.NAME LIKE '%{0}%'", searchString_));
                }

                _queryCount = string.Concat(_queryCount, _where);
                _queryString = string.Concat(_queryString, _where);

                _listReturn.TotalObject = _context.Database.SqlQuery<int>(_queryCount).First();

                if (_listReturn.TotalObject > 0)
                {
                    _queryString =
                        string.Format(@"SELECT * FROM ({0}) AS T ", _queryString);

                    _queryString = string.Concat(_queryString, " WHERE ROWNUM >=", _listReturn.GetMinRowNum, " AND ROWNUM <= ", _listReturn.GetMaxRowNum);

                    var _queryReturn = _context.Users.SqlQuery(_queryString);

                    if (_queryReturn.Count() > 0)
                    {
                        _listReturn.Objects = _queryReturn.ToList();
                        return _listReturn;
                    }
                }
            }

            return null;
        }

        #endregion

        #region Persistence

        public bool Register()
        {
            if (this.GetUserByName() != null)
            {
                this.ModelErrorMsg = "Usuário já cadastrado";
                return false;
            }

            if (this.GetUserByEmail() != null)
            {
                this.ModelErrorMsg = "E-mail já cadastrado";
                return false;
            }

            using (PushToPlayContext _context = new PushToPlayContext())
            {
                this.LastLogon = DateTime.Now;
                this.CreatedDate = DateTime.Now;
                this.UserName = this.UserName.ToUpper();
                this.Password = Helpers.SHA1.Encode(this.Password);
                _context.Users.Add(this);
                _context.SaveChanges();
                return true;
            }
        }

        public void Update()
        {
            using (PushToPlayContext _context = new PushToPlayContext())
            {
                this.ModifiedDate = DateTime.Now;
                _context.Entry(this).State = System.Data.EntityState.Modified;
                _context.SaveChanges();
            }
        }

        #endregion

        #region Friends

        public ListObject<User> GetFriends(int pageNumber_ = 1, RelationFriendEnum relationStatus_ = RelationFriendEnum.Undefined)
        {
            return User.GetFriends(this.Id, pageNumber_, relationStatus_);
        }

        public static ListObject<User> GetFriends(int userId_, int pageNumber_ = 1, RelationFriendEnum relationStatus_ = RelationFriendEnum.Undefined)
        {
            ListObject<User> _listReturn = new ListObject<User>(pageNumber_);

            using (PushToPlayContext _context = new PushToPlayContext())
            {
                string _where = string.Empty;
                string _queryCount = string.Empty;
                string _query = string.Empty;

                _queryCount = "SELECT COUNT(1) ";

                _query = @"SELECT ROW_NUMBER() OVER ( ORDER BY U.NAME ) AS ROWNUM,
                                  U.* ";

                _where = string.Format(@" FROM DBO.USERS U,
                                               DBO.RELATIONFRIENDS RF
                                         WHERE RF.USERBASEID = {0}
                                           AND RF.USERTARGETID = U.ID ", userId_);

                if (relationStatus_ != RelationFriendEnum.Undefined)
                    _where = string.Concat(_where, string.Format(" AND RF.STATUS = {0}", (int)relationStatus_));

                _queryCount = string.Concat(_queryCount, _where);
                _query = string.Concat(_query, _where);

                _listReturn.TotalObject = _context.Database.SqlQuery<int>(_queryCount).First();

                if (_listReturn.TotalObject > 0)
                {
                    _query = string.Format("SELECT * FROM ({0}) AS T ", _query);
                    _query = string.Concat(_query, string.Format(" WHERE ROWNUM >= {0} AND ROWNUM <= {1} ", _listReturn.GetMinRowNum, _listReturn.GetMaxRowNum));

                    var _queryReturn = _context.Users.SqlQuery(_query);

                    if (_queryReturn.Count() > 0)
                    {
                        _listReturn.Objects = _queryReturn.ToList();
                        return _listReturn;
                    }
                }
            }

            return _listReturn;
        }

        public static User GetFriend(int userId_, int friendId_)
        {
            using (PushToPlayContext _context = new PushToPlayContext())
            {
                string _queryString = string.Format(@"SELECT U.* 
                                                        FROM DBO.USERS U,
                                                         DBO.RELATIONFRIENDS RF
                                                       WHERE RF.USERBASEID = {0}
                                                         AND RF.USERTARGETID = {1}
                                                         AND U.ID = RF.USERTARGETID", userId_, friendId_);

                var _query = _context.Users.SqlQuery(_queryString);

                if (_query.Count() > 0)
                    return _query.First();
                else
                    return null;
            }
        }

        #endregion

        #region Groups

        public ListObject<Group> GetGroups(int pageNumber_)
        {
            return Model.UserGroups.GetGroupsByUserName(this.Id, pageNumber_);
        }

        public Group JoinGroup(int groupId_)
        {
            if (!Model.UserGroups.IsAssociated(this.Id, groupId_))
            {
                using (UserGroups _userGroups = new UserGroups())
                {
                    _userGroups.UserId = this.Id;
                    _userGroups.GroupId = groupId_;
                    _userGroups.Status = (int)Model.UserGroups.UserGroupStatus.Accepted;
                    _userGroups.AssociateUserGroup();
                }

                ActivityMessage.CreateActivity(this.Id,
                                               MessageSourceTypeEnum.User,
                                               groupId_,
                                               MessageSourceTypeEnum.Group,
                                               string.Empty,
                                               MessageTypeEnum.Activity,
                                               MessageActionEnum.GroupAdded,
                                               0);
            }

            return Group.GetGroupById(groupId_);
        }

        public Group LeaveGroup(int groupId_)
        {
            Model.UserGroups.DeleteAssociation(this.Id, groupId_);

            Model.ActivityMessage.CreateActivity(this.Id,
                                                 MessageSourceTypeEnum.User,
                                                 groupId_,
                                                 MessageSourceTypeEnum.Group,
                                                 string.Empty,
                                                 MessageTypeEnum.Activity,
                                                 MessageActionEnum.GroupDeleted,
                                                 0);

            return Group.GetGroupById(groupId_);
        }

        public bool IsAssociatedToGroup(int groupId_)
        {
            return Model.UserGroups.IsAssociated(this.Id, groupId_);
        }

        #endregion
    }
}
