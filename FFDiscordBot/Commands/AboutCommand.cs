#region Title Header

// Name: Phillip Smith
// 
// Solution: FFDiscordBot
// Project: FFDiscordBot
// File Name: AboutCommand.cs
// 
// Current Data:
// 2021-07-22 3:59 PM
// 
// Creation Date:
// 2021-07-22 3:55 PM

#endregion

#region usings

using System.Threading.Tasks;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;

#endregion

namespace FFDiscordBot.Commands
{
  public class AboutCommand : BaseCommandModule
  {
    [Command("about")]
    public async Task AboutCommandAsync(CommandContext ctx)
    {
      await ctx.RespondAsync($"{StaticStrings.ApplicationName} {StaticStrings.Version}");
    }
  }
}