using Nudes.Paginator.Core;
using Sample;

var builder = WebApplication.CreateBuilder(args);

Bogus.Faker<Car> carFaker = new Bogus.Faker<Car>()
    .RuleFor(d => d.Model, d => d.Vehicle.Model())
    .RuleFor(d => d.Manufacturer, d => d.Vehicle.Manufacturer());

Bogus.Faker<Person> faker = new Bogus.Faker<Person>()
    .RuleFor(d => d.Id, d => d.IndexFaker)
    .RuleFor(d => d.Name, d => d.Person.FullName)
    .RuleFor(d => d.Email, d => d.Person.Email)
    .RuleFor(d => d.Car, d => carFaker.Generate());


List<Person> dataSource = faker.Generate(5000);

builder.Services.AddControllers();
builder.Services.AddSingleton(dataSource);

var app = builder.Build();

app.MapControllers();

app.Run();
