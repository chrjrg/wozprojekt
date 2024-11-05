/* Context class to hold all context relevant to a session.
 */

class Context {

  Space current;
  bool done = false;

  Space prev;
  
  public Context (Space node) {
    current = node;
  }
  
  public Space GetCurrent() {
    return current;
  }

  public Space GetPrev(){
    return prev; 
  }

  public string GetCurrentName() {
    return current.GetName();
  }
  
  public void Transition (string direction) {
    Space next = current.FollowEdge(direction);
    if (next==null) {
      Console.WriteLine("You are confused, and walk in a circle looking for '"+direction+"'. In the end you give up 😩");
    } else {
      prev = current;
      current = next;
      current.Welcome();
    }
  }
  
  public void MakeDone () {
    done = true;
  }
  
  public bool IsDone () {
    return done;
  }
}

