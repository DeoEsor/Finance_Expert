namespace Users.gRPC.Server.Contexts.Core;

public interface IRepository<T>
{
    
    IEnumerable<T> Data { get; }
    void AddData(T data);
    void AddData(IEnumerable<T> data);
    
    Task AddDataAsync(T data);
    Task AddDataAsync(IEnumerable<T> data);
    
    void ChangeData(int id, T data);
    void ChangeData(int id, IEnumerable<T> data);
}