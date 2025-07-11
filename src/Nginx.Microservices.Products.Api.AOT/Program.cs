var builder = WebApplication.CreateSlimBuilder(args);

builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.TypeInfoResolverChain.Insert(0, AppJsonSerializerContext.Default);
});

var app = builder.Build();

app.MapGet("/api/products", () =>
{
    return new List<Product>() { new(Guid.NewGuid(), "Some awasome product", int.MaxValue) };
});

app.Urls.Add("http://*:8001");
app.Run();

public record Product(Guid Id, string Name, int Quantity);
