using FluentValidation;
using Grpc.Core;
using GrpcGreeeter.Validators;
using Newtonsoft.Json;

namespace GrpcGreeeter.Services;

public class CalculatorService(ILogger<GreeterService> logger,
    IValidator<AddCoordinatesRequest> coordinateValidator,
    ValidationService validationService) : Calculator.CalculatorBase
{
    
    public override Task<AdditionReply> Add(AdditionRequest request, ServerCallContext context)
    {
        if (request.A == 7 || request.B == 7)
        {
            throw new RpcException(new Status(StatusCode.InvalidArgument, "Values cannot be 7"));
        }
        var result = request.A + request.B;
        logger.LogInformation("Addition result: {result}", result);
        return Task.FromResult(new AdditionReply
        {
            Result = request.A + request.B
        });
    }

    public override Task<SumListReply> SumList(SumListRequest request, ServerCallContext context)
    {
        var list = request.Numbers.ToList();
        return Task.FromResult(new SumListReply
        {
            Result = list.Sum()
        });
    }
    
    public override async Task<AddCoordinatesReply> AddCoordinates(AddCoordinatesRequest request, ServerCallContext context)
    {
        await validationService.ValidateGrpcRequest(request);
        
        var coorA = request.A.ToDictionary();
        var coorB = request.B.ToDictionary();

        var result = new AddCoordinatesReply();
        result.Result.Add("X", coorA["X"] + coorB["X"]);
        result.Result.Add("Y", coorA["Y"] + coorB["Y"]);
        
        logger.LogInformation("AddCoordinates result: {result}", JsonConvert.SerializeObject(result));
        
        return result;
    }
}