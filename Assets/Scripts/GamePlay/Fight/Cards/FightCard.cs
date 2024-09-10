namespace GamePlay.Fight
{
    public class FightCard
    {
        public enum Type
        {
            Anger = 1,
            Calm = 2,
            Agile = 3
        }

        public Type CardType { get; private set; }

        public FightCard(Type cardType)
        {
            CardType = cardType;
        }
    }
}