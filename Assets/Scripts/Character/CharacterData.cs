using UnityEngine;

namespace Character
{
    [CreateAssetMenu(menuName = "Data/CharacterData", fileName = "CharacterData_")]
    public class CharacterData : ScriptableObject
    {
        [SerializeField]
        private Texture2D roleAvatar;

        [SerializeField]
        private string roleName;

        [SerializeField]
        private uint roleLevel;

        [SerializeField]
        private CharacterStats characterStats;

        public CharacterStats CharacterStats => characterStats;

        public Texture2D RoleAvatar => roleAvatar;

        public string RoleName => roleName;

        public uint RoleLevel => roleLevel;
    }
}