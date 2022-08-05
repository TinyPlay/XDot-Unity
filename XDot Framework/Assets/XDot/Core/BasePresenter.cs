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
    /// Base Presenter for XDot Framework
    /// </summary>
    internal class BasePresenter : IPresenter
    {
        // Presenter Context (Parameters)
        private IContext _context = null;
        
        // Presenter Components (View / Model)
        private IView _currentView = null;
        private IModel _currentModel = null;
        
        // Content Loader
        private const string _loaderPath = "Loaders/ObjectLoader";
        private GameObject _contentLoader = null;
        
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
        /// Show View Preloader
        /// </summary>
        /// <param name="preloaderPosition"></param>
        public void ShowViewLoader(Vector3 preloaderPosition = new Vector3())
        {
            if (_contentLoader != null)
                return;
            
            _contentLoader = Resources.Load<GameObject>(_loaderPath);
            _contentLoader = GameObject.Instantiate(_contentLoader);
            _contentLoader.transform.SetPositionAndRotation(preloaderPosition, _contentLoader.transform.rotation);
        }
        
        /// <summary>
        /// Destroy View Preloader
        /// </summary>
        private void DestroyViewPreloader()
        {
            if (_contentLoader != null)
            {
                GameObject.DestroyImmediate(_contentLoader);
                _contentLoader = null;
            }
        }

        /// <summary>
        /// Load View using Content Loader
        /// </summary>
        /// <param name="path"></param>
        /// <param name="loader"></param>
        /// <param name="onComplete"></param>
        /// <param name="onError"></param>
        public void LoadView(string path, LoaderType loader, Action<GameObject> onComplete, Action<ErrorHandler> onError)
        {
            // Detect Content Loader
            IContentLoader<GameObject> viewLoader;
            if (loader == LoaderType.Resources)
                viewLoader = new ResourcesLoader<GameObject>();
            else if (loader == LoaderType.Addressable)
            {
                #if UNITASK_ADDRESSABLE_SUPPORT
                viewLoader = new AddressablesLoader<GameObject>();
                #else
                throw new Exception($"Failed to initialize Addressables system to load view {path}");
                #endif
            }
            else
                throw new Exception($"Unknown loader for view {path}");
            
            // Load View
            viewLoader?.Load(path, prefab =>
            {
                DestroyViewPreloader();
                onComplete?.Invoke(prefab);
            }, error =>
            {
                DestroyViewPreloader();
                onError?.Invoke(error);
            });
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
        /// <typeparam name="TDataType"></typeparam>
        public void LoadModel<TDataType>(string path, Action<ErrorHandler> onError) where TDataType : class
        {
            EncryptedDataLoader<TDataType> dataLoader = new EncryptedDataLoader<TDataType>(path);
            TDataType model = dataLoader.LoadData((TDataType)_currentModel);
            if (model == null)
                onError?.Invoke(new ErrorHandler
                {
                    IsCritical = false,
                    Message = $"Failed to Load Model {typeof(TDataType)} from {path}"
                });
            else
                _currentModel = (IModel)model;
        }

        /// <summary>
        /// Save Current Presenter Model using DataLoader
        /// </summary>
        /// <param name="path"></param>
        /// <typeparam name="TDataType"></typeparam>
        public void SaveModel<TDataType>(string path) where TDataType : class
        {
            EncryptedDataLoader<TDataType> dataLoader = new EncryptedDataLoader<TDataType>(path);
            dataLoader.SaveData((TDataType)_currentModel);
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