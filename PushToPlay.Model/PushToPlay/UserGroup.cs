using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PushToPlay.Model
{
    public partial class UserGroups : IDisposable
    {
        public enum UserGroupStatus
        {
            Pending = 0,
            Accepted = 1
        }

        public void AssociateUserGroup()
        {
            using (PushToPlayContext _context = new PushToPlayContext())
            {
                this.CreatedDate = DateTime.Now;
                _context.UserGroups.Add(this);
                _context.SaveChanges();
            }
        }

        public static bool DeleteAssociation(int userId_, int groupId_)
        {
            if (userId_ >= 0 && groupId_ >= 0)
            {
                using (PushToPlayContext _context = new PushToPlayContext())
                {
                    var _userGroup = _context.UserGroups.Where(ug => ug.UserId == userId_ && ug.GroupId == groupId_).FirstOrDefault();

                    if (_userGroup != null)
                    {
                        _context.Entry(_userGroup).State = System.Data.EntityState.Deleted;
                        _context.SaveChanges();
                    }

                    return true;
                }
            }
            else
                return false;
        }

        public static bool IsAssociated(int userId_, int groupId_)
        {
            using (PushToPlayContext _context = new PushToPlayContext())
            {
                return _context.UserGroups.Count(ug => ug.UserId == userId_ && ug.GroupId == groupId_) > 0;                
            }
        }

        public static ListObject<Group> GetGroupsByUserName(int userId_, int pageNumber_)
        {
            ListObject<Group> _listReturn = new ListObject<Group>(pageNumber_);

            string _queryCount = @"SELECT COUNT(1) ";

            string _query = @"SELECT ROW_NUMBER() OVER ( ORDER BY G.NAME ) AS RowNum,
                                     G.* ";

            string _where = string.Format(@"FROM UserGroups UG,
                                                 Groups G
                                           WHERE UG.UserID = {0}
                                             AND UG.GroupID = G.ID", userId_);

            _queryCount = string.Concat(_queryCount, _where);

            using (PushToPlayContext _context = new PushToPlayContext())
            {
                _listReturn.TotalObject = _context.Database.SqlQuery<int>(_queryCount).First();

                if (_listReturn.TotalObject > 0)
                {
                    _query = string.Concat(_query, _where);

                    string _queryString = string.Format(@"SELECT * 
                                                            FROM ({0}) AS T 
                                                           WHERE Rownum >= {1}
                                                             AND RowNum <= {2}",
                                                        _query,
                                                        _listReturn.GetMinRowNum,
                                                        _listReturn.GetMaxRowNum);

                    var _queryReturn = _context.Groups.SqlQuery(_queryString);

                    if (_queryReturn.Count() > 0)
                    {
                        _listReturn.Objects = _queryReturn.ToList();
                        
                    }
                }
            }

            return _listReturn;
            
        }

        public void Dispose()
        {
        }
    }
}
