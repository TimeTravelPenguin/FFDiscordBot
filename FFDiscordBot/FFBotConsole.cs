#region Title Header

// Name: Phillip Smith
// 
// Solution: FFDiscordBot
// Project: FFDiscordBot
// File Name: FFBotConsole.cs
// 
// Current Data:
// 2021-07-22 4:29 PM
// 
// Creation Date:
// 2021-07-22 12:30 PM

#endregion

#region usings

using System;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using AllOverIt.GenericHost;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

#endregion

namespace FFDiscordBot
{
  internal class FFBotConsole : ConsoleWorker
  {
    private readonly FFBotConfiguration _botConfiguration;
    private readonly DiscordClient _client;
    private readonly ILogger<FFBotConsole> _logger;

    public FFBotConsole(IHostApplicationLifetime applicationLifetime, FFBotConfiguration botConfiguration,
      ILogger<FFBotConsole> logger, ILoggerFactory loggerFactory) : base(applicationLifetime)
    {
      _botConfiguration = botConfiguration;
      _logger = logger;

      var config = new DiscordConfiguration
      {
        Token = _botConfiguration.BotToken,
        TokenType = TokenType.Bot,
        LoggerFactory = loggerFactory,
        Intents = DiscordIntents.AllUnprivileged,
        MinimumLogLevel = LogLevel.Debug
      };

      _client = new DiscordClient(config);
      var commands = _client.UseCommandsNext(new CommandsNextConfiguration
      {
        CaseSensitive = false,
        StringPrefixes = new[] {"!"}
      });

      //var classes = Assembly.GetExecutingAssembly()
      //  .GetTypes()
      //  .Where(t => t.IsAssignableTo(typeof(BaseCommandModule)))
      //  .Where(t => t.GetMethods()
      //    .Any(i => i.GetCustomAttributes(typeof(CommandAttribute)).Any()))
      //  .ToList();

      commands.RegisterCommands(Assembly.GetExecutingAssembly());


      _client.MessageCreated += ClientOnMessageCreated;
    }

    private static async Task ClientOnMessageCreated(DiscordClient sender, MessageCreateEventArgs e)
    {
      if (e.Message.Content.ToLowerInvariant().Contains("ping"))
      {
        await e.Message.RespondAsync("Pong!");
      }
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
      // Login
      await _client.ConnectAsync(new DiscordActivity("Trying to be a bot", ActivityType.Watching), UserStatus.Online,
        DateTimeOffset.Now);

      _logger.LogInformation("Discord client connected");

      // Run console forever
      await Task.Run(() => { WaitHandle.WaitAny(new[] {stoppingToken.WaitHandle}); }, stoppingToken);
    }

    protected override async void OnStopping()
    {
      base.OnStopping();

      await _client.DisconnectAsync();

      _logger.LogInformation("Discord client disconnected");
    }
  }
}