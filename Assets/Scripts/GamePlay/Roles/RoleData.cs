using UnityEngine;
using UnityEngine.Serialization;
using GameDateData = GamePlay.GameDate.GameDateData;

namespace Roles
{
    [CreateAssetMenu(fileName = "RoleData_", menuName = "Data/RoleData")]
    public class RoleData : ScriptableObject
    {
        [SerializeField] private string _roleName;
        [SerializeField] private Gender _gender;
        [FormerlySerializedAs("_birthDate")] [SerializeField] private GameDateData birthGameDate;

        [SerializeField] private uint _healthPoint;
        [SerializeField] private uint _force;
        [SerializeField] private uint _intelligence;
        [SerializeField] private uint _charm;
    }
}