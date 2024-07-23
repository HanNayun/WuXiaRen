using System;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Attributes;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Core
{
    public class AddressLoader : MonoBehaviour
    {
        private static AddressLoader s_instance;

        private void Awake()
        {
            s_instance = this;
        }

        public static async Task<TAsset> LoadAssetAsync<TClass, TAsset>([CanBeNull] CancellationTokenSource cts)
        {
            AddressableAttribute addressAttribute = typeof(TClass).GetCustomAttribute<AddressableAttribute>(true);
            if (addressAttribute is null)
            {
                throw new Exception($"The class {nameof(TClass)} do not have AddressableAttribute");
            }

            string address = addressAttribute.Address;
            AsyncOperationHandle<TAsset> loadOperation = Addressables.LoadAssetAsync<TAsset>(address);

            cts?.Token.Register(() => Addressables.Release(loadOperation));

            try
            {
                return await loadOperation.Task;
            }
            catch (TaskCanceledException)
            {
                Addressables.Release(loadOperation);
                throw; // Rethrow the TaskCanceledException to ensure the method exits
            }
            finally
            {
                cts?.Dispose();
            }
        }
    }
}