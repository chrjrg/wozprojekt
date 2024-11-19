/* Command for interacting with "sekretær" or "ekspert" 
 */

class CommandInteract : BaseCommand, ICommand {
  public CommandInteract () {
    description = "Interact with a non-player character";
  }
  
  public void Execute (Context context, string command, string[] parameters) {
    /*if (GuardEq(parameters, 1)) {
      Console.WriteLine("Who do you want to interact with?");
      if (context.GetCurrent().GetNPC(parameters[0]) == null) {
        Console.WriteLine("There is no one here by that name.");
        return;
      } else {
        context.GetCurrent().GetNPC(parameters[0]).Interact();
        return;
      }
      
    }
    */
  }
}