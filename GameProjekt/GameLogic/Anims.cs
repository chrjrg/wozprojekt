using System;
using System.Threading;
using GameLogic;

public static class Anim
{
    public static void DriveAnim(Shape leftShape, Shape rightShape, int initialSpacing, int fart)
    {
        string[] leftAsciiArt = leftShape.GetAsciiArt();
        string[] rightAsciiArt = rightShape.GetAsciiArt();

        // Justér højden på venstre og højre ASCII-tegninger
        int maxHeight = Math.Max(leftAsciiArt.Length, rightAsciiArt.Length);
        leftAsciiArt = AdjustHeight(leftAsciiArt, maxHeight);
        rightAsciiArt = AdjustHeightFromBottom(rightAsciiArt, maxHeight);

        // Find længden af den længste linje i venstre ASCII-tegning
        int maxLeftWidth = GetMaxLineLength(leftAsciiArt);

        // Indstil en bufferafstand, så venstre ikke kommer helt over til højre
        int finalSpacing = initialSpacing / 2;

        // Animation af venstre ASCII-tegning mod højre ASCII-tegning
        for (int step = 0; step < initialSpacing - finalSpacing; step++)
        {
            Console.Clear();

            // Generér hele animationen som én blok per iteration
            for (int i = 0; i < maxHeight; i++)
            {
                // Tilføj dynamisk mellemrum foran venstre ASCII-tegning
                string leftLine = new string(' ', step) + leftAsciiArt[i];

                // Beregn mellemrummet mellem venstre og højre ASCII-tegninger baseret på maxLeftWidth
                int dynamicSpacing = initialSpacing - step + (maxLeftWidth - leftAsciiArt[i].Length);
                string rightLine = new string(' ', dynamicSpacing) + rightAsciiArt[i];

                // Skriv linjen med både venstre og højre ASCII-tegning
                Console.WriteLine(leftLine + rightLine);
            }

            Thread.Sleep(fart);
        }
    }

    private static string[] AdjustHeight(string[] art, int targetHeight)
    {
        string[] adjustedArt = new string[targetHeight];
        int padding = targetHeight - art.Length;

        for (int i = 0; i < padding; i++)
        {
            adjustedArt[i] = ""; // Tilføj tomme linjer for at matche højden
        }
        Array.Copy(art, 0, adjustedArt, padding, art.Length);
        
        return adjustedArt;
    }

    private static string[] AdjustHeightFromBottom(string[] art, int targetHeight)
    {
        string[] adjustedArt = new string[targetHeight];
        int padding = targetHeight - art.Length;

        // Fyld øverste linjer med tomme linjer for at justere bunden af højre tegning
        for (int i = 0; i < padding; i++)
        {
            adjustedArt[i] = ""; // Tilføj tomme linjer for at justere toppen
        }
        Array.Copy(art, 0, adjustedArt, padding, art.Length);

        return adjustedArt;
    }

    private static int GetMaxLineLength(string[] art)
    {
        int maxLength = 0;
        foreach (var line in art)
        {
            if (line.Length > maxLength)
                maxLength = line.Length;
        }
        return maxLength;
    }
}
