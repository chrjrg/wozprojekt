/* Main class for launching the game - This is an upgrade
 */

using System.Security.Principal;
using GameLogic;
using Anims;

class Game {
  static World world = new World();
  static Intro intro = new Intro();
  static Shape vindmølle = new Wind();
  static Shape atom = new Atom();
  static Shape car = new Car();
  static Context  context  = new Context(world.GetEntry());
  static ICommand fallback = new CommandUnknown();
  static Registry registry = new Registry(context, fallback);





  private static void InitRegistry () {
    ICommand cmdExit = new CommandExit();
    registry.Register("exit", cmdExit);
    registry.Register("quit", cmdExit);
    registry.Register("bye", cmdExit);
    registry.Register("go", new CommandGo());
    registry.Register("help", new CommandHelp(registry));
  }
  
  static void Main (string[] byargs) {
    intro.ShowIntro();
    //vindmølle.Show();
    //atom.Show();
    //Anim.DriveAnim(car, 50);
    InitRegistry();
    context.GetCurrent().Welcome();

    
    while (context.IsDone()==false) {
      Console.Write("> ");
      string? line = Console.ReadLine();
      if (line!=null) registry.Dispatch(line);
    }
    Console.WriteLine("Game Over 😥");
  }
}
