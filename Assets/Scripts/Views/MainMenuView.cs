using Core;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UIElements;

namespace Views
{
    public class MainMenuView : DocumentView
    {
        [SerializeField] private AssetReference _gameStartScene;

        [SerializeField] private bool _debugMode;

        private VisualElement _newGameBtn;
        private VisualElement _loadGameBtn;

        private Clickable _onClickNewGame;
        private Clickable _onClickLoadGame;

        private void Awake()
        {
            VisualElement root = GetComponent<UIDocument>().rootVisualElement;

            _onClickNewGame = new Clickable(StartNewGame);
            _onClickLoadGame = new Clickable(() =>
            {
            });
            
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
            SceneLoader.SwitchScene(_debugMode ? AddressableSceneKeys.DebugRoomScene : _gameStartScene);
        }
    }
}