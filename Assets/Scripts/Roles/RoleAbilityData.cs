using UnityEngine;

namespace Roles
{
    [CreateAssetMenu(fileName = "RoleAbilityData", menuName = "Data/RoleAbility", order = 0)]
    public class RoleAbilityData : ScriptableObject
    {
        [SerializeField] string _abilityName;
        
    }
}