using static GameAssets;
using static Anim;

public class NPC
{
    public string name { get; set; }

    public NPC(string name)
    {
        this.name = name;
    }

    public virtual void Interact()
    {}
}

    public class Expert : NPC
    {
        private string current;

        public Expert(string name) : base(name) 
        {
        }

        public void DisplayMandatoryIntro(Context context)
        {
            current = context.GetCurrentName().ToString();

            switch (current)
            {
                case "Atomkraftværk":
                    Anim.CharSplit(db.GetSection("test"),25);
                    break;
                case "Vandanlæg":
                    Anim.CharSplit(db.GetSection("test"),25);
                    break;
                case "Vindanlæg":
                    Anim.CharSplit(db.GetSection("test"),25);
                    break;
                case "Solanlæg":
                    Anim.CharSplit(db.GetSection("test"),25);
                    break;
                default:
                    break;
            }

            Console.WriteLine("Vil du foretage et køb? Svar ja = J nej = N");
            string prompt = Console.ReadLine();
            if (prompt == "J")
            {
                Buy();
            }
            else if (prompt == "N")
            {
                Console.WriteLine("Vil du have mere information fra ekspert eller tilbage til rummet? Svar Info = I Tilbage = T");
                prompt = Console.ReadLine();
                if (prompt == "I")
                {
                    DisplayMoreInfo(context);
                }
                else if (prompt == "T")
                {
                    CommandGoBack();
                }
            }
        }

        public void DisplayMoreInfo(Context context)
        {
            current = context.GetCurrentName().ToString();

            switch (current)
            {
                case "Atomkraftværk":
                    Anim.CharSplit(db.GetSection("Atom ekspert"),25);
                    break;
                case "Vandanlæg":
                    Anim.CharSplit(db.GetSection("Vand ekspert"),25);
                    break;
                case "Vindanlæg":
                    Anim.CharSplit(db.GetSection("Vind ekspert"),25);
                    break;
                case "Solanlæg":
                    Anim.CharSplit(db.GetSection("Sol ekspert"),25);
                    break;
                default:
                    break;
            }
        }

        public void Buy()
        {
            Console.WriteLine("Vil du købe? ");
            string prompt = Console.ReadLine();
            //SignContract();
        }

        private void CommandGoBack()
        {
            // not sure if this is needed 
           
        }
    }   
    public class Secretary : NPC
{
    public Secretary(string name) : base(name) {}
        
     public override void Interact()
    {
        UserChoiceSecratary();
    }


public void SecretaryIntro()
{
    Console.Clear();
    string[] initMessage = TextDatabase.Instance.GetSectionArray("SecIntro");

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


    public void UserChoiceSecratary()
    {
        // Displays choices. get status, EndGame(Skal der her være mulighed for at underskrive det endelige elnet?)
        Console.WriteLine("Hvad kan jeg hjælpe dig med?");
        Console.WriteLine("Hvis du vil se dit elnets status, skriv: GetStatus");
        Console.WriteLine("Hvis du vil afslutte spillet og aflevere dit endelige elnet, skriv: Submit");

        string? UserInput = Console.ReadLine();

        if(UserInput == "GetStatus")
        {
            Status();
        }
            
        else if(UserInput== "submit")
        {
            Submit();
        }
       
        else
        {
            System.Console.WriteLine("Det forstod jeg ikke");
        }
    }

    public void Status()
    {
        Console.WriteLine("Status:");
        Console.WriteLine(GameAssets.budget.GetStatus());
        Console.WriteLine(GameAssets.energi.GetStatus());
        Console.WriteLine(GameAssets.co2.GetStatus());

        Console.WriteLine("Vil du indlevere dit elnet? skriv: Submit");
        Console.WriteLine("Vil du fortsætte spillet? skriv: GoBack");

        string? UserInput = Console.ReadLine();

        if(UserInput == "Submit")
        {
            Submit();
        }
        else if(UserInput == "GoBack")
        {
            //???
        }
        else
        {
            Console.WriteLine("Det forstod jeg ikke");
        }

    }

    public void Submit()
    {
        Console.WriteLine("Dit endelige elnet:");
        Console.WriteLine(GameAssets.budget.GetStatus());
        Console.WriteLine(GameAssets.energi.GetStatus());
        Console.WriteLine(GameAssets.co2.GetStatus());

        //Print antal af forskellige energiformer.

        Console.WriteLine("Indsender data...");
        Evaluate();

        Console.WriteLine("Vil du afslutte spiller? skriv: EndGame");
        Console.WriteLine("Vil du fortsætte spillet? skriv: GoBack");

        string? UserInput = Console.ReadLine();

         if(UserInput == "EndGame")
        {
            EndGame();
        }
        else if(UserInput == "GoBack")
        {
            //???
        }
        else
        {
            Console.WriteLine("Det forstod jeg ikke");
        }

    }

    private void Evaluate()
    {
        Console.WriteLine("Spillet afsluttes...");
        //???
    }

    private void EndGame()
    {

    }

}

