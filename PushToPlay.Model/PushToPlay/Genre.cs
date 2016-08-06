using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PushToPlay.Model
{
    public partial class Genre : IDisposable
    {

        public Genre Add()
        {
            Genre _response = null;
            this.ModifiedDate = null;
            this.CreatedDate = DateTime.Now;

            using (PushToPlayContext _context = new PushToPlayContext())
            {
                _response = _context.Genres.Add(this);
                _context.SaveChanges();
            }

            return _response;        
        }

        public static Genre GetById(int id_)
        {
            using (PushToPlayContext _context = new PushToPlayContext())
            {
                var _query = from Genre _genre in _context.Genres
                             where _genre.Id == id_
                             select _genre;

                if (_query.Count() > 0)
                    return _query.First();
                else
                    return null;
            }
        }

        public static Genre GetByGiantBombId(long giantBombId_)
        {
            using (PushToPlayContext _context = new PushToPlayContext())
            {
                var _query = from Genre _genre in _context.Genres
                             where _genre.GiantBombId == giantBombId_
                             select _genre;

                if (_query.Count() > 0)
                    return _query.First();
                else
                    return null;
            }
        }

        public void Dispose()
        {
        }
    }
}
