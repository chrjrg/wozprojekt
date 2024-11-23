class CommandInteract : BaseCommand, ICommand {
  public CommandInteract() {
    description = "Interact with a non-player character";
  }

  public void Execute(Context context, string command, string[] parameters) {
    if (parameters.Length < 2) {
      List<string> npcNames = context.GetCurrent().GetNPCNames();
      if (npcNames.Count == 0) {
        Console.WriteLine("There are no NPCs in this room.");
      } else {
        Console.Clear();
        Console.WriteLine("NPCs in this room:");
        foreach (string npcName in npcNames) {
          Console.WriteLine($"- {npcName}");
        }
      }
      return;
    }

    NPC npc = context.GetCurrent().GetNPC(parameters[1]);
    if (npc == null) {
      Console.WriteLine("There is no one here by that name.");
    } else {
      npc.Interact(context);
    }
  }
} 
