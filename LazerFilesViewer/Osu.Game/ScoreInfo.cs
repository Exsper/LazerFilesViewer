// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the folder for full licence text.

using Realms;

namespace osu.Game
{
    [MapTo("Score")]
    public class ScoreInfo : RealmObject
    {
        [PrimaryKey]
        public Guid ID { get; set; }
        public BeatmapInfo? BeatmapInfo { get; set; }
        public RulesetInfo Ruleset { get; set; } = null!;
        public IList<RealmNamedFileUsage> Files { get; } = null!;
        public string Hash { get; set; } = string.Empty;
        public bool DeletePending { get; set; }
        public long TotalScore { get; set; }
        public int MaxCombo { get; set; }
        public double Accuracy { get; set; }
        public DateTimeOffset Date { get; set; }
        public double? PP { get; set; }
        [Indexed]
        public long OnlineID { get; set; }
        [MapTo("User")]
        public RealmUser RealmUser { get; set; } = null!;
        [MapTo("Mods")]
        public string ModsJson { get; set; } = string.Empty;
        [MapTo("Statistics")]
        public string StatisticsJson { get; set; } = string.Empty;
        [MapTo("MaximumStatistics")]
        public string MaximumStatisticsJson { get; set; } = string.Empty;
        [MapTo("Rank")]
        public int RankInt { get; set; }
        public int Combo { get; set; }
        public string ClientVersion { get; set; } = string.Empty;
        public string BeatmapHash { get; set; } = string.Empty;
        public int TotalScoreVersion { get; set; }
        public long? LegacyTotalScore { get; set; }
        public bool BackgroundReprocessingFailed { get; set; }
        [Indexed]
        public long LegacyOnlineID { get; set; }
        public bool IsLegacyScore { get; set; }
    }
}
