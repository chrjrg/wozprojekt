using static GameAssets;

namespace GameLogic{
    public class Shape{
        protected string[] asciiArt = Array.Empty<string>();
        protected string[] asciiMinimap = Array.Empty<string>();
        

        public string[] GetAsciiArt()
        {
            return asciiArt;
        }

        public string[] getAsciiMap()
        {
            return asciiMinimap;
        }

        public void Show(int type){
            Console.Clear();
            if (type == 0){
                foreach (string line in asciiArt){
                    Console.WriteLine(line);
                }
            } else {
                foreach (string line in asciiMinimap){
                    Console.WriteLine(line);
                }
            }
            Console.WriteLine("\n");

        }

    }


    public class Wind : Shape{
        public Wind(){
            asciiArt = new string[]
            {
            "     ##        ##",
            "      ##      ##",
            "       ##|--|##",
            "      | ##  ## |",
            "     |___####___|",
            "    |~~~~####~~~~|",
            "     ===##==##===",
            "    |..##....##..|",
            "   |..##......##..|",
            "  |..##........##..|",
            " |........__........|",
            "|........|__|........|",   
            "_________|__|_________|"
            };

            asciiMinimap = new string[]{
"                          --------------------",
"                         |  Atomkraftværk(AK)  |",
"                          --------------------",
"                                    |",
"                                    |",
"#####################      --------------------",
"# Vindmøllepark(VP) # - - | Christiansborg(CB) |",
"#####################      --------------------",
"                                    |",
"                                    |",
"                           -------------------",
"                          | Solcelleanlæg(SP) |",
"                           -------------------"
            };
        }
    }

    public class Solar : Shape{
        public Solar(){
            asciiArt = new string[]
            {                                     
            "     -####.+#### ####-.####-     ",
            "     #####.##### #####.#####     ",
            "    .-----.----- -----.-----.    ",
            "   .+####--##### #####--#####.   ",
            "   -#####.###### ######.#####-   ",
            "                                 ",
            "  -#####+.#####+ +#####.+#####-  ",
            " .######.-#####+ +#####-.######. ",
            " .------ .------ ------. ------. ",
            ".-++++++++++++#####++++++++++++-.",
            "           .--#####--."                                                           
            };

            asciiMinimap = new string[]{
"",
"",
"",
"",
"",
" -------------------       --------------------       -------------------",
"| Vindmøllepark(VP) | - - | Christiansborg(CB) | - - | Vandkraftværk(VK) |",
" -------------------       --------------------       -------------------",
"                                    |",
"                                    |",
"                          #####################",
"                          # Solcelleanlæg(SP) #",
"                          #####################"
            };
        }
    }

    public class Water : Shape{
        public Water(){
            asciiArt = new string[]
            {
            "   ,(   ,(   ,(   ,(   ,(   ,(   ,(   ,(",
            "`-'  `-'  `-'  `-'  `-'  `-'  `-'  `-'  `",
            "   ,(   ,(   ,(   ,(   ,(   ,(   ,(   ,(",
            "`-'  `-'  `-'  `-'  `-'  `-'  `-'  `-'  `",
            "   ,(   ,(   ,(   ,(   ,(   ,(   ,(   ,(",
            "`-'  `-'  `-'  `-'  `-'  `-'  `-'  `-'  `",
            "   ,(   ,(   ,(   ,(   ,(   ,(   ,(   ,(",
            "`-'  `-'  `-'  `-'  `-'  `-'  `-'  `-'  `",
            "   ,(   ,(   ,(   ,(   ,(   ,(   ,(   ,(",
            "`-'  `-'  `-'  `-'  `-'  `-'  `-'  `-'  `",
            };

            asciiMinimap = new string[]{
"                          --------------------",
"                         |  Atomkraftværk(AK)  |",
"                          --------------------",
"                                    |",
"                                    |",
"                           --------------------      #####################",
"                          | Christiansborg(CB) | - - # Vandkraftværk(VK) #",
"                           --------------------      #####################",
"                                    |",
"                                    |",
"                           -------------------",
"                          | Solcelleanlæg(SP) |",
"                           -------------------"
            };
        }
    }


    class ActualMap : Shape {
        public ActualMap(){
            asciiArt = new string[]
            {
"                           -------------------",
"                          | Atomkraftværk(AK) |",
"                           -------------------",
"                                    |",
"                                    |",
" -------------------       --------------------       -------------------",
"| Vindmøllepark(VP) | - - | Christiansborg(CB) | - - | Vandkraftværk(VK) |",
" -------------------       --------------------       -------------------",
"                                    |",
"                                    |",
"                           -------------------",
"                          | Solcelleanlæg(SP) |",
"                           -------------------"
           };
        }
    }

    class Atom : Shape {
        public Atom(){
            asciiArt = new string[]
            {
"                  ) ) )                     ) ) )",
"                ( ( (                      ( ( (",
"              ) ) )                       ) ) )",
"           (~~~~~~~~~)                 (~~~~~~~~~)",
"            | POWER |                   | POWER |",
"            |       |                   |       |",
"            I      _._                  I       _._",
"            I    /'   `l                I     /'   `l",
"            I   |   N   |               I    |   N   |",
"            f   |   |~~~~~~~~~~~~~~|    f    |    |~~~~~~~~~~~~~~|",
"          .'    |   ||~~~~~~~~|    |  .'     |    | |~~~~~~~~|   |",
"        /'______|___||__###___|____|/'_______|____|_|__###___|___|"
            };

            asciiMinimap = new string[]{
"                         #####################",
"                         # Atomkraftværk(AK) #",
"                         #####################",
"                                   |",
"                                   |",
" -------------------       --------------------       -------------------",
"| Vindmøllepark(VP) | - - | Christiansborg(CB) | - - | Vandkraftværk(VK) |",
" -------------------       --------------------       -------------------",
"",
"",
"",
"",
"",
            };
        }
    }

    class Car : Shape {
        public Car(){
            asciiArt = new string[]{
            "        /-----------l",
            "       / ####  ##### l",
            " _____/ #####  ###### l________",
            "|          -        -        ( )l",
            "|     ___               ___      |",
            " l___//-ll_____________//-ll_____/",
            "      l_/               l_/"
            };
        }
    }
}

