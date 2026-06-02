namespace Nesur.Core.System.Save {
    public interface ISaveSystem<T> {
        void Save(T saveObject, string saveName);
        T Load(string saveName);
    }
}