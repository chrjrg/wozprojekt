/* Context class to hold all context relevant to a session.
 */

public class Context {
  Space current;
  Space previous;
  bool done = false;

  public Context(Space start) {
    current = start;
    previous = start; // undgå null warning
  }
  
  public Space GetCurrent() {
    return current;
  }


  public string GetCurrentName() {
    return current.GetName();
  }


  
  public void Transition (string direction) {
    Space? next = (Space?)current.FollowEdge(direction);
    
    if (next==null) {
      Console.WriteLine("Den indtastede lokation: '"+direction+"'Er i øjeblikket ikke tilgængelig😩");
      // Hent og vis de mulige udgange
      HashSet<string> exits = current.GetExits();
      Console.WriteLine("Dine følgende valgmuligheder her:");
      foreach (string exit in exits)
      {
          Console.WriteLine(" - " + exit);
      }
      return;
    
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

  public void MakeDone () {
    done = true;
  }
  
  public bool IsDone () {
    return done;
  }
}

