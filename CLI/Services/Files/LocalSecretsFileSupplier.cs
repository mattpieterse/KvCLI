namespace CLI.Services.Files;

public class LocalSecretsFileSupplier : ISecretsFileSupplier
{
#region Functions

    public string GetKeyStoreDirectory() {
        var directory = Path.Combine(
            path1: Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
            path2: ".config",
            path3: "kvstore"
        );

        Directory.CreateDirectory(directory);
        return Path.Combine(
            directory, "secrets.json"
        );
    }

#endregion
}
