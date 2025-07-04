using System.Text.Json.Serialization;

[JsonSerializable(typeof(List<Product>))]
internal partial class AppJsonSerializerContext : JsonSerializerContext { }