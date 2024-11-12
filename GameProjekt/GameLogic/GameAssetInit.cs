using GameLogic;

public static class GameAssets
{
    static World world = new World();
    public static Intro Intro = new Intro();
    public static Shape Wind = new Wind();
    public static Shape Atom = new Atom();
    public static Shape Car = new Car();


    public static Quiz quiz = new Quiz();
    public static Context  context  = new Context(world.GetEntry());



    public static UserInputHandler inputHandler = new UserInputHandler(context);

    internal static void ClearConsole(Context context)
    {
        throw new NotImplementedException();
    }

    // Statisk metode til at få energityper
    public static EnergyType AtomType = new EnergyType("Atomkraft", 10000, 500, 200);
    public static EnergyType WindType = new EnergyType("Vindmølle", 20000, 1000, 50);
    public static EnergyType WaterType = new EnergyType("Vandkraft", 30000, 2000, 25);
    public static EnergyType SolarType = new EnergyType("Solcelle", 15000, 700, 10);


    public static Resource budget = new Resource("Budget",10000,"kr");
    public static Resource energi = new Resource("Energi",0,"kW");
    public static Resource co2 = new Resource("CO2",0,"tons");
}


