/* World class for modeling the entire in-game world
 */
 using static GameLogic.GameAssets;
 using static Anim;

class World {
    private List<Space> spaces = new List<Space>();

    public World () {
        // Opret rum
        Space christiansborg = new Space("Christiansborg");
        Space wind = new Space("Vindanlæg");
        Space atom = new Space("Atomkraftværk");
        Space water = new Space("Vandanlæg");
        Space solar = new Space("Solanlæg");

        spaces.Add(christiansborg);
        spaces.Add(wind);
        spaces.Add(atom);
        spaces.Add(water);
        spaces.Add(solar);

        // Kald en funktion til at forbinde alle rum med edges
        ConnectSpaces();
        wind.test =() =>     {DriveAnim(Car, Atom, 90, 25); locationAnim("LOKATION: ");};
    }

    private void ConnectSpaces() {
        foreach (var space in spaces)
        {
            foreach (var otherSpace in spaces)
            {
                if (space != otherSpace)  
                {
                    space.AddEdge(otherSpace.GetName(), otherSpace);
                }
            }
        }
    }

    public Space GetEntry() {
        return spaces[0];  // Første rum er vores indgang
    }
}