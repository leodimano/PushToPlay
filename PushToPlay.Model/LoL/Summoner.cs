﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PushToPlay.Model.LoL
{
    public class LoLSummoner
    {
        public int id { get; set; }
        public string name { get; set; }
        public int profileIconId { get; set; }
        public int summonerLevel { get; set; }
        public long revisionDate { get; set; }
    }
}
