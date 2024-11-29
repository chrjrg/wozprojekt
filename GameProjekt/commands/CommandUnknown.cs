/* 
Fallback for when a command is not implemented
*/
using static GameAssets;

class CommandUnknown : BaseCommand, ICommand {
  public void Execute (Context context, string command, string[] parameters) {
    Console.WriteLine(db.GetSection("CommandUnknownHeader") + " '"+command+"' 😕");
  }
}