using System;
using UnityEngine;
using XDot.ContentLoader;
using XDot.DataLoader;
using XDot.Handlers;

namespace XDot.Core
{
    public interface IPresenter
    {
        /// <summary>
        /// Get Presenter Context
        /// </summary>
        /// <typeparam name="TContext"></typeparam>
        /// <returns></returns>
        TContext GetContext<TContext>() where TContext : IContext;

        /// <summary>
        /// Load View using Content Loader
        /// </summary>
        /// <param name="path"></param>
        /// <param name="onComplete"></param>
        /// <param name="onError"></param>
        /// <typeparam name="TContentLoader"></typeparam>
        void LoadView<TContentLoader>(string path, Action<GameObject> onComplete, Action<ErrorHandler> onError)
            where TContentLoader : IContentLoader;

        /// <summary>
        /// Set Current Presenter View
        /// </summary>
        /// <param name="view"></param>
        void SetView(IView view);

        /// <summary>
        /// Get Current Presenter View
        /// </summary>
        /// <typeparam name="TView"></typeparam>
        /// <returns></returns>
        TView GetView<TView>() where TView : IView;

        /// <summary>
        /// Load Model using Data Loader
        /// </summary>
        /// <param name="path"></param>
        /// <param name="onComplete"></param>
        /// <param name="onError"></param>
        /// <typeparam name="TDataLoader"></typeparam>
        void LoadModel<TDataLoader>(string path, Action onComplete, Action<ErrorHandler> onError)
            where TDataLoader : IDataLoader;

        /// <summary>
        /// Set Current Presenter Model
        /// </summary>
        /// <param name="model"></param>
        void SetModel(IModel model);

        /// <summary>
        /// Get Current Presenter Model
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <returns></returns>
        TModel GetModel<TModel>() where TModel : IModel;
    }
}