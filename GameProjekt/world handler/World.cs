/* World class for modeling the entire in-game world
 */
 using static GameAssets;
 using static Anim;

public class World {
  
  Space entry;
  

  public World () {
    Space entry = new Space(db.GetSection("EntryName"));
    Space atom  = new Space(db.GetSection("EnergyAtomName"));
    Space wind  = new Space(db.GetSection("EnergyWindName"));
    Space solar = new Space(db.GetSection("EnergySolarName"));
    Space water = new Space(db.GetSection("EnergyWaterName"));

    this.entry = entry;

    // Add edges for entry (Christiansborg)
    entry.AddEdge(db.GetSection("EnergyAtomName"), atom);
    entry.AddEdge(db.GetSection("EnergyWaterName"), water);
    entry.AddEdge(db.GetSection("EnergySolarName"), solar);
    entry.AddEdge(db.GetSection("EnergyWindName"), wind);

    // Add edges for atom (north room)
    atom.AddEdge(db.GetSection("EnergyWaterName"), water);
    atom.AddEdge(db.GetSection("EnergyWindName"), wind);
    atom.AddEdge(db.GetSection("EntryName"), entry);

    // Add edges for water (East room)
    water.AddEdge(db.GetSection("EnergyAtomName"), atom);
    water.AddEdge(db.GetSection("EnergySolarName"), solar);
    water.AddEdge(db.GetSection("EntryName"), entry);

    // Add edges for solar (south room)
    solar.AddEdge(db.GetSection("EnergyWindName"), wind);
    solar.AddEdge(db.GetSection("EnergyWaterName"), water);
    solar.AddEdge(db.GetSection("EntryName"), entry);

    // Add edges for wind (west room)
    wind.AddEdge(db.GetSection("EnergyAtomName"), atom);
    wind.AddEdge(db.GetSection("EnergySolarName"), solar);
    wind.AddEdge(db.GetSection("EntryName"), entry);

    // Add NPC's to rooms
    entry.AddNPC(db.GetSection("NPCSecretaryName"), new Secretary(db.GetSection("NPCSecretaryName")));
    atom.AddNPC(db.GetSection("NPCExpertName"), new Expert(db.GetSection("NPCExpertName")));
    water.AddNPC(db.GetSection("NPCExpertName"), new Expert(db.GetSection("NPCExpertName")));
    solar.AddNPC(db.GetSection("NPCExpertName"), new Expert(db.GetSection("NPCExpertName")));
    wind.AddNPC(db.GetSection("NPCExpertName"), new Expert(db.GetSection("NPCExpertName")));

    
    //atom.test =() => {DriveAnim(Car, Atom, 90, 25); Atom.Show(1);}; 
    //wind.test =() => {DriveAnim(Car, Wind, 90, 25); Wind.Show(1);}; 
    //solar.test =() => {DriveAnim(Car, Wind, 90, 25); Solar.Show(1);};
    //water.test =() => {DriveAnim(Car, Wind, 90, 25); Water.Show(1);}; 


  }
  
  public Space GetEntry () {
    return entry;
  }
}

