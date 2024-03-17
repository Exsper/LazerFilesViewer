// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the folder for full licence text.

using Realms;

namespace osu.Game
{
    [MapTo("KeyBinding")]
    public class RealmKeyBinding : RealmObject
    {
        [PrimaryKey]
        public Guid ID { get; set; }
        public string? RulesetName { get; set; }
        public int? Variant { get; set; }
        [MapTo("Action")]
        public int ActionInt { get; set; }
        [MapTo("KeyCombination")]
        public string KeyCombinationString { get; set; } = null!;
    }
}
