using static GameAssets;

//Her opretter vi klassen Resource, som indeholder vores parametre/resources.
public class Resource
{
    private string name;
    private double value;
    private string unit;


    public static int WindAmount=0;
    public static int WaterAmount=0;
    public static int SunAmount=0;
    public static int AtomAmount=0;


    public Resource(string name,double initialValue,string unit)
    {
        this.name = name;
        this.value = initialValue;
        this.unit = unit;
    }

    //Metoder, der kan hente de variable.
    public double GetValue() => value;

    public void Adjust(double amount) => value += amount;  //Denne kan tilføje en amount til value, hvis nødvendigt.

    public string GetStatus() => $"{name}: {value} {unit}"; //Denne henter status for parametrende.
}