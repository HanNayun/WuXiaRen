using System.Collections.Generic;
using Character;
using UnityEngine;

namespace Team
{
    [CreateAssetMenu(menuName = "Data/TeamData", fileName = "TeamData_")]
    public class TeamData : ScriptableObject
    {
        [SerializeField]
        private List<CharacterData> characterList;

        public List<CharacterData> CharacterList => characterList;
    }
}