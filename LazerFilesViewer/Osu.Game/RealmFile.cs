// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the folder for full licence text.

using Realms;

namespace osu.Game
{
    [MapTo("File")]
    public class RealmFile : RealmObject
    {
        [PrimaryKey]
        public string Hash { get; set; }
        /*
        [Backlink(nameof(RealmNamedFileUsage.File))]
        public IQueryable<RealmNamedFileUsage> Usages { get; } = null!;
        */
    }
}
