/* World class for modeling the entire in-game world
 */
 using static GameAssets;
 using static Anim;

class World {
  
  Space entry;

  public World () {
    Space entry = new Space("Christiansborg");
    Space atom  = new Space("Atomkraftværk");
    Space wind  = new Space("Vindanlæg");
    Space solar = new Space("Solanlæg");
    Space water = new Space("Vandanlæg");
    this.entry = entry;
    
    // Add edges for entry (Christiansborg)
    entry.AddEdge("atomkraftværk", atom);
    entry.AddEdge("Vandanlæg", water);
    entry.AddEdge("Solanlæg", solar);
    entry.AddEdge("Vindanlæg", wind);
  
    
    // Add edges for atom (north room)
    atom.AddEdge("Vandanlæg", water);
    atom.AddEdge("Vindanlæg", wind);
    atom.AddEdge("Christiansborg", entry);

    // Add edges for water (East room)
    water.AddEdge("Atomkraftværk", atom);
    water.AddEdge("Solanlæg", solar);
    water.AddEdge("Christiansborg", entry);

    // Add edges for solar (south room)
    solar.AddEdge("Vindanlæg", wind);
    solar.AddEdge("Vandanlæg", water);
    solar.AddEdge("Christiansborg", entry);

    // Add edges for wind (west room)
    wind.AddEdge("Atomkraftværk", atom);
    wind.AddEdge("Solanlæg", solar);
    wind.AddEdge("Christiansborg", entry);
    



    wind.test =() =>     {DriveAnim(Car, Wind, 90, 25); locationAnim("LOKATION: ");}; 

  }
  
  public Space GetEntry () {
    return entry;
  }
}

