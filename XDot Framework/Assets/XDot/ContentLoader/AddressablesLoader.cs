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
#if UNITASK_ADDRESSABLE_SUPPORT

using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using XDot.Handlers;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

namespace XDot.ContentLoader
{
    /// <summary>
    /// Addressables Loader
    /// </summary>
    public class AddressablesLoader<TContent> : IContentLoader<TContent> where TContent : class
    {
        private Action<ErrorHandler> _onError;
        private CancellationTokenSource _cancellation = new CancellationTokenSource();

        public AddressablesLoader()
        {
            
        }

        /// <summary>
        /// Load Object from Addressables
        /// </summary>
        /// <param name="path"></param>
        /// <param name="onComplete"></param>
        /// <param name="onError"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public async UniTask<TContent> Load(string path, Action<TContent> onComplete = null,
            Action<ErrorHandler> onError = null)
        {
            _cancellation = new CancellationTokenSource();
            _cancellation.CancelAfterSlim(TimeSpan.FromSeconds(30));
            
            AsyncOperationHandle<TContent> asyncHandler = Addressables.LoadAssetAsync<TContent>(addressablePath);
            asyncHandler.GetAwaiter();
            await asyncHandler;
            
            while (!asyncHandler.IsDone)
            {
                await UniTask.Yield();
            }

            if (asyncHandler.Status == AsyncOperationStatus.Succeeded)
            {
                onComplete?.Invoke(asyncHandler.Result);
            }
            else
            {
                onError?.Invoke(new ErrorHandler
                {
                    IsCritical = false,
                    Message = $"Failed to load addressable {path} content"
                });
            }
            
            return asyncHandler.Result ?? throw new InvalidOperationException($"Failed to load content {path} from addressables");
        }
        
        /// <summary>
        /// Cancel Content Loading
        /// </summary>
        public void Cancel()
        {
            _cancellation?.Cancel();
            _cancellation?.Dispose();
        }
        
        /// <summary>
        /// Dispose Content Loading
        /// </summary>
        public void Dispose()
        {
            Cancel();
        }
    }
}
#endif