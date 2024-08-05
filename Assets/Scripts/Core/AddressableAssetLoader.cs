using System;
using System.Collections;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Core
{
    public class AddressableAssetLoader : MonoBehaviour
    {
        private static AddressableAssetLoader _instance;

        private void Awake()
        {
            _instance = this;
        }

        public static async Task<TAsset> LoadAssetAsync<TAsset>(string address,
                                                                CancellationToken? cancellationToken = null)
        {
            var taskCompletionSource = new TaskCompletionSource<TAsset>();
            cancellationToken?.Register(() => taskCompletionSource.TrySetCanceled());
            _instance.StartCoroutine(LoadAssetCoroutine(address, taskCompletionSource, cancellationToken));

            return await taskCompletionSource.Task;
        }

        private static IEnumerator LoadAssetCoroutine<TAsset>(string address,
                                                              TaskCompletionSource<TAsset> taskCompletionSource,
                                                              CancellationToken? cancellationToken)
        {
            AsyncOperationHandle<TAsset> operation = Addressables.LoadAssetAsync<TAsset>(address);

            while (!operation.IsDone)
            {
                if (cancellationToken?.IsCancellationRequested ?? false)
                {
                    Addressables.Release(operation);
                    taskCompletionSource.TrySetCanceled();
                    yield break;
                }

                yield return null;
            }

            if (operation.Status == AsyncOperationStatus.Succeeded)
            {
                taskCompletionSource.TrySetResult(operation.Result);
            }
            else
            {
                taskCompletionSource.TrySetException(new Exception("Failed to load asset."));
            }
        }
    }
}