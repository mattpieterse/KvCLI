using CLI.Commands;
using CLI.Injection;
using CLI.Services;
using CLI.Services.Files;
using CLI.Services.Locks;
using CLI.Services.Serialization;
using CLI.Services.Store;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CLI;

/// <summary>
/// The main entry-point for the CLI application.
/// </summary>
internal abstract class App
{
#region Lifecycle

    /// <summary>
    /// Entry point for the application.
    /// </summary>
    /// <param name="args">
    /// Arguments parsed from the command-line upon execution of the application
    /// via a terminal. Used by the <see cref="Spectre.Console.Cli.CommandApp"/>
    /// to execute commands with arguments.
    /// </param>
    /// <returns>
    /// <see cref="Environment.ExitCode"/>
    /// </returns>
    private static void Main(string[] args) {
        var app = Configure(args).BuildApplication();

        // Application Configuration
        app.Configure(((options) => {
            options.Settings.ApplicationName = "KvCLI";

            // Commands

            options.AddCommand<SetCommand>("set");
            options.AddCommand<GetCommand>("get");
            options.AddCommand<PopCommand>("pop");
            options.AddCommand<ListCommand>("list");
            options.AddCommand<ClearCommand>("clear");
        }));

        app.Run(args);
    }

#endregion

#region Internals

    private static IHostBuilder Configure(string[] args) {
        var builder = Host.CreateDefaultBuilder(args);

        // Dependency Injection (DI)
        builder.ConfigureServices(static (_, services) => {
            services.AddSingleton<ILock, FileMutexLock>();
            services.AddSingleton<ILockFactory, FileMutexLockFactory>();
            services.AddSingleton<ISecretsFileSupplier, LocalSecretsFileSupplier>();
            services.AddSingleton<IKeyValueSerializer, JsonKeyValueSerializer>();
            services.AddSingleton<ValueStore>();
        });

        return builder;
    }

#endregion
}
