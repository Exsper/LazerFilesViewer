// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the folder for full licence text.

using Realms;

namespace osu.Game
{
    [MapTo("RealmNamedFileUsage")]
    public class RealmNamedFileUsage : RealmObject
    {
        public RealmFile File { get; set; } = null!;
        public string Filename { get; set; } = null!;
    }
}
