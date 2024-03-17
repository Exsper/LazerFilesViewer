﻿// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the folder for full licence text.

using Realms;

namespace osu.Game
{
    [MapTo("Skin")]
    public class SkinInfo : RealmObject
    {
        [PrimaryKey]
        public Guid ID { get; set; }
        public string? Name { get; set; }
        public string? Creator { get; set; }
        public string? InstantiationInfo { get; set; }
        public string? Hash { get; set; }
        public bool Protected { get; set; }
        public IList<RealmNamedFileUsage> Files { get; } = null!;
        public bool DeletePending { get; set; }
    }
}
