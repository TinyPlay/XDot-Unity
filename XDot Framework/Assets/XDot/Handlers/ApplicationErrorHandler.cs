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

namespace XDot.Handlers
{
    /// <summary>
    /// Application Error Handler
    /// </summary>
    public struct ApplicationErrorHandler
    {
        public ErrorType ErrorType;
        public string StackTrace;
        public string Message;
    }

    public enum ErrorType
    {
        Error,
        Exception
    }
}