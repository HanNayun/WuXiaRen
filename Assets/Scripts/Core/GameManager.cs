using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Core
{
    public class GameManager : MonoBehaviour
    {
        private const string GameManagerAddress = "P_GameManager";

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void InstantiateGameManager()
        {
            Addressables.InstantiateAsync(GameManagerAddress).Completed += objectHandle =>
            {
                DontDestroyOnLoad(objectHandle.Result);
            };
        }
    }
}