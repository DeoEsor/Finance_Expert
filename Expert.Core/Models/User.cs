namespace Expert.Core.Models;

public partial class User
{
    public int Id { get; set; }
    public string Login { get; set; }
    public string Password { get; set; }
    public UserStatus Status { get; set; }
    public string Name { get; set; }
}