namespace CLI.Services.Locks;

internal interface ILockFactory
{
#region Contracts

    public ILock Create(string filepath);

#endregion
}
