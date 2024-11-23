/* Command for transitioning between spaces
 */

class CommandGo : BaseCommand, ICommand {
  public CommandGo () {
    description = "Follow an exit";
  }
  
  public void Execute (Context context, string command, string[] parameters) {
    if (GuardEq(parameters, 1)) {
      Console.WriteLine("I don't seem to know where that is 🤔");
      return;
    }
    context.Transition(parameters[0]);
  }
}

class CommandGoBack : BaseCommand, ICommand {
  public CommandGoBack () {
    description = "Go back from previous space";
  }
  
  public void Execute (Context context, string command, string[] parameters) {
    context.TransitionBack();
  }
}

class CommandGoBackHere : BaseCommand, ICommand {
  public CommandGoBackHere () {
    description = "Go back to the same space";
  }
  
  public void Execute (Context context, string command, string[] parameters) {
    Console.Clear();
    context.TransitionBackHere();
  }
  }
  