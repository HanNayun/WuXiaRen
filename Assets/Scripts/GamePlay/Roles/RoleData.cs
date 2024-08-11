using GamePlay.Date;
using Roles;
using UnityEngine;

namespace GamePlay.Roles
{
    [CreateAssetMenu(fileName = "RoleData_", menuName = "Data/RoleData")]
    public class RoleData : ScriptableObject
    {
        [SerializeField]
        private string _roleName;

        [SerializeField]
        private Gender _gender;

        [SerializeField]
        private GameDate _birthGameDate;

        [SerializeField]
        private uint _healthPoint;

        [SerializeField]
        private uint _force;

        [SerializeField]
        private uint _intelligence;

        [SerializeField]
        private uint _charm;
    }
}