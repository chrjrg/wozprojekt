/*
Command for clearing the console.
*/
using static GameAssets;

class CommandClear : BaseCommand, ICommand {
  // Sets the command description during initialization.
  public CommandClear () {
    description = db.GetSection("CommandClearDescription");
  }
  
  // Clears the console when executed.
  public void Execute (Context context, string command, string[] parameters) {
    Console.Clear();
  }
}