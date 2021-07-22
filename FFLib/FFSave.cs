#region Title Header

// Name: Phillip Smith
// 
// Solution: FFDiscordBot
// Project: FFLib
// File Name: FFSave.cs
// 
// Current Data:
// 2021-07-22 12:44 PM
// 
// Creation Date:
// 2021-07-22 12:41 PM

#endregion

#region usings

using System.Collections.Generic;

#endregion

namespace FFLib
{
  public class FFSave
  {
    public IEnumerable<FFUser> RegisteredUsers { get; init; }
  }
}