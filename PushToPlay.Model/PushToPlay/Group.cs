using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PushToPlay.Model
{
    public enum GroupTypeEnum
    {
        GROUP = 1,
        CLAN = 2
    }

    /// <summary>
    /// Classe parcial para manter as regras de negocio do modelo
    /// </summary>
    public partial class Group : ModelBase, IDisposable
    {

        #region Search Methods 

        public static Group GetGroupById(int groupId_)
        {
            using (PushToPlayContext _context = new PushToPlayContext())
            {
                var _query = _context.Groups.Where(g => g.Id == groupId_);

                if (_query.Count() > 0)
                    return _query.First();
                else
                    return null;
            }
        }

        public static Group GetGroupByName(string name_, GroupTypeEnum groupType_)
        {
            using (PushToPlayContext _context = new PushToPlayContext())
            {
                var _query = _context.Groups.Where(g => g.Name.Equals(name_, StringComparison.InvariantCultureIgnoreCase) && g.GroupType == (int)groupType_);

                if (_query.Count() > 0)
                    return _query.First();
                else
                    return null;
            }
        }

        public static ListObject<Group> GetGroupByName(string groupName_, GroupTypeEnum groupType_, int pageNumber_)
        {
            ListObject<Group> _listReturn = new ListObject<Group>(pageNumber_);

            using (PushToPlayContext _context = new PushToPlayContext())
            {
                string _where = string.Empty;

                string _queryCount = @"SELECT COUNT(1) ";

                string _queryString =
                    @"SELECT ROW_NUMBER() OVER ( ORDER BY G.NAME ) AS ROWNUM,
                             G.*";

                _where = string.Format(" FROM DBO.GROUPS G WHERE G.GROUPTYPE = {0} ", (int)groupType_);

                if (!string.IsNullOrWhiteSpace(groupName_))
                {
                    _where = string.Concat(_where, string.Format("AND G.NAME LIKE '{0}%' ", groupName_));
                }

                _queryCount = string.Concat(_queryCount, _where);
                _queryString = string.Concat(_queryString, _where);

                _listReturn.TotalObject = _context.Database.SqlQuery<int>(_queryCount).First();

                if (_listReturn.TotalObject > 0)
                {
                    _queryString =
                        string.Format(@"SELECT T.* FROM ({0}) AS T ", _queryString);

                    _queryString = string.Concat(_queryString, "WHERE ROWNUM >= ", _listReturn.GetMinRowNum, " AND ROWNUM <= ", _listReturn.GetMaxRowNum);

                    var _queryReturn = _context.Groups.SqlQuery(_queryString);

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
            if (GetGroupByName(this.Name, (GroupTypeEnum)this.GroupType) != null)
            {
                this.ModelErrorMsg = "Grupo já cadastrado com este nome.";
                return false;
            }

            using (PushToPlayContext _context = new PushToPlayContext())
            {
                this.ModifiedDate = null;
                this.CreatedDate = DateTime.Now;
                _context.Groups.Add(this);
                _context.SaveChanges();
            }

            return true;
        }

        #endregion

        public void Dispose()
        {
        }
    }
}
