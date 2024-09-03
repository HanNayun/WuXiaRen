namespace GamePlay.Roles.Bodys
{
    public class Leg:RoleBodyPart
    {
        public Leg(int healthPoint) : base(2, healthPoint)
        {
            FataType = VitalFataType.NonFatality;
        }
    }
}