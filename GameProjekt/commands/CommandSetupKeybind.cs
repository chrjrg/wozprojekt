/* 
Command for entering keybind mode.
This command allows the player to use the arrow keys to navigate the game world.
*/
using static GameAssets;

class CommandKeybind : BaseCommand, ICommand {
  public CommandKeybind () {
    description = db.GetSection("CommandKeybindDescription");
  }
  
  public void Execute (Context context, string command, string[] parameters) {
    inputHandler.ListenForKeyPress();
  }
}