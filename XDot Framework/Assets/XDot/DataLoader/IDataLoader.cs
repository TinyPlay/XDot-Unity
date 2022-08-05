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
namespace XDot.DataLoader
{
    /// <summary>
    /// Base Data Loader Interface
    /// </summary>
    public interface IDataLoader<TData> where TData : class
    {
        void SaveData(TData dataToSave);
        TData LoadData(TData inputObject = null);
    }
}