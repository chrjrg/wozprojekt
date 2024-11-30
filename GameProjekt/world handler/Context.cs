/* Context class to hold all context relevant to a session.
 */
using static GameAssets;

public class Context {

  private Space current;
  private Space previous;
  bool done = false;

  public Context(Space start) {
    current = start ?? throw new ArgumentNullException(nameof(start), "cannot be null.");
    previous = start; // Avoid null warnings
    done = false;
    }

  
  public Space GetCurrent() {
    return current;
  }

    public void SetCurrent(Space space)
    {
        if (space == null)
        {
            throw new ArgumentNullException(nameof(space), "Space cannot be null.");
        }
        previous = current; // Update previous before changing current
        current = space;
    }

    public string GetCurrentName() // This is used to get the name of the current location
    {
      return current?.GetName() ?? db.GetSection("ContextErrorNoLocation"); 
    }
  
  public void Transition (string direction) {
    Console.Clear();
    previous = current;
    Space next = current.FollowEdge(direction);
    if (next==null) {
      TransitionBackHere();
    } else {
      current = next;
      current.Welcome();
    }
  }

   public void TransitionBack() {
      Space temp = current;
      current = previous;
      previous = temp;
      Console.WriteLine(db.GetSection("ContextReturned") + " " + current.GetName() + "\n");
      current.WelcomeBack();
  }

    
   public void TransitionBackHere() {
      Console.WriteLine(db.GetSection("ContextReturned") + " " + current.GetName() + "\n");
      current.WelcomeBack();
  }

  public void MakeDone () { // MakeDone is a boolean that is used to check if the game is done
    done = true;
  }
  
  public bool IsDone () { // Isdone is a boolean that is used to check if the game is done
    return done;
  }

  public override string ToString() // This is for debugging
  {
    return $"Context(Current: {GetCurrentName()}, Previous: {previous?.GetName() ?? "None"})";
  }

  public void ClickNext() {
    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine("\n" + db.GetSection("PressAnyKey"));
    Console.ReadKey(true);
    Console.ForegroundColor = ConsoleColor.White;
  }
}


