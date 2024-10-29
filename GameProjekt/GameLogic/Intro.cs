namespace GameLogic{
    class Intro {


        string[] prompt = {"Velkommen til Power Simulator \n","Der er super travlt inde på Christiansborg op til det kommende valg i 2025, og der er derfor brug for din hjælp!\n","Miljøministeren ønsker at komme med udkast til hvordan Danmarks energinet skal se ud inden 2030, som led i Parisaftalen.\n","Han har dog svært ved at vide hvilken retning han skal gå, når det kommer til at vælge energiform.\n"};
        public string text = "";

        public int textTimer;


        public void ShowIntro(){
            for(int i=0;i<prompt.Length;i++){

                text = prompt[i];

                if(i==0){
                    Console.ForegroundColor = ConsoleColor.Green;
                    textTimer = 50;
                    CharSplit();
                }
                else {
                    Console.ForegroundColor = ConsoleColor.White;
                    textTimer = 25;
                    CharSplit();
                }
                if(i==4){
                    Console.WriteLine("");
                }
                System.Threading.Thread.Sleep(500);
            }
        }

        public void CharSplit(){
            foreach (char letter in text){
                Console.Write(letter);
                System.Threading.Thread.Sleep(textTimer);

            } 
        }

    }
}
