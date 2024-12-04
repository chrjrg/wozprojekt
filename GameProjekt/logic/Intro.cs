/* 
Class for managing the introductory sequence of the game, including text display and animations.
*/
using static Anim;
using static GameAssets;

namespace GameLogic{
    public class Intro {
        
        // Array containing intro text sections fetched from the database
        string[] prompt = db.GetSectionArray("Intro");

        // Current text to be displayed
        public string text = "";

        // Timer for controlling text display speed
        public int textTimer;

        /* 
        Displays text with a typing effect, where each character is printed with a delay defined by `tid`.
        */
        private void BaseTyping(int tid) 
        {
            Console.ForegroundColor = ConsoleColor.White;
            textTimer = tid;

            // Split text into lines and print each one with a delay
            var lines = text.Split('\n');
            foreach (var line in lines)
            {
                CharSplit(line, 25); // Print each character with a delay
                Console.WriteLine(); // Add a newline after each line
            }
        }

        /* 
        Similar to BaseTyping but with green-colored text.
        */
        private void GreenTyping(int tid)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            textTimer = tid;

            // Split and display each line with a delay
            var lines = text.Split('\n');
            foreach (var line in lines)
            {
                CharSplit(line, 25);
                Console.WriteLine();
            }
        }

        /* 
        Shows the intro text in sequence, controlling text styles and timing.
        */
        public void ShowIntro(){
            for (int i = 0; i < prompt.Length; i++)
            {
                Console.WriteLine($"{prompt[i]}"); // Debug: Print each prompt line

                // Control text style and timing based on the current line
                text = prompt[i];
                switch (i)
                {
                    case 0:
                        Console.Clear();
                        GreenTyping(50); // Use green text for the first line
                        break;
                    case 5:
                        System.Threading.Thread.Sleep(5000); // Pause before clearing screen
                        Console.Clear();
                        break;
                    case 7:
                    case 9:
                    case 11:
                        GreenTyping(25); // Use green text for these lines
                        break;
                    default:
                        BaseTyping(10); // Default typing effect
                        break;
                }

                System.Threading.Thread.Sleep(500); // Delay between lines
            }

            // Prompt user to continue to the next section
            context.ClickNext();

            // Proceed to the Secretary intro if available
            if (secretary != null)
            {
                secretary.SecretaryIntro();
            }
            else
            {
                Console.WriteLine(db.GetSection("SecretaryNotFound"));
            }
        }
    }
}
