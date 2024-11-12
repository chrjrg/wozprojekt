/* 
Klassen contracts skal printe contract teskt fra en .txt fil og have instancer af contracts, med parametrene Context ContractName og ContractTekst for hvert rum af vind vand sol og atomkræft.
Disse contracts skal så kunne køre en af to metoder som er SignContract eller Don'tSignContract for enten at gennemføre købet eller anullere købet.

*/

class Contract 
{
    public CreateContracts(ContractName)
    {
        Contract ContractName=new Contract;
        //check rummet og brug txt fil til rum hvor kontrakten bliver lavet
        switch (Context.GetCurrent)
        {
            case Context.GetCurrent==Wind:
                return "Wind.txt"
            case Context.GetCurrent==Water:
                return "Water.txt"
            case Context.GetCurrent==Sun
                return "Sun.txt"
            case Context.GetCurrent==Atom:
                return "Atom.txt"
            default:
        }
    }



    public bool SignContract(string "Input")
    {
        if(Input==Spillernavn)
        {
            return true
            System.Console.WriteLine($"Kontrakten {ContractName} er underskrevet af {Spillernavn}");    
        }
        else(Input!=Spillernavn)
        {
            return false
            System.Console.WriteLine($"Kontrakten blev ikke Underskrevet!!!");
        }
    }
}