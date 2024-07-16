using Core;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UIElements;

namespace Views
{
    public class MainMenuView : MonoBehaviour
    {
        [SerializeField]
        private AssetReference _gameStartScene;

        [SerializeField]
        private bool _debugMode;

        private VisualElement _newGameBtn;

        private Clickable _onClickNewGame;

        private void Awake()
        {
            VisualElement root = GetComponent<UIDocument>().rootVisualElement;

            _onClickNewGame = new Clickable(StartNewGame);
            _newGameBtn = root.Q("BtnNewGame");
        }


        private void OnEnable()
        {
            _newGameBtn.AddManipulator(_onClickNewGame);
        }

        private void OnDisable()
        {
            _newGameBtn.RemoveManipulator(_onClickNewGame);
        }

        private void StartNewGame()
        {
            if (_debugMode)
            {
                SceneLoadManager.LoadScene(SceneKey.DebugRoomScene).Load();
            }
            else
            {
                SceneLoadManager.LoadScene(_gameStartScene).Load();
            }
        }
    }
}