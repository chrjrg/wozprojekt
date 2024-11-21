///* 
//Klassen contracts skal printe contract teskt fra en .txt fil og have instancer af contracts, med parametrene Context ContractName og ContractTekst for hvert rum af vind vand sol og atomkræft.
//Disse contracts skal så kunne køre en af to metoder som er SignContract eller Don'tSignContract for enten at gennemføre købet eller anullere købet.

using static GameAssets;
class Contract 
{
    public Contract(string ContractText){}
    public static void CreateContract()
    {
        //check hvilket rum spilleren er i og brug txt fil til dette til skabelsen af kontrakten
        switch (context.GetCurrentName())
        {
            case "Vindanlæg":
                Contract WindContract=new Contract(db.GetSection("WindContract"));
                System.Console.WriteLine(WindContract);
                break;
            case "Vandanlæg":
                Contract WaterContract=new Contract(db.GetSection("WaterContract"));
                System.Console.WriteLine(WaterContract);
                break;
            case "Solanlæg":
                Contract SunContract=new Contract(db.GetSection("SunContract"));
                System.Console.WriteLine(SunContract);
                break;
            case "Atomkraftværk" :
                Contract AtomContract=new Contract(db.GetSection("AtomContract"));
                System.Console.WriteLine(AtomContract);
                break;
        }
    }

    /*public bool SignContract(string input)
    {
        if(input==quiz.GetUserName())
        {
            System.Console.WriteLine($"Kontrakten er underskrevet af {input}");
            return true;
        }
        else
        {
            System.Console.WriteLine($"Kontrakten blev ikke Underskrevet");
            return false;
        }   
    }*/
}

     