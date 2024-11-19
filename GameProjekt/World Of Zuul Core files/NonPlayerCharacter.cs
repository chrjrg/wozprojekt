using System.Net;
using System.Transactions;

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

public class Secretary : NPC
{
    public Secretary(string name) : base(name) {}
        
     public override void Interact()
    {
        UserChoiceSecratary();
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
    public class Expert : NPC
    {
        public Expert(string name) : base(name)
        {
        }
        public override void Interact()
        {
        }
    }


