namespace Role.RoleBodyParts
{
    public class RoleBodyPart
    {
        public enum BodyPartType
        {
            Head = 1,
            Body = 2,
            Heart = 3,
            LeftArm = 4,
            RightArm = 5,
            LeftLeg = 6,
            RightLeg = 7
        }

        public string Name { get; private set; }
        public BodyPartType Type { get; private set; } 
        public int MaxCrucialPoint { get; private set;   }
        public int CurrentCrucialPoint { get; private set; }
    }
}