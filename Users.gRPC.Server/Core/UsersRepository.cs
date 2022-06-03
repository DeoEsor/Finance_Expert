using Expert.Core.Models;

namespace Users.gRPC.Server.Contexts.Core;

public class UsersRepository : IRepository<User>, IDisposable, IAsyncDisposable
{
    private UsersDb UsersDb { get; set; } = new UsersDb();

    public IEnumerable<User> Data => UsersDb.Users;

    public void AddData(User data)
    {
        UsersDb.Users.Add(data);
        UsersDb.SaveChanges();
    }

    public void AddData(IEnumerable<User> data)
    {
        foreach (var user in data)
        {
            if (UsersDb.Users.Any(u => u.Id == user.Id))
                throw new ArgumentException(nameof(user.Id));
            UsersDb.Users.Add(user);
        }
        UsersDb.SaveChanges();
    }

    public async Task AddDataAsync(User data)
    {
        if (UsersDb.Users.Any(u => u.Id == data.Id))
            throw new ArgumentException(nameof(data.Id));
        UsersDb.Users.Add(data);
        await UsersDb.SaveChangesAsync();
    }

    public async Task AddDataAsync(IEnumerable<User> data)
    {
        foreach (var user in data)
        {
            if (UsersDb.Users.Any(u => u.Id == user.Id))
                throw new ArgumentException(nameof(user.Id));
            UsersDb.Users.Add(user);
        }
        await UsersDb.SaveChangesAsync();
    }

    public void ChangeData(int id, User data)
    {
        throw new NotImplementedException();
    }

    public void ChangeData(int id, IEnumerable<User> data)
    {
        throw new NotImplementedException();
    }

    #region Dispose Region

    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }

    public async ValueTask DisposeAsync()
    {
        await DisposeAsyncCore().ConfigureAwait(false);

        Dispose(disposing: false);
#pragma warning disable CA1816 // Dispose methods should call SuppressFinalize
        GC.SuppressFinalize(this);
#pragma warning restore CA1816 // Dispose methods should call SuppressFinalize
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            UsersDb?.Dispose();
            UsersDb = null!;
        }
    }

    protected virtual async ValueTask DisposeAsyncCore()
    {
        if (UsersDb is not null)
        {
            await UsersDb.DisposeAsync().ConfigureAwait(false);
        }

        UsersDb = null;
    }

    #endregion

}