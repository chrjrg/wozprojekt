using static GameAssets;

//Her opretter vi klassen Resource, som indeholder vores parametre/resources.
public class Resource{
    private string name;
    private double value;
    private string unit;


    public Resource(string name,double initialValue,string unit)
    {
        this.name = name;
        this.value = initialValue;
        this.unit = unit;
    }

    //Metoder, der kan hente de variable.
    public double GetValue() => value;

    public string GetName() => name;

    public void Adjust(double amount) => value += amount;  //Denne kan tilføje en amount til value, hvis nødvendigt.

    public string GetFormattedValue()
    {
        switch (unit)
        {
            case "kr":
                if (value >= 1_000_000_000) return $"{value / 1_000_000_000:0.##} mia. {unit}";
                else if (value >= 1_000_000) return $"{value / 1_000_000:0.##} mio. {unit}";
                else if (value >= 1_000) return $"{value / 1_000:0.##} lapper {unit}";
                else return $"{value:0.##} {unit}";
            default:
                return $"{value:0.##} {unit}";
        }
    }


    public void GetStatus(){
        // Gemmer den nuværende farve for at kunne gendanne den senere
        var originalColor = Console.ForegroundColor;

        // Skift farve til grøn for "name"
        Console.ForegroundColor = ConsoleColor.Green;
        Console.Write(name);

        // Skift farve til hvid for resten
        Console.ForegroundColor = ConsoleColor.White;

        switch (unit)
        {
            case "kr":
                if (value >= 1_000_000_000) // Milliarder
                {
                    Console.WriteLine($": {value / 1_000_000_000:0.##} mia. {unit}");
                }
                else if (value >= 1_000_000) // Millioner
                {
                    Console.WriteLine($": {value / 1_000_000:0.##} Mio. {unit}");
                }
                else if (value >= 1_000) // Tusinder
                {
                    Console.WriteLine($": {value / 1_000:0.##} lapper {unit}");
                }
                else
                {
                    Console.WriteLine($": {value:0.##} {unit}");
                }
                break;

            default:
                Console.WriteLine($": {value:0.##} {unit}");
                break;
        }

        // Gendanner den oprindelige farve
        Console.ForegroundColor = originalColor;
    }

    public static void DisplayAllStatuses(params Resource[] resources)
    {
        Console.ForegroundColor = ConsoleColor.White; 
        Console.WriteLine("______________________________________________");Console.WriteLine("");
        Console.ForegroundColor = ConsoleColor.Green;
        Console.Write("Dine parametre: ");
        Console.ForegroundColor = ConsoleColor.White; 
        Console.Write("Din nuværende status");
        Console.WriteLine("");Console.WriteLine("______________________________________________");
        foreach (var resource in resources)
        {
            Console.ForegroundColor = ConsoleColor.Green; // Farve for navn
            System.Console.WriteLine("");
            Console.Write(" - " + resource.GetName() + ": ");
            Console.ForegroundColor = ConsoleColor.White; // Farve for værdien
            Console.WriteLine(resource.GetFormattedValue());
        }

        // Gendan standardfarven
        Console.ResetColor();
        Console.WriteLine("______________________________________________");
        System.Console.WriteLine("");
        Test.ShowInventory();
    }


}

