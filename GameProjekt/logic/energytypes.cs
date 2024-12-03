/*
Class for energy types and inventory management
*/
using static GameAssets;

public class EnergyType // This class is used to create new energy types, with the parameters: Name, Price, EnergyOutput, CO2Emission and stability.
{
    public string Name { get; }
    public double Price { get; }
    public double EnergyOutput { get; }
    public double CO2Emission { get; }
    public double Stability { get; }

    // Constructor for EnergyType
    public EnergyType(string name, double price, double energyOutput, double co2Emission, double stability)
    {
        Name = name;
        Price = price;
        EnergyOutput = energyOutput;
        CO2Emission = co2Emission;
        Stability = stability;
    }
}

public static class EnergyStore // This class is used to manage the inventory of energy types.
{
    public static bool BuyEnergy(EnergyType energyType, int quantity) {  
        double totalCost = energyType.Price * quantity;
        double totalEnergyOutput = energyType.EnergyOutput * quantity;
        double totalCO2Emission = energyType.CO2Emission * quantity;

        if (budget.GetValue() >= totalCost) //Hvos total omkostningen, for den købte energiform er mindre eller lig med budget...
        {
            budget.Adjust(-totalCost); //trækker totalCost fra budget
            energi.Adjust(totalEnergyOutput);  //tilføjer Energiforsyningen fra den købte energiform til parameteren energyOutput.
            co2.Adjust(totalCO2Emission); //Tilføjer co2 udledningen fra den købte energiform til parameteren co2Emission.

            inventory.AddEnergy(energyType, quantity); // Opdater lagerstatus
            return true;
        }
        else
        {
            Console.WriteLine(db.GetSection("BuyInsufficientFunds") + "\n"); //Hvis ikke penge nok printer den, dene besked.
            context.TransitionBackHere(); //Går tilbage til den tidligere lokation.
            return false;
        }
    }
        public static void ShowInventory(){
        inventory.PrintInventory(); // Udskriv lagerstatus
        }
}

public class EnergyInventory // This class is used to manage the inventory of energy types.
{
    // Dictionary til at gemme antallet af hver energitype
    private Dictionary<string, int> energyCounts = new Dictionary<string, int>();

    // Tilføj købt energi
    public void AddEnergy(EnergyType energyType, int quantity)
    {
        if (energyCounts.ContainsKey(energyType.Name))
        {
            energyCounts[energyType.Name] += quantity; // Opdater antallet
        }
        else
        {
            energyCounts[energyType.Name] = quantity; // Opret ny energitype med antal
        }
    }
    
    // Returns quantity of energy type
    public int GetQuantity(EnergyType energyType)
    {
        return energyCounts.ContainsKey(energyType.Name) ? energyCounts[energyType.Name] : 0;
    }

    // Prints the inventory
    public void PrintInventory()
    {
        if (!energyCounts.Any()){
            return;
        } else {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write(db.GetSection("EnergyInventoryHeader") + " ");
            Console.ForegroundColor = ConsoleColor.White; 
            Console.Write(db.GetSection("EnergyInventoryHeader2") + "\n");
            Console.WriteLine("______________________________________________" + "\n");
            foreach (var energy in energyCounts)
            {     
                Console.ForegroundColor = ConsoleColor.DarkBlue;
                Console.Write($"{energy.Key}: ");Console.ForegroundColor = ConsoleColor.White;
                Console.Write($"{energy.Value}");Console.Write(" " + db.GetSection("EnergyInventoryUnit") + "\n");
            }
            Console.WriteLine("______________________________________________" + "\n");
            Console.WriteLine($"{db.GetSection("StabilityPrefix")} {Math.Round(CalculateOverallStability(AtomType, SolarType, WindType, WaterType), 1)}%");
            Console.WriteLine("______________________________________________" + "\n");
            Console.ResetColor();
            context.ClickNext();
            Console.Clear();
            secretary.UserChoiceSecretary();
        }
    }

    // Calculate overall stability
    public double CalculateOverallStability(EnergyType atom, EnergyType solar, EnergyType wind, EnergyType water)
    {
        double atomAmount = GetQuantity(atom); // Get the quantity of 'atom' energy type from the inventory
        double solarAmount = GetQuantity(solar); // ... 'solar' energy type ...
        double windAmount = GetQuantity(wind); // ... 'wind' energy type ...
        double waterAmount = GetQuantity(water); // ... 'water' energy type ...
    
        double totalOutput = (atom.EnergyOutput * atomAmount) + 
                             (solar.EnergyOutput * solarAmount) + 
                             (wind.EnergyOutput * windAmount) + 
                             (water.EnergyOutput * waterAmount);

        if (totalOutput == 0)
        {
            return 0; // division by zero issue solved
        }
        double overallStability = ((atom.EnergyOutput * atom.Stability * atomAmount) + 
                                   (solar.EnergyOutput * solar.Stability * solarAmount) + 
                                   (wind.EnergyOutput * wind.Stability * windAmount) + 
                                   (water.EnergyOutput * water.Stability * waterAmount)) / totalOutput;
        return overallStability;
    }
}