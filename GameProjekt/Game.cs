using static GameAssets;
using static Anim;

/*
  - DriveAnim(GameAssets.Car, 50, 25); - Viser anim med følge syntaks <GameAssets.(Objekt), afstand, sleeptimer tid>
  - Intro.ShowIntro(); - Viser spillets indledende intro;
*/


class Game {
  static void Main (string[] byargs) {
    TextDatabase db = TextDatabase.Instance;
    db.LoadFile(Path.Combine(Directory.GetCurrentDirectory(), "World Of Zuul Core files/data.txt").ToString()); // Load the file, we do this in main to avoid loading the file multiple times
    Intro.ShowIntro();
    InitRegistry();
    
    context.GetCurrent().Welcome();
    


    while (context.IsDone()==false) {
      Console.Write("> ");
      string? line = Console.ReadLine();
      if (line!=null)  registry.Dispatch(line);
  }
  Console.WriteLine("Game Over 😥");;
}

}
