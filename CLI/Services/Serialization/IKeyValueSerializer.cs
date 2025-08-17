namespace CLI.Services.Serialization;

internal interface IKeyValueSerializer
{
#region Contracts

    string Serialize(Dictionary<string, string> secrets);
    Dictionary<string, string> Deserialize(string oJson);

#endregion
}
