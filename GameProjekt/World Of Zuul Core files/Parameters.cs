using System.Reflection.Metadata;
using System.Security.Cryptography;
public class Parameters
{

    string cantAfford = "Det har du desværre ikke penge til.";

    private string name1;
    private double value1;
    private string type1;
    private double energi1;
    private double co21;
   
   //Price
    public double atomPrice= 37500000000;
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
    public Parameters(string name, double value, string type)
    {
        name1 = name;
        value1 = value;
        type1 = type;
 
    }

    //Methods
    public string GetName()
    {
        return name1;
    }
    public double GetValue()
    {
        return value1;
    }
    public double GetEnergi()
    {
        return energi1;
    }


    public string GetStatus()
    {
        return name1 + ": " + value1+ " "+type1;
    }

    //Method to buy Atom
    public void BuyAtom(int antal)
    {
        if(value1 !< atomPrice * antal)
        {
            value1 -= atomPrice * antal;
            energi1 += atomEnergi * antal;
            co21 += atomCo2 * antal;

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
        if(value1 !< windPrice)
        {
            value1 -= windPrice * antal;
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
        if(value1 !< waterPrice)
        {
            value1 -= waterPrice * antal;
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
        if(value1 !< sunPrice)
        {
            value1 -= sunPrice * antal;
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
