/* Command interface
 */

public interface ICommand {
  void Execute (Context context, string command, string[] parameters);
  string GetDescription ();
}

