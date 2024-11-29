/*
Command for transitioning between/or in spaces
*/
using static GameAssets;

class CommandGo : BaseCommand, ICommand { // Command for transitioning between spaces
  public CommandGo () {
    description = db.GetSection("CommandGoDescription"); 
  }
  
  public void Execute (Context context, string command, string[] parameters) {
    if (GuardEq(parameters, 1)) { // Check if the number of parameters is not equal to 1
      Console.Clear(); 
      Console.WriteLine(db.GetSection("CommandGoLocationError") + "\n");
      context.GetCurrent().Welcome(); // Print the welcome message of the current space
      return;
    }
    context.Transition(parameters[0]); // Transition to the space "parameters[0]"
  }
}

class CommandGoBack : BaseCommand, ICommand { // Command for going back to the previous space
  public CommandGoBack () {
    description = db.GetSection("CommandGoBackDescription");
  }
  
  public void Execute (Context context, string command, string[] parameters) {
    Console.Clear();
    context.TransitionBack();
  }
}

class CommandGoBackHere : BaseCommand, ICommand { // Command for going back to the same space
  public CommandGoBackHere () {
    description = db.GetSection("CommandGoBackHereDescription");
  }
  
  public void Execute (Context context, string command, string[] parameters) {
    Console.Clear();
    context.TransitionBackHere();
  }
  }
  