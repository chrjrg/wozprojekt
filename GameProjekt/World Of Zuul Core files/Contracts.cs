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
            case var wind when wind == db.GetSection("EnergyWindName"):
                HandleContract(context, "WindContract", db.GetSection("ContractWindName"), WindType, ref Resource.WindAmount);
                break;
            case var water when water == db.GetSection("EnergyWaterName"):
                HandleContract(context, "WaterContract", db.GetSection("ContractWaterName"), WaterType, ref Resource.WaterAmount);
                break;
            case var solar when solar == db.GetSection("EnergySolarName"):
                HandleContract(context, "SunContract", db.GetSection("ContractSolarName"), SolarType, ref Resource.SolarAmount);
                break;
            case var atom when atom == db.GetSection("EnergyAtomName"):
                HandleContract(context, "AtomContract", db.GetSection("ContractAtomName"), AtomType, ref Resource.AtomAmount);
                break;
            default:
                Console.WriteLine(db.GetSection("ContextErrorNoLocation"));
                break;
        }
    }

    public static void HandleContract(Context context, string contractKey, string itemName, EnergyType energyType, ref int resourceAmount)
    {
        string contractTemplate = db.GetSection("Contract"); // Fetch contract template from database
        Console.WriteLine($"{db.GetSection("ContractAmount1")} {itemName} {db.GetSection("ContractAmount2")}");
        if (!int.TryParse(Console.ReadLine(), out int amount) || amount <= 0)
        {
            Console.WriteLine(db.GetSection("ContractInvalidAmount"));
            HandleContract(context, contractKey, itemName, energyType, ref resourceAmount); // Recursive retry
            return;
        }

        Console.Clear();

        // Fill in contract details
        string updatedContract = contractTemplate
            .Replace(db.GetSection("ContractReplaceAmount"), amount.ToString())
            .Replace(db.GetSection("ContractReplaceType"), itemName)
            .Replace(db.GetSection("ContractReplaceName"), quiz.GetUserName().ToString())
            .Replace("DATE", DateTime.Now.ToString("dd/MM/yyyy"));

        // Display contract and ask for signature
        Contract contract = new Contract(updatedContract);
        Console.WriteLine(updatedContract);
        if (contract.SignContract() && EnergyStore.BuyEnergy(energyType, amount)) // If contract is signed and purchase is successful
        {
            resourceAmount += amount; // Update resource amount
            //EnergyStore.BuyEnergy(energyType, antal); // Finalize purchase
            Console.WriteLine($"\n{db.GetSection("ContractSuccessful")} {amount} {itemName}\n");
            context.TransitionBackHere();
        }
    }

//SignContract skal få brugeren til at skrive sit input så det kan tjekkes
    public bool SignContract()
    {
        Console.WriteLine(db.GetSection("ContractSign"));
        string input=Console.ReadLine()!.ToLower();//tager spillerinput
        if(input==db.GetSection("BooleanDecisionYes"))//Sammenligner spillerindput med username for underskrivelse af kontrakten
        {
            Console.Clear();
            return true;
        }
        else if (input==db.GetSection("BooleanDecisionNo")) {
            Console.Clear();
            System.Console.WriteLine("\n" + db.GetSection("ContractNotSigned") + "\n");
            context.TransitionBackHere();
            return false;
        } else {
            Console.WriteLine("\n" + db.GetSection("InputError") + "\n");
            SignContract();
            return false;
        }   
    }   
}