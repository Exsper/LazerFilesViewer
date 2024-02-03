// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using osu.Game.Models;
using Realms;

namespace osu.Game.Skinning
{
    [MapTo("Skin")]
    public class SkinInfo : RealmObject, IEquatable<SkinInfo>
    {
        internal static readonly Guid TRIANGLES_SKIN = new Guid("2991CFD8-2140-469A-BCB9-2EC23FBCE4AD");
        internal static readonly Guid ARGON_SKIN = new Guid("CFFA69DE-B3E3-4DEE-8563-3C4F425C05D0");
        internal static readonly Guid ARGON_PRO_SKIN = new Guid("9FC9CF5D-0F16-4C71-8256-98868321AC43");
        internal static readonly Guid CLASSIC_SKIN = new Guid("81F02CD3-EEC6-4865-AC23-FAE26A386187");
        internal static readonly Guid RANDOM_SKIN = new Guid("D39DFEFB-477C-4372-B1EA-2BCEA5FB8908");

        [PrimaryKey]
        public Guid ID { get; set; }

        public string Name { get; set; } = null!;

        public string Creator { get; set; } = null!;

        public string InstantiationInfo { get; set; } = null!;

        public string Hash { get; set; } = string.Empty;
        public bool Protected { get; set; }

        public IList<RealmNamedFileUsage> Files { get; } = null!;

        public bool DeletePending { get; set; }

        public SkinInfo(string? name = null, string? creator = null, string? instantiationInfo = null)
        {
            Name = name ?? string.Empty;
            Creator = creator ?? string.Empty;
            InstantiationInfo = instantiationInfo ?? string.Empty;
            ID = Guid.NewGuid();
        }

        private SkinInfo()
        {
        }

        public bool Equals(SkinInfo? other)
        {
            if (ReferenceEquals(this, other)) return true;
            if (other == null) return false;

            return ID == other.ID;
        }

        public override string ToString()
        {
            string author = string.IsNullOrEmpty(Creator) ? string.Empty : $"({Creator})";
            return $"{Name} {author}".Trim();
        }

    }
}
