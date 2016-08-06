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
    public partial class UserGame
    {
        public void AssociateUserGameDetail()
        {
            if (this.UserId >= 0 && this.GameDetailId >= 0)
            {
                if (!UserGameAssociated(this.UserId, this.GameDetailId))
                {
                    using (PushToPlayContext _context = new PushToPlayContext())
                    {
                        _context.UserGames.Add(this);
                        _context.SaveChanges();
                    }

                    ActivityMessage.CreateActivity(this.UserId,
                                                   MessageSourceTypeEnum.User,
                                                   this.GameDetailId,
                                                   MessageSourceTypeEnum.Game,
                                                   string.Empty,
                                                   MessageTypeEnum.Activity,
                                                   MessageActionEnum.GameAdded,
                                                   0);
                }
            }
        }

        public static int GetGamesbyUserId(int userId_)
        {
            using (PushToPlayContext _context = new PushToPlayContext())
            {
                return _context.UserGames.Count(ug => ug.UserId == userId_);
            }
        }

        public static int GetUsersByGameDetail(int gameDetailId_)
        {
            using (PushToPlayContext _context = new PushToPlayContext())
            {
                return _context.UserGames.Count(ug => ug.GameDetailId == gameDetailId_);
            }
        }

        public static bool UserGameAssociated(int userId_, int gameDetailId_)
        {
            if (userId_ >= 0)
            {
                using (PushToPlayContext _context = new PushToPlayContext())
                {
                    int _countAssociations = _context.UserGames.Count(ug => ug.UserId == userId_ && ug.GameDetailId == gameDetailId_);
                    return _countAssociations > 0;
                }
            }
            else
                return false;
        }

        public static bool DeleteAssociation(int userId_, int gameDetailId_)
        {
            if (userId_ >= 0 && gameDetailId_ >= 0)
            {
                using (PushToPlayContext _context = new PushToPlayContext())
                {
                    var _ug = _context.UserGames.Where(ug => ug.UserId == userId_ && ug.GameDetailId == gameDetailId_).FirstOrDefault();

                    if (_ug != null)
                    {
                        _context.Entry(_ug).State = System.Data.EntityState.Deleted;
                        _context.SaveChanges();
                    }

                    ActivityMessage.CreateActivity(userId_,
                                                   MessageSourceTypeEnum.User,
                                                   gameDetailId_,
                                                   MessageSourceTypeEnum.Game,
                                                   string.Empty,
                                                   MessageTypeEnum.Activity,
                                                   MessageActionEnum.GameDeleted,
                                                   0);

                    return true;
                }
            }
            else
                return false;
        }
    }
}
