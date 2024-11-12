

/* Command for Secretary */
class CmdSecretary : BaseCommand, ICommand
{
    public CmdSecretary() //constructor 
    {
    }


    public void Execute(Context context, string command, string[] parameters) //implementation of method 
    {
        Console.WriteLine("The secretary says: 'Here's the current status .........");
        //promt skal være på baggrund af en anden funktion der trækker parameter fra spillet, som ikke er lavet da jeg ikke havde parameter
    }
}


/* Command for Expert */
class CmdExpert : BaseCommand, ICommand
{
    public CmdExpert() //constructor 
    {
    }

    public void Execute(Context context, string command, string[] parameters) //implementation of method
    {
        Console.WriteLine("The expert says .............");
        SignContract();
    }
}

/* Command for Driver*/
class CmdDriver : BaseCommand, ICommand
{
    public CmdDriver() //constructor 
    {
    }

    public void Execute(string command) //implementation of method
    {
        Console.WriteLine("Hvor vil du gerne hen? 1. Solcelleanlæg 2. Vindmøllepark 3. Atomkraftværk 4. Vankraftværk 5. Christiansborg");
        string place = Console.ReadLine();
        switch (place)
        {
            case "1":
                place = "Solcelleanlæg";
                break;
            case "2":
                place = "Vindmøllepark";
                break;
            case "3":
                place = "Atomkraftværk";
                break;
            case "4":
                place = "Vandkraftværk";
                break;
            case "5":
                place = "Christiansborg";
                break;
            default:
                Console.WriteLine("Ugyldigt svar. Vælg venlist en af de fem anviste");
                return;
        }
        Console.WriteLine("I will take you to " + place);
        context.Transition(place);
    }
}
