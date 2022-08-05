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
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;

namespace XDot.Encryption
{
    /// <summary>
    /// RSA Encryption Module
    /// </summary>
    public class RSA
    {
        /// <summary>
        /// Generate Key Pair
        /// </summary>
        /// <param name="keySize"></param>
        /// <returns></returns>
        public static KeyValuePair<string, string> GenrateKeyPair(int keySize){
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(keySize);
            string publicKey = rsa.ToXmlString(false);
            string privateKey = rsa.ToXmlString(true);
            return new KeyValuePair<string, string>(publicKey, privateKey);
        }
        
        /// <summary>
        /// Encrypt
        /// </summary>
        /// <param name="plane"></param>
        /// <param name="publicKey"></param>
        /// <returns></returns>
        public static string Encrypt(string plane, string publicKey)
        {
            byte[] encrypted = Encrypt(Encoding.UTF8.GetBytes(plane), publicKey);
            return Convert.ToBase64String(encrypted);
        }
        
        /// <summary>
        /// Encrypt Binary
        /// </summary>
        /// <param name="src"></param>
        /// <param name="publicKey"></param>
        /// <returns></returns>
        public static byte[] Encrypt(byte[] src, string publicKey)
        {
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
            {
                rsa.FromXmlString(publicKey);
                byte[] encrypted = rsa.Encrypt(src, false);
                return encrypted;
            }
        }
        
        /// <summary>
        /// Decrypt
        /// </summary>
        /// <param name="encrtpted"></param>
        /// <param name="privateKey"></param>
        /// <returns></returns>
        public static string Decrypt(string encrtpted, string privateKey)
        {
            byte[] decripted = Decrypt(Convert.FromBase64String(encrtpted), privateKey);
            return Encoding.UTF8.GetString(decripted);
        }
        
        /// <summary>
        /// Decrypt Binary
        /// </summary>
        /// <param name="src"></param>
        /// <param name="privateKey"></param>
        /// <returns></returns>
        public static byte[] Decrypt(byte[] src, string privateKey)
        {
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
            {
                rsa.FromXmlString(privateKey);
                byte[] decrypted = rsa.Decrypt(src, false);
                return decrypted;
            }
        }
    }
}