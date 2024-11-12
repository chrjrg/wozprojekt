//*****************************
//  CommandClear - Bruges til at rydde konsollen for et bedre overblik.
//  NEED: Som metode
//*****************************
using static GameAssets;


class CommandKeybind : BaseCommand, ICommand {
  public CommandKeybind () {
    description = "(KEYBIND MODE) | Nu kan du bruge piletasterne for gå til rum (ESC: Afslut | C: Renser konsol)";
  }
  
  public void Execute (Context context, string command, string[] parameters) {
    inputHandler.ListenForKeyPress();
  }

}