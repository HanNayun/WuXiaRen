using System.Threading;
using System.Threading.Tasks;
using Core;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UIElements;

namespace UI
{
    public class MainMenuView : DocumentView
    {
        [SerializeField] private AssetReference gameStartScene;

        [SerializeField] private bool debugMode;

        private VisualElement newGameBtn;
        private VisualElement loadGameBtn;

        private Clickable onClickNewGame;
        private Clickable onClickLoadGame;

        private void Awake()
        {
            VisualElement root = GetComponent<UIDocument>().rootVisualElement;

            onClickNewGame = new Clickable(StartNewGame);
            onClickLoadGame = new Clickable(Handler);

            newGameBtn = root.Q("BtnNewGame");
            loadGameBtn = root.Q("BtnLoadGame");

            return;

            async void Handler()
            {
                CancellationTokenSource cancellationTokenSource = new();
                try
                {
                    Task<ScriptableObject> task =
                        AddressableAssetLoader.LoadAssetAsync<ScriptableObject>(
                            "Data/Date/DateData_StoryStartDate.asset",
                            cancellationTokenSource.Token
                        );
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
            newGameBtn.AddManipulator(onClickNewGame);
            loadGameBtn.AddManipulator(onClickLoadGame);
        }

        private void OnDisable()
        {
            newGameBtn.RemoveManipulator(onClickNewGame);
            loadGameBtn.RemoveManipulator(onClickLoadGame);
        }

        private void StartNewGame()
        {
            SceneLoader.SwitchScene(debugMode ? AddressableSceneKeys.DebugRoomScene : gameStartScene);
        }
    }
}