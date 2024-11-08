/* Space class for modeling spaces (rooms, caves, ...)
 */

public class Space : Node {

  public Action? test { get; set; }

  public Space (String name) : base(name)
  {

  }
  
  public void Welcome () {
    test?.Invoke();
    Console.ForegroundColor = ConsoleColor.Blue;
    Console.WriteLine(name);
    Console.WriteLine("");
    Console.ResetColor();
    HashSet<string> exits = edges.Keys.ToHashSet();
    Console.WriteLine("Current exits are:");
    foreach (String exit in exits) {
      Console.WriteLine(" - "+exit);
    }
  }
  
  public void Goodbye () {
  }
  
  public override Node? FollowEdge(string direction)
  {
      return base.FollowEdge(direction);
  }
}
