/* 
Space class for modeling spaces (rooms)
*/
using static GameAssets;

public class Space : Node 
{
    public bool alreadyBeenHere = false;
    public bool selectedInfo = false;
    public Action? test { get; set; }

    // Constructor: Initializes space with a name
    public Space(string name) : base(name)
    {
        this.name = name ?? "Unnamed Space";
    }

    // Display a welcome message for the current space
    public void Welcome() 
    {
        test?.Invoke();
        Console.WriteLine($"{db.GetSection("WelcomeHeader")} '{name}'\n");
        HashSet<string> exits = edges.Keys.ToHashSet();
        HeaderHelp();
        Console.WriteLine(db.GetSection("CurrentExits"));
        foreach (string exit in exits) 
        {
            string formattedExit = char.ToUpper(exit[0]) + exit.Substring(1).ToLower();
            Console.WriteLine(" - " + formattedExit);
        }
    }

    // Display a welcome back message for revisiting the space
    public void WelcomeBack() 
    {
        test?.Invoke();
        HashSet<string> exits = edges.Keys.ToHashSet();
        HeaderHelp();
        Console.WriteLine(db.GetSection("CurrentExits"));
        foreach (string exit in exits) 
        {
            string formattedExit = char.ToUpper(exit[0]) + exit.Substring(1).ToLower();
            Console.WriteLine(" - " + formattedExit);
        }
    }

    // Display a goodbye message when leaving the space
    public void Goodbye() 
    {
        Console.Clear();
        Console.WriteLine(db.GetSection("GoodbyeHeader") + "\n");
        context.ClickNext();
    }

    // Display header and available commands for the space
    public void HeaderHelp() 
    {
        string commandGo = db.GetSection("CommandKeyGo");
        string commandInteract = db.GetSection("CommandKeyInteract");
        string commandHelp = db.GetSection("CommandKeyHelp");
        Console.WriteLine(db.GetSection("HeaderCommand1").Replace(db.GetSection("HeaderCommandReplace"), commandGo));
        Console.WriteLine(db.GetSection("HeaderCommand2").Replace(db.GetSection("HeaderCommandReplace"), commandInteract));
        Console.WriteLine(db.GetSection("HeaderCommand3").Replace(db.GetSection("HeaderCommandReplace"), commandHelp) + "\n");
    }

    // Override FollowEdge to return a Space object
    public override Space FollowEdge(string direction) 
    {
        return (Space)(base.FollowEdge(direction))!;
    }

    private new readonly string name; 
    private Dictionary<string, NPC> npcs = new Dictionary<string, NPC>();

    // Get the name of the space
    public new string GetName() 
    {
        return string.IsNullOrEmpty(name) ? "Unnamed Space" : name;
    }

    // Add an NPC to the space
    public void AddNPC(string name, NPC npc) 
    {
        npcs[name] = npc;
    }

    // Get an NPC by name
    public NPC? GetNPC(string name) 
    {
        return npcs.ContainsKey(name) ? npcs[name] : null;
    }

    // Get a list of NPC names in the space
    public List<string> GetNPCNames() 
    {
        return new List<string>(npcs.Keys);
    }
}
