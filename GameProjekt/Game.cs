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
    Console.WriteLine("Sprog? (DAN/ENG/DE)");
    string userInput = Console.ReadLine()!.ToLower();
    if (userInput == "dan") {
        db.LoadFile(Path.Combine(Directory.GetCurrentDirectory(), "txtfiles/DAN.txt").ToString());
    } else if (userInput == "eng") {
        db.LoadFile(Path.Combine(Directory.GetCurrentDirectory(), "txtfiles/ENG.txt").ToString());
    } else if (userInput == "de") {
        db.LoadFile(Path.Combine(Directory.GetCurrentDirectory(), "txtfiles/DE.txt").ToString());
    } else {
        Console.WriteLine("Forkert input. Prøv igen.");
    }
    Console.Clear();

    // db.LanguageChange();   ... SKAL VIRKE I STEDET FOR MAIN METODEN

    InitRegistry();

    //Intro.ShowIntro();

    EnergyStore.BuyEnergy(AtomType,1);
    EnergyStore.BuyEnergy(WindType,1);
    EnergyStore.BuyEnergy(SolarType,1);
    EnergyStore.BuyEnergy(WaterType,1);

    
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
