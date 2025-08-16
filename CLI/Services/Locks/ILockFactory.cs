namespace CLI.Services.Locks;

public interface ILockFactory
{
#region Contracts

    public ILock Create(string filepath);

#endregion
}
