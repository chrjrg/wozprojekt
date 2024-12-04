/* 
Class for managing the player's current and previous location
*/
using static GameAssets;

public class Context 
{
    private Space current;
    private Space previous;
    private bool done = false;

    // Constructor: Initializes with a starting space
    public Context(Space start) 
    {
        current = start ?? throw new ArgumentNullException(nameof(start), "Cannot be null.");
        previous = start; // Set previous to start to avoid null warnings
        done = false;
    }

    // Get the current space
    public Space GetCurrent() 
    {
        return current;
    }

    // Set the current space and update previous space
    public void SetCurrent(Space space)
    {
        if (space == null)
        {
            throw new ArgumentNullException(nameof(space), "Space cannot be null.");
        }
        previous = current; // Save current as previous before setting new current
        current = space;
    }

    // Get the name of the current space, or an error message if null
    public string GetCurrentName() 
    {
        return current?.GetName() ?? db.GetSection("ContextErrorNoLocation"); 
    }

    // Transition to a new space based on direction
    public void Transition(string direction) 
    {
        Console.Clear();
        previous = current;
        Space next = current.FollowEdge(direction); // Find the next space in the specified direction
        if (next == null) 
        {
            TransitionBackHere(); // If no valid transition, go back to the current space
        } 
        else 
        {
            current = next;
            current.Welcome(); // Greet the user in the new space
        }
    }

    // Transition back to the previous space
    public void TransitionBack() 
    {
        Space temp = current;
        current = previous;
        previous = temp;
        Console.WriteLine(db.GetSection("ContextReturned") + " " + current.GetName() + "\n");
        current.WelcomeBack(); // Welcome back to the previous space
    }

    // Transition back to the current space with a return message
    public void TransitionBackHere() 
    {
        Console.WriteLine(db.GetSection("ContextReturned") + " " + current.GetName() + "\n");
        current.WelcomeBack(); // Welcome back to the current space
    }

    // Mark the game as done
    public void MakeDone() 
    {
        done = true;
    }

    // Check if the game is done
    public bool IsDone() 
    {
        return done;
    }

    // For debugging: Provides string representation of the context
    public override string ToString() 
    {
        return $"Context(Current: {GetCurrentName()}, Previous: {previous?.GetName() ?? "None"})";
    }

    // Display a message to prompt the user to press a key to continue
    public void ClickNext() 
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("\n" + db.GetSection("PressAnyKey"));
        Console.ReadKey(true); // Wait for user input
        Console.ForegroundColor = ConsoleColor.White;
    }
}