// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the folder for full licence text.

using Realms;

namespace osu.Game
{
    [MapTo("BeatmapDifficulty")]
    public class BeatmapDifficulty : EmbeddedObject
    {
        public float DrainRate { get; set; }
        public float CircleSize { get; set; }
        public float OverallDifficulty { get; set; }
        public float ApproachRate { get; set; }
        public double SliderMultiplier { get; set; }
        public double SliderTickRate { get; set; }
    }
}
