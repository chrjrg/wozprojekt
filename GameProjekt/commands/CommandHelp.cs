/* 
Command for displaying a list of available commands
*/
using static GameAssets;

class CommandHelp : BaseCommand, ICommand {
  Registry registry;
  
  // Constructor for initializing the registry and setting the description.
  public CommandHelp (Registry registry) {
    this.registry = registry; // Dependency Injection
    this.description = db.GetSection("CommandHelpDescription");
  }
  
  // Executes the command to display a list of available commands.
  public void Execute (Context context, string command, string[] parameters) {
    Console.Clear();
    string[] commandNames = registry.GetCommandNames(); // Fetches all command names
    Array.Sort(commandNames); // Sorts the command names alphabetically
    
    // Finds the maximum length of command names for formatting
    int max = 0;
    foreach (String commandName in commandNames) {
      int length = commandName.Length;
      if (length > max) max = length;
    }
    
    // Displays the list of commands with descriptions
    Console.WriteLine(db.GetSection("CommandHelpHeader") + "\n");
    foreach (String commandName in commandNames) {
      string description = registry.GetCommand(commandName).GetDescription();
      Console.WriteLine(" - {0,-" + max + "} " + description, commandName);
    }
    
    context.ClickNext(); // Prompts to click next
    Console.Clear();
    context.TransitionBackHere(); // Returns to the previous space
  }
}