using System;
using System.Collections.Generic;
using GamePlay.Fight.FightRoles;

namespace GamePlay.Fight.Skills
{
    public abstract class Skill
    {
        public string Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public IList<FightCard.Type> CostCards { get; private set; }
        public uint CoolDown { get; private set; }
        public uint RemainCoolDownTime { get; private set; }

        public bool CanUse => RemainCoolDownTime == 0;

        public abstract void Invoke(FightRole launcher, FightRole[] target);

        public void PassOneRound()
        {
            RemainCoolDownTime = Math.Max(0, --RemainCoolDownTime);
        }
    }
}