using System;
using System.Collections.Generic;
using GamePlay.Roles;
using GamePlay.Roles.Bodys;

namespace GamePlay.Fight.FightRoles
{
    public class FightRole : Role
    {
        public IList<FightCard> Postures { get; private set; }
        public IList<FightCard> DiscardPile { get; private set; }
        public IList<FightCard> DrawPile { get; private set; }
        public uint DefenseCount { get; private set; }

        public void SufferAttack(uint attackPoint, in RoleBodyPart[] attackTargets)
        {
            Array.Sort(attackTargets, (a, b) => a.FataType.CompareTo(b.FataType));
            foreach (RoleBodyPart roleBodyPart in attackTargets)
            {
                if (roleBodyPart.FataType is RoleBodyPart.VitalFataType.Fatality)
                {
                    if (Body.LeftArm.Status > RoleBodyPart.HealthStatus.SevereInjury)
                    {
                        Body.LeftArm.SufferAttack(attackPoint);
                    }
                    else if (Body.RightArm.Status > RoleBodyPart.HealthStatus.SevereInjury)
                    {
                        Body.RightArm.SufferAttack(attackPoint);
                    }
                }
                
                roleBodyPart.SufferAttack(attackPoint);
            }
        }
    }
}