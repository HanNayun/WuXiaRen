using System.Collections.Generic;
using GamePlay.Roles.Bodys;
using UnityEngine;

namespace GamePlay.Roles
{
    [CreateAssetMenu(fileName = "RoleData_", menuName = "Data/RoleData")]
    public class RoleData : ScriptableObject
    {
        [SerializeField] private string _name;
        [SerializeField] private Gender _gender;
        [SerializeField] private uint _age;
        [SerializeField] private string _avatar;
        [SerializeField] private string _portraits;

        [SerializeField] private uint _defaultDefensePointCount;
        [SerializeField] private uint _defaultPerRountRecoverDefensePointCount;
        [SerializeField] private uint _defaultPerRoundDrawCardCount;
        [SerializeField] private uint _maxHandCardCount;
        [SerializeField] private Body _body;
        [SerializeField] private IDictionary<uint, uint> _itemIdToCount;

        public string Name => _name;

        public Gender Gender => _gender;

        public uint Age => _age;

        public string Avatar => _avatar;

        public string Portraits => _portraits;
    }
}