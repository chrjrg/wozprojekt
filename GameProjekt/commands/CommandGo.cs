/*
Command for transitioning between/or in spaces
*/
using static GameAssets;

class CommandGo : BaseCommand, ICommand { // Command for transitioning between spaces
  public CommandGo () {
    description = db.GetSection("CommandGoDescription");
  }
  
  // Executes the transition to a new space.
  public void Execute (Context context, string command, string[] parameters) {
    if (GuardEq(parameters, 1)) { // Ensures only one parameter is passed
      Console.Clear();
      Console.WriteLine(db.GetSection("CommandGoLocationError") + "\n");
      context.GetCurrent().Welcome(); // Displays the current space's welcome message
      return;
    }
    context.Transition(parameters[0]); // Transitions to the specified space
  }
}

class CommandGoBack : BaseCommand, ICommand { // Command for going back to the previous space
  public CommandGoBack () {
    description = db.GetSection("CommandGoBackDescription");
  }
  
  // Executes the transition back to the previous space.
  public void Execute (Context context, string command, string[] parameters) {
    Console.Clear();
    context.TransitionBack(); // Transitions back to the previous space
  }
}

class CommandGoBackHere : BaseCommand, ICommand { // Command for going back to the same space
  public CommandGoBackHere () {
    description = db.GetSection("CommandGoBackHereDescription");
  }
  
  // Executes the transition back to the same space.
  public void Execute (Context context, string command, string[] parameters) {
    Console.Clear();
    context.TransitionBackHere(); // Transitions back to the current space
  }
}
  