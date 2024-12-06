/* 
Fallback for when a command is not implemented
*/
using System.Data;
using static GameAssets;

class CommandMap : BaseCommand, ICommand {
  public void Execute (Context context, string command, string[] parameters) { 
    description = db.GetSection("CommandGoBackDescription");
    string[] Map=ActualMap.GetAsciiArt();
    Console.Clear();
    foreach(string lines in Map){
      Console.WriteLine(lines);
    }
    context.GetCurrent().Welcome(); // Print the welcome message of the current space
  }
}