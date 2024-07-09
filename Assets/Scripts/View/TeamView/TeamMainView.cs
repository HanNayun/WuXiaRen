using Character;
using Team;
using UnityEngine;
using UnityEngine.UIElements;

namespace View.TeamView
{
    public class TeamMainView : MonoBehaviour
    {
        [SerializeField]
        private TeamData teamData;


        private VisualElement _rootViewElement;

        private void Awake()
        {
            _rootViewElement = GetComponent<UIDocument>().rootVisualElement;

            var bodyDiv = _rootViewElement.Q<VisualElement>("BodyDiv");
            bodyDiv.Clear();

            foreach (CharacterData characterData in teamData.CharacterList)
            {
                var characterCom = new CharacterCom(characterData)
                {
                    style =
                    {
                        flexBasis = Length.Percent(25.0f),
                        flexGrow = 1
                    }
                };
                bodyDiv.Add(characterCom);
            }
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