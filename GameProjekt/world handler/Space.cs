/* 
Space class for modeling spaces (rooms)
*/
using System.Xml;
using static GameAssets;

public class Space : Node {
  public bool alreadyBeenHere = false;
  public bool selectedInfo = false;

  public Action? test { get; set; }

  public Space(String name) : base(name)
  {
    this.name = name ?? "Unnamed Space";
  }

  public void Welcome () {
    test?.Invoke();
    Console.WriteLine($"{db.GetSection("WelcomeHeader")} '{name}'" + "\n");
    HashSet<string> exits = edges.Keys.ToHashSet();
    Console.WriteLine(db.GetSection("CurrentExits"));
    foreach (String exit in exits) {
        string formattedExit = char.ToUpper(exit[0]) + exit.Substring(1).ToLower();
        Console.WriteLine(" - "+formattedExit);
    }

  }

  public void WelcomeBack () {
    test?.Invoke();
    HashSet<string> exits = edges.Keys.ToHashSet();
    Console.WriteLine(db.GetSection("CurrentExits"));
    foreach (String exit in exits) {
        string formattedExit = char.ToUpper(exit[0]) + exit.Substring(1).ToLower();
        Console.WriteLine(" - "+formattedExit);
    }
  }
  
  public void Goodbye () {
  }
  
  
  public override Space FollowEdge (string direction) {
      return (Space) (base.FollowEdge(direction))!;
  }

  private new readonly string name; 
  private Dictionary<string, NPC> npcs = new Dictionary<string, NPC>();


  public new string GetName() {
    return string.IsNullOrEmpty(name) ? "Unnamed Space" : name;
  }

  public void AddNPC(string name, NPC npc) {
    npcs[name] = npc;
  }

  public NPC? GetNPC(string name) {
    if (npcs.ContainsKey(name)) {
      return npcs[name];
    } else {
      return null; // Return null if the NPC is not found
    }
  }

    public List<string> GetNPCNames() {
    return new List<string>(npcs.Keys);
  }
}
