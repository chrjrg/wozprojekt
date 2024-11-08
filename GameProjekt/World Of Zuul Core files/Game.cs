using static GameLogic.GameAssets;
using static Anim;

/*
  - DriveAnim(GameAssets.Car, 50, 25); - Viser anim med følge syntaks <GameAssets.(Objekt), afstand, sleeptimer tid>
  - Intro.ShowIntro(); - Viser spillets indledende intro;
*/


public class Game {
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
    registry.Register("clear",new CommandClear());
  }
  
  static void Main (string[] byargs) {
    ClearConsole();
    //Intro.ShowIntro();
    InitRegistry();
 /*    Param[] param = new Param[]{
      new Param("Balance: ",1000000000," kr."),
      new Param("Energiforsnyning: ",2," GW."),
      new Param("CO\u2082: ",1," Tons")
    };

    foreach(Param p in param){
      Console.WriteLine(p.getStatus());

    }
     */
    context.GetCurrent().Welcome();
    //balance.getStatus();
    

    
    while (context.IsDone()==false) {
      Console.Write("> ");
      string? line = Console.ReadLine();
      if (line!=null) registry.Dispatch(line);
    }
    Console.WriteLine("Game Over 😥");
  }
}
