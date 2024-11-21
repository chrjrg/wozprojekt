using GameLogic;


public static class GameAssets
{
    public static TextDatabase db = TextDatabase.Instance;
    static World world = new World();
    public static Intro Intro = new Intro();
    public static Shape Wind = new Wind();
    public static Shape Atom = new Atom();
    public static Shape Car = new Car();
    public static Secretary secretary = new Secretary("Sussane");
    public static Quiz quiz = new Quiz();


    public static Context  context  = new Context(world.GetEntry());

    public static UserInputHandler inputHandler = new UserInputHandler(context);

    internal static void ClearConsole(Context context)
    {
        throw new NotImplementedException();
    }


    // Statisk metode til at få energityper. initilisere energityper.
    //Her initialiseres energityperne med navn, pris, energyoutput og co2Emission. Hentes fra filen energyTypes.
    //De er public og statiske, så vi kan tilgå instanserne i fra andre steder i koden.
    public static EnergyType AtomType = new EnergyType("Atomkraft", 21, 500, 200);
    public static EnergyType WindType = new EnergyType("Vindmølle", 10, 1000, 50);
    public static EnergyType WaterType = new EnergyType("Vandkraft", 30, 2000, 25);
    public static EnergyType SolarType = new EnergyType("Solcelle", 15, 700, 10);

    //Her initialiseres parametrende med navn, startværdi og type.
    public static Resource budget = new Resource("Budget",10000,"kr");
    public static Resource energi = new Resource("Energi",0,"kW");
    public static Resource co2 = new Resource("CO2",0,"tons");
}


