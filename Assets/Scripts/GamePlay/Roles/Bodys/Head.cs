namespace GamePlay.Roles.Bodys
{
    public class Head : RoleBodyPart
    {
        public Head(int healthPoint) : base(1, healthPoint)
        {
            FataType = VitalFataType.Fatality;
        }
    }
}