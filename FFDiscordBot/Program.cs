#region Title Header

// Name: Phillip Smith
// 
// Solution: FFDiscordBot
// Project: FFDiscordBot
// File Name: Program.cs
// 
// Current Data:
// 2021-07-22 1:13 PM
// 
// Creation Date:
// 2021-07-22 11:01 AM

#endregion

#region usings

using System;
using System.IO;
using System.Threading.Tasks;
using AllOverIt.GenericHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;

#endregion

namespace FFDiscordBot
{
  public static class Program
  {
    public static async Task Main(string[] args)
    {
      var botConfig = GetBotConfig();

      if (botConfig is null)
      {
        return;
      }

      await CreateHostBuilder(args)
        .ConfigureServices(services =>
        {
          services.AddHostedService<FFBotConsole>();
          services.AddSingleton(p => botConfig);
        })
        .RunConsoleAsync(options => options.SuppressStatusMessages = true);
    }

    private static FFBotConfiguration GetBotConfig()
    {
      // Check for botconfig.json
      var configFile = Path.Combine(AppContext.BaseDirectory, "botconfig.json");

      if (!File.Exists(configFile))
      {
        Console.WriteLine("Creating 'botconfig.json'. Please add your bot token within...");

        var serialized =
          JsonConvert.SerializeObject(new FFBotConfiguration {BotToken = "token here"}, Formatting.Indented);

        File.WriteAllText(configFile, serialized);

        Environment.Exit(0);
        return null;
      }

      var configData = File.ReadAllText(configFile);
      FFBotConfiguration botConfiguration;
      try
      {
        botConfiguration = JsonConvert.DeserializeObject<FFBotConfiguration>(configData) ??
                           throw new InvalidOperationException("Unable to deserialize songs list.");
      }
      catch (Exception e)
      {
        Console.WriteLine("There is an error with the formatting of 'botconfig.json'." + Environment.NewLine);
        Console.WriteLine(e.Message);
        Environment.Exit(0);
        return null;
      }

      return botConfiguration;
    }

    private static IHostBuilder CreateHostBuilder(string[] args)
    {
      return GenericHost
        .CreateConsoleHostBuilder(args)
        .ConfigureServices((hostContext, services) => { services.AddSingleton<IConsoleApp, FFBotClient>(); });
    }
  }
}