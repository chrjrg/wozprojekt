using static GameAssets;
/*
  - DriveAnim(GameAssets.Car, 50, 25); - Viser anim med følge syntaks <GameAssets.(Objekt), afstand, sleeptimer tid>
  - Intro.ShowIntro(); - Viser spillets indledende intro;
*/

class Game {
  static void Main (string[] byargs) {
    TextDatabase db = TextDatabase.Instance;
    TextDatabase.LanguageSelector.SelectLanguageAndLoadFile(db);
    

    InitRegistry();

    Intro.ShowIntro();

    //EnergyStore.BuyEnergy(AtomType,10); //for testing
    //EnergyStore.BuyEnergy(WindType,1); //for testing
    //EnergyStore.BuyEnergy(SolarType,135); //for testing
    //EnergyStore.BuyEnergy(WaterType,1); //for testing 

    context.GetCurrent().Welcome();

    while (context.IsDone()==false) {
      Console.Write("> ");
      string? line = Console.ReadLine();
      if (line!=null)  registry.Dispatch(line);
      //Console.WriteLine(context.ToString()); //.... For debugging
    }
    Console.WriteLine("Game Over");
  }

}
