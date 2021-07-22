#region Title Header

// Name: Phillip Smith
// 
// Solution: FFDiscordBot
// Project: FFDiscordBot
// File Name: FFBotClient.cs
// 
// Current Data:
// 2021-07-22 1:28 PM
// 
// Creation Date:
// 2021-07-22 12:30 PM

#endregion

#region usings

using System;
using System.Threading;
using System.Threading.Tasks;
using AllOverIt.GenericHost;
using Microsoft.Extensions.Logging;

#endregion

namespace FFDiscordBot
{
  internal class FFBotClient : ConsoleAppBase
  {
    private readonly ILogger<FFBotClient> _logger;

    public FFBotClient(ILogger<FFBotClient> logger)
    {
      _logger = logger;
    }

    public override Task StartAsync(CancellationToken cancellationToken = new())
    {
      _logger.LogInformation("Bot client is running...");

      Console.WriteLine("Press ESC to exit");

      while (true)
      {
        var kp = Console.ReadKey(true);

        if (kp.Key == ConsoleKey.Escape)
        {
          break;
        }
      }

      return Task.CompletedTask;
    }
  }
}