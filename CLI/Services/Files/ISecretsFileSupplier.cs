namespace CLI.Services.Files;

internal interface ISecretsFileSupplier
{
#region Contracts

    string GetKeyStoreDirectory();

#endregion
}
