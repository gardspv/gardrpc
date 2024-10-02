using Grpc.Core;
using Grpc.Net.Client;
using GrpcGreeeterClient;

// The port number must match the port of the gRPC server.
using var channel = GrpcChannel.ForAddress("http://localhost:5148");
var client = new Greeter.GreeterClient(channel);
var reply = await client.SayHelloAsync(
    new HelloRequest { Name = "GreeterClient" });
Console.WriteLine("Greeting: " + reply.Message);

var additionReply = await client.AddAsync(new AdditionRequest
{
    A = 12, B = 47
});
Console.WriteLine($"Addition result: {additionReply.Result}");

var calculatorClient = new Calculator.CalculatorClient(channel);
try
{
    var calculatorReply = await calculatorClient.AddAsync(new AdditionRequest
    {
        A = 7, B = 47
    });
    Console.WriteLine($"Addition result: {calculatorReply.Result}");
}
catch (RpcException ex)
{
    Console.WriteLine("An error occured");
    Console.WriteLine("Status code: " + ex.Status.StatusCode);
    Console.WriteLine("Message: " + ex.Status.Detail);
}

var sumListReply = await calculatorClient.SumListAsync(new SumListRequest
{
    Numbers = { 1, 2, 3, 4, 5 }
});
Console.WriteLine("Sum of list: " + sumListReply.Result);

var addCoordinatesRequest = new AddCoordinatesRequest();
addCoordinatesRequest.A.Add("X", -1);
addCoordinatesRequest.A.Add("Y", 4);
addCoordinatesRequest.B.Add("X", 5);
addCoordinatesRequest.B.Add("Y", 6);

var addCoordinatesReply = await calculatorClient.AddCoordinatesAsync(
    addCoordinatesRequest);
    
    Console.WriteLine("Coordinates reply: " + addCoordinatesReply.Result["X"] + ", " + addCoordinatesReply.Result["Y"]);