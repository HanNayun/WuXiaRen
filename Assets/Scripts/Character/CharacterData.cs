using UnityEngine;

namespace Character
{
    [CreateAssetMenu(menuName = "Data/CharacterData", fileName = "CharacterData_")]
    public class CharacterData : ScriptableObject
    {
        public const int RoleMaxLevel = 10;
        public const int RoleMinLevel = 1;

        [SerializeField]
        private Texture2D roleAvatar;

        [SerializeField]
        private string roleName;

        [SerializeField, Range(RoleMinLevel, RoleMaxLevel)]
        private uint roleStartLevel = 1;

        [SerializeField]
        private CharacterStats characterStats;

        private int _roleLevel;

        public int RoleLevel
        {
            get => _roleLevel;
            set
            {
                if (_roleLevel == value || value is > RoleMaxLevel or < RoleMinLevel) return;
                _roleLevel = value;
            }
        }

        public CharacterStats CharacterStats => characterStats;

        public Texture2D RoleAvatar => roleAvatar;

        public string RoleName => roleName;

        public uint RoleStartLevel => roleStartLevel;
    }
}