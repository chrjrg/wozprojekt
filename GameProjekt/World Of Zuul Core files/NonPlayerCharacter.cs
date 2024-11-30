using static GameAssets;
using static Anim;
using System.Collections.Concurrent;

public abstract class NPC {
    public string name;
    protected bool exitInteraction;

  public NPC(string name) {
    this.name = name;
    this.exitInteraction = false;
  }

  public string GetName() {
    return name;
  }

  public void Interact(Context context) {
    Console.Clear();
    string currentLocation = context.GetCurrentName().ToString();
    //Console.WriteLine($"You interact with {GetName()} in {currentLocation}\n");
    PerformAction(context);   
  }

    public abstract void DisplayMandatoryIntro(Context context);
    public abstract void PerformAction(Context context);

    
}


public class Expert : NPC {
  private string current;

  public Expert(string name) : base(name) {
      current = string.Empty; 
  }

  public override void DisplayMandatoryIntro(Context context) {
    current = context.GetCurrentName().ToString();
    switch (current)
    {
      case var atom when atom == db.GetSection("EnergyAtomName"): 
        DisplaySection("AtomExpertIntro");
        break;
      case var water when water == db.GetSection("EnergyWaterName"):
        DisplaySection("WaterExpertIntro");
        break;
      case var solar when solar == db.GetSection("EnergySolarName"): 
        DisplaySection("SolarExpertIntro");
        break;
      case var wind when wind == db.GetSection("EnergyWindName"):  
        DisplaySection("WindExpertIntro");
        break;
      default:  
        break;
    }
  }

  public override void PerformAction(Context context) {
    Space currentSpace = context.GetCurrent();
    if (!currentSpace.alreadyBeenHere) 
    {
      DisplayMandatoryIntro(context);
      context.ClickNext();
      currentSpace.alreadyBeenHere = true;
    } 
      UserChoiseExpert();
    }  


  public void DisplayInfo (Context context) {
    current = context.GetCurrentName().ToString();
    switch (current)
    {
      case var atom when atom == db.GetSection("EnergyAtomName"):
        DisplaySection("AtomExpertShortInfo");
        break;
      case var water when water == db.GetSection("EnergyWaterName"):
        DisplaySection("WaterExpertShortInfo");
        break;
      case var solar when solar == db.GetSection("EnergySolarName"): 
        DisplaySection("SolarExpertShortInfo");
        break;
      case var wind when wind == db.GetSection("EnergyWindName"):  
        DisplaySection("WindExpertShortInfo");
        break;
      default:  
        break;
    }
  }

  private void DisplaySection(string section) {
    Anim.CharSplit(db.GetSection(section) + "\n",10);
  }

  public void UserChoiseExpert() {
      Console.Clear();
      Space currentSpace = context.GetCurrent();
      DisplayInfo(context);
      if (currentSpace.selectedInfo == false) {
        Console.WriteLine(db.GetSection("ExpertInfoLock"));
      }
      Console.WriteLine($"{db.GetSection("ExpertInfoOption1")} '{db.GetSection("ExpertInfoChoice")}'");
      Console.WriteLine($"{db.GetSection("ExpertInfoOption2")} '{db.GetSection("ExpertBuyChoice")}'");
      Console.WriteLine($"{db.GetSection("ExpertInfoOption3")} '{db.GetSection("ExpertBackChoice")}'");

      Console.Write("> ");
      string userInput = Console.ReadLine()!.ToLower();
      if (userInput == db.GetSection($"ExpertInfoChoice")) {
        Console.Clear();
        switch (current)
        {
          case var atom when atom == db.GetSection("EnergyAtomName"):
            DisplaySection("AtomExpertMoreInfo");
            break;
          case var water when water == db.GetSection("EnergyWaterName"):
            DisplaySection("WaterExpertMoreInfo");
            break;
          case var solar when solar == db.GetSection("EnergySolarName"): 
            DisplaySection("SolarExpertMoreInfo");
            break;
          case var wind when wind == db.GetSection("EnergyWindName"):  
            DisplaySection("WindExpertMoreInfo");
            break;
          default:  
            break;
        }
        currentSpace.selectedInfo = true;
        context.ClickNext();
        UserChoiseExpert(); 
      } else if (userInput == db.GetSection("ExpertBuyChoice")) {
        Console.Clear();
        if (currentSpace.selectedInfo == true) {
          Contract.CreateContract(context);
        } else {
          UserChoiseExpert();
        }
      } else if (userInput == db.GetSection("ExpertBackChoice")) {
        Console.Clear();
        context.TransitionBackHere();
      } else {
        Console.WriteLine(db.GetSection("InputError"));
      }
  }

}

public class Secretary : NPC {

  public Secretary(string name) : base(name) { }
    
  public override void PerformAction(Context context) {
      Space currentSpace = context.GetCurrent();
      if (!currentSpace.alreadyBeenHere) 
      {
        DisplayMandatoryIntro(context);
        currentSpace.alreadyBeenHere = true;
        context.ClickNext();
        Console.Clear();
        UserChoiceSecretary();
      } else {
        UserChoiceSecretary();
      }
    }

  public override void DisplayMandatoryIntro(Context context) {
      DisplaySection("SecretaryIntro");
    }

  public void UserChoiceSecretary() {
    Console.WriteLine(db.GetSection("SecretaryOptionIntro") + "\n");
    Console.WriteLine($"{db.GetSection("SecretaryInfoOption1")} '{db.GetSection("SecretaryInfoStatus")}'");
    Console.WriteLine($"{db.GetSection("SecretaryInfoOption2")} '{db.GetSection("SecretarySubmitChoice")}'");
    Console.WriteLine($"{db.GetSection("SecretaryInfoOption3")} '{db.GetSection("SecretaryBackChoice")}'");

    Console.Write("> ");
    string userInput = Console.ReadLine()!.ToLower();
    if (userInput == db.GetSection("SecretaryInfoStatus")) {
      Resource.DisplayAllStatuses(budget,energi,co2);
    } else if (userInput == db.GetSection("SecretarySubmitChoice")) {
      Submit();
    } else if (userInput == db.GetSection("SecretaryBackChoice")) {
      Console.Clear();
      context.TransitionBackHere();
    } else {
      Console.Clear();
      Console.WriteLine(db.GetSection("InputError") + "\n");
      UserChoiceSecretary();
    }
  }



  public void Submit() {
    budget.GetStatus();
    energi.GetStatus();
    co2.GetStatus();

    // Print antal af forskellige energiformer.

    Evaluate();

    Console.WriteLine($"\n{db.GetSection("EndGameOption1")} '{db.GetSection("EndGameSubmitChoice")}'");
    Console.WriteLine($"{db.GetSection("EndGameOption2")} '{db.GetSection("EndGameBackChoice")}'");

    Console.Write("> ");
    string userInput = Console.ReadLine()!.ToLower();
    if (userInput == db.GetSection("EndGameSubmitChoice")) {
      context.MakeDone();
    } else if (userInput == db.GetSection("EndGameBackChoice")) {
      context.TransitionBackHere();
    } else {
      Console.WriteLine(db.GetSection("InputError"));
    }
  }

    private void DisplaySection(string section) {
    Anim.CharSplit(db.GetSection(section) + "\n",25);
    }

  private void Evaluate() {
    // Console.WriteLine("Spillet afsluttes...");
    // Implement if needed
  }

  public void SecretaryIntro(){
      Console.Clear();
      string[] initMessage = db.GetSectionArray("SecIntro");

      foreach (string text in initMessage)
      {
          CharSplit(text, 15);
          Thread.Sleep(100);
          System.Console.WriteLine();
      }
      // Bed brugeren om at trykke på en tast for at starte quizzen
      Console.ForegroundColor = ConsoleColor.Green;
      Console.WriteLine("\n" + db.GetSection("QuizPressKey"));
      Console.ReadKey(true);
      Console.ForegroundColor = ConsoleColor.White;

      // Start quizzen
      if (quiz != null)
      {
          quiz.StartQuiz();
      }
      else
      {
          Console.WriteLine("Quiz-object not initialized!");
      }  
  }


}