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
using System.Xml.Serialization;
using UnityEngine;

namespace XDot.DataLoader
{
    /// <summary>
    /// Simple XML Data Loader
    /// </summary>
    /// <typeparam name="TData"></typeparam>
    public class XmlDataLoader<TData> : IDataLoader<TData> where TData : class
    {
        private readonly string _basePath;
        private readonly Encoding _baseEncoding;
        
        /// <summary>
        /// Data Loader Constructor
        /// </summary>
        /// <param name="dataPath"></param>
        /// <param name="encoding"></param>
        public XmlDataLoader(string dataPath = "", Encoding encoding = null)
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
            XmlSerializer serializer = new XmlSerializer(typeof(TData));
            using (TextWriter writer = new StreamWriter(_basePath, false, _baseEncoding))
            {
                serializer.Serialize(writer, dataToSave);
            }
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
            
            XmlSerializer deserializer = new XmlSerializer(typeof(TData));
            using (TextReader reader = new StreamReader(_basePath, _baseEncoding))
            {
                inputObject = (TData) deserializer.Deserialize(reader);
            }
            
            return inputObject;
        }
    }
}