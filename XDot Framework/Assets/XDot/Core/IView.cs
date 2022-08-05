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
    /// Base View Interface for Installer
    /// </summary>
    public interface IView
    {
        IView SetContext(IContext context);
        TContext GetContext<TContext>() where TContext : IContext;
        IView SetAsGlobalView();
        Transform GetViewTransform();
    }
}