using System.Threading;
using System.Threading.Tasks;
using Core;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UIElements;

namespace Views
{
    public class MainMenuView : DocumentView
    {
        [SerializeField] private AssetReference gameStartScene;

        [SerializeField] private bool debugMode;

        private VisualElement _newGameBtn;
        private VisualElement _loadGameBtn;

        private Clickable _onClickNewGame;
        private Clickable _onClickLoadGame;

        private void Awake()
        {
            VisualElement root = GetComponent<UIDocument>().rootVisualElement;

            _onClickNewGame = new Clickable(StartNewGame);
            _onClickLoadGame = new Clickable(Handler);

            _newGameBtn = root.Q("BtnNewGame");
            _loadGameBtn = root.Q("BtnLoadGame");
            
            return;
            
            async void Handler()
            {
                CancellationTokenSource cancellationTokenSource = new();
                try
                {
                    Task<ScriptableObject> task =
                        AddressableAssetLoader.LoadAssetAsync<ScriptableObject>(
                            "Data/Date/DateData_StoryStartDate.asset", cancellationTokenSource.Token);
                    await Task.WhenAll(task, Task.Run(() => cancellationTokenSource.Cancel()));
                }
                catch (TaskCanceledException)
                {
                    Debug.LogError("Error loading asset");
                }
                finally
                {
                    cancellationTokenSource.Dispose();
                }
            }
        }


        private void OnEnable()
        {
            _newGameBtn.AddManipulator(_onClickNewGame);
            _loadGameBtn.AddManipulator(_onClickLoadGame);
        }

        private void OnDisable()
        {
            _newGameBtn.RemoveManipulator(_onClickNewGame);
            _loadGameBtn.RemoveManipulator(_onClickLoadGame);
        }

        private void StartNewGame()
        {
            SceneLoader.SwitchScene(debugMode ? AddressableSceneKeys.DebugRoomScene : gameStartScene);
        }
    }
}