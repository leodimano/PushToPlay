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
    public partial class Platform : IDisposable
    {
        public void Dispose()
        {

        }

        public void Add()
        {
            using (PushToPlayContext _context = new PushToPlayContext())
            {
                this.GiantBombID = TranslatePlatform(Convert.ToInt32( this.GiantBombID));
                this.CreatedDate = DateTime.Now;
                this.ModifiedDate = null;
                _context.Platforms.Add(this);
                _context.SaveChanges();
            }
        }

        public static List<Platform> GetAll()
        {
            using (PushToPlayContext _context = new PushToPlayContext())
            {
                return _context.Platforms.ToList();
            }
        }

        public static int TranslatePlatform(int id)
        {
            switch (id)
            {
                case (int)Model.GiantBomb.GiantBombPlatformEnum.LIVEX360:
                    return (int)Model.GiantBomb.GiantBombPlatformEnum.X360;
                case (int)Model.GiantBomb.GiantBombPlatformEnum.PSN3:
                    return (int)Model.GiantBomb.GiantBombPlatformEnum.PS3;
                default:
                    return id;
            }
        }

        public static Platform GetPlatformByGiantBombId(int id)
        {                        
            using (PushToPlayContext _context = new PushToPlayContext())
            {
                id = TranslatePlatform(id);
                
                var _query = from Platform _platform in _context.Platforms
                             where _platform.GiantBombID == id
                             select _platform;

                if (_query.Count() > 0)
                    return _query.First();
                else
                    return null;
            }
        }

        public Platform GetPlatformByGiantBombId()
        {
            return GetPlatformByGiantBombId(this.Id);
        }

        public static Platform GetPlatformById(int id_)
        {
            using (PushToPlayContext _context = new PushToPlayContext())
            {
                var _query = _context.Platforms.Where(p => p.Id == id_);

                if (_query.Count() > 0)
                    return _query.First();
                else
                    return null;
            }
        }

        public void Update()
        {
        }

        public void Delete()
        {

        }
    }
}

