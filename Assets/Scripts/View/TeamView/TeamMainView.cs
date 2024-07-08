using UnityEngine;
using UnityEngine.UIElements;

namespace View.TeamView
{
    public class TeamMainView : MonoBehaviour
    {
        [SerializeField]
        private Texture2D roleAvatar;

        [SerializeField]
        private string roleName;


        private VisualElement _rootViewElement;
        private Label _roleNameLabel1;
        private VisualElement _roleAvatarElement1;

        private void Awake()
        {
            _rootViewElement = GetComponent<UIDocument>().rootVisualElement;
            _roleNameLabel1 = _rootViewElement.Q<Label>("Label");
            _roleAvatarElement1 = _rootViewElement.Q<VisualElement>("AvatarImg");

            _roleAvatarElement1.style.backgroundImage = roleAvatar;
            _roleNameLabel1.text = roleName;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _rootViewElement.style.display =
                    _rootViewElement.style.display == DisplayStyle.Flex
                        ? DisplayStyle.None
                        : DisplayStyle.Flex;
            }
        }
    }
}