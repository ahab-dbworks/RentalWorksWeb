using System;
using System.Collections.Generic;

namespace FwStandard.Mobile
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
            string result = Convert.ToBase64String(data).Replace("+", "-").Replace("/", "_").Replace("=", "*");
            return result;
        }
        //---------------------------------------------------------------------------------------------
        public static byte[] AjaxDecode(string data)
        {
            //byte[] result = System.Text.Encoding.ASCII.GetBytes(data);
            byte[] result = Convert.FromBase64String(data.Replace("-", "+").Replace("_", "/").Replace("*", "="));
            return result;
        }
        //---------------------------------------------------------------------------------------------
    }
}
