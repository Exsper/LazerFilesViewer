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
        public RulesetInfo? Ruleset { get; set; }
        public IList<RealmNamedFileUsage> Files { get; }
        public string? Hash { get; set; }
        public bool DeletePending { get; set; }
        public int TotalScore { get; set; }
        public int MaxCombo { get; set; }
        public double Accuracy { get; set; }
        public DateTimeOffset Date { get; set; }
        public double? PP { get; set; }
        public int OnlineID { get; set; }
        public RealmUser? User { get; set; }
        public string? Mods { get; set; }
        public string? Statistics { get; set; }
        public string? MaximumStatistics { get; set; }
        public int Rank { get; set; }
        public int Combo { get; set; }
        public string? ClientVersion { get; set; }
        public string? BeatmapHash { get; set; }
        public int TotalScoreVersion { get; set; }
        public int? LegacyTotalScore { get; set; }
        public bool BackgroundReprocessingFailed { get; set; }
        public int LegacyOnlineID { get; set; }
        public bool IsLegacyScore { get; set; }
    }
}
