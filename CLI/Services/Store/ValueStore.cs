using CLI.Services.Files;
using CLI.Services.Locks;
using CLI.Services.Serialization;

namespace CLI.Services.Store;

internal sealed class ValueStore(
    ISecretsFileSupplier secretsFileSupplier,
    IKeyValueSerializer serializer,
    ILockFactory lockFactory
)
{
#region Functions

    public string? FetchOne(
        string key
    ) {
        var filepath = secretsFileSupplier.GetKeyStoreDirectory();
        using var mutex = lockFactory.Create(filepath);

        mutex.Acquire();
        var secrets = LoadSecrets(filepath);
        mutex.Release();

        return secrets.Result.GetValueOrDefault(key);
    }


    public IReadOnlyDictionary<string, string> FetchAll() {
        var filepath = secretsFileSupplier.GetKeyStoreDirectory();
        using var mutex = lockFactory.Create(filepath);

        mutex.Acquire();
        var secrets = LoadSecrets(filepath);
        mutex.Release();

        return secrets.Result;
    }


    public void Upsert(
        string key, string value
    ) {
        var filepath = secretsFileSupplier.GetKeyStoreDirectory();
        using var mutex = lockFactory.Create(filepath);

        mutex.Acquire();

        var secrets = LoadSecrets(filepath);
        secrets.Result[key] = value;

        SaveSecrets(filepath, secrets.Result);

        mutex.Release();
    }


    public void Delete(
        string key
    ) {
        var filepath = secretsFileSupplier.GetKeyStoreDirectory();
        using var mutex = lockFactory.Create(filepath);

        mutex.Acquire();
        var secrets = LoadSecrets(filepath);
        if (secrets.Result.Remove(key)) {
            SaveSecrets(filepath, secrets.Result);
        }

        ;

        mutex.Release();
    }


    public void Destroy() {
        var filepath = secretsFileSupplier.GetKeyStoreDirectory();
        using var mutex = lockFactory.Create(filepath);

        mutex.Acquire();
        SaveSecrets(filepath, new Dictionary<string, string>());
        mutex.Release();
    }

#endregion

#region Internals

    private async Task<Dictionary<string, string>> LoadSecrets(
        string filepath
    ) {
        if (!File.Exists(filepath)) {
            return new Dictionary<string, string>();
        }

        var secretsJson = await File.ReadAllTextAsync(filepath);
        var secretsDict = serializer.Deserialize(secretsJson);
        return secretsDict;
    }


    private void SaveSecrets(
        string filepath, Dictionary<string, string> secretsDict
    ) {
        var secretsFile = $"{filepath}.tmp";
        var secretsJson = serializer.Serialize(secretsDict);
        File.WriteAllText(secretsFile, secretsJson);

        if (File.Exists(filepath)) {
            File.Replace(secretsFile, filepath, null);
        }
        else {
            File.Move(secretsFile, filepath);
        }
    }

#endregion
}
