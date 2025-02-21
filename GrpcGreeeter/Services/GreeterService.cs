using Grpc.Core;
using GrpcGreeeter;

namespace GrpcGreeeter.Services;

public class GreeterService : Greeter.GreeterBase
{
    private readonly ILogger<GreeterService> _logger;
    public GreeterService(ILogger<GreeterService> logger)
    {
        _logger = logger;
    }

    public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
    {
        return Task.FromResult(new HelloReply
        {
            Message = "Hello " + request.Name
        });
    }
    
    public override Task<AdditionReply> Add(AdditionRequest request, ServerCallContext context)
    {
        return Task.FromResult(new AdditionReply
        {
            Result = request.A + request.B
        });
    }
}
