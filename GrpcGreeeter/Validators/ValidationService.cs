using FluentValidation;
using Grpc.Core;

namespace GrpcGreeeter.Validators;

public class ValidationService(
    IServiceProvider serviceProvider)
{
    public async Task ValidateGrpcRequest<T>(T request)
    {
        var genericType = typeof(IValidator<T>);
        var validator = (IValidator<T>?)serviceProvider.GetService(genericType);
        if (validator == null)
        {
            return;
        }
        var validationResult = await validator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            throw new RpcException(new Status(StatusCode.InvalidArgument,
                validationResult.Errors.First().ErrorMessage));
        }
    }
}