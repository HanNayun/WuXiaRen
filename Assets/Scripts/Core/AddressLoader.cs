using System;
using System.Collections.Generic;
using System.Reflection;
using Attributes;
using Unity.VisualScripting;
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

        public static AsyncOperationHandle<TAsset> LoadAsset<TClass, TAsset>(Action onLoadComplete)
        {
            AddressableAttribute addressAttribute = typeof(TClass).GetCustomAttribute<AddressableAttribute>(true);
            if (addressAttribute is null)
            {
                throw new Exception($"The class {nameof(TClass)} do not have AddressableAttribute");
            }

            string address = addressAttribute.Address;
            var op=  Addressables.LoadAssetAsync<TAsset>(address);
        }
    }
}