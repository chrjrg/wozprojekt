using static GameAssets;

public class Resource
{
    private double value;
    private string name;
    private string unit;


    public Resource(string name,double initialValue,string unit)
    {
        this.name = name;
        this.value = initialValue;
        this.unit = unit;
    }

    public double GetValue() => value;

    public void Adjust(double amount) => value += amount;

    public string GetStatus() => $"{name}: {value} {unit}";

    
}


