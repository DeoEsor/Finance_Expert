using Grpc.Core;
using Users.gRPC.Server.Contexts.Core;
using UserService.Messages;

namespace Users.gRPC.Server.Services;

public class UsersService : UserService.UserService.UserServiceBase
{
    private readonly ILogger<UsersService> _logger;
    private readonly UsersRepository Db;
    
    public UsersService(ILogger<UsersService> logger, UsersRepository db)
    {
        _logger = logger;
        Db = db;
    }
    public override async Task<UserReply> Auth(AuthRequest request, ServerCallContext context)
    {
        var reply = new UserReply
        {
            StatusCode =(int) StatusCode.OK
        };
        var user = Db.Data.FirstOrDefault(s => s.Name == request.Username);
        if (user == null)
        {
            reply.StatusCode = (int)StatusCode.NotFound;
            return reply;
        }

        reply.User = new User
        {
            Id = user.Id,
            Username = user.Name,
            Password = user.Password,
            Status = (UserStatus)user.Status
        };
        return reply;
    }

    public override async Task<UserReply> Register(RegisterRequest request, ServerCallContext context)
    {
        var reply = new UserReply
        {
            StatusCode = (int)StatusCode.OK
        };
        try
        {
            var random = new Random();
            _logger.LogInformation($"Getted to register {request.Username}");
            if (Db.Data.FirstOrDefault(s => s.Name == request.Username) != null)
            {
                reply.StatusCode = (int)StatusCode.InvalidArgument;
                _logger.LogInformation($"Same user already registrated");
                return reply;
            }

            Db.AddData(new Expert.Core.Models.User()
            {
                Name = request.Username,
                Login = request.Username,
                Password = request.Password,
                Status = (Expert.Core.Models.User.UserStatus)request.Status
            });
            var user = Db.Data.FirstOrDefault(user => user.Name == request.Username);
            if (user == null)
            {
                reply.StatusCode = (int)StatusCode.NotFound;
                return reply;
            }

            reply.User = new User
            {
                Id = user.Id,
                Username = user.Name,
                Password = user.Password,
                Status = (UserStatus)user.Status
            };
            return reply;
        }
        catch(Exception e)
        {
            _logger.LogCritical($"{e.Message}");
            reply.StatusCode = (int)StatusCode.Aborted;
            return reply;
        }
        finally
        {
            
        }
    }
}