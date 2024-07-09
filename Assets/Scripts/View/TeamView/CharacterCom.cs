using System.Collections.Generic;
using Character;
using UnityEngine;
using UnityEngine.UIElements;

namespace View.TeamView
{
    public class CharacterCom : VisualElement
    {
        private readonly IList<VisualElement> _stateDivs = new List<VisualElement>();
        private readonly TemplateContainer _templateContainer;


        public CharacterCom()
        {
            _templateContainer = Resources.Load<VisualTreeAsset>("RoleDataCom").Instantiate();
            _templateContainer.style.flexGrow = 1.0f;
            hierarchy.Add(_templateContainer);
        }

        public CharacterCom(CharacterData characterData) : this()
        {
            userData = characterData;
            _templateContainer.Q("AvatarImg").style.backgroundImage = characterData.RoleAvatar;
            _templateContainer.Q<Label>("NameLabel").text = characterData.RoleName;
            _stateDivs = _templateContainer.Query("StateDiv").ToList();

            UpdateStats();
        }

        private void UpdateStats()
        {
            var characterData = (CharacterData)userData;

            SetState(0, "Level", characterData.RoleStartLevel.ToString());
            SetState(1, "Initiative", characterData.CharacterStats.initiative.ToString());
            SetState(2, "HP", characterData.CharacterStats.maxHealth.ToString());
            SetState(3, "MP", characterData.CharacterStats.maxMagic.ToString());
            SetState(4, "Attack", characterData.CharacterStats.attack.ToString());
            SetState(5, "Defense", characterData.CharacterStats.defense.ToString());

            void SetState(int idx, string stateName, string stateValue)
            {
                _stateDivs[idx].Q<Label>("StateNameP").text = stateName;
                _stateDivs[idx].Q<Label>("StateValueP").text = stateValue;
            }
        }


        public new class UxmlFactory : UxmlFactory<CharacterCom>
        {
        }
    }
}