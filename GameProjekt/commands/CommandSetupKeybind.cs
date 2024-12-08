/* 
Command for entering keybind mode.
Allows the player to use the arrow keys to navigate the game world.
*/
using static GameAssets;

class CommandKeybind : BaseCommand, ICommand {
  public CommandKeybind () {
    description = db.GetSection("CommandKeybindDescription");
  }
  
  // Enters keybind mode and listens for key presses.
  public void Execute (Context context, string command, string[] parameters) {
    inputHandler.TextHelp();
    inputHandler.ListenForKeyPress(); // Starts listening for key presses
  }
}