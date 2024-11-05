/* Space class for modeling spaces (rooms, caves, ...)
 */

class Space : Node {

  public Action? test { get; set; }

  public Space (String name) : base(name)
  {

  }
  
  
  public void Welcome () {
    test?.Invoke();
    Console.WriteLine("You are now at "+name);
    HashSet<string> exits = edges.Keys.ToHashSet();
    Console.WriteLine("Current exits are:");
    foreach (String exit in exits) {
      Console.WriteLine(" - "+exit);
    }
  }
  
  public void Goodbye () {
  }
  
  public override Space FollowEdge (string direction) {
    return (Space) (base.FollowEdge(direction));
  }

  

}
