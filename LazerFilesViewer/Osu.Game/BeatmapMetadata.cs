// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the folder for full licence text.

using Realms;

namespace osu.Game
{
    [MapTo("BeatmapMetadata")]
    public class BeatmapMetadata : RealmObject
    {
        public string? Title { get; set; }
        public string? TitleUnicode { get; set; }
        public string? Artist { get; set; }
        public string? ArtistUnicode { get; set; }
        public RealmUser? Author { get; set; }
        public string? Source { get; set; }
        public string? Tags { get; set; }
        public int PreviewTime { get; set; }
        public string? AudioFile { get; set; }
        public string? BackgroundFile { get; set; }
    }
}
