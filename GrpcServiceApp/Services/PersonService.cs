using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using GrpcServiceApp.Models;

namespace GrpcServiceApp.Services;

public class PersonService : PersonManager.PersonManagerBase
{
    private static List<Person> Persons { get; } = [];

    public override Task<RegisterResponse> RegisterNewPerson(PersonRequest request, ServerCallContext context)
    {
        var person = new Person
        {
            Name = request.Name,
            Age = request.Age
        };

        Console.WriteLine(person);
        Persons.Add(person);

        return Task.FromResult(new RegisterResponse
        {
            Message = $"{person.Name} registered",
            Start = Timestamp.FromDateTime(DateTime.UtcNow),
            Duration = Duration.FromTimeSpan(TimeSpan.FromHours(2))
        });
    }

    public override Task<AnyPersonExistsResponse> CheckAnyPersonExists(VoidRequest request, ServerCallContext context)
    {
        return Task.FromResult(new AnyPersonExistsResponse
        {
            Exists = Persons.Count != 0
        });
    }
}