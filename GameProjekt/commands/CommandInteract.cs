/* 
Command for interacting with non-player characters
*/
using static GameAssets;

class CommandInteract : BaseCommand, ICommand {
  public CommandInteract() {
    description = db.GetSection("CommandInteractDescription");
  }

  // Executes the command to interact with NPCs.
  public void Execute(Context context, string command, string[] parameters) { 
    if (parameters.Length < 2) { // If no specific NPC is mentioned
      List<string> npcNames = context.GetCurrent().GetNPCNames(); // Fetch all NPC names
      if (npcNames.Count == 0) {
        Console.WriteLine(db.GetSection("CommandInteractNoNPCs")); // No NPCs available to interact with
      } else {
        Console.Clear();
        Console.WriteLine(db.GetSection("CommandInteractHeader"));
        Console.WriteLine(db.GetSection("CommandInteractHeader2") + "\n");
        Console.WriteLine(db.GetSection("CommandInteractNPCs"));
        foreach (string npcName in npcNames) { // Display all NPC names
          Console.WriteLine($"- {npcName}");
        }
      }
      return;
    }

    // Check if the provided NPC name is valid
    NPC? npc = context.GetCurrent().GetNPC(parameters[1]);
    if (npc == null) { 
      Console.WriteLine(db.GetSection("CommandInteractNPCError")); // Error message if NPC not found
    } else {
      npc.Interact(context); // Initiate interaction with the NPC
    } 
  } 
}