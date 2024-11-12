//*****************************
//  CommandClear - Bruges til at rydde konsollen for et bedre overblik.
//  NEED: Som metode
//*****************************



class CommandClear : BaseCommand, ICommand {
  public CommandClear () {
    description = "Rydder hele konsollen for bedre overblik";
  }
  
  public void Execute (Context context, string command, string[] parameters) {
    Console.Clear();
  }

}