using Core;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UIElements;

namespace Views
{
    public class MainMenuView : View
    {
        [SerializeField] private AssetReference _gameStartScene;

        [SerializeField] private bool _debugMode;

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
            SceneLoadProcess sceneLoadProcess = _debugMode
                ? new SceneLoadProcess(AddressableSceneKeys.DebugRoomScene)
                : new SceneLoadProcess(_gameStartScene, true, false);
            
        }
    }
}