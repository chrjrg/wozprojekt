/*
Class for animations in the console window
*/
using GameLogic;

public static class Anim
{
    public static bool isAnimating = false;

    public static void DriveAnim(Shape l, Shape r, int initialSpacing, int speed) // Method for driving animation
    {
        isAnimating = true; // Set animation status to true

        string[] left = l.GetAsciiArt();
        string[] right = r.GetAsciiArt();

        // Adjust the height of the left and right ASCII art to match the taller one
        int maxHeight = Math.Max(left.Length, right.Length);
        left = AdjustHeight(left, maxHeight);
        right = AdjustHeightFromBottom(right, maxHeight);

        // Find the maximum line width of the left ASCII art
        int maxLeftWidth = GetMaxLineLength(left);

        // Set a final spacing to ensure the left art doesn’t overlap the right one
        int finalSpacing = initialSpacing / 5;

        // Animate the left ASCII art moving towards the right ASCII art
        for (int step = 0; step < initialSpacing - finalSpacing; step++)
        {
            Console.Clear();

            // Generate the entire animation by iterating through each line of art
            for (int i = 0; i < maxHeight; i++)
            {
                // Dynamically add spaces to the left art depending on the step in the animation
                string leftLine = new string(' ', step) + left[i];

                // Calculate dynamic spacing between the left and right ASCII art
                int dynamicSpacing = initialSpacing - step + (maxLeftWidth - left[i].Length);
                string rightLine = new string(' ', dynamicSpacing) + right[i];

                // Print the lines of both left and right ASCII art together
                Console.WriteLine(leftLine + rightLine);
            }

            // Pause for the duration specified by the speed of the animation
            Thread.Sleep(speed);
        }
        isAnimating = false; // Set animation status to false when finished
    }

    // Adjust the height of the ASCII art by adding padding to the top
    private static string[] AdjustHeight(string[] art, int targetHeight)
    {
        string[] adjustedArt = new string[targetHeight];
        int padding = targetHeight - art.Length;

        // Add empty lines at the top to match the target height
        for (int i = 0; i < padding; i++)
        {
            adjustedArt[i] = ""; // Empty line for padding
        }
        Array.Copy(art, 0, adjustedArt, padding, art.Length);
        
        return adjustedArt;
    }

    // Adjust the height of the ASCII art by adding padding to the bottom
    private static string[] AdjustHeightFromBottom(string[] art, int targetHeight)
    {
        string[] adjustedArt = new string[targetHeight];
        int padding = targetHeight - art.Length;

        // Add empty lines at the top to align the bottom of the right art
        for (int i = 0; i < padding; i++)
        {
            adjustedArt[i] = ""; // Empty line for padding at the top
        }
        Array.Copy(art, 0, adjustedArt, padding, art.Length);

        return adjustedArt;
    }

    // Get the length of the longest line in the ASCII art
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

    // Animate the location text character by character with a delay
    public static void locationAnim(string text){
        CharSplit(text, 25); // Split and animate the text with a delay of 25 milliseconds per character
    }

    // Print each character of the text with a delay between characters
    public static void CharSplit(string text, int textimer){
        foreach (char letter in text){
            Console.Write(letter);
            System.Threading.Thread.Sleep(textimer); // Delay for each character
        } 
    }
}