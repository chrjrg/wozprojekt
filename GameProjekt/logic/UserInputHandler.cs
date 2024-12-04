using System;
using static GameAssets;
using GameLogic; // Sørg for, at du importerer de nødvendige namespaces

public class UserInputHandler
{
    private readonly Context _context;

    public UserInputHandler(Context context)
    {
        _context = context;
    }

    public void ListenForKeyPress() 
    {
        while (true)
        {
            if (Anim.isAnimating)
            {
                // Hvis der kører en animation, springer vi input over
                continue;
            }

            var key = Console.ReadKey(intercept: true).Key;

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
                case ConsoleKey.M: // Map?
                    Console.Clear();
                    break;
                case ConsoleKey.C:
                    Console.Clear();
                    break;
                case ConsoleKey.Escape:
                    Console.Clear();
                    Console.WriteLine(db.GetSection("KeybindExiting"));
                    return;
                default:
                    Console.Clear();
                    Console.WriteLine(db.GetSection("KeybindInvalid"));
                    break;
            }
        }
    }


    private void GoToRoom(string direction)
    {
        // Forsøg at skifte til rummet
        _context.Transition(direction);

    }
}
