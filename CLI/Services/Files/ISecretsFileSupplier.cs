namespace CLI.Services.Files;

public interface ISecretsFileSupplier
{
#region Contracts

    string GetKeyStoreDirectory();

#endregion
}
