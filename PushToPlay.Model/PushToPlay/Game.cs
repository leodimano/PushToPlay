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
    public partial class Game : IDisposable
    {
        public void Dispose()
        {
        }

        public static Game GetGameByGiantBombId(int id)
        {
            using (PushToPlayContext _context = new PushToPlayContext())
            {
                var _query = _context.Games
                    .Include("GameDetails")
                    .Where(g => g.GiantBombID == id);

                if (_query.Count() > 0)
                    return _query.First();
                else
                    return null;
            }
        }

        public static List<Game> GetGames()
        {
            using (PushToPlayContext _context = new PushToPlayContext())
            {
                var _query = _context.Games
                    .Include("Genres")
                    .Include("GameDetails");

                if (_query.Count() > 0)
                    return _query.ToList();
                else
                    return null;
            }
        }

        public static List<Game> GetGamesWithouSteamOnly()
        {
            using (PushToPlayContext _context = new PushToPlayContext())
            {
                var _query = _context.Games.Where(g => g.SteamAppId == null);

                if (_query.Count() > 0)
                    return _query.ToList();
                else
                    return null;
            }
        }

        public static Game GetGameBySteamAppId(int steamAppId_)
        {
            using (PushToPlayContext _context = new PushToPlayContext())
            {
                var _query = _context.Games.Where(g => g.SteamAppId == steamAppId_);

                if (_query.Count() > 0)
                    return _query.First();
                else
                    return null;
            }
        }

        public static List<Game> GetGameByName(string gameName_)
        {
            using (PushToPlayContext _context = new PushToPlayContext())
            {
                var _query = _context.Games.Where(g => g.Name.Equals(gameName_, StringComparison.InvariantCultureIgnoreCase));

                if (_query.Count() > 0)
                    return _query.ToList();
                else
                    return null;
            }
        }

        public static List<Game> GetGameByNameWithDetails(string gameName_)
        {
            using (PushToPlayContext _context = new PushToPlayContext())
            {
                var _query = _context.Games.
                    Include("GameDetails").
                    Include("Genres").
                    Where(g => g.Name.Equals(gameName_, StringComparison.InvariantCultureIgnoreCase));

                if (_query.Count() > 0)
                {
                    var queryList = _query.ToList();
                    queryList.ForEach(game => game.GameDetails.ToList().ForEach(_detail => _detail.Platform = PushToPlay.Model.Platform.GetPlatformById(_detail.PlatformId)));
                    return queryList;
                }
                else
                    return null;
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

        public Game Add()
        {
            Game _gameReturn = null;

            using (PushToPlayContext _context = new PushToPlayContext())
            {
                this.ModifiedDate = null;
                this.CreatedDate = DateTime.Now;

                /* Update GenreSet */
                if (this.Genres != null)
                {
                    foreach (Genre _genre in this.Genres)
                    {
                        _context.Genres.Attach(_genre);
                    }
                }

                /* Insert GameDetail by Platform */
                foreach (GameDetail _gameDetail in this.GameDetails)
                {
                    _gameDetail.ModifiedDate = null;
                    _gameDetail.CreatedDate = DateTime.Now;

                    /* Update Platform */
                    if (_gameDetail.Platform != null)
                    {
                        _context.Platforms.Attach(_gameDetail.Platform);
                    }
                }

                /* Insert Game */
                _context.Games.Add(this);
                _context.SaveChanges();
            }

            return _gameReturn;
        }

    }
}
