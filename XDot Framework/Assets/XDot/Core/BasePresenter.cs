using System;
using UnityEngine;
using XDot.ContentLoader;
using XDot.DataLoader;
using XDot.Handlers;

namespace XDot.Core
{
    internal class BasePresenter : IPresenter
    {
        // Presenter Context (Parameters)
        private IContext _context = null;
        
        // Presenter Components (View / Model)
        private IView _currentView = null;
        private IModel _currentModel = null;
        
        /// <summary>
        /// Base Presenter Constructor
        /// </summary>
        /// <param name="context"></param>
        public BasePresenter(IContext context = null)
        {
            _context = context;
        }

        /// <summary>
        /// Get Context of Presenter
        /// </summary>
        /// <typeparam name="TContext"></typeparam>
        /// <returns></returns>
        public TContext GetContext<TContext>() where TContext : IContext
        {
            return (TContext) _context;
        }

        /// <summary>
        /// Load View using Content Loader
        /// </summary>
        /// <param name="path"></param>
        /// <param name="onComplete"></param>
        /// <param name="onError"></param>
        /// <typeparam name="TContentLoader"></typeparam>
        public void LoadView<TContentLoader>(string path, Action<GameObject> onComplete, Action<ErrorHandler> onError) where TContentLoader : IContentLoader
        {
            
        }

        /// <summary>
        /// Set View
        /// </summary>
        /// <param name="view"></param>
        public void SetView(IView view)
        {
            _currentView = view;
        }

        /// <summary>
        /// Get View
        /// </summary>
        /// <returns></returns>
        public TView GetView<TView>() where TView : IView
        {
            return (TView)_currentView;
        }

        /// <summary>
        /// Load Current Presenter Model using DataLoader
        /// </summary>
        /// <param name="path"></param>
        /// <param name="onComplete"></param>
        /// <param name="onError"></param>
        /// <typeparam name="TDataLoader"></typeparam>
        public void LoadModel<TDataLoader>(string path, Action onComplete, Action<ErrorHandler> onError) where TDataLoader : IDataLoader
        {
            
        }

        /// <summary>
        /// Set Current Presenter Model
        /// </summary>
        /// <param name="model"></param>
        public void SetModel(IModel model)
        {
            _currentModel = model;
        }

        /// <summary>
        /// Get Current Presenter Model
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <returns></returns>
        public TModel GetModel<TModel>() where TModel : IModel
        {
            return (TModel) _currentModel;
        }
    }
}