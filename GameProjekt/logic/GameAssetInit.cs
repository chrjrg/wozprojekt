using GameLogic;


public static class GameAssets
{
    public static TextDatabase db = TextDatabase.Instance;
    public static World world = new World();
    public static Context context = new Context(world.GetEntry());
    

    public static Intro Intro = new Intro();
    public static Shape Wind = new Wind();
    public static Shape Atom = new Atom();
    public static Shape Car = new Car();
    public static Secretary secretary = new Secretary("Sussane");
    public static Quiz quiz = new Quiz();
    public static EnergyInventory inventory = new EnergyInventory(); // Tilføj lager



    static ICommand fallback = new CommandUnknown();
    public static Registry registry = new Registry(context, fallback);

    public static UserInputHandler inputHandler = new UserInputHandler(context);

    internal static void ClearConsole(Context context)
    {
        throw new NotImplementedException();
    }


    // Statisk metode til at få energityper. initilisere energityper.
    //Her initialiseres energityperne med navn, pris, energyoutput og co2Emission. Hentes fra filen energyTypes.
    //De er public og statiske, så vi kan tilgå instanserne i fra andre steder i koden.
    public static EnergyType AtomType = new EnergyType("Atomkraftværk", 4_970_000_000, 1750, 500_017.5);
    public static EnergyType WindType = new EnergyType("Vindmølleanlæg", 497_000_000, 350, 10003.5);
    public static EnergyType WaterType = new EnergyType("Vandkraftværk", 310_625_000, 175, 20003.5);
    public static EnergyType SolarType = new EnergyType("Solcelleanlæg", 37_275_000, 350, 5001.75);

    //Her initialiseres parametrende med navn, startværdi og type.
    public static Resource budget = new Resource("Budget",100_000_000_000,"kr");
    public static Resource energi = new Resource("Energi",0,"GWt");
    public static Resource co2 = new Resource("CO2",0,"tons");


    public static void InitRegistry () {
        registry.Register(db.GetSection("CommandKeyExit"), new CommandExit());
        registry.Register(db.GetSection("CommandKeyGo"), new CommandGo());
        registry.Register(db.GetSection("CommandKeyHelp"), new CommandHelp(registry));
        registry.Register(db.GetSection("CommandKeyGoBack"), new CommandGoBack());
        registry.Register(db.GetSection("CommandKeyKeybind"),new CommandKeybind());
        registry.Register(db.GetSection("CommandKeyClear"), new CommandClear());
        registry.Register(db.GetSection("CommandKeyInteract"), new CommandInteract());
        registry.Register(db.GetSection("CommandKeyBack"), new CommandGoBackHere());
    }
}


