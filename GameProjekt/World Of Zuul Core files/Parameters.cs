using System.Reflection.Metadata;
using System.Security.Cryptography;
public class Parameters
{
    string cantAfford = "Det har du desværre ikke penge til.";
    private string name;
    private double value;
    private string type;
    private double energi1;
    private double co21;
   
   //Price
    public double atomPrice= 10;
    public double windPrice= 444000000;
    public double waterPrice= 500000000;
    public double sunPrice= 130000000;
   
   //Energi
    public double atomEnergi;
    public double windEnergi;
    public double waterEnergi;
    public double sunEnergi;
    //CO2
    public double atomCo2;
    public double windCo2;
    public double waterCo2;
    public double sunCo2;
    //Constructors 
    public Parameters(string n, double v, string t)
    {
        name = n;
        value = v;
        type = t;
    }
    //Methods
    public string GetName()
    {
        return name;
    }
    public double GetValue()
    {
        return value;
    }
    public double GetEnergi()
    {
        return energi1;
    }
     public double GetCo2()
    {
        return co21;
    }
    public string GetStatus()
    {
        return name + ": " + value+ " "+type;
    }

    //Method to buy Atom
    public void BuyAtom(Parameters placeholderMoney ,int antal)
    {
        if(placeholderMoney.GetValue() !< atomPrice * antal)
        {
            this.value -= atomPrice * antal;
            System.Console.WriteLine($"Du har nu gennemført dit køb af {antal} atomkræftværker til prisen: {atomPrice * antal} kr.");
        }
        else
        {
            System.Console.WriteLine(cantAfford);
        }
    }

    //Method to buy wind
    public void BuyWind(int antal)
    {
        if(value !< windPrice)
        {
            value -= windPrice * antal;
            energi1 += windEnergi * antal;
            co21 += windCo2 * antal;
            System.Console.WriteLine($"Du har gennemført dit køb af {antal} vindmølleparker til prisen {windPrice * antal} kr.");
        }
        else
        {
            System.Console.WriteLine(cantAfford);
        }
    }
    //Method to buy water
     public void BuyWater(int antal)
    {
        if(value !< waterPrice)
        {
            value -= waterPrice * antal;
            energi1 += waterEnergi * antal;
            co21 += waterCo2 * antal;
            System.Console.WriteLine($"Du har gennemført dit køb af {antal} vindmølleparker til prisen {waterPrice * antal} kr.");
        }
        else
        {
            System.Console.WriteLine(cantAfford);
        }
    }
    //Method to buy sun
     public void BuySun(int antal)
    {
        if(value !< sunPrice)
        {
            value -= sunPrice * antal;
            energi1 += sunEnergi * antal;
            co21 += sunCo2 * antal;
            System.Console.WriteLine($"Du har gennemført dit køb af {antal} vindmølleparker til prisen {sunPrice * antal} kr.");
        }
        else
        {
            System.Console.WriteLine(cantAfford);
        }
    }
    public static implicit operator Parameter(Parameters v)
    {
        throw new NotImplementedException();
    }
}

