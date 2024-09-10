using System;
using System.Collections.Generic;
using System.Linq;
using Core;

namespace GamePlay.Fight
{
    public class CardPile
    {
        public IDictionary<FightCard.Type, uint> CardTypeToCount { get; } = new Dictionary<FightCard.Type, uint>();
        public bool IsCardEmpty => CardTypeToCount.Values.Sum(cardCount => (int)cardCount) == 0;

        public void AddCards(IList<FightCard.Type> cards)
        {
            foreach (FightCard.Type card in cards)
            {
                CardTypeToCount.TryAdd(card, 0U);
                CardTypeToCount[card]++;
            }
        }

        public IList<FightCard.Type> GetCards(uint count = 1)
        {
            IList<FightCard.Type> cards = new List<FightCard.Type>();
            while (!IsCardEmpty && count > 0)
            {
                --count;
                FightCard.Type cardType = Forture.LotteryOne(RemainCardTypes());
                cards.Add(cardType);
                CardTypeToCount[cardType]--;
            }

            return cards;
        }

        public IList<FightCard.Type> GetSpecificTypeCards(FightCard.Type cardType, uint count = 1)
        {
            IList<FightCard.Type> cards = new List<FightCard.Type>();
            if (!CardTypeToCount.TryGetValue(cardType, out uint cardCount))
            {
                return cards;
            }

            uint drawCount = Math.Min(cardCount, count);
            for (int i = 0; i < drawCount; ++i)
            {
                cards.Add(cardType);
                CardTypeToCount[cardType]--;
            }

            return cards;
        }

        private IList<FightCard.Type> RemainCardTypes()
        {
            return (from cardTypeToCount in CardTypeToCount
                    where cardTypeToCount.Value > 0
                    select cardTypeToCount.Key).ToList();
        }
    }
}