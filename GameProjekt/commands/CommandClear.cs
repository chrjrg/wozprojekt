/*
Command for clearing the console.
*/
using static GameAssets;

class CommandClear : BaseCommand, ICommand {
  public CommandClear () {
    description = db.GetSection("CommandClearDescription");
  }
  
  public void Execute (Context context, string command, string[] parameters) {
    Console.Clear();
  }

}