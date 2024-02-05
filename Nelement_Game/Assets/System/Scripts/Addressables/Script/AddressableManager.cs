using System;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace ProyectNelement.System.AddressablesHandeling
{
    public static class AddressableManager
    {
        #region PRIVATE_FIELDS
        private static Dictionary<string, AsyncOperationHandle> operations = null;
        #endregion

        #region PUBLIC_METHODS
        public static void LoadAssetsByLabel<T>(string label, Action<List<T>> onSuccess, Action onFailure)
        {
            AsyncOperationHandle<IList<T>> loadOperation = Addressables.LoadAssetsAsync<T>(label, obj =>{});
            loadOperation.Completed +=
                (opResult) =>
                {
                    if (opResult.Result != null && opResult.Result.Count > 0)
                    {
                        List<T> result = new List<T>();
                        result.AddRange(opResult.Result);
                        onSuccess.Invoke(result);
                    }
                    else
                    {
                        onFailure.Invoke();
                        Debug.LogError("Fail to load Assets by label.");
                    }
                };
            
            RegisterOperationHandle(label, loadOperation);
        }

        public static void LoadAsset<T>(object assetRef, string loadKey, Action<T> onSuccess, Action onFailure)
        {
            AsyncOperationHandle<T> loadOperation = Addressables.LoadAssetAsync<T>(assetRef);
            loadOperation.Completed += 
                (handle)=>
                {
                    if (handle.Result != null)
                    {
                        onSuccess.Invoke(handle.Result);
                    }
                    else
                    {
                        onFailure.Invoke();
                        Debug.LogError("Fail to load Asset by AssetReference.");
                    }
                };

            RegisterOperationHandle(loadKey, loadOperation);
        }

        public static void ReleaseOperationByKey(string operationKey)
        {
            if (operations.TryGetValue(operationKey, out AsyncOperationHandle handle))
            {
                Addressables.Release(handle);
                Debug.Log("Operation Handle released Successfully.");
            }
            else
            {
                Debug.LogError("Fail to release operation handle with key: " + operationKey + ". The dictionary of handles doesn't have that Key registered.");
            }
        }
        #endregion

        #region PRIVATE_METHODS
        private static void RegisterOperationHandle(string key, AsyncOperationHandle op)
        {
            if (operations == null)
            {
                operations = new Dictionary<string, AsyncOperationHandle>();
            }
            
            operations.Add(key, op);
        }
        #endregion
    }
}