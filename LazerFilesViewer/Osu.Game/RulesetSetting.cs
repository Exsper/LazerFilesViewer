// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the folder for full licence text.

using Realms;

namespace osu.Game
{
    [MapTo("RulesetSetting")]
    public class RulesetSetting : RealmObject
    {
        [Indexed]
        public string? RulesetName { get; set; }
        [Indexed]
        public int Variant { get; set; }
        [Required]
        public string Key { get; set; }
        [Required]
        public string Value { get; set; }
    }
}
