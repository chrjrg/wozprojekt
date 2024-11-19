using static GameAssets;
using static Anim;

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
    string currentLocation = context.GetCurrentName();
    Console.WriteLine($"You interact with {currentLocation}");
        switch (context.GetCurrentName())
        {
            case "Christiansborg":
                Console.WriteLine("Velkommen til Christiansborg");
                break;
            case "Vindanlæg":
                Console.WriteLine("Velkommen til Vindanlæg");
                break;
            case "Vandanlæg":
                break;
            case "Solanlæg":
                break;
            case "Atomkraftværk" :
                break;
            default:
                break;
                
        }
        PerformAction(context);   
     }
    public abstract void PerformAction(Context context);
    public abstract void DisplayMandatoryIntro(Context context);
}


public class Expert : NPC {
  private string current;

  public Expert(string name) : base(name) { }

  public override void PerformAction(Context context) {
  }

  public override void DisplayMandatoryIntro(Context context) {
    current = context.GetCurrentName().ToString();
    DisplaySection("test");

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
  }

  public void DisplayMoreInfo(Context context) {
    current = context.GetCurrentName().ToString();
    DisplaySection($"{current} ekspert");
  }

  private void DisplaySection(string section) {
    Anim.CharSplit(db.GetSection(section));
  }

  public void Buy() {
    Console.WriteLine("Vil du købe? ");
    string prompt = Console.ReadLine();
    //SignContract();
  }

  private void CommandGoBack() {
    // Implement if needed
  }
}

public class Secretary : NPC {
  public Secretary(string name) : base(name) { }
    
    public override void PerformAction(Context context) {
        UserChoiceSecretary();
        
    }

    public override void DisplayMandatoryIntro(Context context) {
        Console.WriteLine("Velkommen til sekretæren");
        UserChoiceSecretary();
    }

  public void UserChoiceSecretary() {
    Console.WriteLine("Hvad kan jeg hjælpe dig med?");
    Console.WriteLine("Hvis du vil se dit elnets status, skriv: GetStatus");
    Console.WriteLine("Hvis du vil afslutte spillet og aflevere dit endelige elnet, skriv: Submit");

    string? userInput = Console.ReadLine();
    if (userInput == "GetStatus") {
      Status();
    } else if (userInput == "Submit") {
      Submit();
    } else {
      Console.WriteLine("Det forstod jeg ikke");
    }
  }

  public void Status() {
    Console.WriteLine("Status:");
    Console.WriteLine(GameAssets.budget.GetStatus());
    Console.WriteLine(GameAssets.energi.GetStatus());
    Console.WriteLine(GameAssets.co2.GetStatus());

    Console.WriteLine("Vil du indlevere dit elnet? skriv: Submit");
    Console.WriteLine("Vil du fortsætte spillet? skriv: GoBack");

    string? userInput = Console.ReadLine();
    if (userInput == "Submit") {
      Submit();
    } else if (userInput == "GoBack") {
      // Implement if needed
    } else {
      Console.WriteLine("Det forstod jeg ikke");
    }
  }

  public void Submit() {
    Console.WriteLine("Dit endelige elnet:");
    Console.WriteLine(GameAssets.budget.GetStatus());
    Console.WriteLine(GameAssets.energi.GetStatus());
    Console.WriteLine(GameAssets.co2.GetStatus());

    // Print antal af forskellige energiformer.

    Console.WriteLine("Indsender data...");
    Evaluate();

    Console.WriteLine("Vil du afslutte spiller? skriv: EndGame");
    Console.WriteLine("Vil du fortsætte spillet? skriv: GoBack");

    string? userInput = Console.ReadLine();
    if (userInput == "EndGame") {
      EndGame();
    } else if (userInput == "GoBack") {
      // Implement if needed
    } else {
      Console.WriteLine("Det forstod jeg ikke");
    }
  }

  private void Evaluate() {
    Console.WriteLine("Spillet afsluttes...");
    // Implement if needed
  }

  private void EndGame() {
    Console.WriteLine("Spillet er afsluttet");
    // Implement if needed
  }
}