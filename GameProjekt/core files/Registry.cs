/* 
Command registry
 */

public class Registry {
  Context context; // Holds the game context (current state)
  ICommand fallback; // The fallback command if no other match is found
  Dictionary<string, ICommand> commands = new Dictionary<string, ICommand>(); // Stores registered commands

  // Constructor to initialize context and fallback command
  public Registry (Context context, ICommand fallback) {
    this.context = context;
    this.fallback = fallback;
  }

  // Register a new command by its name and associated ICommand
  public void Register (string name, ICommand command) {
    commands.Add(name, command); // Adds the command to the dictionary
  }

  // Dispatches the command based on user input
  public void Dispatch(string line) {
    line = line.ToLower(); // Convert the input to lowercase for case-insensitive comparison

    // Check if the line matches an NPC name and trigger interaction
    NPC? npc = context.GetCurrent()?.GetNPC(line);
    if (npc != null) {
      npc.Interact(context); // If NPC found, interact with it
      return;
    }

    // Check if the entire command is in the line (e.g., "go back")
    if (commands.ContainsKey(line)) {
        string cmd = line; // Command name
        string[] parameter = new string[0]; // No parameters for single-word commands
        GetCommand(cmd).Execute(context, cmd, parameter); // Execute the command
        return;
    }

    // Split the input into command and parameters, fallback to standard command processing
    string[] elements = line.Split(" ");
    string command = elements[0]; // The first word is the command
    string[] parameters = GetParameters(elements); // The rest are parameters
    (commands.ContainsKey(command) ? GetCommand(command) : fallback).Execute(context, command, parameters); // Execute the command or fallback
  }

  // Retrieves the command by name from the dictionary
  public ICommand GetCommand (string commandName) {
    return commands[commandName]; // Return the ICommand associated with the command name
  }

  // Returns an array of all command names in the registry
  public string[] GetCommandNames () {
    return commands.Keys.ToArray(); // Converts dictionary keys (command names) to an array
  }

  // Helper method to extract parameters from the input
  private string[] GetParameters (string[] input) {
    string[] output = new string[input.Length-1]; // Exclude the command itself (first word)
    for (int i=0 ; i<output.Length ; i++) {
      output[i] = input[i+1]; // Copy the remaining words as parameters
    }
    return output; // Return the parameters
  }

    internal void Execute(string v1, Context context, string v2, string[] strings)
    {
        throw new NotImplementedException();
    }
}