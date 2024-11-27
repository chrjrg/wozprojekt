using static GameAssets;

public class Contract
{ 
    private static string? current;

    public Contract(string contractText){}
    
    public static void CreateContract(Context context)
    {
        current = context.GetCurrentName().ToString();
        Console.Clear();

        switch (current)
        {
            case "Vindanlæg":
                HandleContract(context, "WindContract", "vindmøller", WindType, ref Resource.WindAmount);
                break;
            case "Vandanlæg":
                HandleContract(context, "WaterContract", "vandanlæg", WaterType, ref Resource.WaterAmount);
                break;
            case "Solanlæg":
                HandleContract(context, "SunContract", "solceller", SolarType, ref Resource.SolarAmount);
                break;
            case "Atomkraftværk":
                HandleContract(context, "AtomContract", "reaktorer", AtomType, ref Resource.AtomAmount);
                break;
            default:
                Console.WriteLine("Ukendt lokation.");
                break;
        }
    }

    public static void HandleContract(Context context, string contractKey, string itemName, EnergyType energyType, ref int resourceAmount)
    {
        string contractTemplate = db.GetSection("Contract"); // Fetch contract template from database
        Console.WriteLine($"Hvor mange {itemName} vil du købe?");
        if (!int.TryParse(Console.ReadLine(), out int antal) || antal <= 0)
        {
            Console.WriteLine("Noget gik galt. Venligst prøv igen og sørg for at bruge hele tal over 0");
            HandleContract(context, contractKey, itemName, energyType, ref resourceAmount); // Recursive retry
            return;
        }

        Console.Clear();

        // Fill in contract details
        string updatedContract = contractTemplate
            .Replace("ANTAL", antal.ToString())
            .Replace("TYPE", itemName)
            .Replace("NAVN", quiz.GetUserName().ToString())
            .Replace("DATO", DateTime.Now.ToString("dd/MM/yyyy"));

        // Display contract and ask for signature
        Contract contract = new Contract(updatedContract);
        Console.WriteLine(updatedContract);
        if (contract.SignContract() && Test.BuyEnergy(energyType, antal))
        {
            resourceAmount += antal; // Update resource amount
            Test.BuyEnergy(energyType, antal); // Finalize purchase
            Console.WriteLine($"\nKøbet er gennemført! Du har nu købt {antal} {itemName}\n");
            context.TransitionBackHere();
        }
    }

//SignContract skal få brugeren til at skrive sit input så det kan tjekkes
    public bool SignContract()
    {
        System.Console.WriteLine($"Hvis du gerne vil købe så skriv ja, ellers nej");
        string input=Console.ReadLine()!;//tager spillerinput
        if(input=="ja")//Sammenligner spillerindput med username for underskrivelse af kontrakten
        {
            Console.Clear();
            // System.Console.WriteLine($"Kontrakten er underskrevet" + "\n");
            return true;
        }
        else if (input=="nej") {
            Console.Clear();
            System.Console.WriteLine($"\nKontrakten blev ikke Underskrevet" + "\n");
            context.TransitionBackHere();
            return false;
        } else {
            Console.WriteLine("\nDet forstod jeg ikke.." + "\n");
            SignContract();
            return false;
        }   
        }   
    }