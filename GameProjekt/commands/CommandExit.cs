/*
Command for exiting the game
*/
using static GameAssets;

class CommandExit : BaseCommand, ICommand {
  // Initializes the command description.
  public CommandExit () {
    description = db.GetSection("CommandExitDescription");
  }
  
  // Executes the command to exit the game.
  public void Execute (Context context, string command, string[] parameters) {
    context.MakeDone();
  }
}


 