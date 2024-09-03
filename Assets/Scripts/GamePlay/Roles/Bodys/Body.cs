using System;

namespace GamePlay.Roles.Bodys
{
    public class Body
    {
        public Head Head { get; private set; }
        public Heart Heart { get; private set; }
        public Trunk Trunk { get; private set; }
        public Arm LeftArm { get; private set; }
        public Arm RightArm { get; private set; }
        public Leg LeftLeg { get; private set; }
        public Leg RightLeg { get; private set; }

        public bool IsBrinkDeath
        {
            get
            {
                RoleBodyPart[] importantParts = { Head, Heart, Trunk };
                return Array.Exists(importantParts, bodyPart => bodyPart.Status is RoleBodyPart.HealthStatus.SevereInjury);
            }
        }

        public bool IsLoseActiveAbility
        {
            get
            {
                if (IsBrinkDeath)
                {
                    return true;
                }

                if (LeftArm.Status is RoleBodyPart.HealthStatus.SevereInjury &&
                    RightArm.Status is RoleBodyPart.HealthStatus.SevereInjury)
                {
                    return true;
                }

                if (LeftLeg.Status is RoleBodyPart.HealthStatus.SevereInjury &&
                    RightLeg.Status is RoleBodyPart.HealthStatus.SevereInjury)
                {
                    return true;
                }

                return false;
            }
        }
    }
}