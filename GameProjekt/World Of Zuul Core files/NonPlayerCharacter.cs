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
      case "Atomkraftværk":
        DisplaySection("AtomEkspertIntro");
        break;
      case "Vandanlæg":
        DisplaySection("VandEkspertIntro");
        break;
      case "Solanlæg":  
        DisplaySection("SolEkspertIntro");
        break;
      case "p":  
        DisplaySection("VindEkspertIntro");
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
      ClickNext();
      currentSpace.alreadyBeenHere = true;
    } 
      UserChoiseExpert();
    }  


  public void DisplayInfo (Context context) {
    current = context.GetCurrentName().ToString();
    DisplaySection($"{current} ekspert");
  }

  private void DisplaySection(string section) {
    Anim.CharSplit(db.GetSection(section) + "\n",10);
  }


  public void ClickNext() {
      Console.ForegroundColor = ConsoleColor.Green;
      Console.WriteLine("\nTryk på en vilkårlig tast for at fortsætte");
      Console.ReadKey(true);
      Console.ForegroundColor = ConsoleColor.White;
  }

  public void UserChoiseExpert() {
      Console.Clear();
      Space currentSpace = context.GetCurrent();

      DisplayInfo(context);
      if (currentSpace.selectedInfo == false) {
        Console.WriteLine("Se info før du kan købe");
      }
      Console.WriteLine("Vil du have info? Skriv info");
      Console.WriteLine("Vil du fortage et køb? Skriv buy");
      Console.WriteLine("Vil du talbage til spillet? Skriv back");

      Console.Write("> ");
      string userInput = Console.ReadLine()!.ToLower();
      if (userInput == "info") {
        Console.Clear();
        Console.WriteLine("Info tekst her");
        currentSpace.selectedInfo = true;
        ClickNext();
        UserChoiseExpert();
      } else if (userInput == "buy") {
        if (currentSpace.selectedInfo == true) {
          Contract.CreateContract(context);
        } else {
          UserChoiseExpert();
        }
      } else if (userInput == "back") {
        context.TransitionBackHere();
      } else {
        Console.WriteLine("Det forstod jeg ikke");
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
      } else {
        UserChoiceSecretary();
      }
    }

  public override void DisplayMandatoryIntro(Context context) {
      DisplaySection("SekretærIntro");
    }

  public void UserChoiceSecretary() {
    Console.WriteLine("Hvad kan jeg hjælpe dig med?");
    Console.WriteLine("Hvis du vil se dit elnets status, skriv: Status");
    Console.WriteLine("Hvis du vil afslutte spillet og aflevere dit endelige elnet, skriv: Submit");

    Console.Write("> ");
    string userInput = Console.ReadLine()!.ToLower();
    if (userInput == "status") {
      Resource.DisplayAllStatuses(budget,energi,co2);
    } else if (userInput == "submit") {
      Submit();
    } else {
      Console.WriteLine("Det forstod jeg ikke");
      UserChoiceSecretary();
    }
  }



  public void Submit() {
    Console.WriteLine("Dit endelige elnet:");
    budget.GetStatus();
    energi.GetStatus();
    co2.GetStatus();

    // Print antal af forskellige energiformer.

    Console.WriteLine("Indsender data...");
    Evaluate();

    Console.WriteLine("Vil du afslutte spiller? skriv: End");
    Console.WriteLine("Vil du fortsætte spillet? skriv: Back");

    Console.Write("> ");
    string userInput = Console.ReadLine()!.ToLower();
    if (userInput == "end") {
      context.MakeDone();
    } else if (userInput == "back") {
      context.TransitionBackHere();
       
    } else {
      Console.WriteLine("Det forstod jeg ikke");
    }
  }

    private void DisplaySection(string section) {
    Anim.CharSplit(db.GetSection(section) + "\n",25);
    }

  private void Evaluate() {
    Console.WriteLine("Spillet afsluttes...");
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
      Console.WriteLine("\nTryk på en vilkårlig tast for at starte quizzen...");
      Console.ReadKey(true);
      Console.ForegroundColor = ConsoleColor.White;

      // Start quizzen
      if (quiz != null)
      {
          quiz.StartQuiz();
      }
      else
      {
          Console.WriteLine("Quiz-objektet er ikke initialiseret!");
      }  
  }


}

/*


    Console.WriteLine("Vil du foretage et køb? Svar ja = J nej = N");
    string prompt = Console.ReadLine();
    if (prompt == "J") {
      Buy();
    } else if (prompt == "N") {
      Console.WriteLine("Vil du have mere information fra ekspert eller tilbage til rummet? Svar Info = I Tilbage = T");
      prompt = Console.ReadLine();
      if (prompt == "I") {
        DisplayMoreInfo(context);
      } else if (prompt == "T") {
        CommandGoBack();
      }
    }
  
  */