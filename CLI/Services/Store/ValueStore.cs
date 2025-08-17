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
        var filePath = secretsFileSupplier.GetKeyStoreDirectory();
        using var mutex = lockFactory.Create(filePath);

        mutex.Acquire();
        try {
            var secrets = LoadSecrets(filePath);
            return secrets.GetValueOrDefault(key);
        }
        finally {
            mutex.Release();
        }
    }


    public IReadOnlyDictionary<string, string> FetchAll() {
        var filePath = secretsFileSupplier.GetKeyStoreDirectory();
        using var mutex = lockFactory.Create(filePath);

        mutex.Acquire();
        try {
            var secrets = LoadSecrets(filePath);
            return secrets;
        }
        finally {
            mutex.Release();
        }
    }


    public void Upsert(
        string key, string value
    ) {
        var filePath = secretsFileSupplier.GetKeyStoreDirectory();
        using var mutex = lockFactory.Create(filePath);

        mutex.Acquire();
        try {
            var secrets = LoadSecrets(filePath);
            secrets[key] = value;

            SaveSecrets(filePath, secrets);
        }
        finally {
            mutex.Release();
        }
    }


    public void Delete(
        string key
    ) {
        var filePath = secretsFileSupplier.GetKeyStoreDirectory();
        using var mutex = lockFactory.Create(filePath);

        mutex.Acquire();
        try {
            var secrets = LoadSecrets(filePath);
            if (secrets.Remove(key)) {
                SaveSecrets(filePath, secrets);
            }
        }
        finally {
            mutex.Release();
        }
    }


    public void Destroy() {
        var filePath = secretsFileSupplier.GetKeyStoreDirectory();
        using var mutex = lockFactory.Create(filePath);

        mutex.Acquire();
        try {
            SaveSecrets(filePath, new Dictionary<string, string>());
        }
        finally {
            mutex.Release();
        }
    }

#endregion

#region Internals

    private Dictionary<string, string> LoadSecrets(
        string filePath
    ) {
        if (!File.Exists(filePath)) {
            return new Dictionary<string, string>();
        }

        var secretsJson = File.ReadAllText(filePath);
        var secretsDict = serializer.Deserialize(secretsJson);
        return secretsDict;
    }


    private void SaveSecrets(
        string filePath, Dictionary<string, string> secretsDict
    ) {
        var secretsFile = $"{filePath}.tmp";
        var secretsJson = serializer.Serialize(secretsDict);
        File.WriteAllText(secretsFile, secretsJson);

        if (File.Exists(filePath)) {
            File.Replace(secretsFile, filePath, null);
        }
        else {
            File.Move(secretsFile, filePath);
        }
    }

#endregion
}
