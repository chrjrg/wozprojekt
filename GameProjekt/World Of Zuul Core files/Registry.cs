/* Command registry
 */

public class Registry {
  Context context;
  ICommand fallback;
  Dictionary<string, ICommand> commands = new Dictionary<string, ICommand>();
  
  public Registry (Context context, ICommand fallback) {
    this.context = context;
    this.fallback = fallback;
  }
  
  public void Register (string name, ICommand command) {
    commands.Add(name, command);

  }
  public void Dispatch(string line) {
    line = line.ToLower(); // Convert to lowercase 
    // Check if the command is an NPC name
    NPC? npc = context.GetCurrent()?.GetNPC(line);
    if (npc != null) {
      npc.Interact(context);
      return;
    }
    // check if entire command is in line (e.g., "go back")
    if (commands.ContainsKey(line)) {
        string cmd = line;
        string[] parameter = new string[0]; // no parameters in this case
        GetCommand(cmd).Execute(context, cmd, parameter);
        return;
    }

    // Fallback if standard 
    string[] elements = line.Split(" ");
    string command = elements[0];
    string[] parameters = GetParameters(elements);
    (commands.ContainsKey(command) ? GetCommand(command) : fallback).Execute(context, command, parameters);
}
  

  public ICommand GetCommand (string commandName) {
    return commands[commandName];
  }
  
  public string[] GetCommandNames () {
    return commands.Keys.ToArray();
  }
  
  // helpers
  
  private string[] GetParameters (string[] input) {
    string[] output = new string[input.Length-1];
    for (int i=0 ; i<output.Length ; i++) {
      output[i] = input[i+1];
    }
    return output;
  } 
}