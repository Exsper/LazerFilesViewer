// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the folder for full licence text.

using Realms;

namespace osu.Game
{
    [MapTo("BeatmapSet")]
    public class BeatmapSetInfo : RealmObject
    {
        [PrimaryKey]
        public Guid ID { get; set; }
        [Indexed]
        public int OnlineID { get; set; }
        public DateTimeOffset DateAdded { get; set; }
        public DateTimeOffset? DateSubmitted { get; set; }
        public DateTimeOffset? DateRanked { get; set; }
        public IList<BeatmapInfo> Beatmaps { get; } = null!;
        public IList<RealmNamedFileUsage> Files { get; } = null!;
        public int Status { get; set; }
        public bool DeletePending { get; set; }
        public string Hash { get; set; } = string.Empty;
        public bool Protected { get; set; }
    }
}
