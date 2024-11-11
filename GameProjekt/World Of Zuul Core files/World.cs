/* World class for modeling the entire in-game world
 */
 using static GameLogic.GameAssets;
 using static Anim;

class World {
  Space entry;
  
  public World () {
    Space entry = new Space("Christiansborg");
    Space wind  = new Space("Vindanlæg");
    Space atom  = new Space("Atomkraftværk");
    Space water = new Space("Vandanlæg");
    Space solar = new Space("Solanlæg");
    
    entry.AddEdge("Vindanlæg", wind);
    entry.AddEdge("Atomkraftværk", atom);
    this.entry = entry;

    wind.test =() =>     {DriveAnim(Car, Wind, 90, 25); locationAnim("LOKATION: ");}; 
  }
  
  public Space GetEntry () {
    return entry;
  }
}