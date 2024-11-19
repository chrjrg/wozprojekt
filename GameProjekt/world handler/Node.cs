/* Node class for modeling graphs
 */

public class Node {
  protected string name;
  protected Dictionary<string, Node> edges = new Dictionary<string, Node>();
  
  public Node (string name) {
    this.name = name;
  }
  
  public String GetName () {
    return name;
  }
  
  public void AddEdge (string name, Node node) {
    edges.Add(name.ToLower(), node);
  }
  
  public virtual Node? FollowEdge(string direction)
  {
      Console.WriteLine($"Tilgængelige edges fra {this.name}: {string.Join(", ", edges.Keys)}");

      if (edges.ContainsKey(direction))
      {
          return edges[direction];
      }

      Console.WriteLine($"Ingen edge fundet for '{direction}'");
      return null;
  }

}

