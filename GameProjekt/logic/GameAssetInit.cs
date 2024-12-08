/* 
Class for managing and initializing game assets, including resources, energy types, and commands.
*/
using GameLogic;

public static class GameAssets 
{
    // Singleton instance of the database to fetch game-related data
    public static TextDatabase db = TextDatabase.Instance;

    // Game world and context initialization
    public static World world = new World();
    public static Context context = new Context(world.GetEntry());

    // Endgame instance for managing the game conclusion
    public static Endgame endgameInstance = new Endgame();

    // Game components initialization
    public static Intro Intro = new Intro();
    public static Shape Wind = new Wind();
    public static Shape Atom = new Atom();
    public static Shape Car = new Car();
    public static Secretary secretary = new Secretary(db.GetSection("SecretaryName"));
    public static Quiz quiz = new Quiz();
    public static Shape ActualMap = new ActualMap();
    public static EnergyInventory inventory = new EnergyInventory(); // Add inventory

    // Fallback command in case of invalid user input
    static ICommand fallback = new CommandUnknown();
    public static Registry registry = new Registry(context, fallback);

    // Input handler to process user commands
    public static UserInputHandler inputHandler = new UserInputHandler(context);

    // Method to clear the console (not implemented)
    internal static void ClearConsole(Context context)
    {
        throw new NotImplementedException();
    }

    // Energy types initialized with name, price, energy output, and CO2 emissions
    public static EnergyType AtomType = new EnergyType(db.GetSection("EnergyAtomName"), 5_000_000_000, 1750, 500_000, 92.5);
    public static EnergyType WindType = new EnergyType(db.GetSection("EnergyWindName"), 500_000_000, 350, 10000, 30);
    public static EnergyType WaterType = new EnergyType(db.GetSection("EnergyWaterName"), 310_000_000, 175, 20000, 50);
    public static EnergyType SolarType = new EnergyType(db.GetSection("EnergySolarName"), 370_000_000, 350, 5000, 17.5);


    // Resources initialized with name, start value, and type
    public static Resource budget = new Resource(db.GetSection("BudgetName"), 100_000_000_000, db.GetSection("BudgetUnit"));
    public static Resource energi = new Resource(db.GetSection("EnergyName"), 0, db.GetSection("EnergyUnit"));
    public static Resource co2 = new Resource("CO2", 0, db.GetSection("CO2Unit"));

    /* 
    Initializes the registry with various game commands.
    Commands are registered with associated actions (e.g., exit, help, etc.).
    */
    public static void InitRegistry () {
        registry.Register(db.GetSection("CommandKeyExit"), new CommandExit());
        registry.Register(db.GetSection("CommandKeyGo"), new CommandGo());
        registry.Register(db.GetSection("CommandKeyHelp"), new CommandHelp(registry));
        registry.Register(db.GetSection("CommandKeyGoBack"), new CommandGoBack());
        registry.Register(db.GetSection("CommandKeyKeybind"), new CommandKeybind());
        registry.Register(db.GetSection("CommandKeyClear"), new CommandClear());
        registry.Register(db.GetSection("CommandKeyInteract"), new CommandInteract());
        registry.Register(db.GetSection("CommandKeyBack"), new CommandGoBackHere());
        registry.Register(db.GetSection("CommandKeyMap"), new CommandMap());
    }
}