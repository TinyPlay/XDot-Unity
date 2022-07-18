using XDot.Core;

namespace Demo.Camera
{
    internal class CameraPresenter : BasePresenter
    {
        public struct Context : IContext
        {
            public string MyMagic;
        }
        
        public CameraPresenter(Context context) : base(context)
        {
            
        }
    }
}