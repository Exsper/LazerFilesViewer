// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the folder for full licence text.

using Realms;

namespace osu.Game
{
    [MapTo("Beatmap")]
    public class BeatmapInfo : RealmObject
    {
        [PrimaryKey]
        public Guid ID { get; set; }
        public string DifficultyName { get; set; } = string.Empty;
        public RulesetInfo Ruleset { get; set; } = null!;
        public BeatmapDifficulty Difficulty { get; set; } = null!;
        public BeatmapMetadata Metadata { get; set; } = null!;
        [Backlink(nameof(ScoreInfo.BeatmapInfo))]
        public IQueryable<ScoreInfo> Scores { get; } = null!;
        public BeatmapUserSettings UserSettings { get; set; } = null!;
        public BeatmapSetInfo? BeatmapSet { get; set; }
        public int Status { get; set; }
        [Indexed]
        public int OnlineID { get; set; }
        public double Length { get; set; }
        public double BPM { get; set; }
        public string Hash { get; set; } = string.Empty;
        public double StarRating { get; set; }
        [Indexed]
        public string MD5Hash { get; set; } = string.Empty;
        public string OnlineMD5Hash { get; set; } = string.Empty;
        public DateTimeOffset? LastLocalUpdate { get; set; }
        public DateTimeOffset? LastOnlineUpdate { get; set; }
        public bool Hidden { get; set; }
        public double AudioLeadIn { get; set; }
        public float StackLeniency { get; set; }
        public bool SpecialStyle { get; set; }
        public bool LetterboxInBreaks { get; set; }
        public bool WidescreenStoryboard { get; set; }
        public bool EpilepsyWarning { get; set; }
        public bool SamplesMatchPlaybackRate { get; set; }
        public DateTimeOffset? LastPlayed { get; set; }
        public double DistanceSpacing { get; set; }
        public int BeatDivisor { get; set; }
        public int GridSize { get; set; }
        public double TimelineZoom { get; set; }
        public int CountdownOffset { get; set; }
        public int EndTimeObjectCount { get; set; }
        public int TotalObjectCount { get; set; }
        public double? EditorTimestamp { get; set; }
    }
}
