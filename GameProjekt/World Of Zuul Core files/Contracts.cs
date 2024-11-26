using System.ComponentModel;
using System.Reflection.Metadata.Ecma335;
using static GameAssets;
public class Contract 
{
    private static string ?current;

    public Contract(string ContractText){}

    public static void CreateContract(Context context)
    {
        current = context.GetCurrentName().ToString();
        //check hvilket rum spilleren er i og brug txt fil til dette til skabelsen af kontrakten
        Console.Clear();
        switch (current)
        {
            case "Vindanlæg":
                string ContractTextWind=db.GetSection("WindContract");//laver en string ud fra textdatabasen
                System.Console.WriteLine($"Hvor mange vindmøller vil du købe");
                int WindAntal=int.Parse(Console.ReadLine()!);//tager spillerindput for hvor mange der skal købes
                if (WindAntal > 0)
                {
                    string UpdatedContract=ContractTextWind
                    .Replace("ANTAL", WindAntal.ToString())//erstatter ordet ANTAL i teksten Contracttext med nummered Antal som string
                    .Replace("NAVN", quiz.GetUserName().ToString())
                    .Replace("DATO", DateTime.Now.ToString("dd/MM/yyyy"));
                    Contract WindContract=new Contract(UpdatedContract);//instansiere Windcontract så SignContract metoden virker på contrakten
                    System.Console.WriteLine(UpdatedContract);//Skriver kontrakten for spilleren
                    bool signiture=WindContract.SignContract();
                    if(signiture==true && Test.BuyEnergy(WindType,WindAntal) == true)
                    {
                        for (int i = 0; i < WindAntal; i++)
                        {
                            Resource.WindAmount++;
                        }
                        Test.BuyEnergy(WindType,WindAntal);//Fuldener købet ved at ændre de overordnede parametre for Balance, Energi og CO2.
                    }
                }
                else
                {
                    Console.WriteLine("Noget gik galt. Venligst prøv igen og sørg for at bruge hele tal over 0");
                    CreateContract(context);// Burde loop tilbage til hvor mange du vil købe.
                }
                break;
            case "Vandanlæg":
                string ContractTextWater= db.GetSection("WaterContract");//Laver en string ud fra textdatabasen
                System.Console.WriteLine($"Hvor mange vindmøller vil du købe");
                int WaterAntal=int.Parse(Console.ReadLine()!);//Tager spillerindput for hvor mange der skal købes
                if (WaterAntal > 0)
                {
                    string UpdatedContract=ContractTextWater.Replace("ANTAL",WaterAntal.ToString());//erstatter ordet ANTAL i teksten Contracttext med nummered Antal som string
                    Contract WaterContract=new Contract(UpdatedContract);//Instansiere Windcontract så SignContract metoden virker på contrakten
                    System.Console.WriteLine(UpdatedContract);//Skriver kontrakten for spilleren
                    bool signiture=WaterContract.SignContract();
                    if(signiture==true && Test.BuyEnergy(WaterType,WaterAntal) == true)
                    {
                        for (int i = 0; i < WaterAntal; i++) //øger mængden af vandværker gennem forloopet
                        {
                            Resource.WaterAmount++;
                        }
                        Test.BuyEnergy(WaterType,WaterAntal);//fuldener købet ved at ændre de overordnede parametre for Balance, Energi og CO2.
                    }
                }
                else
                {
                    Console.WriteLine("Noget gik galt. Venligst prøv igen og sørg for at bruge hele tal over 0");
                    CreateContract(context);
                }
                break;
            case "Solanlæg":
                string ContractTextSun=db.GetSection("SunContract");//laver en string ud fra textdatabasen
                System.Console.WriteLine($"Hvor mange vindmøller vil du købe");
                int SunAntal=int.Parse(Console.ReadLine()!);//tager spillerindput for hvor mange der skal købes
                if (SunAntal > 0)
                {
                    string UpdatedContract=ContractTextSun.Replace("ANTAL",SunAntal.ToString());//erstatter ordet ANTAL i teksten Contracttext med nummered Antal som string
                    Contract SunContract=new Contract(UpdatedContract);//instansiere Windcontract så SignContract metoden virker på contrakten
                    System.Console.WriteLine(UpdatedContract);//Skriver kontrakten for spilleren
                    bool signiture=SunContract.SignContract();
                    if(signiture==true && Test.BuyEnergy(SolarType,SunAntal) == true)
                    {
                        for (int i = 0; i < SunAntal; i++)
                        {
                            Resource.SunAmount++;
                        }
                        Test.BuyEnergy(SolarType,SunAntal);//fuldener købet ved at ændre de overordnede parametre for Balance, Energi og CO2.

                    }
                }
                else
                {
                    Console.WriteLine("Noget gik galt. Venligst prøv igen og sørg for at bruge hele tal over 0");
                    CreateContract(context);
                }
                break;
            case "Atomkraftværk" :
                string ContractTextAtom=db.GetSection("AtomContract");//laver en string ud fra textdatabasen
                System.Console.WriteLine($"Hvor mange vindmøller vil du købe");
                int AtomAntal=int.Parse(Console.ReadLine()!);//tager spillerindput for hvor mange der skal købes
                if (AtomAntal > 0)
                {
                    string UpdatedContract=ContractTextAtom.Replace("ANTAL",AtomAntal.ToString());//erstatter ordet ANTAL i teksten Contracttext med nummered Antal som string
                    Contract AtomContract=new Contract(UpdatedContract);//instansiere Windcontract så SignContract metoden virker på contrakten
                    System.Console.WriteLine(UpdatedContract);//Skriver kontrakten for spilleren
                    bool signiture=AtomContract.SignContract();
                    if(signiture==true && Test.BuyEnergy(AtomType,AtomAntal) == true)
                    {
                        for (int i = 0; i < AtomAntal; i++)
                        {
                            Resource.AtomAmount++;
                        }
                        Test.BuyEnergy(AtomType,AtomAntal);//fuldener købet ved at ændre de overordnede parametre for Balance, Energi og CO2.
                    }
                }
                else
                {
                    Console.WriteLine("Noget gik galt. Venligst prøv igen og sørg for at bruge hele tal over 0");
                    CreateContract(context);
                }
                break;
        }
    }

//SignContract skal få brugeren til at skrive sit input så det kan tjekkes
    public bool SignContract()
    {
        System.Console.WriteLine($"Hvis du gerne vil købe så skriv ja, ellers nej");
        string input=Console.ReadLine()!;//tager spillerinput
        if(input=="ja")//Sammenligner spillerindput med username for underskrivelse af kontrakten
        {
            Console.Clear();
            System.Console.WriteLine($"Kontrakten er underskrevet" + "\n");
            context.TransitionBackHere();
            return true;
        }
        else if (input=="nej") {
            System.Console.WriteLine($"Kontrakten blev ikke Underskrevet");
            context.TransitionBackHere();
            return false;
        } else {
            Console.WriteLine("Det forstod jeg ikke..");
            SignContract();
            return false;
        }   
        }   
    }

     



     