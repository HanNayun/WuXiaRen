namespace GamePlay.Roles.Bodys
{
    public class Arm : RoleBodyPart
    {
        public Arm(int healthPoint) : base(2, healthPoint)
        {
            FataType = VitalFataType.NonFatality;
        }
    }
}