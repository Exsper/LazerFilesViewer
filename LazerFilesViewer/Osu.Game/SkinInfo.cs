// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the folder for full licence text.

using Realms;

namespace osu.Game
{
    [MapTo("Skin")]
    public class SkinInfo : RealmObject
    {
        [PrimaryKey]
        public Guid ID { get; set; }
        public string Name { get; set; } = null!;
        public string Creator { get; set; } = null!;
        public string InstantiationInfo { get; set; } = null!;
        public string Hash { get; set; } = string.Empty;
        public bool Protected { get; set; }
        public IList<RealmNamedFileUsage> Files { get; } = null!;
        public bool DeletePending { get; set; }
    }
}
