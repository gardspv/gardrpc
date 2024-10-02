using Grpc.Core;
using Grpc.Core.Interceptors;
using GrpcGreeeter.Validators;

namespace GrpcGreeeter.Interceptors;

public class ValidationInterceptor(
    ValidationService validationService) : Interceptor
{
    public override async Task<TResponse> UnaryServerHandler<TRequest, TResponse>(
        TRequest request,
        ServerCallContext context,
        UnaryServerMethod<TRequest, TResponse> continuation)
    {
        //TODO: authentication can also be added here. E.g. map function call to required scope,
        //retrieve scope from claims to verify, and of course also ask STS about the validity of the token.
        
        await validationService.ValidateGrpcRequest(request);
        return await continuation(request, context);
    }
}