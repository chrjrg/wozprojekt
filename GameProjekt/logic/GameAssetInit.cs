using GameLogic;


public static class GameAssets
{
    public static TextDatabase db = TextDatabase.Instance;
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
}


