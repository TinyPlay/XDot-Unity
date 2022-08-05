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
using UnityEngine;

namespace XDot.DataLoader
{
    /// <summary>
    /// Simple JSON Data Loader
    /// </summary>
    /// <typeparam name="TData"></typeparam>
    public class JsonDataLoader<TData> : IDataLoader<TData> where TData : class
    {
        private readonly string _basePath;
        private readonly Encoding _baseEncoding;
        
        /// <summary>
        /// Data Loader Constructor
        /// </summary>
        /// <param name="dataPath"></param>
        /// <param name="encoding"></param>
        public JsonDataLoader(string dataPath = "", Encoding encoding = null)
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
            string jsonConverted = JsonUtility.ToJson(dataToSave);
            File.WriteAllText(_basePath, jsonConverted, _baseEncoding);
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

            string reader = File.ReadAllText(_basePath, _baseEncoding);
            inputObject = JsonUtility.FromJson<TData>(reader);
            return inputObject;
        }
    }
}