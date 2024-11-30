/* 
Command interface, used to define the command pattern
*/
public interface ICommand {
  void Execute (Context context, string command, string[] parameters);
  string GetDescription ();
}

