// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the folder for full licence text.

using Realms;

namespace osu.Game
{
    [MapTo("BeatmapUserSettings")]
    public class BeatmapUserSettings : EmbeddedObject
    {
        public double Offset { get; set; }
    }
}
