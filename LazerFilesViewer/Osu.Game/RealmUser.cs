// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the folder for full licence text.

using Realms;

namespace osu.Game
{
    [MapTo("RealmUser")]
    public class RealmUser : RealmObject
    {
        public int OnlineID { get; set; }
        public string Username { get; set; } = string.Empty;
        [MapTo("CountryCode")]
        public string? CountryString { get; set; }
    }
}
