/* 
Fallback for when a command is not implemented
*/
using static GameAssets;

class CommandUnknown : BaseCommand, ICommand {
  // Executes when an unknown command is entered.
  public void Execute (Context context, string command, string[] parameters) { 
    Console.WriteLine(db.GetSection("CommandUnknownHeader") + " '"+command+"' 😕"); // Display message for unknown command
  }
}