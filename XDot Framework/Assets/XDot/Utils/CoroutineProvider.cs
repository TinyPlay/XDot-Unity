//=====================================================
//  XDot Framework for Unity
//  
//  Developed by Ilya Rastorguev (Pixel Incubator)
//  Provided MIT license
//  
//  @version        1.0.0
//  @url            https://github.com/TinyPlay/XDot-Unity
//  @website        https://pixinc.club/
//=====================================================
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace XDot.Utils
{
    /// <summary>
    /// Base Coroutine Provider
    /// for Non-Mono Classes
    /// </summary>
    public class CoroutineProvider : MonoBehaviour
    {
        private static CoroutineProvider _singleton;
        private static readonly Dictionary<string, IEnumerator> Routines = new Dictionary<string, IEnumerator>(100);

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void InitializeType()
        {
            _singleton = new GameObject($"#{nameof(CoroutineProvider)}").AddComponent<CoroutineProvider>();
            DontDestroyOnLoad(_singleton);
        }

        public static Coroutine Start(IEnumerator routine) => _singleton.StartCoroutine(routine);
        public static Coroutine Start(IEnumerator routine, string id)
        {
            Coroutine coroutine = _singleton.StartCoroutine(routine);
            if (!Routines.ContainsKey(id))
            {
                Routines.Add(id, routine);
            }
            else
            {
                _singleton.StopCoroutine(Routines[id]);
                Routines[id] = routine;
            }
            return coroutine;
        }
        public static void Stop(IEnumerator routine) => _singleton.StopCoroutine(routine);
        public static void Stop(string id)
        {
            if (Routines.TryGetValue(id, out IEnumerator routine))
            {
                _singleton.StopCoroutine(routine);
                Routines.Remove(id);
            }
            else
            {
                Debug.LogWarning($"coroutine '{id}' not found");
            }
        }
        public static void StopAll() => _singleton.StopAllCoroutines();
    }
}