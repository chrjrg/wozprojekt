/* 
Command interface, used to define the command pattern
*/
// Each command must implement Execute to perform an action and GetDescription to provide information about the command.

public interface ICommand {
  void Execute (Context context, string command, string[] parameters); // Execute the command
  string GetDescription (); // Get the description of the command
}

