using System.Security.Cryptography;
using System.Text;

namespace CLI.Services.Locks;

internal sealed class FileMutexLock : ILock
{
#region Fields

    private readonly Mutex _mutex;

#endregion

#region Construct

    public FileMutexLock(string filepath) {
        var hash = Convert.ToHexString(
            SHA1.HashData(Encoding.UTF8.GetBytes(filepath))
        );

        _mutex = new Mutex(false, $"Global\\KvStore_{hash}");
    }

#endregion

#region Functions

    public void Acquire() {
        _mutex.WaitOne();
    }


    public void Release() {
        _mutex.ReleaseMutex();
    }


    public void Dispose() {
        _mutex.Dispose();
    }

#endregion
}
