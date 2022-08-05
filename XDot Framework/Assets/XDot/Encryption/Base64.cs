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
using System.Security.Cryptography;

namespace XDot.Encryption
{
    /// <summary>
    /// Base64 Encryption Module
    /// </summary>
    public class Base64
    {
        /// <summary>
        /// Encode
        /// </summary>
        /// <param name="decodedText"></param>
        /// <returns></returns>
        public static string Encode(string decodedText)
        {
            byte[] bytesToEncode = Encoding.UTF8.GetBytes (decodedText);
            string encodedText = Convert.ToBase64String (bytesToEncode);
            return encodedText;
        }
        
        /// <summary>
        /// Encode Binary
        /// </summary>
        /// <param name="decodedBytes"></param>
        /// <returns></returns>
        public static string EncodeBinary(byte[] decodedBytes)
        {
            string encodedText = Convert.ToBase64String(decodedBytes);
            return encodedText;
        }
        
        /// <summary>
        /// Decode
        /// </summary>
        /// <param name="encodedText"></param>
        /// <returns></returns>
        public static string Decode(string encodedText)
        {
            byte[] decodedBytes = Convert.FromBase64String (encodedText);
            string decodedText = Encoding.UTF8.GetString (decodedBytes);
            return decodedText;
        }
        
        /// <summary>
        /// Decode Binary
        /// </summary>
        /// <param name="encodedText"></param>
        /// <returns></returns>
        public static byte[] DecodeBinary(string encodedText)
        {
            byte[] decodedBytes = Convert.FromBase64String(encodedText);
            return decodedBytes;
        }
    }
}