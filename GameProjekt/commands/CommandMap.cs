/* 
Fallback for when a command is not implemented
*/
using System.Data;
using GameLogic;
using static GameAssets;
using System;

class CommandMap : BaseCommand, ICommand {
  // Initializes the command description.
  public CommandMap () {
    description = db.GetSection("CommandMapDescription");
  }
  
  // Executes the command to exit the game.
  public void Execute (Context context, string command, string[] parameters) {
    string[] Map = GameAssets.ActualMap.GetAsciiArt();
    Console.Clear();
    foreach(string lines in Map){
      Console.WriteLine(lines);
    }
    context.GetCurrent().Welcome(); // Print the welcome message of the current space
  }
}


