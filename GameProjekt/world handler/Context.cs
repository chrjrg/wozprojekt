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

    public string GetCurrentName()
    {
      return current?.GetName() ?? "Unknown Location"; //
    }
  
  public void Transition (string direction) {
    previous = current;
    Space next = current.FollowEdge(direction);
    if (next==null) {
      Console.WriteLine("You are confused, and walk in a circle looking for '"+direction+"'. In the end you give up 😩");
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
      Console.WriteLine("You have returned to " + current.GetName());
      current.WelcomeBack();
  }

  public void MakeDone () {
    done = true;
  }
  
  public bool IsDone () {
    return done;
  }

  public override string ToString()
  {
    return $"Context(Current: {GetCurrentName()}, Previous: {previous?.GetName() ?? "None"})";
  }
  
}


