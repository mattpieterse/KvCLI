using System.Text.Json;

namespace CLI.Services.Serialization;

internal sealed class JsonKeyValueSerializer : IKeyValueSerializer
{
#region Functions

    public string Serialize(Dictionary<string, string> secrets) {
        return JsonSerializer.Serialize(
            secrets, new JsonSerializerOptions {
                WriteIndented = true
            }
        );
    }


    public Dictionary<string, string> Deserialize(string oJson) {
        return JsonSerializer.Deserialize<Dictionary<string, string>>(oJson)
               ?? new Dictionary<string, string>();
    }

#endregion
}
