


class CommandClear : BaseCommand, ICommand {
  public CommandClear () {
    description = "Rydder hele konsollen for bedre overblik";
  }
  
  public void Execute (Context context, string command, string[] parameters) {
    Console.Clear();
  }

}
