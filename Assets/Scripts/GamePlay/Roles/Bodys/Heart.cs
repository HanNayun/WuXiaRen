namespace GamePlay.Roles.Bodys
{
    public class Heart : RoleBodyPart
    {
        public Heart(int healthPoint) : base(1, healthPoint)
        {
            FataType = VitalFataType.Fatality;
        }
    }
}