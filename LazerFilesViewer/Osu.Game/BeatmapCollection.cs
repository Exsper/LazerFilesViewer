// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the folder for full licence text.

using Realms;

namespace osu.Game
{
    [MapTo("BeatmapCollection")]
    public class BeatmapCollection : RealmObject
    {
        [PrimaryKey]
        public Guid ID { get; set; }
        public string? Name { get; set; }
        public IList<string> BeatmapMD5Hashes { get; }
        public DateTimeOffset LastModified { get; set; }
    }
}
