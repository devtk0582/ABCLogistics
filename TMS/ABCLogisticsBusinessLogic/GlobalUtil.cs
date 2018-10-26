using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.IO;

namespace ABCLogisticsBusinessLogic
{
    public class GlobalUtil
    {
        public static Guid StringToGUID(string value)
        {
            try
            {
                var md5Hasher = MD5.Create();
                var data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(value));
                return new Guid(data);
            }
            catch (Exception ex)
            {
                return Guid.Empty;
            }
        }

        public static bool IsEmail(string inputEmail)
        {
            string strRedex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
            Regex re = new Regex(strRedex);
            if (re.IsMatch(inputEmail))
                return true;
            else
                return false;
        }

        public static bool IsNumeric(string value)
        {
            foreach (char c in value)
            {
                int n = Convert.ToInt16(c);
                if (n <= 47 || n >= 58)
                {
                    return false;
                }
            }
            return true;
        }

        public static bool IsDecimal(string value)
        {
            foreach (char c in value)
            {
                int n = Convert.ToInt16(c);
                if (n < 46 || n >= 58)
                {
                    return false;
                }
            }
            return true;
        }

        public static bool IsNumericPhone(string value)
        {
            foreach (char c in value)
            {
                int n = Convert.ToInt16(c);
                if ((n <= 47 || n >= 58) && (n != 45))
                {
                    return false;
                }
            }
            return true;
        }

        public static bool IsTime(string value)
        {
            string time = Convert.ToString(Convert.ToDateTime(value));
            bool isTime = Regex.IsMatch(time, "^((0?[1-9]|1[012])(:[0-5]\\d){0,2}(\\ [AP]M))$|^([01]\\d|2[0-3])(:[0-5]\\d){0,2}$");
            return isTime;
        }

        public static bool IsDate(string value)
        {
            string date = Convert.ToString(Convert.ToDateTime(value));
            bool isDate = Regex.IsMatch(date, "^([0-9]{1,2})[./-]+([0-9]{1,2})[./-]+([0-9]{2}|[0-9]{4})$");
            return isDate;
        }

        public static string GeneratePassword()
        {
            string index = "abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ0123456789";
            Random rnd = new Random();
            char[] randomPW = new char[6];
            int indexLength = index.Length;
            for (int i = 0; i < 6; i++)
            {
                randomPW[i] = index[Convert.ToInt32(Math.Truncate(indexLength * rnd.NextDouble()))];
            }
            return new string(randomPW);
        }

        public static string EncryptString(string strInput)
        {
            // Create a new instance of the hash crypto service provider.
            HashAlgorithm hashAlg = new SHA256CryptoServiceProvider();
            // Convert the data to hash to an array of Bytes.
            byte[] bytesValue = System.Text.Encoding.UTF8.GetBytes(strInput);
            // Compute the Hash. This returns an array of Bytes.
            byte[] bytesHash = hashAlg.ComputeHash(bytesValue);
            // Optionally, represent the hash value as a base64-encoded string, 
            // For example, if you need to display the value or transmit it over a network.
            return Convert.ToBase64String(bytesHash);
        }

        private static string CreateSalt(int size)
        {
            // Generate a cryptographic random number using the cryptographic     
            // service provider    
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] buff = new byte[size];
            rng.GetBytes(buff);
            // Return a Base64 string representation of the random number    
            return Convert.ToBase64String(buff);
        }

        public static string GetTemplateContent(string templatePath)
        {
            try
            {
                string mailBody = String.Empty;

                using (StreamReader sr = new StreamReader(templatePath))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        mailBody += line;
                    }
                }
                return mailBody;
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }
    }

    public class AESCryto
    {
        private byte[] Key = { 121, 217, 18, 11, 24, 26, 85, 45, 114, 184, 27, 162, 37, 112, 222, 209, 241, 24, 175, 144, 173, 53, 195, 29, 24, 26, 17, 218, 131, 236, 53, 201 };
        private byte[] Vector = { 146, 64, 191, 111, 23, 1, 113, 121, 231, 121, 221, 112, 79, 32, 114, 156 };


        private ICryptoTransform EncryptorTransform, DecryptorTransform;
        private System.Text.UTF8Encoding UTFEncoder;

        public AESCryto()
        {
            //This is our encryption method
            RijndaelManaged rm = new RijndaelManaged();

            //Create an encryptor and a decryptor using our encryption method, key, and vector.
            EncryptorTransform = rm.CreateEncryptor(this.Key, this.Vector);
            DecryptorTransform = rm.CreateDecryptor(this.Key, this.Vector);

            //Used to translate bytes to text and vice versa
            UTFEncoder = new System.Text.UTF8Encoding();

        }

        public string EncryptToString(string TextValue)
        {
            return ByteArrToString(Encrypt(TextValue));
        }

        public byte[] Encrypt(string TextValue)
        {
            //Translates our text value into a byte array.
            byte[] bytes = UTFEncoder.GetBytes(TextValue);
            byte[] encrypted = null;

            //Used to stream the data in and out of the CryptoStream.
            using (MemoryStream memoryStream = new MemoryStream())
            using (CryptoStream cs = new CryptoStream(memoryStream, EncryptorTransform, CryptoStreamMode.Write))
            {
                #region Write the decrypted value to the encryption stream
                cs.Write(bytes, 0, bytes.Length);
                cs.FlushFinalBlock();
                #endregion

                #region Read encrypted value back out of the stream
                memoryStream.Position = 0;
                encrypted = new byte[memoryStream.Length];
                memoryStream.Read(encrypted, 0, encrypted.Length);
                #endregion

                //Clean up.
                cs.Close();
                memoryStream.Close();
            }

            return encrypted;
        }


        public string DecryptString(string EncryptedString)
        {
            return Decrypt(StrToByteArray(EncryptedString));
        }

        public string Decrypt(byte[] EncryptedValue)
        {
            byte[] decryptedBytes = null;
            using (MemoryStream encryptedStream = new MemoryStream())
            using (CryptoStream decryptStream = new CryptoStream(encryptedStream, DecryptorTransform, CryptoStreamMode.Write))
            {
                #region Write the encrypted value to the decryption stream
                decryptStream.Write(EncryptedValue, 0, EncryptedValue.Length);
                decryptStream.FlushFinalBlock();
                #endregion

                #region Read the decrypted value from the stream.
                encryptedStream.Position = 0;
                decryptedBytes = new byte[encryptedStream.Length];
                encryptedStream.Read(decryptedBytes, 0, decryptedBytes.Length);
                decryptStream.Close();
                encryptedStream.Close();
                #endregion
            }
            return UTFEncoder.GetString(decryptedBytes);
        }

        public byte[] StrToByteArray(string str)
        {
            if (str.Length == 0)
                throw new Exception("Invalid string value in StrToByteArray");

            byte val;
            byte[] byteArr = new byte[str.Length / 3];
            int i = 0;
            int j = 0;
            do
            {
                val = byte.Parse(str.Substring(i, 3));
                byteArr[j++] = val;
                i += 3;
            }
            while (i < str.Length);
            return byteArr;
        }

        public string ByteArrToString(byte[] byteArr)
        {
            byte val;
            string tempStr = "";
            for (int i = 0; i <= byteArr.GetUpperBound(0); i++)
            {
                val = byteArr[i];
                if (val < (byte)10)
                    tempStr += "00" + val.ToString();
                else if (val < (byte)100)
                    tempStr += "0" + val.ToString();
                else
                    tempStr += val.ToString();
            }
            return tempStr;
        }
    }

    public enum ABCLogisticsApp
    {
        Home = 1, Shipping = 2, Invoicing = 3, Warehousing = 4, 
        Maintenance = 5, Breeze = 6, Quotes = 7, FFM = 8, 
        Communications = 9, Auditing = 10, Reporting = 11, TMS = 12
    }

    public enum ABCLogisticsRole
    {
        SuperAdmin = 1,
        CorpAdmin = 2,
        CorpUser = 3,
        TerminalAdmin = 4,
        TerminalSupervisor = 5,
        TerminalCustomerService = 6,
        TerminalSalesman = 7,
        TerminalWarehouse = 8,
        TerminalDriver = 9,
        Customer = 10
    }

    public class ReportColumnMain
    {
        public List<ReportColumn> ColumnOrig {get;set;}
        public List<ReportColumn> ColumnDest{ get;set;}
    }
}
