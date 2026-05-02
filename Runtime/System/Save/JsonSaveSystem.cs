using System.IO;
using NesurPackage.Util;
using UnityEngine;

namespace Nesur.Core.System.Save {
    public class JsonSaveSystem<T> : ISaveSystem<T> {
        public void Save(T saveObject, string path) {
            string json = JsonUtility.ToJson(saveObject);
            byte[] encryptedJson = EncryptionUtil.EncryptUsingAes(json);
            File.WriteAllBytes(path, encryptedJson);
        }

        public T Load(string path) {
            if (File.Exists(path)) {
                byte[] encryptedJson = File.ReadAllBytes(path);
                string json = EncryptionUtil.DecryptUsingAes(encryptedJson);
                return JsonUtility.FromJson<T>(json);
            }
            return default(T);
        }
    }
}