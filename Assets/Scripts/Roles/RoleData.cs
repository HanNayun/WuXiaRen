using Date;
using UnityEngine;

namespace Roles
{
    [CreateAssetMenu(fileName = "RoleData_", menuName = "Data/RoleData")]
    public class RoleData : ScriptableObject
    {
        [SerializeField] private string _roleName;
        [SerializeField] private Gender _gender;
        [SerializeField] private DateData _birthDate;

        [SerializeField] private uint _healthPoint;
        [SerializeField] private uint _force;
        [SerializeField] private uint _intelligence;
        [SerializeField] private uint _charm;
    }
}