/* Context class to hold all context relevant to a session.
 */

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
      return current?.GetName() ?? "Unknown Location"; 
    }
  
  public void Transition (string direction) {
    previous = current;
    Space next = current.FollowEdge(direction);
    if (next==null) {
      Console.WriteLine("Der er ingen udgang i den retning. Prøv en anden retning.");
    } else {
      current = next;
      current.Welcome();
    }
  }

   public void TransitionBack() {
      Space temp = current;
      current = previous;
      previous = temp;
      Console.WriteLine("You have returned to " + current.GetName());
      current.WelcomeBack();
  }

    
   public void TransitionBackHere() {
      Console.Clear();
      Console.WriteLine("You have returned to " + current.GetName());
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
  
}


