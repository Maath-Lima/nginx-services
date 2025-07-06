var builder = WebApplication.CreateSlimBuilder(args);

builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.TypeInfoResolverChain.Insert(0, AppJsonSerializerContext.Default);
});

var app = builder.Build();

app.MapGet("/api/users", () => {
    return new List<User>() { new("Some Name", "someemail@email.com") };
});

app.Run();

public record User(string Name, string Email);