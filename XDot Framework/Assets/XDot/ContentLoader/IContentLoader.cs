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
using Cysharp.Threading.Tasks;
using XDot.Handlers;

namespace XDot.ContentLoader
{
    /// <summary>
    /// Base Content Loader
    /// </summary>
    public interface IContentLoader<TContent> where TContent : class
    {
        /// <summary>
        /// Load Content
        /// </summary>
        /// <param name="path"></param>
        /// <param name="onComplete"></param>
        /// <param name="onError"></param>
        /// <returns></returns>
        UniTask<TContent> Load(string path, Action<TContent> onComplete = null,
            Action<ErrorHandler> onError = null);

        /// <summary>
        /// Cancel Content Loading
        /// </summary>
        void Cancel();

        /// <summary>
        /// Dispose Content Loader
        /// </summary>
        void Dispose();
    }
}