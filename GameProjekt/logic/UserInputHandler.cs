/* 
Class for handling user input and responding to key presses.
*/
using static GameAssets;

public class UserInputHandler
{
    private readonly Context _context;

    public UserInputHandler(Context context)
    {
        _context = context;
    }

    // Listens for key press input and responds accordingly
    public void ListenForKeyPress() 
    {
        while (true)
        {
            if (Anim.isAnimating)
            {
                // Skip input if an animation is running
                continue;
            }

            var key = Console.ReadKey(intercept: true).Key;

            // Handle different key presses
            switch (key)
            {
                case ConsoleKey.UpArrow:
                    Console.Clear();
                    GoToRoom(db.GetSection("EnergyAtomName").ToLower());
                    break;
                case ConsoleKey.DownArrow:
                    Console.Clear();
                    GoToRoom(db.GetSection("EnergySolarName").ToLower());
                    break;
                case ConsoleKey.LeftArrow:
                    Console.Clear();
                    GoToRoom(db.GetSection("EnergyWindName").ToLower());
                    break;
                case ConsoleKey.RightArrow:
                    Console.Clear();
                    GoToRoom(db.GetSection("EnergyWaterName").ToLower());
                    break;
                case ConsoleKey.Spacebar:
                    Console.Clear();
                    GoToRoom(db.GetSection("EntryName").ToLower());
                    break;
                case ConsoleKey.C: // Clear screen
                    Console.Clear();
                    break;
                case ConsoleKey.Escape: // Exit the program
                    Console.Clear();
                    Console.WriteLine(db.GetSection("KeybindExiting")+ "\n");
                    context.GetCurrent().Welcome();
                    return;
                default:
                    Console.Clear();
                    Console.WriteLine(db.GetSection("KeybindInvalid"));
                    break;
            }
        }
    }

    // Handles the transition to a new room based on direction
    private void GoToRoom(string direction)
    {
        // Attempt to transition to the specified room
        _context.Transition(direction);
    }

    public void TextHelp() {
        Console.Clear();
        Console.WriteLine($"\n{db.GetSection("KeybindHelpNavigation")}\n");
        Console.WriteLine($"{db.GetSection("KeybindHelpMap")}\n");
        Console.WriteLine(db.GetSection("KeybindHelpExit"));
    }
}
    