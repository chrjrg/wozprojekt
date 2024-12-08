using static GameAssets;
/*
  - DriveAnim(GameAssets.Car, 50, 25); - Viser anim med følge syntaks <GameAssets.(Objekt), afstand, sleeptimer tid>
  - Intro.ShowIntro(); - Viser spillets indledende intro;
*/

class Game {
  static void Main (string[] byargs) {
    TextDatabase db = TextDatabase.Instance; // Singleton instance of the text database
    TextDatabase.LanguageSelector.SelectLanguageAndLoadFile(db); // Select language and load text file
    InitRegistry(); // Initialize the game registry with commands
    Intro.ShowIntro();

    context.GetCurrent().Welcome(); // Print the welcome message of the current space

    while (context.IsDone()==false) { // Main game loop
      Console.Write("> ");
      string? line = Console.ReadLine();
      if (line!=null)  registry.Dispatch(line);
    }
    Console.WriteLine("Game Over");
  }
}