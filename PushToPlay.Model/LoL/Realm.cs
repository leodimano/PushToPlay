using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PushToPlay.Model.LoL
{
    public class LoLNumber
    {
        public string champion { get; set; }
        public string profileicon { get; set; }
        public string item { get; set; }
        public string mastery { get; set; }
        public string language { get; set; }
        public string summoner { get; set; }
        public string rune { get; set; }
    }

    public class LoLRealm
    {
        public string v { get; set; }
        public string dd { get; set; }
        public string cdn { get; set; }
        public string lg { get; set; }
        public LoLNumber n { get; set; }
        public int profileiconmax { get; set; }
        public string l { get; set; }
        public string css { get; set; }
    }
}
