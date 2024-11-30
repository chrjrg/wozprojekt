using static GameAssets;
using static Anim;

/*
  - DriveAnim(GameAssets.Car, 50, 25); - Viser anim med følge syntaks <GameAssets.(Objekt), afstand, sleeptimer tid>
  - Intro.ShowIntro(); - Viser spillets indledende intro;
*/


class Game {
  static void Main (string[] byargs) {
    Console.Clear();
    TextDatabase db = TextDatabase.Instance;

    bool validInput = false;
    while (!validInput) {
    Console.WriteLine("Sprog? Skriv: (DAN/ENG/DE)");
    string userInput = Console.ReadLine()!.ToLower();
    if (userInput == "dan") {
        db.LoadFile(Path.Combine(Directory.GetCurrentDirectory(), "txtfiles/DAN.txt").ToString());
        validInput = true;
    } else if (userInput == "eng") {
        db.LoadFile(Path.Combine(Directory.GetCurrentDirectory(), "txtfiles/ENG.txt").ToString());
        validInput = true;
    } else if (userInput == "de") {
        db.LoadFile(Path.Combine(Directory.GetCurrentDirectory(), "txtfiles/DE.txt").ToString());
        validInput = true;
    } else {
        Console.Clear();
        Console.WriteLine("Forkert input. Prøv igen." + "\n");
    }
}
    Console.Clear();

    // db.LanguageChange();   ... SKAL VIRKE I STEDET FOR MAIN METODEN

    InitRegistry();

    //Intro.ShowIntro();

    EnergyStore.BuyEnergy(AtomType,10);
    EnergyStore.BuyEnergy(WindType,0);
    EnergyStore.BuyEnergy(SolarType,20);
    EnergyStore.BuyEnergy(WaterType,0);

    
    context.GetCurrent().Welcome();

    while (context.IsDone()==false) {
      Console.Write("> ");
      string? line = Console.ReadLine();
      if (line!=null)  registry.Dispatch(line);
      //Console.WriteLine(context.ToString()); //.... For debugging
    }

    Console.WriteLine("Game Over 😥");
  }

}
