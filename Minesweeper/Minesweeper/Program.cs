using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Minesweeper.Core;
using Minesweeper.Core.Interfaces;
using Minesweeper.Core.Service;

public class Program
{
    static void Main(string[] args)
    {
        using IHost host = CreateHostBuilder(args).Build();
        host.Services.GetService<App>().OnStart();
    }

    static IHostBuilder CreateHostBuilder(string[] args) =>
         Host.CreateDefaultBuilder(args).
             ConfigureServices((_, services) =>
                    services.AddTransient<App>()
                            .AddTransient<IGridGeneratorService, GridGeneratorService>()
                            .AddTransient<IConsoleDisplayService, ConsoleDisplayService>()
                            .AddTransient<ICollisionDetectorService, CollisionDetectionService>()
                            .AddTransient<IPlayerNavigationService, PlayerNavigationService>());

}
