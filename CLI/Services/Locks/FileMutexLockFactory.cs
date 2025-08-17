namespace CLI.Services.Locks;

public class FileMutexLockFactory : ILockFactory
{
#region Factory

    public ILock Create(string filepath) {
        return new FileMutexLock(filepath);
    }

#endregion
}
