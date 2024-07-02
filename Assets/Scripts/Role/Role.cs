using System.Collections.Generic;
using Role.RoleBodyParts;
using UnityEngine;

namespace Role
{
    public class Role
    {
        public string Name { get; private set; }
        public IDictionary<RoleBodyPart.BodyPartType, RoleBodyPart> BodyParts { get; private set; }
    }
}