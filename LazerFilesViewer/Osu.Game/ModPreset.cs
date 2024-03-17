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
        public RulesetInfo? Ruleset { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Mods { get; set; }
        public bool DeletePending { get; set; }
    }
}
