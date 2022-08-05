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

namespace XDot.Core
{
    /// <summary>
    /// Base View for XDot Framework
    /// Must be Initialized by Presenter
    /// </summary>
    internal class BaseView : MonoBehaviour, IView
    {
        private IContext _context;
        private bool _isEnabled = false;
        
        /// <summary>
        /// Set Context
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public virtual IView SetContext(IContext context)
        {
            _context = context;
            return this;
        }
        
        /// <summary>
        /// Get Context
        /// </summary>
        /// <typeparam name="TContext"></typeparam>
        /// <returns></returns>
        public TContext GetContext<TContext>() where TContext : IContext
        {
            return (TContext) _context;
        }
        
        /// <summary>
        /// Set as Global View
        /// </summary>
        /// <returns></returns>
        public IView SetAsGlobalView()
        {
            DontDestroyOnLoad(this);
            return this;
        }
        
        /// <summary>
        /// Set View Position
        /// </summary>
        /// <param name="position"></param>
        private void SetPosition(Vector3 position)
        {
            transform.position = position;
        }
        
        /// <summary>
        /// Set View Rotation
        /// </summary>
        /// <param name="rotation"></param>
        private void SetRotation(Quaternion rotation)
        {
            transform.rotation = rotation;
        }
        
        /// <summary>
        /// Set View Scale
        /// </summary>
        /// <param name="scale"></param>
        private void SetScale(Vector3 scale)
        {
            transform.localScale = scale;
        }

        /// <summary>
        /// Get View Transform
        /// </summary>
        /// <returns></returns>
        public Transform GetViewTransform()
        {
            return transform;
        }
    }
}