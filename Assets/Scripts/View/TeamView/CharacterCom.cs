using UnityEngine;
using UnityEngine.UIElements;

namespace View.TeamView
{
    public class CharacterCom : VisualElement
    {
        private readonly TemplateContainer _templateContainer;
        private Label _roleNameLabel;

        public CharacterCom()
        {
            _templateContainer = Resources.Load<VisualTreeAsset>("RoleDataCom").Instantiate();
            _templateContainer.style.flexGrow = 1.0f;
            hierarchy.Add(_templateContainer);
        }

        public new class UxmlFactory : UxmlFactory<CharacterCom>
        {
        }
    }
}