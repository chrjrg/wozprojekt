using static Anim;
using static GameAssets;

namespace GameLogic{
    public class Intro {


        string[] prompt = db.GetSectionArray("Intro");

        public string text = "";

        public int textTimer;

        
        private void BaseTyping(int tid) 
        {
            Console.ForegroundColor = ConsoleColor.White;
            textTimer = tid;

            // Split teksten op på \n og udskriv hver linje separat
            var lines = text.Split('\n');
            foreach (var line in lines)
            {
                CharSplit(line,25);
                Console.WriteLine(); // Tilføj ny linje efter hver linje
            }
        }

        private void GreenTyping(int tid)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            textTimer = tid;

            // Split teksten op på \n og udskriv hver linje separat
            var lines = text.Split('\n');
            foreach (var line in lines)
            {
                CharSplit(line,25);
                Console.WriteLine(); // Tilføj ny linje efter hver linje
            }
        }



        public void ShowIntro(){
            for (int i = 0; i < prompt.Length; i++)
            {
                Console.WriteLine($"{prompt[i]}"); // Debug-udskrift af linjerne
            }

            // Kør normal intro-logik
            for (int i = 0; i < prompt.Length; i++)
            {
                text = prompt[i];

                switch (i)
                {
                    case 0:
                        Console.Clear();
                        GreenTyping(50);
                        break;
                    case 5:
                        System.Threading.Thread.Sleep(5000);
                        Console.Clear();
                        break;
                    case 7:
                        GreenTyping(25);
                        break;
                    case 9:
                        GreenTyping(25);
                        break;
                    case 11:
                        GreenTyping(25);
                        Console.ForegroundColor = ConsoleColor.White;
                        System.Threading.Thread.Sleep(5000);
                        Console.Clear();
                        break;

                    default:
                        BaseTyping(10);
                        break;
                }

                System.Threading.Thread.Sleep(500);
            }
            // Bed brugeren om at trykke på en tast for at fortsætte til næste sektion
            context.ClickNext();

            // Gå videre til Secretary-intro
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
