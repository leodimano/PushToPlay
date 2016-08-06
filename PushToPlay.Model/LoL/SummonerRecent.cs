using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PushToPlay.Model.LoL
{

    public class LoLFellowPlayer
    {
        public int championId { get; set; }
        public int teamId { get; set; }
        public int summonerId { get; set; }
    }

    public class LoLSummonerRecentStats
    {
        public int totalDamageDealtToChampions { get; set; }
        public int item2 { get; set; }
        public int goldEarned { get; set; }
        public int item1 { get; set; }
        public int totalDamageTaken { get; set; }
        public int item0 { get; set; }
        public int trueDamageDealtPlayer { get; set; }
        public int physicalDamageDealtPlayer { get; set; }
        public int trueDamageDealtToChampions { get; set; }
        public int killingSprees { get; set; }
        public int totalUnitsHealed { get; set; }
        public int largestCriticalStrike { get; set; }
        public int level { get; set; }
        public int magicDamageDealtToChampions { get; set; }
        public int magicDamageDealtPlayer { get; set; }
        public int assists { get; set; }
        public int magicDamageTaken { get; set; }
        public int numDeaths { get; set; }
        public int totalTimeCrowdControlDealt { get; set; }
        public int largestMultiKill { get; set; }
        public int physicalDamageTaken { get; set; }
        public bool win { get; set; }
        public int team { get; set; }
        public int totalDamageDealt { get; set; }
        public int largestKillingSpree { get; set; }
        public int totalHeal { get; set; }
        public int item4 { get; set; }
        public int item3 { get; set; }
        public int item6 { get; set; }
        public int item5 { get; set; }
        public int minionsKilled { get; set; }
        public int timePlayed { get; set; }
        public int physicalDamageDealtToChampions { get; set; }
        public int championsKilled { get; set; }
        public int trueDamageTaken { get; set; }
        public int goldSpent { get; set; }
        public int? neutralMinionsKilledYourJungle { get; set; }
        public int? neutralMinionsKilledEnemyJungle { get; set; }
        public int? neutralMinionsKilled { get; set; }
        public int? barracksKilled { get; set; }
        public int? wardPlaced { get; set; }
        public int? doubleKills { get; set; }
        public int? turretsKilled { get; set; }
        public int? sightWardsBought { get; set; }
    }

    public class LoLGame
    {
        public List<LoLFellowPlayer> fellowPlayers { get; set; }
        public string gameType { get; set; }
        public LoLSummonerRecentStats stats { get; set; }
        public int gameId { get; set; }
        public int ipEarned { get; set; }
        public int spell1 { get; set; }
        public int teamId { get; set; }
        public int spell2 { get; set; }
        public string gameMode { get; set; }
        public int mapId { get; set; }
        public int level { get; set; }
        public bool invalid { get; set; }
        public string subType { get; set; }
        public object createDate { get; set; }
        public int championId { get; set; }
    }

    public class LoLSummonerRecent
    {
        public List<LoLGame> games { get; set; }
        public int summonerId { get; set; }
    }    

}
