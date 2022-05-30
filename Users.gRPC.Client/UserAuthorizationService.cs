using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Grpc.Net.Client;
using Microsoft.Extensions.Logging;
using UserService.Messages;
using static UserService.UserService;

namespace Users.gRPC.Client;

public sealed class UserAuthorizationService 
{
    private UserServiceClient Client { get; init; }
    private readonly ILogger<UserAuthorizationService> _logger;
    private readonly GrpcChannel Channel;
    
    public UserAuthorizationService(ILogger<UserAuthorizationService> logger = null)
    {
        _logger = logger;

        var httpHandler = new HttpClientHandler();

        httpHandler.ServerCertificateCustomValidationCallback =
            HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
		
        Channel  = GrpcChannel.ForAddress("https://localhost:7173", 
                                            new GrpcChannelOptions { HttpHandler = httpHandler });

        Client = new UserServiceClient(Channel);
    }
    
    public  UserReply Auth(string Username, string Password, CancellationToken cancellationToken = default)
    {
        Metadata headers = null!;
        DateTime? deadline = null!;
        var request = new AuthRequest
        {
            Username = Username,
            Password = Password
        };
        return Client.Auth(request, headers, deadline, cancellationToken);
    }

    public UserReply Register(string Username, string Password, CancellationToken cancellationToken = default)
    {
        Metadata headers = null!;
        DateTime? deadline = null!;
        var request = new RegisterRequest()
        {
            Username = Username,
            Password = Password
        };
        return Client.Register(request, headers, deadline, cancellationToken);
    }
}