using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using System.Data;
using System.Reflection;
using System.Drawing.Imaging;


namespace ACSMinCapture
{
    static class CUtils
    {
        //Password = "d4n13l4$";
        static byte[] Encrypt(byte[] clearData, byte[] Key, byte[] IV)
        {
            
            MemoryStream ms = new MemoryStream();

            Rijndael alg = Rijndael.Create();

            alg.Key = Key;
            alg.IV = IV;

            CryptoStream cs = new CryptoStream(ms,
               alg.CreateEncryptor(), CryptoStreamMode.Write);

 
            cs.Write(clearData, 0, clearData.Length);

            cs.Close();

            byte[] encryptedData = ms.ToArray();

            return encryptedData;
        }
        static byte[] Decrypt(byte[] cipherData, byte[] Key, byte[] IV)
        {
            MemoryStream ms = new MemoryStream();
            Rijndael alg = Rijndael.Create();

            alg.Key = Key;
            alg.IV = IV;


            try
            {
                CryptoStream cs = new CryptoStream(ms,
                    alg.CreateDecryptor(), CryptoStreamMode.Write);
                cs.Write(cipherData, 0, cipherData.Length);
                cs.Close();
                cs.Dispose();
            }
            catch
            {

            }
            finally
            {
            }

            byte[] decryptedData = ms.ToArray();
            
            ms.Dispose();
            

            return decryptedData;
        }

        public static string Decrypt(string Value, string Password = "d4n13l4$")
        {
            byte[] cipherBytes = Convert.FromBase64String(Value);
            PasswordDeriveBytes pdb = new PasswordDeriveBytes(Password,
                new byte[] {0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 
            0x64, 0x76, 0x65, 0x64, 0x65, 0x76});
            byte[] decryptedData = Decrypt(cipherBytes,
                pdb.GetBytes(32), pdb.GetBytes(16));
            return System.Text.Encoding.Unicode.GetString(decryptedData);
        }

        public static string Encrypt(string Value, string Password = "d4n13l4$")
        {
            byte[] clearBytes =
              System.Text.Encoding.Unicode.GetBytes(Value);

            PasswordDeriveBytes pdb = new PasswordDeriveBytes(Password,
                new byte[] {0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 
            0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76});

            byte[] encryptedData = Encrypt(clearBytes,
                     pdb.GetBytes(32), pdb.GetBytes(16));

            return Convert.ToBase64String(encryptedData);

        }

        public static DataTable LINQToDataTable<T>(IEnumerable<T> linqList)
        {
            var dtReturn = new DataTable();
            PropertyInfo[] columnNameList = null;

            if (linqList == null) return dtReturn;

            foreach (T t in linqList)
            {
                // Use reflection to get property names, to create table, Only first time, others will follow 
                if (columnNameList == null)
                {
                    columnNameList = ((Type)t.GetType()).GetProperties();

                    foreach (PropertyInfo columnName in columnNameList)
                    {
                        Type columnType = columnName.PropertyType;

                        if ((columnType.IsGenericType) && (columnType.GetGenericTypeDefinition() == typeof(Nullable<>)))
                        {
                            columnType = columnType.GetGenericArguments()[0];
                        }

                        dtReturn.Columns.Add(new DataColumn(columnName.Name, columnType));
                    }
                }

                DataRow dataRow = dtReturn.NewRow();

                foreach (PropertyInfo columnName in columnNameList)
                {
                    dataRow[columnName.Name] =
                        columnName.GetValue(t, null) == null ? DBNull.Value : columnName.GetValue(t, null);
                }

                dtReturn.Rows.Add(dataRow);
            }

            return dtReturn;
        }

        public static ImageFormat GetImageFormat(string Format)
        {

            switch (Format.ToLower())
            {
                case @".bmp":
                    return ImageFormat.Bmp;

                case @".gif":
                    return ImageFormat.Gif;

                case @".ico":
                    return ImageFormat.Icon;

                case @".jpg":
                case @".jpeg":
                    return ImageFormat.Jpeg;

                case @".png":
                    return ImageFormat.Png;

                case @".tif":
                case @".tiff":
                    return ImageFormat.Tiff;

                default:
                    {
                        return ImageFormat.Jpeg;
                    }
            }



        }



    }
}
