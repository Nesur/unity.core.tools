using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

namespace Nesur.Core.Util {
    public class EncryptionUtil {
        public static byte[] EncryptUsingAes(string dataToEncrypt) {
            // Check arguments.
            if (dataToEncrypt == null || dataToEncrypt.Length <= 0)
                throw new ArgumentNullException(nameof(dataToEncrypt));
            byte[] encrypted;

            // Create an Aes object
            // with the specified key and IV.
            using (Aes aesAlg = Aes.Create()) {
                aesAlg.Key = GetAesKey();
                aesAlg.IV = GetAesIv();

                // Create an encryptor to perform the stream transform.
                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for encryption.
                using (MemoryStream msEncrypt = new MemoryStream()) {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write)) {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt)) {
                            //Write all data to the stream.
                            swEncrypt.Write(dataToEncrypt);
                        }
                    }

                    encrypted = msEncrypt.ToArray();
                }
            }

            return encrypted;
        }

        public static string DecryptUsingAes(byte[] encryptedData) {
            // Check arguments.
            if (encryptedData == null || encryptedData.Length <= 0)
                throw new ArgumentNullException(nameof(encryptedData));
           
            // Declare the string used to hold
            // the decrypted text.
            string plaintext = null;

            // Create an Aes object
            // with the specified key and IV.
            using (Aes aesAlg = Aes.Create()) {
                aesAlg.Key = GetAesKey();
                aesAlg.IV = GetAesIv();

                // Create a decryptor to perform the stream transform.
                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for decryption.
                using (MemoryStream msDecrypt = new MemoryStream(encryptedData)) {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read)) {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt)) {
                            // Read the decrypted bytes from the decrypting stream
                            // and place them in a string.
                            plaintext = srDecrypt.ReadToEnd();
                        }
                    }
                }
            }

            return plaintext;
        }

        public static byte[] CreateSha256Hash(string data) {
            using (SHA256 sha = SHA256.Create()) {
                return sha.ComputeHash(Encoding.UTF8.GetBytes(data));
            }
        }

        private static byte[] GetAesKey() {
            return CreateSha256Hash(SystemInfo.deviceUniqueIdentifier);
        }

        private static byte[] GetAesIv() {
            return new byte[16] { 32, 20, 0, 23, 55, 233, 23, 9, 24, 1, 2, 45, 90, 75, 36, 63 };
        }
    }
}