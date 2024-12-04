/* 
Contract class for handling energy contracts between the player and the game
*/
using static GameAssets;

public class Contract
{
    private static string? current; // Holds the name of the current space

    // Constructor: Initializes a new contract with the contract text (not currently used in this class)
    public Contract(string contractText) { }

    // Create a new contract based on the player's current location in the game
    public static void CreateContract(Context context)
    {
        current = context.GetCurrentName().ToString(); // Get the current location name
        Console.Clear();

        // Handle contract creation based on the current location
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
                Console.WriteLine(db.GetSection("ContextErrorNoLocation")); // Error if no valid location
                break;
        }
    }

    // Handle the contract logic for a specific energy type (wind, water, solar, or atom)
    public static void HandleContract(Context context, string contractKey, string itemName, EnergyType energyType, ref int resourceAmount)
    {
        string contractTemplate = db.GetSection("Contract"); // Fetch contract template from the database
        Console.WriteLine($"\n{db.GetSection("ContractUnitPrice")} {FormatValueWithUnit(energyType.GetPrice(), db.GetSection("MoneyUnit"))}");
        Console.WriteLine($"{db.GetSection("ContractBudget")} {FormatValueWithUnit(budget.GetValue(), db.GetSection("MoneyUnit"))}\n");
        Console.WriteLine($"{db.GetSection("ContractAmount1")} {itemName} {db.GetSection("ContractAmount2")}");

        // Ask for the amount to contract, and ensure it's valid
        if (!int.TryParse(Console.ReadLine(), out int amount) || amount <= 0)
        {
            Console.Clear();
            Console.WriteLine(db.GetSection("ContractInvalidAmount"));
            HandleContract(context, contractKey, itemName, energyType, ref resourceAmount); // Recursive retry if invalid input
            return;
        }

        Console.Clear();

        // Fill in the contract details with the user-provided amount and other dynamic values
        string updatedContract = contractTemplate
            .Replace(db.GetSection("ContractReplaceAmount"), amount.ToString())
            .Replace(db.GetSection("ContractReplaceType"), itemName)
            .Replace(db.GetSection("ContractReplaceName"), quiz.GetUserName().ToString())
            .Replace("DATE", DateTime.Now.ToString("dd/MM/yyyy"));

        // Display the contract and ask the user to sign it
        Contract contract = new Contract(updatedContract);
        Console.WriteLine(updatedContract);
        if (contract.SignContract() && EnergyStore.BuyEnergy(energyType, amount)) // If signed and purchase is successful
        {
            resourceAmount += amount; // Update the resource amount
            Console.WriteLine($"\n{db.GetSection("ContractSuccessful")} {amount} {itemName}\n");
            context.TransitionBackHere(); // Return to the previous space
        }
    }

    // Sign the contract by verifying the user's input (Yes/No decision)
    public bool SignContract()
    {
        Console.WriteLine(db.GetSection("ContractSign"));
        string input = Console.ReadLine()!.ToLower(); // Get user input for signing the contract
        if (input == db.GetSection("BooleanDecisionYes")) // If user agrees, return true
        {
            Console.Clear();
            return true;
        }
        else if (input == db.GetSection("BooleanDecisionNo")) // If user disagrees, return false
        {
            Console.Clear();
            Console.WriteLine("\n" + db.GetSection("ContractNotSigned") + "\n");
            context.TransitionBackHere(); // Return to the previous space without signing
            return false;
        }
        else
        {
            Console.WriteLine("\n" + db.GetSection("InputError") + "\n");
            SignContract(); // Retry if the input is invalid
            return false;
        }
    }

    // Format the value with its appropriate unit (e.g., money, energy)
    public static string FormatValueWithUnit(double value, string unit)
    {
        switch (unit)
        {
            case var currency when currency == db.GetSection("MoneyUnit"):
                // Format large values with appropriate units (Billion, Million, etc.)
                if (value >= 1_000_000_000) return $"{value / 1_000_000_000:0.##} {db.GetSection("BillionUnit")} {unit}";
                else if (value >= 1_000_000) return $"{value / 1_000_000:0.##} {db.GetSection("MillionUnit")} {unit}";
                else if (value >= 1_000) return $"{value / 1_000:0.##} {db.GetSection("ThousandUnit")} {unit}";
                else return $"{value:0.##} {unit}";
            default:
                return $"{value:0.##} {unit}"; // Default format for other units
        }
    }
}