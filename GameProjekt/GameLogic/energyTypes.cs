using static GameAssets;
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

    public static void BuyEnergy(EnergyType energyType, int quantity)
    {
        double totalCost = energyType.Price * quantity;
        double totalEnergyOutput = energyType.EnergyOutput * quantity;
        double totalCO2Emission = energyType.CO2Emission * quantity;

        if (budget.GetValue() >= totalCost)
        {
            budget.Adjust(-totalCost);
            energi.Adjust(totalEnergyOutput);
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

