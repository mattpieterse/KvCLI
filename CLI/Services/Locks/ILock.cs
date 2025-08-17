namespace CLI.Services.Locks;

public interface ILock : IDisposable
{
#region Contracts

    void Acquire();
    void Release();

#endregion
}
