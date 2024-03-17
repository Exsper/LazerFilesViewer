// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the folder for full licence text.

using Realms;

namespace osu.Game
{
    [MapTo("ModPreset")]
    public class ModPreset : RealmObject
    {
        [PrimaryKey]
        public Guid ID { get; set; }
        public RulesetInfo Ruleset { get; set; } = null!;
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        [MapTo("Mods")]
        public string ModsJson { get; set; } = string.Empty;
        public bool DeletePending { get; set; }
    }
}
