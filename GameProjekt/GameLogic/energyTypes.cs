
public class EnergyType
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

public static class Test
{
    public static Resource budget = new Resource("Budget", 100000, "kr");
    public static Resource energy = new Resource("Energi", 0, "kW");
    public static Resource co2 = new Resource("CO₂-udledning", 0, "tons");

    public static void BuyEnergy(EnergyType energyType, int quantity)
    {
        double totalCost = energyType.Price * quantity;
        double totalEnergyOutput = energyType.EnergyOutput * quantity;
        double totalCO2Emission = energyType.CO2Emission * quantity;

        if (budget.GetValue() >= totalCost)
        {
            budget.Adjust(-totalCost);
            energy.Adjust(totalEnergyOutput);
            co2.Adjust(totalCO2Emission);

            Console.WriteLine($"Købt {quantity} {energyType.Name} til en samlet pris på {totalCost} kr. " +
                              $"Energi tilføjet: {totalEnergyOutput} kW, CO₂-udledning tilføjet: {totalCO2Emission} tons.");
        }
        else
        {
            Console.WriteLine("Ikke nok penge til dette køb.");
        }
    }
}

