/*
Command for exiting the game
*/
using static GameAssets;

class CommandExit : BaseCommand, ICommand {
  public CommandExit () {
    description = db.GetSection("CommandExitDescription");
  }
  public void Execute (Context context, string command, string[] parameters) {
    context.MakeDone();
  }
}


 