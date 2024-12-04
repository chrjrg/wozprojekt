/* 
Node class for modeling graphs
*/
using static GameAssets;

public class Node 
{
    protected string name;
    protected Dictionary<string, Node> edges = new Dictionary<string, Node>();

    // Constructor: Initializes the node with a name
    public Node(string name) 
    {
        this.name = name;
    }

    // Get the name of the node
    public string GetName() 
    {
        return name;
    }

    // Add an edge (connection) to another node
    public void AddEdge(string name, Node node) 
    {
        edges.Add(name.ToLower(), node);
    }

    // Follow an edge in the given direction and return the corresponding node
    public virtual Node? FollowEdge(string direction) 
    {
        if (edges.ContainsKey(direction)) 
        {
            return edges[direction];
        }

        Console.WriteLine($"{db.GetSection("ContextNoEdgesFound")} '{direction}'\n");
        return null;
    }
}

