namespace Nesur.Core.System.Save {
    public interface ISaveSystem<T> {
        void Save(T saveObject, string saveName, string secret);
        T Load(string saveName, string secret);
    }
}