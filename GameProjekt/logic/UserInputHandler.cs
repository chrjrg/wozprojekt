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
                    GoToRoom("atomkraftværk");
                    break;
                case ConsoleKey.DownArrow:
                    Console.Clear();
                    GoToRoom("vindanlæg");
                    break;
                case ConsoleKey.LeftArrow:
                    Console.Clear();
                    GoToRoom("christiansborg");
                    break;
                case ConsoleKey.RightArrow:
                    Console.Clear();
                    GoToRoom("solanlæg");
                    break;
                case ConsoleKey.C:
                    Console.Clear();
                    break;
                case ConsoleKey.Escape:
                    Console.Clear();
                    Console.WriteLine("Afslutter tastaturindgang...");
                    return;
                default:
                    Console.Clear();
                    Console.WriteLine("Ugyldigt tastetryk. Brug piletasterne.");
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
