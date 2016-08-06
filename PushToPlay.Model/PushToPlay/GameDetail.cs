using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PushToPlay.Model
{
    public partial class GameDetail
    {
        public static bool FindGameDetailByImageFile(string imageFile_)
        {
            using (PushToPlayContext _context = new PushToPlayContext())
            {
                var _query = from GameDetail g in _context.GameDetails
                             where g.IconImage == imageFile_ || g.GameImage == imageFile_
                             select true;

                if (_query.Count() > 0)
                    return true;
                else
                    return false;
            }
        }

        public void Add()
        {
            using (PushToPlayContext _context = new PushToPlayContext())
            {
                this.CreatedDate = DateTime.Now;

                if (this.Game != null)
                    _context.Games.Attach(this.Game);

                if (this.Platform != null)
                    _context.Platforms.Attach(this.Platform);

                _context.GameDetails.Add(this);
                _context.SaveChanges();
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

        public static GameDetail GetGameDetailByGiantBombPlatform(int gameId_, int platformId_)
        {
            platformId_ = Platform.TranslatePlatform(platformId_);

            using (PushToPlayContext _context = new PushToPlayContext())
            {
                var _query = from _detail in _context.GameDetails
                             where _detail.GameId == gameId_ &&
                                   _detail.Platform.GiantBombID == platformId_
                             select _detail;

                if (_query.Count() > 0)
                    return _query.First();
                else
                    return null;
            }
        }

        public static ListObject<GameDetail> GetGameDetailByLikeName(string gameName_, List<int> listPlatform_ = null, int pageNumber = 1)
        {
            ListObject<GameDetail> _listReturn = new ListObject<GameDetail>(pageNumber);

            using (PushToPlayContext _context = new PushToPlayContext())
            {
                string _where = string.Empty;

                string _queryCount = @"SELECT COUNT(1) ";

                string _queryString =
                    @"SELECT ROW_NUMBER() OVER ( ORDER BY G.NAME ) AS RowNum,
                             GD.ID,
                             GD.GAMEID,
                             GD.PLATFORMID,
                             GD.PRICE,
                             GD.ICONIMAGE,
                             GD.GAMEIMAGE,
                             GD.RELEASEDATE,
                             GD.MODIFIEDDATE,
                             GD.CREATEDDATE";

                _where =
                       @" FROM dbo.Games G,
                             dbo.GameDetails GD,
                             dbo.Platforms P
                       WHERE G.ID = GD.GAMEID
                         AND GD.PLATFORMID = P.ID ";


                if (!string.IsNullOrWhiteSpace(gameName_))
                {
                    _where = string.Concat(_where, string.Format("AND G.NAME LIKE '{0}%' ", gameName_));
                }

                if (listPlatform_ != null && listPlatform_.Count > 0)
                {
                    _where = string.Concat(_where, string.Format("AND P.ID IN ({0}) ", Helpers.FactoryHelper.NumberListToString(listPlatform_)));
                }

                _queryCount = string.Concat(_queryCount, _where);
                _queryString = string.Concat(_queryString, _where);

                _listReturn.TotalObject = _context.Database.SqlQuery<int>(_queryCount).First();

                if (_listReturn.TotalObject > 0)
                {

                    _queryString =
                        string.Format(@"SELECT T.ID,
                                           T.GAMEID,
                                           T.PLATFORMID,
                                           T.PRICE,
                                           T.ICONIMAGE,
                                           T.GAMEIMAGE,
                                           T.RELEASEDATE,
                                           T.MODIFIEDDATE,
                                           T.CREATEDDATE 
                                      FROM ({0}) AS T ", _queryString);

                    _queryString = string.Concat(_queryString, "WHERE RowNum >= ", _listReturn.GetMinRowNum, " AND RowNum <= ", _listReturn.GetMaxRowNum);

                    var _queryReturn = _context.GameDetails.SqlQuery(_queryString);


                    if (_queryReturn.Count() > 0)
                    {
                        var _returnList = _queryReturn.ToList();
                        _returnList.ForEach(detail => detail.Game = detail.Game);
                        _returnList.ForEach(detail => detail.Platform = detail.Platform);

                        _listReturn.Objects = _returnList;

                        return _listReturn;
                    }
                }
            }

            return null;
        }

        public static ListObject<GameDetail> GetGameDetailByLikeName(string gameName_, List<int> listPlatform_ = null)
        {
            return GetGameDetailByLikeName(gameName_, listPlatform_, 1);
        }

        public static GameDetail GetGameDetailById(int gameDetailId_)
        {
            using (PushToPlayContext _context = new PushToPlayContext())
            {
                return _context.GameDetails.
                    Include("Game").
                    Include("Platform").
                    Where(gd => gd.Id == gameDetailId_).FirstOrDefault();
            }
        }

        public static ListObject<GameDetail> GetGameDetailByUser(int userId_, int pageNumber = 1)
        {
            ListObject<GameDetail> _listReturn = new ListObject<GameDetail>(pageNumber);

            using (PushToPlayContext _context = new PushToPlayContext())
            {
                string _where = string.Empty;

                string _queryCount = @"SELECT COUNT(1) ";

                string _queryString =
                    @"SELECT ROW_NUMBER() OVER ( ORDER BY G.NAME ) AS RowNum,
                             GD.ID,
                             GD.GAMEID,
                             GD.PLATFORMID,
                             GD.PRICE,
                             GD.ICONIMAGE,
                             GD.GAMEIMAGE,
                             GD.RELEASEDATE,
                             GD.MODIFIEDDATE,
                             GD.CREATEDDATE";

                _where = string.Format(
                       @" FROM dbo.GAMES G,
                               dbo.GAMEDETAILS GD,
                               dbo.USERGAMES UG
                       WHERE G.ID = GD.GAMEID 
                         AND UG.GAMEID       = G.ID
                         AND UG.GAMEDETAILID = GD.ID 
                         AND UG.USERID = {0} ", userId_);

                _queryCount = string.Concat(_queryCount, _where);
                _queryString = string.Concat(_queryString, _where);

                _listReturn.TotalObject = _context.Database.SqlQuery<int>(_queryCount).First();

                if (_listReturn.TotalObject > 0)
                {

                    _queryString =
                        string.Format(@"SELECT T.ID,
                                           T.GAMEID,
                                           T.PLATFORMID,
                                           T.PRICE,
                                           T.ICONIMAGE,
                                           T.GAMEIMAGE,
                                           T.RELEASEDATE,
                                           T.MODIFIEDDATE,
                                           T.CREATEDDATE 
                                      FROM ({0}) AS T ", _queryString);

                    _queryString = string.Concat(_queryString, "WHERE RowNum >= ", _listReturn.GetMinRowNum, " AND RowNum <= ", _listReturn.GetMaxRowNum);

                    var _queryReturn = _context.GameDetails.SqlQuery(_queryString);


                    if (_queryReturn.Count() > 0)
                    {
                        var _returnList = _queryReturn.ToList();
                        _returnList.ForEach(detail => detail.Game = detail.Game);
                        _returnList.ForEach(detail => detail.Platform = detail.Platform);

                        _listReturn.Objects = _returnList;

                        return _listReturn;
                    }
                }

                if (_listReturn.Objects == null && pageNumber > 1)
                {
                    return GetGameDetailByUser(userId_, pageNumber - 1);
                }

                return null;
            }
        }
    }
}
