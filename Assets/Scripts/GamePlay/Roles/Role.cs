using System.Collections.Generic;
using GamePlay.Fight;

namespace GamePlay.Roles
{
    public class Role : RoleData
    {
        public bool IsPlayer { get; private set; }
        public Bodys.Body Body { get; private set; }
        public IList<string> LearnedSkillIds { get; private set; }
        public IList<string> EquippedSkillIds { get; private set; }
        public IDictionary<FightCard.Type, uint> FightCardToCount { get; private set; }
    }
}