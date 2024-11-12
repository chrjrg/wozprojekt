using System;
using System.Threading;
using GameLogic;

public static class Anim
{
    public static bool isAnimating = false;


    public static void DriveAnim(Shape venste, Shape hoejre, int initialSpacing, int fart)
    {
        isAnimating = true; // Start af animation

        string[] left = venste.GetAsciiArt();
        string[] right = hoejre.GetAsciiArt();

        // Justér højden på venstre og højre ASCII-tegninger
        int maxHeight = Math.Max(left.Length, right.Length);
        left = AdjustHeight(left, maxHeight);
        right = AdjustHeightFromBottom(right, maxHeight);

        // Find længden af den længste linje i venstre ASCII-tegning
        int maxLeftWidth = GetMaxLineLength(left);

        // Indstil en bufferafstand, så venstre ikke kommer helt over til højre
        int finalSpacing = initialSpacing / 5;

        // Animation af venstre ASCII-tegning mod højre ASCII-tegning
        for (int step = 0; step < initialSpacing - finalSpacing; step++)
        {
            Console.Clear();

            // Generér hele animationen som én blok per iteration - Heranvendes et nested loop
            for (int i = 0; i < maxHeight; i++)
            {
                // Tilføj dynamisk mellemrum foran venstre ASCII-tegning
                string leftLine = new string(' ', step) + left[i];

                // Beregn mellemrummet mellem venstre og højre ASCII-tegninger baseret på maxLeftWidth
                int dynamicSpacing = initialSpacing - step + (maxLeftWidth - left[i].Length);
                string rightLine = new string(' ', dynamicSpacing) + right[i];

                // Skriv linjen med både venstre og højre ASCII-tegning
                Console.WriteLine(leftLine + rightLine);
            }

            Thread.Sleep(fart);
        }
        isAnimating = false; // Slut af animation
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

    public static void locationAnim(string text){
        CharSplit(text);
    }

    
    public static void CharSplit(string text){
        foreach (char letter in text){
            Console.Write(letter);
            System.Threading.Thread.Sleep(10);
        } 
    }


}
