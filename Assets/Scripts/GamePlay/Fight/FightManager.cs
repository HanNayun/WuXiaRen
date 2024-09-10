using System.Collections;
using System.Collections.Generic;
using GamePlay.Fight.FightRoles;

namespace GamePlay.Fight
{
    public class FightManager
    {
        IList<FightRole> enemies;
        FightRole player;
        public uint TurnCount { get; private set; }

        
    }
}