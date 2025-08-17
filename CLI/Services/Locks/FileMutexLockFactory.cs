namespace CLI.Services.Locks;

internal sealed class FileMutexLockFactory : ILockFactory
{
#region Factory

    public ILock Create(string filepath) {
        return new FileMutexLock(filepath);
    }

#endregion
}
