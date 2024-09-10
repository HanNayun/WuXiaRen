using System;
using System.Collections;
using System.Collections.Generic;
using GamePlay.Roles;
using GamePlay.Roles.Bodys;

namespace GamePlay.Fight.FightRoles
{

    public class FightRole : Role
    {
        public IList<FightCard> Postures { get; private set; }
        public IList<FightCard> HandCards { get; private set; }
        public IList<FightCard> DrawPile { get; private set; }
        public IList<FightCard> DiscardPile { get; private set; }
        public uint DefenseCount { get; private set; }
    }
}