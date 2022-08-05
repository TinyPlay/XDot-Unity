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
using UnityEngine;
using XDot.ContentLoader;
using XDot.DataLoader;
using XDot.Handlers;

namespace XDot.Core
{
    /// <summary>
    /// Base Presenter Interface for XDot Framework
    /// </summary>
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
        /// <param name="loader"></param>
        /// <param name="onComplete"></param>
        /// <param name="onError"></param>
        void LoadView(string path, LoaderType loader, Action<GameObject> onComplete, Action<ErrorHandler> onError);

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
        /// <param name="onError"></param>
        /// <typeparam name="TDataType"></typeparam>
        void LoadModel<TDataType>(string path, Action<ErrorHandler> onError) where TDataType : class;

        /// <summary>
        /// Save Model using Data Loader
        /// </summary>
        /// <param name="path"></param>
        /// <typeparam name="TDataType"></typeparam>
        void SaveModel<TDataType>(string path) where TDataType : class;

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