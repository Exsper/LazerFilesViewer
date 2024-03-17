// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the folder for full licence text.

using Realms;

namespace osu.Game
{
    [MapTo("KeyBinding")]
    public class KeyBinding : RealmObject
    {
        [PrimaryKey]
        public Guid ID { get; set; }
        public string? RulesetName { get; set; }
        public int? Variant { get; set; }
        public int Action { get; set; }
        public string? KeyCombination { get; set; }
    }
}
