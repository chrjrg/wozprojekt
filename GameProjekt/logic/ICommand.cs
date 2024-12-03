/* 
Command interface, used to define the command pattern
*/
public interface ICommand {
  void Execute (Context context, string command, string[] parameters); // Execute the command
  string GetDescription (); // Get the description of the command
}

