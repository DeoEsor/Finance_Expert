using CryptoServer.Core;
using Microsoft.EntityFrameworkCore;

namespace CryptoServer.DBContexts;

public sealed class UsersDb : DbContext
{
    public DbSet<User> Users => Set<User>();

    private readonly string connectionString =
        "Server=localhost,1433;Database=UsersDB;Trusted_Connection=False;User ID=sa;Password=Pa55w0rd";
     
    public UsersDb()
    {
        Database.EnsureCreated();
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(connectionString);
    }
}
/*
 * using var a = new UsersDb();
    a.Users.AddRange(new List<User>()
    {
        new()
        {
            Username = "Vasya"
        },
        new()
        {
            Username = "Kolya"
        },
    });
    a.Chats.AddRange( new List<Chat>()
    {
        new()
        {
            ChatId = 0,
            UserId = 0
        },
        new()
        {
            ChatId = 0,
            UserId = 1
        }
    });
    a.Messages.Add(new Message
    {
        ChatId = 0,
        Created = DateTime.Now,
        Author = 0,
        File = null,
        Text = new Texts
        {
            Id = 0,
            Text = "Da"
        }
    });
    a.SaveChanges();
 */