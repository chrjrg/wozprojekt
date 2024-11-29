/* 
Command for inteacting with non-player characters
*/
using static GameAssets;

class CommandInteract : BaseCommand, ICommand {
  public CommandInteract() {
    description = db.GetSection("CommandInteractDescription");
  }

  public void Execute(Context context, string command, string[] parameters) { 
    if (parameters.Length < 2) {
      List<string> npcNames = context.GetCurrent().GetNPCNames(); // Get all NPC names
      if (npcNames.Count == 0) {
        Console.WriteLine(db.GetSection("CommandInteractNoNPCs")); // No NPCs to interact with
      } else {
        Console.Clear();
        Console.WriteLine(db.GetSection("CommandInteractHeader"));
        Console.WriteLine(db.GetSection("CommandInteractHeader2") + "\n");
        Console.WriteLine(db.GetSection("CommandInteractNPCs"));
        foreach (string npcName in npcNames) { // Print all NPC names
          Console.WriteLine($"- {npcName}");
        }
      }
      return;
    }

    // Check if the player has provided a valid NPC name
    NPC? npc = context.GetCurrent().GetNPC(parameters[1]);
    if (npc == null) { 
      Console.WriteLine(db.GetSection("CommandInteractNPCError")); 
    } else {
      npc.Interact(context); // Interact with the NPC
    } 
  } 
} 
