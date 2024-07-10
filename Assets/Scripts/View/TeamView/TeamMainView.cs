using System.Linq;
using Character;
using Team;
using UnityEngine;
using UnityEngine.UIElements;

namespace View.TeamView
{
    public class TeamMainView : MonoBehaviour
    {
        #region Serialized Fields

        [SerializeField] private TeamData teamData;

        #endregion

        private VisualElement _rootViewElement;

        #region Event Functions

        private void Awake()
        {
            _rootViewElement = GetComponent<UIDocument>().rootVisualElement;

            VisualElement bodyDiv = _rootViewElement.Q<VisualElement>("BodyDiv");
            bodyDiv.Clear();

            foreach (CharacterCom characterCom in teamData.CharacterList.Select(CreateCharacterCom))
            {
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

        #endregion

        private static CharacterCom CreateCharacterCom(CharacterData data)
        {
            return new CharacterCom(data)
            {
                style =
                {
                    flexBasis = Length.Percent(25.0f),
                    flexGrow = 1
                }
            };
        }
    }
}