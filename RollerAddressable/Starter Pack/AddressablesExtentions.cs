using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using System.Threading.Tasks;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceLocations;
using System;
using UnityEngine.SceneManagement;

namespace GW_Lib.Utility
{
    public static class AddressablesExtentions
    {
        //Loading Means, things are added to memory.
        //Creating, means things are added to scene.
        #region LoadAssets
        public static void LoadAssetsSequenceAsync<T>(this MonoBehaviour caller, List<string> labels, Action<T> m_onSingleLoaded,Action<List<T>> m_onSingleLabelLoaded,Action<List<T>> onLoaded) where T : UnityEngine.Object
        {
            caller.StartCoroutine(LoadAssetsSequenceAsyncCoro(caller,labels,m_onSingleLoaded,m_onSingleLabelLoaded,onLoaded));
        }
        public static void LoadAssetAsync<T>(this MonoBehaviour caller, string labelOrKey, Action<T> onLoadCompleted) where T : UnityEngine.Object
        {
            caller.StartCoroutine(LoadAssetAsync<T>(labelOrKey, onLoadCompleted));
        }
        public static void LoadAssetsAsync<T>(this MonoBehaviour caller, string labelOrKey, Action<T> m_onSingleLoad, Action<List<T>> onCompletedLoad) where T : UnityEngine.Object
        {
            caller.StartCoroutine(LoadAssetsAsync<T>(labelOrKey, m_onSingleLoad,onCompletedLoad));
        }
        public static IEnumerator LoadAssetAsync<T>(string labelOrKey, Action<T> onLoadCompleted) where T : UnityEngine.Object
        {
            var operation = Addressables.LoadAssetAsync<T>(labelOrKey);
            yield return operation;
            onLoadCompleted?.Invoke(operation.Result);
        }
        public static IEnumerator LoadAssetsAsync<T>(string labelOrKey, Action<T> m_onSingleLoad,Action<List<T>> onCompletedLoad) where T : UnityEngine.Object
        {
            List<T> loadedObjs = new List<T>();
            void onSingleLoad(T loadedObj)
            {
                loadedObjs.Add(loadedObj);
                m_onSingleLoad?.Invoke(loadedObj);
            }
            var operation = Addressables.LoadAssetsAsync<T>(labelOrKey, onSingleLoad);
            yield return operation;
            onCompletedLoad?.Invoke(loadedObjs);
        }
        public static IEnumerator LoadAssetsSequenceAsyncCoro<T>(this MonoBehaviour caller, List<string> labels, Action<T> m_onSingleLoaded,Action<List<T>> m_onSingleLabelLoaded,Action<List<T>> onLoaded) where T : UnityEngine.Object
        {
            List<T> allObjs = new List<T>();

            void OnCompletedLabelLoad(List<T> objs)
            {
                m_onSingleLabelLoaded?.Invoke(objs);
            }
            void OnSingleLoaded(T g)
            {
                allObjs.Add(g);
                m_onSingleLoaded?.Invoke(g);
            }
            for (int i = 0; i < labels.Count; i++)
            {
                string label = labels[i];
                yield return caller.StartCoroutine(LoadAssetsAsync<T>(label,OnSingleLoaded,OnCompletedLabelLoad));
            }

            onLoaded?.Invoke(allObjs);
        }
        #endregion

        #region LoadAssetLocations
        public static void LoadAssetLocationsAsync(this MonoBehaviour caller, string label, Action<List<IResourceLocation>> onLoadCompleted)
        {
            caller.StartCoroutine(LoadAssetLocationsAsync(label, onLoadCompleted));
        }
        public static IEnumerator LoadAssetLocationsAsync(string label, Action<List<IResourceLocation>> onLoadCompleted)
        {
            List<IResourceLocation> loadedLocations = new List<IResourceLocation>();
            AsyncOperationHandle<IList<IResourceLocation>> locations = Addressables.LoadResourceLocationsAsync(label);
            yield return locations;
            if (locations.Status == AsyncOperationStatus.Succeeded)
            {
                foreach (var l in locations.Result)
                {
                    loadedLocations.Add(l);
                }
            }
            onLoadCompleted?.Invoke(loadedLocations);
        }
        #endregion

        #region CreateAsset
        public static void CreateAssetAsync(this MonoBehaviour caller, AssetReference ar, Action<GameObject> onLoadCompleted)
        {
            caller.StartCoroutine(CreateAssetAsync(ar, onLoadCompleted));
        }
        public static void CreateAssetAsync(this MonoBehaviour caller, IResourceLocation location, Action<GameObject> onLoadCompleted)
        {
            caller.StartCoroutine(CreateAssetAsync(location, onLoadCompleted));
        }
        public static void CreateAssetAsync(this MonoBehaviour caller, string nameOrLabel,Action<GameObject> onLoadCompleted)
        {
            caller.StartCoroutine(CreateAssetAsync(nameOrLabel,onLoadCompleted));
        }

        public static IEnumerator CreateAssetAsync(AssetReference ar, Action<GameObject> onLoadCompleted)
        {
            AsyncOperationHandle<GameObject> op = ar.InstantiateAsync();
            yield return op;
            onLoadCompleted?.Invoke(op.Result);
        }
        public static IEnumerator CreateAssetAsync(IResourceLocation ar, Action<GameObject> onLoadCompleted)
        {
            AsyncOperationHandle<GameObject> op = Addressables.InstantiateAsync(ar);
            yield return op;
            onLoadCompleted?.Invoke(op.Result);
        }
        public static IEnumerator CreateAssetAsync(string nameOrLabel,Action<GameObject> onLoadCompleted)
        {
            var operation = Addressables.InstantiateAsync(nameOrLabel);
            yield return operation;
            onLoadCompleted?.Invoke(operation.Result);
        }
        #endregion

        #region CreateAssets
        public static void CreateAssetsSequenceAsync(this MonoBehaviour caller, List<string> labels, Action<GameObject> m_onSingleLoaded,Action<List<GameObject>> m_onSingleLabelLoaded,Action<List<GameObject>> onLoaded)
        {
            caller.StartCoroutine(CreateAssetsSequenceAsyncCoro(caller,labels, m_onSingleLoaded,m_onSingleLabelLoaded, onLoaded));
        }
        public static void CreateAssetsAsync(this MonoBehaviour caller, List<AssetReference> ars, Action<GameObject> m_onSingleLoad,Action<List<GameObject>> onLoadCompleted)
        {
            caller.StartCoroutine(CreateAssetsAsync(ars, m_onSingleLoad,onLoadCompleted));
        }
        public static void CreateAssetsAsync(this MonoBehaviour caller, List<IResourceLocation> locations, Action<GameObject> m_onSingleLoad,Action<List<GameObject>> onLoadCompleted)
        {
            caller.StartCoroutine(CreateAssetsAsync(locations, m_onSingleLoad,onLoadCompleted));
        }
        public static void CreateAssetsAsync(this MonoBehaviour caller, string label,Action<GameObject> m_onSingleLoad,Action<List<GameObject>> onLoadCompleted)
        {
            caller.StartCoroutine(CreateAssetsAsync(label,m_onSingleLoad,onLoadCompleted));
        }
        public static IEnumerator CreateAssetsAsync(List<AssetReference> ars, Action<GameObject> m_onSingleLoad,Action<List<GameObject>> onLoadCompleted)
        {
            List<GameObject> loadedObjs = new List<GameObject>();
            //Get All Operations
            List<AsyncOperationHandle<GameObject>> loadingRefs = new List<AsyncOperationHandle<GameObject>>();
            foreach (var ar in ars)
            {
                AsyncOperationHandle<GameObject> op = ar.InstantiateAsync();
                loadingRefs.Add(op);
            }

            //Keep Checking All Operations Every Frame
            while (loadingRefs.Count > 0)
            {
                yield return 0;
                List<AsyncOperationHandle<GameObject>> toBeRemoved = new List<AsyncOperationHandle<GameObject>>();

                foreach (var op in loadingRefs)
                {
                    if (op.Status == AsyncOperationStatus.None)
                        continue;

                    //If operation is completed, we assign it to be removed
                    toBeRemoved.Add(op);
                    //if we succesfully found one, we add it to loaded
                    if (op.Status == AsyncOperationStatus.Succeeded)
                    {
                        loadedObjs.Add(op.Result);
                        m_onSingleLoad?.Invoke(op.Result);
                    }
                }
                //We Remove all completed tasks
                loadingRefs.RemoveAll(l => toBeRemoved.Contains(l));
            }

            onLoadCompleted?.Invoke(loadedObjs);
        }
        public static IEnumerator CreateAssetsAsync(IList<IResourceLocation> locations, Action<GameObject> m_onSingleLoad,Action<List<GameObject>> onLoadCompleted)
        {
            List<GameObject> loadedObjs = new List<GameObject>();
            List<AsyncOperationHandle<GameObject>> operations = new List<AsyncOperationHandle<GameObject>>();
            foreach (var l in locations)
            {
                AsyncOperationHandle<GameObject> op = Addressables.InstantiateAsync(l);
                operations.Add(op);
            }
            List<AsyncOperationHandle<GameObject>> toBeRemoved = new List<AsyncOperationHandle<GameObject>>();

            while (operations.Count > 0)
            {
                yield return 0;
                foreach (var op in operations)
                {
                    if (op.Status == AsyncOperationStatus.None)
                        continue;

                    toBeRemoved.Add(op);
                    if (op.Status == AsyncOperationStatus.Succeeded)
                    {
                        loadedObjs.Add(op.Result);
                        m_onSingleLoad?.Invoke(op.Result);
                    }
                }
                operations.RemoveAll(o => toBeRemoved.Contains(o));
            }
            onLoadCompleted?.Invoke(loadedObjs);
        }
        public static IEnumerator CreateAssetsAsync(string label,Action<GameObject> m_onSingleLoad,Action<List<GameObject>> onLoadCompleted)
        {
            List<GameObject> prefabs = new List<GameObject>();
            List<GameObject> loadedObjs = new List<GameObject>();
            void OnSingleLoad(GameObject newLoad)
            {
                prefabs.Add(newLoad);
                GameObject cloned = UnityEngine.Object.Instantiate(newLoad);
                loadedObjs.Add(cloned);
                m_onSingleLoad?.Invoke(cloned);
            }
            
            var operation = Addressables.LoadAssetsAsync<GameObject>(label,OnSingleLoad);
            yield return operation;
            
            onLoadCompleted?.Invoke(loadedObjs);
        }
        public static IEnumerator CreateAssetsSequenceAsyncCoro(this MonoBehaviour caller, List<string> labels, Action<GameObject> m_onSingleLoaded,Action<List<GameObject>> m_onSingleLabelLoaded,Action<List<GameObject>> onLoaded)
        {
            List<GameObject> allObjs = new List<GameObject>();

            void OnCompletedLabelLoad(List<GameObject> objs)
            {
                m_onSingleLabelLoaded?.Invoke(objs);
            }
            void OnSingleLoaded(GameObject g)
            {
                allObjs.Add(g);
                m_onSingleLoaded?.Invoke(g);
            }
            for (int i = 0; i < labels.Count; i++)
            {
                string label = labels[i];
                yield return caller.StartCoroutine(CreateAssetsAsync(label,OnSingleLoaded,OnCompletedLabelLoad));
            }

            onLoaded?.Invoke(allObjs);
        }
        #endregion

        #region CreateScene
        public static void CreateSceneAsync(this MonoBehaviour caller, object key, LoadSceneMode mode, bool autoActivate, Action<string> onLoadCompleted)
        {
            caller.StartCoroutine(CreateSceneAsync(key, mode, autoActivate, onLoadCompleted));
        }
        //key is, an IResourceLocation, or a "key" in the addressables windows
        public static IEnumerator CreateSceneAsync(object key, LoadSceneMode mode, bool autoActivate, Action<string> onLoadCompleted)
        {
            yield return 0;
            var a = Addressables.LoadSceneAsync(key, mode, autoActivate);
            yield return a;
            string sceneName = a.Result.Scene.name;
            onLoadCompleted?.Invoke(sceneName);
        }
        #endregion
    }
}