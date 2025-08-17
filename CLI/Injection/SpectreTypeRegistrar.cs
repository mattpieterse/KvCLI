using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Spectre.Console.Cli;

namespace CLI.Injection;

public sealed class SpectreTypeRegistrar(IHostBuilder builder)
    : ITypeRegistrar
{
#region Inherited

    public void Register(Type service, Type implementation) =>
        builder.ConfigureServices((_, svcs) => svcs.AddSingleton(service, implementation));


    public void RegisterInstance(Type service, object implementation) =>
        builder.ConfigureServices((_, svcs) => svcs.AddSingleton(service, implementation));


    public void RegisterLazy(Type service, Func<object> factory) =>
        builder.ConfigureServices((_, svcs) => svcs.AddSingleton(service, (_) => factory()));


    public ITypeResolver Build() => new SpectreTypeResolver(builder.Build());

#endregion
}
