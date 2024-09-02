namespace GamePlay.Fight
{
    public class FightCard
    {
        public enum Type
        {
            Anger,
            Calm,
            Agile
        }

        public Type CardType { get; private set; }

        public FightCard(Type cardType)
        {
            CardType = cardType;
        }
    }
}