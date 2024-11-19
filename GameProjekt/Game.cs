using static GameAssets;
using static Anim;

/*
  - DriveAnim(GameAssets.Car, 50, 25); - Viser anim med følge syntaks <GameAssets.(Objekt), afstand, sleeptimer tid>
  - Intro.ShowIntro(); - Viser spillets indledende intro;
  - quiz.initQuiz(); - Henter methoden til at vise vores quiz i konsollen. 
*/


class Game {
  static ICommand fallback = new CommandUnknown();
  static Registry registry = new Registry(context, fallback);


  private static void InitRegistry () {
    ICommand cmdExit = new CommandExit();
    registry.Register("exit", cmdExit);
    registry.Register("quit", cmdExit);
    registry.Register("bye", cmdExit);
    registry.Register("go", new CommandGo());
    registry.Register("help", new CommandHelp(registry));
    registry.Register("clear", new CommandClear());
    registry.Register("go back", new CommandGoBack());
    registry.Register("kb", new CommandKeybind());
    registry.Register("interact", new CommandInteract());
    

  }
  
  static void Main (string[] byargs) {

    db.LoadFile(Path.Combine(Directory.GetCurrentDirectory(), "World Of Zuul Core files/data.txt").ToString()); // Load the file, we do this in main to avoid loading the file multiple times

    // Intro.ShowIntro();
    //quiz.initQuiz();
    InitRegistry();
    context.GetCurrent().Welcome();
    


    while (context.IsDone()==false) {
      Console.Write("> ");
      string? line = Console.ReadLine();
      if (line!=null)  registry.Dispatch(line);
    }

    Console.WriteLine("Game Over 😥");
    Console.WriteLine(db.GetSection("test")); // test af database
}
}