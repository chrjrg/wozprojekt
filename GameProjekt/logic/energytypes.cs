/* 
Class for energy types and inventory management
*/
using static GameAssets;

public class EnergyType // Represents different energy types with key parameters
{
    public string Name { get; }
    public double Price { get; }
    public double EnergyOutput { get; }
    public double CO2Emission { get; }
    public double Stability { get; }

    // Constructor for initializing an energy type with specified properties
    public EnergyType(string name, double price, double energyOutput, double co2Emission, double stability)
    {
        Name = name;
        Price = price;
        EnergyOutput = energyOutput;
        CO2Emission = co2Emission;
        Stability = stability;
    }

    public double GetPrice()
    {
        return Price; // Returns the price of the energy type
    }
}

public static class EnergyStore // Manages the purchasing and inventory of energy types
{
    // Buys a specified quantity of energy type and updates the relevant stats
    public static bool BuyEnergy(EnergyType energyType, int quantity) {  
        double totalCost = energyType.Price * quantity;
        double totalEnergyOutput = energyType.EnergyOutput * quantity;
        double totalCO2Emission = energyType.CO2Emission * quantity;

        if (budget.GetValue() >= totalCost) { // Checks if there are sufficient funds
            budget.Adjust(-totalCost); // Deducts the cost from the budget
            energi.Adjust(totalEnergyOutput);  // Adds energy output to the total
            co2.Adjust(totalCO2Emission); // Adds CO2 emission to the total

            inventory.AddEnergy(energyType, quantity); // Updates the inventory with the purchased energy
            return true;
        }
        else {
            Console.WriteLine(db.GetSection("BuyInsufficientFunds") + "\n"); // Displays error if funds are insufficient
            context.TransitionBackHere(); // Returns to the previous location
            return false;
        }
    }

    // Displays the current inventory of energy types
    public static void ShowInventory(){
        inventory.PrintInventory(); // Prints the current inventory status
    }
}

public class EnergyInventory // Manages the inventory of energy types
{
    private Dictionary<string, int> energyCounts = new Dictionary<string, int>(); // Stores energy counts by name

    // Adds energy to the inventory
    public void AddEnergy(EnergyType energyType, int quantity)
    {
        if (energyCounts.ContainsKey(energyType.Name)) {
            energyCounts[energyType.Name] += quantity; // Updates existing energy type quantity
        }
        else {
            energyCounts[energyType.Name] = quantity; // Adds new energy type with quantity
        }
    }

    // Returns the quantity of a specific energy type
    public int GetQuantity(EnergyType energyType)
    {
        return energyCounts.ContainsKey(energyType.Name) ? energyCounts[energyType.Name] : 0; 
    }

    // Prints the inventory of energy types and their quantities
    public void PrintInventory()
    {
        if (!energyCounts.Any()) {
            return; // Exits if there are no energy types in the inventory
        } else {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write(db.GetSection("EnergyInventoryHeader") + " ");
            Console.ForegroundColor = ConsoleColor.White; 
            Console.Write(db.GetSection("EnergyInventoryHeader2") + "\n");
            Console.WriteLine("______________________________________________" + "\n");
            foreach (var energy in energyCounts) { // Loops through energy types
                Console.ForegroundColor = ConsoleColor.DarkBlue;
                Console.Write($"{energy.Key}: "); Console.ForegroundColor = ConsoleColor.White;
                Console.Write($"{energy.Value}"); Console.Write(" " + db.GetSection("EnergyInventoryUnit") + "\n");
            }
            Console.WriteLine("______________________________________________" + "\n");
            Console.WriteLine($"{db.GetSection("StabilityPrefix")} {Math.Round(CalculateOverallStability(AtomType, SolarType, WindType, WaterType), 1)}%");
            Console.WriteLine("______________________________________________" + "\n");
            Console.ResetColor();
            context.ClickNext(); 
            Console.Clear();
            secretary.UserChoiceSecretary(); // Prompts user to make a choice after printing the inventory
        }
    }

    // Calculates the overall stability based on energy type quantities and their stability
    public double CalculateOverallStability(EnergyType atom, EnergyType solar, EnergyType wind, EnergyType water)
    {
        double atomAmount = GetQuantity(atom); 
        double solarAmount = GetQuantity(solar); 
        double windAmount = GetQuantity(wind); 
        double waterAmount = GetQuantity(water); 
    
        double totalOutput = (atom.EnergyOutput * atomAmount) + 
                             (solar.EnergyOutput * solarAmount) + 
                             (wind.EnergyOutput * windAmount) + 
                             (water.EnergyOutput * waterAmount);

        if (totalOutput == 0) {
            return 0; // Avoids division by zero
        }
        double overallStability = ((atom.EnergyOutput * atom.Stability * atomAmount) + 
                                   (solar.EnergyOutput * solar.Stability * solarAmount) + 
                                   (wind.EnergyOutput * wind.Stability * windAmount) + 
                                   (water.EnergyOutput * water.Stability * waterAmount)) / totalOutput;
        return overallStability;
    }

    internal static int GetPrice(object price)
    {
        throw new NotImplementedException(); // Not implemented method
    }
}