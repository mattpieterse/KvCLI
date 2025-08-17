namespace CLI.Services.Locks;

internal interface ILock : IDisposable
{
#region Contracts

    void Acquire();
    void Release();

#endregion
}
