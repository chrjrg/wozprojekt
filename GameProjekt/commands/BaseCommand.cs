/* 
Base class for commands
*/
using static GameAssets;

class BaseCommand {
  // Command description fetched from the database.
  protected string description = db.GetSection("BaseCommandDescription");
  
  // Validates if the parameter count matches the expected value.
  protected bool GuardEq (string[] parameters, int bound) {
    return parameters.Length != bound;
  }
  
  // Retrieves the command description.
  public String GetDescription () {
    return description;
  }
}