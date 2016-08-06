using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PushToPlay.Model.LoL
{
    public class LoLAggregatedStats
    {
        public int totalChampionKills { get; set; }
        public int totalAssists { get; set; }
        public int totalTurretsKilled { get; set; }
        public int? totalNeutralMinionsKilled { get; set; }
        public int? totalMinionKills { get; set; }
        public int? averageNodeCaptureAssist { get; set; }
        public int? maxNodeNeutralizeAssist { get; set; }
        public int? maxChampionsKilled { get; set; }
        public int? averageChampionsKilled { get; set; }
        public int? averageNodeCapture { get; set; }
        public int? averageNumDeaths { get; set; }
        public int? maxNodeNeutralize { get; set; }
        public int? averageNodeNeutralize { get; set; }
        public int? averageTeamObjective { get; set; }
        public int? averageTotalPlayerScore { get; set; }
        public int? maxNodeCapture { get; set; }
        public int? maxObjectivePlayerScore { get; set; }
        public int? averageNodeNeutralizeAssist { get; set; }
        public int? averageAssists { get; set; }
        public int? maxTotalPlayerScore { get; set; }
        public int? maxAssists { get; set; }
        public int? maxCombatPlayerScore { get; set; }
        public int? averageCombatPlayerScore { get; set; }
        public int? maxNodeCaptureAssist { get; set; }
        public int? totalNodeCapture { get; set; }
        public int? totalNodeNeutralize { get; set; }
        public int? maxTeamObjective { get; set; }
        public int? averageObjectivePlayerScore { get; set; }
    }

    public class LoLPlayerStatSummary
    {
        public string playerStatSummaryType { get; set; }
        public LoLAggregatedStats aggregatedStats { get; set; }
        public object modifyDate { get; set; }
        public int wins { get; set; }
        public int? losses { get; set; }
    }

    public class LoLSummonerStats
    {
        public List<LoLPlayerStatSummary> playerStatSummaries { get; set; }
        public int summonerId { get; set; }
    }
}
