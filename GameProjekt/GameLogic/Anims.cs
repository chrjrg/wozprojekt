using System;
using System.Threading;
using GameLogic;

    public static class Anim
    {
        public static void DriveAnim(Shape shape, int spacesCount, int fart)
        {
            string[] asciiArt = shape.GetAsciiArt();
            string spacing = "";
            int sleepTimer = fart;

            // Bevæg ASCII-kunsten ved at tilføje mellemrum ét ad gangen
            for (int step = 0; step < spacesCount; step++)
            {
                Console.Clear();
                
                // Opdater spacing og tilføj det til hver linje
                spacing += " ";
                string[] updatedArt = new string[asciiArt.Length];
                
                for (int i = 0; i < asciiArt.Length; i++)
                {
                    updatedArt[i] = spacing + asciiArt[i];
                }
                
                // Vis den opdaterede ASCII-kunst
                DisplayAnim(updatedArt);
                Thread.Sleep(fart);
            }
        }

        public static void DisplayAnim(string[] array)
        {
            foreach (string text in array)
            {
                Console.WriteLine(text);
            }
        }
    }

