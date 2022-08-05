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

using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using XDot.Handlers;
using UnityEngine;

namespace XDot.ContentLoader
{
    /// <summary>
    /// Resources Loader
    /// </summary>
    public class ResourcesLoader<TContent> : IContentLoader<TContent> where TContent : class
    {
        private Action<ErrorHandler> _onError;
        private CancellationTokenSource _cancellation = new CancellationTokenSource();

        public ResourcesLoader() { }

        /// <summary>
        /// Load Object from Resources
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
            UnityEngine.Object asset = await Resources.LoadAsync(path).WithCancellation(_cancellation.Token);
            
            if (asset is TContent content)
            {
                onComplete?.Invoke(content);
            }
            else
            {
                onError?.Invoke(new ErrorHandler
                {
                    IsCritical = false,
                    Message = $"Failed to load resource {path}"
                });
            }


            return (asset as TContent) ?? throw new InvalidOperationException($"Failed to load resource {path}");
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