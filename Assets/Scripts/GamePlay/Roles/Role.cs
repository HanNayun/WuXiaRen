using System.Collections.Generic;
using GamePlay.Fight;

namespace GamePlay.Roles
{
    public class Role
    {
        public bool IsPlayer { get; private set; }
        public string Name { get; private set; }
        public Gender Gender { get; private set; }
        public Bodys.Body Body { get; private set; }
        public IList<string> LearnedSkillIds { get; private set; }
        public IList<string> EquippedSkillIds { get; private set; }
        public IList<FightCard> Deck { get; private set; }
    }
}