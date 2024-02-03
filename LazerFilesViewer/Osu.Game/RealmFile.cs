// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using Realms;

namespace osu.Game.Models
{
    [MapTo("File")]
    public class RealmFile : RealmObject
    {
        [PrimaryKey]
        public string Hash { get; set; } = string.Empty;

        [Backlink(nameof(RealmNamedFileUsage.File))]
        public IQueryable<RealmNamedFileUsage> Usages { get; } = null!;
    }
}
