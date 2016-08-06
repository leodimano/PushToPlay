using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PushToPlay.Model.Steam
{
    public class SteamAppAPI
    {
        public long AppID { get; set; }
        public string Name { get; set; }

        public bool isGame { get; set; }
        public long CurrentPlaying { get; set; }
    }
}
