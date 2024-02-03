// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using Realms;

namespace osu.Game.Models
{
    public class RealmUser : EmbeddedObject
    {
        public int OnlineID { get; set; } = 1;

        public string Username { get; set; } = string.Empty;


        public bool IsBot => false;


    }
}
