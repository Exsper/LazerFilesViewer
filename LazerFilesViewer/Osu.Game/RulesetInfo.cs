// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the folder for full licence text.

using Realms;

namespace osu.Game
{
    [MapTo("Ruleset")]
    public class RulesetInfo : RealmObject
    {
        [PrimaryKey]
        public string? ShortName { get; set; }
        [Indexed]
        public int OnlineID { get; set; }
        public string? Name { get; set; }
        public string? InstantiationInfo { get; set; }
        public int LastAppliedDifficultyVersion { get; set; }
        public bool Available { get; set; }
    }
}
