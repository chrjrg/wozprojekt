using static GameAssets;

//Klassen indeholder oplysninger om pris, navn, energyoutput og co2udledning.
//Klassen EnergyType bruges i GameAssetInit til at definere energityperne og tildele dem navn og værdier.
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
    //Denne metode kaldes når der købes nye energiformer. Den ændre parametrende, alt efter energitype og antal.
    public static void BuyEnergy(EnergyType energyType, int quantity)
    {
        double totalCost = energyType.Price * quantity;
        double totalEnergyOutput = energyType.EnergyOutput * quantity;
        double totalCO2Emission = energyType.CO2Emission * quantity;

        if (budget.GetValue() >= totalCost) //Hvos total omkostningen, for den købte energiform er mindre eller lig med budget...
        {
            budget.Adjust(-totalCost); //trækker totalCost fra budget
            energi.Adjust(totalEnergyOutput);  //tilføjer Energiforsyningen fra den købte energiform til parameteren energyOutput.
            co2.Adjust(totalCO2Emission); //Tilføjer co2 udledningen fra den købte energiform til parameteren co2Emission.

            Console.WriteLine($"Købt {quantity} {energyType.Name} til en samlet pris på {totalCost} kr. " +
                              $"Energi tilføjet: {totalEnergyOutput} kW, CO₂-udledning tilføjet: {totalCO2Emission} tons.");
        }
        else
        {
            Console.WriteLine("Ikke nok penge til dette køb."); //Hvis ikke penge nok printer den, dene besked.
        }
    }
}

