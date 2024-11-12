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

    public static Parameters money = new Parameters("Budget",1,"kr");
    public static Parameters co2 = new Parameters("CO\u2082",0,"Tons");
    public static Parameters energy = new Parameters("Energi",0,"GW");



    public static UserInputHandler inputHandler = new UserInputHandler(context);

    internal static void ClearConsole(Context context)
    {
        throw new NotImplementedException();
    }
}


