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
    /// AES Encryption Module
    /// </summary>
    public class AES
    {
        // Encryption Key Parameters
        private static int BufferKeySize = 32;
        private static int BlockSize = 256;
        private static int KeySize = 256;
        
        /// <summary>
        /// Update Encryption Key Size
        /// </summary>
        /// <param name="bufferKeySize"></param>
        /// <param name="blockSize"></param>
        /// <param name="keySize"></param>
        public static void UpdateEncryptionKeySize(int bufferKeySize = 32, int blockSize = 256, int keySize = 256)
        {
            BufferKeySize = bufferKeySize;
            BlockSize = blockSize;
            KeySize = keySize;
        }
        
        public static string Encrypt(string plane, string password)
        {
            byte[] encrypted = Encrypt(Encoding.UTF8.GetBytes(plane), password);
            return Convert.ToBase64String(encrypted);
        }
        
        public static byte[] Encrypt(byte[] src, string password)
        {
            RijndaelManaged rij = SetupRijndaelManaged;

            // A pseudorandom number is newly generated based on the inputted password
            Rfc2898DeriveBytes deriveBytes = new Rfc2898DeriveBytes(password, BufferKeySize);
            // The missing parts are specified in advance to fill in 0 length
            byte[] salt = new byte[BufferKeySize];
            // Rfc2898DeriveBytes gets an internally generated salt
            salt = deriveBytes.Salt;
            // The 32-byte data extracted from the generated pseudorandom number is used as a password
            byte[] bufferKey = deriveBytes.GetBytes(BufferKeySize);

            rij.Key = bufferKey;
            rij.GenerateIV();

            using (ICryptoTransform encrypt = rij.CreateEncryptor(rij.Key, rij.IV))
            {
                byte[] dest = encrypt.TransformFinalBlock(src, 0, src.Length);
                // first 32 bytes of salt and second 32 bytes of IV for the first 64 bytes
                List<byte> compile = new List<byte>(salt);
                compile.AddRange(rij.IV);
                compile.AddRange(dest);
                return compile.ToArray();
            }
        }
        
        public static string Decrypt(string encrtpted, string password)
        {
            byte[] decripted = Decrypt(Convert.FromBase64String(encrtpted), password);
            return Encoding.UTF8.GetString(decripted);
        }
        
        public static byte[] Decrypt(byte[] src, string password)
        {
            RijndaelManaged rij = SetupRijndaelManaged;

            List<byte> compile = new List<byte>(src);

            // First 32 bytes are salt.
            List<byte> salt = compile.GetRange(0, BufferKeySize);
            // Second 32 bytes are IV.
            List<byte> iv = compile.GetRange(BufferKeySize, BufferKeySize);
            rij.IV = iv.ToArray();

            Rfc2898DeriveBytes deriveBytes = new Rfc2898DeriveBytes(password, salt.ToArray());
            byte[] bufferKey = deriveBytes.GetBytes(BufferKeySize);    // Convert 32 bytes of salt to password
            rij.Key = bufferKey;

            byte[] plain = compile.GetRange(BufferKeySize * 2, compile.Count - (BufferKeySize * 2)).ToArray();

            using (ICryptoTransform decrypt = rij.CreateDecryptor(rij.Key, rij.IV))
            {
                byte[] dest = decrypt.TransformFinalBlock(plain, 0, plain.Length);
                return dest;
            }
        }
        
        private static RijndaelManaged SetupRijndaelManaged
        {
            get
            {
                RijndaelManaged rij = new RijndaelManaged();
                rij.BlockSize = BlockSize;
                rij.KeySize = KeySize;
                rij.Mode = CipherMode.CBC;
                rij.Padding = PaddingMode.PKCS7;
                return rij;
            }
        }
    }
}