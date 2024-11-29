/*
Class for energy types and inventory management
*/
using static GameAssets;

public class EnergyType // This class is used to create new energy types, with the parameters: Name, Price, EnergyOutput and CO2Emission.
{
    public string Name { get; }
    public double Price { get; }
    public double EnergyOutput { get; }
    public double CO2Emission { get; }

    public EnergyType(string name, double price, double energyOutput, double co2Emission)
    {
        Name = name;
        Price = price;
        EnergyOutput = energyOutput;
        CO2Emission = co2Emission;
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
            Console.WriteLine("Ikke nok penge til dette køb."); //Hvis ikke penge nok printer den, dene besked.
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

    // Hent antallet for en specifik energitype
    public int GetQuantity(EnergyType energyType)
    {
        return energyCounts.ContainsKey(energyType.Name) ? energyCounts[energyType.Name] : 0;
    }

    // Udskriv lagerstatus
    public void PrintInventory()
    {
        if (!energyCounts.Any()){
            return;
        } else {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("Energilager: ");
            Console.ForegroundColor = ConsoleColor.White; 
            Console.Write("Oversigt over energikilder");
            Console.WriteLine("");

            Console.WriteLine("______________________________________________");
            Console.WriteLine("");
            foreach (var energy in energyCounts)
            {     
   
                Console.ForegroundColor = ConsoleColor.DarkBlue;
                Console.Write($"{energy.Key}: ");Console.ForegroundColor = ConsoleColor.White;
                Console.Write($"{energy.Value}");Console.Write(" stk.");System.Console.WriteLine("");
                

            }
            Console.WriteLine("______________________________________________");
            Console.WriteLine("");
            Console.ResetColor();
            context.ClickNext();
            Console.Clear();
            secretary.UserChoiceSecretary();
        }

    }
}