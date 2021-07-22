#region Title Header

// Name: Phillip Smith
// 
// Solution: FFDiscordBot
// Project: FFDiscordBot
// File Name: AboutCommand.cs
// 
// Current Data:
// 2021-07-22 4:48 PM
// 
// Creation Date:
// 2021-07-22 3:55 PM

#endregion

#region usings

using System.Threading.Tasks;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;

#endregion

namespace FFDiscordBot.Commands
{
  public class AboutCommand : BaseCommandModule
  {
    [Command("about")]
    public async Task AboutCommandAsync(CommandContext ctx)
    {
      await new DiscordMessageBuilder()
        .WithContent($"{StaticStrings.ApplicationName} {StaticStrings.Version}")
        .WithEmbed(new DiscordEmbedBuilder()
          .WithTitle("GitHub")
          .WithDescription("Check out my GitHub repository!")
          .WithUrl(@"https://github.com/TimeTravelPenguin/FFDiscordBot")
          .Build())
        .SendAsync(ctx.Channel);
    }
  }
}