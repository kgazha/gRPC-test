using GreeterClientApp;
using Grpc.Net.Client;

using var channel = GrpcChannel.ForAddress("http://localhost:5241");
var client = new PersonManager.PersonManagerClient(channel);

var x = await client.CheckAnyPersonExistsAsync(new VoidRequest());
Console.WriteLine(x.Exists);

Console.Write("Enter your name: ");
var name = Console.ReadLine();
Console.Write("Enter your age: ");
var age = int.Parse(Console.ReadLine());

var reply = await client.RegisterNewPersonAsync(
    new PersonRequest
    {
        Name = name,
        Age = age
    });

Console.WriteLine($"Response: {reply.Message} start:{reply.Start.ToDateTime()} dur:{reply.Duration.Seconds}");
Console.ReadKey();

x = await client.CheckAnyPersonExistsAsync(new VoidRequest());
Console.WriteLine(x.Exists);