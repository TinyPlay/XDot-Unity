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
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace XDot.DataLoader
{
    /// <summary>
    /// Simple Binary Data Loader
    /// </summary>
    /// <typeparam name="TData"></typeparam>
    public class BinaryDataLoader<TData> : IDataLoader<TData> where TData : class
    {
        private readonly string _basePath;
        private readonly Encoding _baseEncoding;
        
        /// <summary>
        /// Data Loader Constructor
        /// </summary>
        /// <param name="dataPath"></param>
        /// <param name="encoding"></param>
        public BinaryDataLoader(string dataPath = "", Encoding encoding = null)
        {
            _basePath = (!String.IsNullOrEmpty(dataPath)) ? dataPath : "";
            _baseEncoding = encoding ?? Encoding.UTF8;
        }
        
        /// <summary>
        /// Save Data to File
        /// </summary>
        /// <param name="dataToSave"></param>
        public void SaveData(TData dataToSave)
        {
            BinaryFormatter converter = new BinaryFormatter();
            FileStream dataStream = new FileStream(_basePath, FileMode.Create);
            converter.Serialize(dataStream, dataToSave);
            dataStream.Close();
        }

        /// <summary>
        /// Load Data from File
        /// </summary>
        /// <param name="inputObject"></param>
        /// <returns></returns>
        public TData LoadData(TData inputObject = null)
        {
            if (!File.Exists(_basePath))
                return null;

            BinaryFormatter converter = new BinaryFormatter();
            FileStream dataStream = new FileStream(_basePath, FileMode.Open);
            inputObject = converter.Deserialize(dataStream) as TData;
            dataStream.Close();
            return inputObject;
        }
    }
}