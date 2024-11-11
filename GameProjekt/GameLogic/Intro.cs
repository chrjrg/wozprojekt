using static Anim;

namespace GameLogic{
    public class Intro {


        string[] prompt = {"Velkommen til Power Simulator \n",
        "Der er super travlt inde på Christiansborg op til det kommende valg i 2025, og der er derfor brug for din hjælp!\n",
        "Miljøministeren ønsker at komme med udkast til hvordan Danmarks energinet skal se ud inden 2030, som led i Parisaftalen.\n",
        "Han har dog svært ved at vide hvilken retning han skal gå, når det kommer til at vælge energiform.\n",
        "Du har nu til opgave at at udforske og vælge hvilken energiforsyning vi skal bruge i Danmark!\n",
        "",
        "LOKATION: ",
        "Christiansborg\n",
        "DATO: ",
        "27. Januar 2025\n",
        "TID: ",
        "13:32",
        };
        


        public string text = "";

        public int textTimer;

        private void BaseTyping(int tid){
            Console.ForegroundColor = ConsoleColor.White;
            textTimer = tid;
            CharSplit(text);
        }
        private void GreenTyping(int tid){
            Console.ForegroundColor = ConsoleColor.Green;
            textTimer = tid;
            CharSplit(text);
        }


        public void ShowIntro(){
            for(int i=0;i<prompt.Length;i++){

                text = prompt[i];

                switch (i){
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
        }

    }
}
