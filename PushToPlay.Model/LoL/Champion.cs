using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace PushToPlay.Model.LoL
{

    public partial class LoLChampionImage
    {
        public string full { get; set; }
        public string sprite { get; set; }
        public string group { get; set; }
        public int x { get; set; }
        public int y { get; set; }
        public int w { get; set; }
        public int h { get; set; }
    }

    public partial class LoLChampionSkin
    {
        public int id { get; set; }
        public string name { get; set; }
        public int num { get; set; }
    }

    public class LoLChampionInfo
    {
        public int attack { get; set; }
        public int defense { get; set; }
        public int magic { get; set; }
        public int difficulty { get; set; }
    }

    public class LoLChampionStats
    {
        public double armor { get; set; }
        public double armorperlevel { get; set; }
        public double attackdamage { get; set; }
        public double attackdamageperlevel { get; set; }
        public double attackrange { get; set; }
        public double attackspeedoffset { get; set; }
        public double attackspeedperlevel { get; set; }
        public double crit { get; set; }
        public double critperlevel { get; set; }
        public double hp { get; set; }
        public double hpperlevel { get; set; }
        public double hpregen { get; set; }
        public double hpregenperlevel { get; set; }
        public double movespeed { get; set; }
        public double mp { get; set; }
        public double mpperlevel { get; set; }
        public double mpregen { get; set; }
        public double mpregenperlevel { get; set; }
        public double spellblock { get; set; }
        public double spellblockperlevel { get; set; }
    }

    public class LoLChampionLeveltip
    {
        public List<string> label { get; set; }
        public List<string> effect { get; set; }
    }

    public partial class LoLChampionImageSpell
    {
        public string full { get; set; }
        public string sprite { get; set; }
        public string group { get; set; }
        public int x { get; set; }
        public int y { get; set; }
        public int w { get; set; }
        public int h { get; set; }
    }

    public class LoLChampionVar
    {
        public string key { get; set; }
        public string link { get; set; }
        public List<double> coeff { get; set; }
    }

    public partial class LoLChampionSpell
    {
        public string name { get; set; }
        public string description { get; set; }
        public string sanitizedDescription { get; set; }
        public string tooltip { get; set; }
        public string sanitizedTooltip { get; set; }
        public LoLChampionLeveltip leveltip { get; set; }
        public LoLChampionImageSpell image { get; set; }
        public string resource { get; set; }
        public int maxrank { get; set; }
        public List<int> cost { get; set; }
        public string costType { get; set; }
        public string costBurn { get; set; }
        public List<double> cooldown { get; set; }
        public string cooldownBurn { get; set; }
        public List<List<double>> effect { get; set; }
        public List<string> effectBurn { get; set; }
        public List<LoLChampionVar> vars { get; set; }

        [JsonConverter(typeof(SpellRangeConverter))]
        public List<int> range { get; set; }

        public string rangeBurn { get; set; }
        public string key { get; set; }
    }

    public partial class LoLChampionImagePassive
    {
        public string full { get; set; }
        public string sprite { get; set; }
        public string group { get; set; }
        public int x { get; set; }
        public int y { get; set; }
        public int w { get; set; }
        public int h { get; set; }
    }

    public class LoLChampionPassive
    {
        public string name { get; set; }
        public string description { get; set; }
        public string sanitizedDescription { get; set; }
        public LoLChampionImagePassive image { get; set; }
    }

    public class LoLChampionItem
    {
        public int id { get; set; }
        public int count { get; set; }
    }

    public class LoLChampionBlock
    {
        public string type { get; set; }
        public List<LoLChampionItem> items { get; set; }
    }

    public class LoLChampionRecommended
    {
        public string champion { get; set; }
        public string title { get; set; }
        public string type { get; set; }
        public string map { get; set; }
        public string mode { get; set; }
        public bool priority { get; set; }
        public List<LoLChampionBlock> blocks { get; set; }
    }

    public class LoLChampion
    {
        public int id { get; set; }
        public string key { get; set; }
        public string name { get; set; }
        public string title { get; set; }
        public LoLChampionImage image { get; set; }
        public List<LoLChampionSkin> skins { get; set; }
        public string lore { get; set; }
        public string blurb { get; set; }
        public List<string> allytips { get; set; }
        public List<string> enemytips { get; set; }
        public List<string> tags { get; set; }
        public string partype { get; set; }
        public LoLChampionInfo info { get; set; }
        public LoLChampionStats stats { get; set; }
        public List<LoLChampionSpell> spells { get; set; }
        public LoLChampionPassive passive { get; set; }
        public List<LoLChampionRecommended> recommended { get; set; }
    }
    
}
