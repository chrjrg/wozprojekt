/* 
Baseclass for commands
*/
using static GameAssets;

class BaseCommand {
  protected string description = db.GetSection("BaseCommandDescription");
  
  // Checks if the number of parameters does not match the expected value.
  protected bool GuardEq (string[] parameters, int bound) {
    return parameters.Length!=bound;
  }
  
   // Returns the description of the command.
  public String GetDescription () {
    return description;
  }
}
