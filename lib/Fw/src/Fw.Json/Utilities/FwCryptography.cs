using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Web.Configuration;
using Fw.Json.SqlServer;

namespace Fw.Json.Utilities
{
    public class FwCryptography
    {
        //---------------------------------------------------------------------------------------------
        public class FwDataDecryptionException : Exception { public FwDataDecryptionException() : base("Unable to decrypt data.") { } }
        //---------------------------------------------------------------------------------------------
        private static readonly string[] keyStrings = new string[] {
            "689AAD3114298EABFD539D35A6B76D709C1006FDE7CA4ABADCAB1",
            "46BB4A7B5346E5878D879E74AD6A7CE",
            "B0A4ACE067B8443C988DBD0BC8D900B9E90375DB4087B830F71E38C71BABC12401118430EF9259BE1366CD1FB606A14DC16CB0D8B34F401F54726B0BA83CE3F",
            "C6324EDE7BEFE3A7C82611A81B00F381D7D8CCDE7D28AB3",
            "292D3289CB25455871794EC9A3B84EC9BE94591489EF319164E3E3B624786"
        };
        private static List<byte[]> keys = new List<byte[]>();
        //---------------------------------------------------------------------------------------------
        static FwCryptography()
        {
            for (int i = 0; i < keyStrings.Length; i++)
            {
                byte[] keyBuffer = System.Text.Encoding.ASCII.GetBytes(keyStrings[i]);
                keys.Add(keyBuffer);
            }
        }
        //---------------------------------------------------------------------------------------------
        public static byte[] XorCrypt(byte[] data, byte[] key)
        {
            byte[] cryptedData = new byte[data.Length];
            int keyPosition = 0;
            for (int dataPosition = 0; dataPosition < data.Length; dataPosition++)
            {
                byte resultByte = data[dataPosition];
                if (keyPosition == key.Length)
                {
                    keyPosition = 0;
                }
                resultByte = (byte)(resultByte ^ key[keyPosition]);
                keyPosition++;
                cryptedData[dataPosition] = resultByte;
            }
            return cryptedData;
        }
        //---------------------------------------------------------------------------------------------
        public static string AjaxEncrypt(string data)
        {
            //string result = string.Empty;
            //if (!string.IsNullOrEmpty(data))
            //{
            //    string hashcode = (data).GetHashCode().ToString("X"); 
            //    string dataWithHashCode = hashcode + "," + data;
            //    byte[] buffer = System.Text.Encoding.ASCII.GetBytes(dataWithHashCode);
            //    //buffer = FwCompression.Compress(buffer);
            //    for (int i = 0; i < keys.Count; i++)
            //    {
            //        buffer = XorCrypt(buffer, keys[i]);
            //    }
            //    string encodedEncryptedDataWithHashCode = AjaxEncode(buffer);
            //    result = encodedEncryptedDataWithHashCode;
            //}
            //return result;
            return data;
        }
        //---------------------------------------------------------------------------------------------
        public static string AjaxEncrypt2(string data)
        {
            string result = string.Empty;
            if (!string.IsNullOrEmpty(data))
            {
                string hashcode = (data).GetHashCode().ToString("X");
                string dataWithHashCode = hashcode + "," + data;
                byte[] buffer = System.Text.Encoding.ASCII.GetBytes(dataWithHashCode);
                //buffer = FwCompression.Compress(buffer);
                for (int i = 0; i < keys.Count; i++)
                {
                    buffer = XorCrypt(buffer, keys[i]);
                }
                string encodedEncryptedDataWithHashCode = AjaxEncode(buffer);
                result = encodedEncryptedDataWithHashCode;
            }
            return result;
        }
        //---------------------------------------------------------------------------------------------
        public static string AjaxDecrypt(string encodedEncryptedDataWithHashCode)
        {
            //string data = string.Empty;
            //if (!string.IsNullOrEmpty(encodedEncryptedDataWithHashCode))
            //{
            //    try
            //    {
            //        byte[] buffer = AjaxDecode(encodedEncryptedDataWithHashCode);                    
            //        for (int i = keys.Count - 1; i >= 0; i--)
            //        {
            //            buffer = XorCrypt(buffer, keys[i]);
            //        }
            //        //buffer = FwCompression.Decompress(buffer);
            //        string dataWithHashCode = System.Text.Encoding.ASCII.GetString(buffer);
            //        int commaIndex = dataWithHashCode.IndexOf(',');
            //        int hashcode = Convert.ToInt32(dataWithHashCode.Substring(0, commaIndex), 16);
            //        data = dataWithHashCode.Substring(commaIndex + 1);
            //        if (hashcode != (data).GetHashCode())
            //        {
            //            throw new FwDataDecryptionException();
            //        }          
            //    }
            //    catch
            //    {
            //        throw new FwDataDecryptionException();
            //    }
            //}
            //return data;
            return encodedEncryptedDataWithHashCode;
        }
        //---------------------------------------------------------------------------------------------
        public static string AjaxDecrypt2(string encodedEncryptedDataWithHashCode)
        {
            string data = string.Empty;
            if (!string.IsNullOrEmpty(encodedEncryptedDataWithHashCode))
            {
                try
                {
                    byte[] buffer = AjaxDecode(encodedEncryptedDataWithHashCode);
                    for (int i = keys.Count - 1; i >= 0; i--)
                    {
                        buffer = XorCrypt(buffer, keys[i]);
                    }
                    //buffer = FwCompression.Decompress(buffer);
                    string dataWithHashCode = System.Text.Encoding.ASCII.GetString(buffer);
                    int commaIndex = dataWithHashCode.IndexOf(',');
                    int hashcode = Convert.ToInt32(dataWithHashCode.Substring(0, commaIndex), 16);
                    data = dataWithHashCode.Substring(commaIndex + 1);
                    if (hashcode != (data).GetHashCode())
                    {
                        throw new FwDataDecryptionException();
                    }
                }
                catch
                {
                    throw new FwDataDecryptionException();
                }
            }
            return data;
        }
        //---------------------------------------------------------------------------------------------        
        public static string AjaxEncode(byte[] data)
        {
            //string result = System.Text.Encoding.ASCII.GetString(data);
            string result = Convert.ToBase64String(data).Replace("+","-").Replace("/", "_").Replace("=","*");
            return result;
        }
        //---------------------------------------------------------------------------------------------
        public static byte[] AjaxDecode(string data)
        {
            //byte[] result = System.Text.Encoding.ASCII.GetBytes(data);
            byte[] result = Convert.FromBase64String(data.Replace("-","+").Replace("_", "/").Replace("*","="));
            return result;
        }
        //---------------------------------------------------------------------------------------------
        public static string DbwLocalEncrypt(string dataToEncrypt)
        {
            int y, z, strLen;
            uint decVal, seed, minVal;
            string decStr;

            decStr = "";
            strLen = dataToEncrypt.Length;
            y = strLen * 3 + 2;
            z = 24;
            minVal = (uint)((y <= z) ? y : z);
            for (int x = 1; x <= strLen; x++) {
                seed = (uint)(128 - ((x * x * x * x * x * x * x) % minVal));
                decVal = (uint)(dataToEncrypt[x - 1] + seed);
                decStr = decStr + (char)decVal;
            }
            return decStr;
        }
        //---------------------------------------------------------------------------------------------
        public static string DbwEncrypt(FwSqlConnection conn, string dataToEncrypt)
        {
            return FwSqlData.Encrypt(conn, dataToEncrypt);
        }
        //---------------------------------------------------------------------------------------------
        public static string DbwLocalDecrypt(string dataToDecrypt)
        {
            int y, z, strLen;
            uint decVal, seed, minVal;
            string decStr;

            decStr = "";
            strLen = dataToDecrypt.Length;
            y = strLen * 3 + 2;
            z = 24;
            minVal = (uint)((y <= z) ? y : z);
            for (int x = 1; x <= strLen; x++) {
                seed = (uint)(128 - ((x * x * x * x * x * x * x) % minVal));
                decVal = (uint)(dataToDecrypt[x - 1] - seed);
                decStr = decStr + (char)decVal;
            }
            return decStr;
        }
        //---------------------------------------------------------------------------------------------
        public static string DbwDecrypt(FwSqlConnection conn, string dataToDecrypt)
        {
            return FwSqlData.Decrypt(conn, dataToDecrypt);
        }
        //---------------------------------------------------------------------------------------------
        public static string AesEncrypt(string dataToEncrypt)
        {
            string result = string.Empty;
            //Get the decryption key from the machine key section of the web.config
            MachineKeySection machineKey = (MachineKeySection)ConfigurationManager.GetSection("system.web/machineKey");
            string key = machineKey.DecryptionKey;
            byte[] keyBytes = UTF8Encoding.UTF8.GetBytes(key);
            Rfc2898DeriveBytes rfc = new Rfc2898DeriveBytes(key, keyBytes, 1000);

            // Use the AES managed encryption provider
            AesManaged encryptor = new AesManaged();
            encryptor.Key = rfc.GetBytes(16);
            encryptor.IV = rfc.GetBytes(16);

            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream encrypt = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    byte[] dataBytes = new UTF8Encoding(false).GetBytes(dataToEncrypt);
                    encrypt.Write(dataBytes, 0, dataBytes.Length);
                    encrypt.FlushFinalBlock();
                    encrypt.Close();
                    result = Convert.ToBase64String(ms.ToArray());
                }
            }
            return result;
        }
        //---------------------------------------------------------------------------------------------
        public static string AesDecrypt(string dataToDeCrypt)
        {
            string result = string.Empty;
            //Get the decryption key from the machine key section of the web.config
            MachineKeySection machineKey = (MachineKeySection)ConfigurationManager.GetSection("system.web/machineKey");
            string key = machineKey.DecryptionKey;
            byte[] keyBytes = new UTF8Encoding(false).GetBytes(key);
            Rfc2898DeriveBytes rfc = new Rfc2898DeriveBytes(key, keyBytes, 1000);

            AesManaged decryptor = new AesManaged();
            decryptor.Key = rfc.GetBytes(16);
            decryptor.IV = rfc.GetBytes(16);

            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream decrypt = new CryptoStream(ms, decryptor.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    byte[] dataBytes = Convert.FromBase64String(dataToDeCrypt);
                    decrypt.Write(dataBytes, 0, dataBytes.Length);
                    decrypt.FlushFinalBlock();
                    decrypt.Close();

                    result = new UTF8Encoding(false).GetString(ms.ToArray());
                }
            }
            return result;            
        }
        //---------------------------------------------------------------------------------------------
    }
}
