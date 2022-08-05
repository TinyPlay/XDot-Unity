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
using XDot.Encryption;

namespace XDot.DataLoader
{
    /// <summary>
    /// Encrypted Data Loader
    /// </summary>
    public class EncryptedDataLoader<TData> : IDataLoader<TData> where TData : class
    {
        private readonly string _basePath;
        private readonly Encryption _baseEncryption;
        private string _encryptionKey;
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataPath"></param>
        /// <param name="encryption"></param>
        /// <param name="encryptionKey"></param>
        public EncryptedDataLoader(string dataPath = "", Encryption encryption = Encryption.AES, string encryptionKey = "")
        {
            _basePath = (!String.IsNullOrEmpty(dataPath)) ? dataPath : "";
            _encryptionKey = (encryptionKey.Length>0)?encryptionKey:UnityEngine.Random.Range(0, 9999999).ToString();
            _baseEncryption = encryption;
        }
        
        /// <summary>
        /// Save Encoded Data to File
        /// </summary>
        /// <param name="dataToSave"></param>
        public void SaveData(TData dataToSave)
        {
            string convertedData = JsonUtility.ToJson(dataToSave);
            
            if (_baseEncryption == Encryption.AES)
                convertedData = AES.Encrypt(convertedData, _encryptionKey);
            if (_baseEncryption == Encryption.RSA)
                convertedData = RSA.Encrypt(convertedData, _encryptionKey);
            if (_baseEncryption == Encryption.BASE64)
                convertedData = Base64.Encode(convertedData);

            File.WriteAllText(_basePath, convertedData);
        }
        
        /// <summary>
        /// Load Encoded data from file to object
        /// </summary>
        /// <param name="inputObject"></param>
        /// <returns></returns>
        public TData LoadData(TData inputObject = null)
        {
            if (!File.Exists(_basePath))
                return null;

            string reader = File.ReadAllText(_basePath);
            
            if (_baseEncryption == Encryption.AES)
                reader = AES.Decrypt(reader, _encryptionKey);
            if (_baseEncryption == Encryption.RSA)
                reader = RSA.Decrypt(reader, _encryptionKey);
            if (_baseEncryption == Encryption.BASE64)
                reader = Base64.Decode(reader);
            
            inputObject = JsonUtility.FromJson<TData>(reader);
            return inputObject;
        }
    }

    /// <summary>
    /// Encryption Type
    /// </summary>
    public enum Encryption
    {
        AES,
        RSA,
        BASE64
    }
}