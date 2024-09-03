namespace GamePlay.Roles.Bodys
{
    public class Trunk : RoleBodyPart
    {
        public Trunk(int healthPoint) : base(3, healthPoint)
        {
            FataType = VitalFataType.NonFatality;
        }
    }
}