using static GameAssets;
using static Anim;

/*
  - DriveAnim(GameAssets.Car, 50, 25); - Viser anim med følge syntaks <GameAssets.(Objekt), afstand, sleeptimer tid>
  - Intro.ShowIntro(); - Viser spillets indledende intro;
*/


class Game {
  static World world = new World();
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
    //Intro.ShowIntro();
    InitRegistry();
    context.(GetCurrent).Welcome();

    
    while (context.IsDone()==false) {
      Console.Write("> ");
      string? line = Console.ReadLine();
      if (line!=null) registry.Dispatch(line);
    }
    Console.WriteLine("Game Over 😥");
    Console.WriteLine(context.GetPrev());

  }
}
