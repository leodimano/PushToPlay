//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PushToPlay.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class Genre
    {
        public Genre()
        {
            this.Games = new HashSet<Game>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ModifiedDate { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public long GiantBombId { get; set; }
    
        public virtual ICollection<Game> Games { get; set; }
    }
}