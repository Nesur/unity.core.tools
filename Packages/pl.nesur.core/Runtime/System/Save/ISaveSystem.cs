namespace Nesur.Core.System.Save {
    public interface ISaveSystem<T> {
        void Save(T saveObject, string path);
        T Load(string path);
    }
}