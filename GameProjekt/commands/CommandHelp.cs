/* 
Command for displaying a list of available commands
*/
using static GameAssets;

class CommandHelp : BaseCommand, ICommand {
  Registry registry;
  
  public CommandHelp (Registry registry) {
    this.registry = registry; // Dependency Injection
    this.description = db.GetSection("CommandHelpDescription"); 
  }
  
  public void Execute (Context context, string command, string[] parameters) {
    Console.Clear();
    string[] commandNames = registry.GetCommandNames(); // Get all command names
    Array.Sort(commandNames);
    
    // Finds max length of command name
    int max = 0;
    foreach (String commandName in commandNames) {
      int length = commandName.Length;
      if (length>max) max = length;
    }
    
    // Presents list of commands
    Console.WriteLine(db.GetSection("CommandHelpHeader") + "\n");
    foreach (String commandName in commandNames) {
      string description = registry.GetCommand(commandName).GetDescription();
      Console.WriteLine(" - {0,-"+max+"} "+description, commandName);
    }
    context.ClickNext();
    Console.Clear();
    context.TransitionBackHere();
  }
}
