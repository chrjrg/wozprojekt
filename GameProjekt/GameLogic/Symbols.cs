using System.Reflection.Metadata;
using System;
using System.Reflection.Metadata.Ecma335;

namespace GameLogic{
    class Shape{
        protected string[] asciiArt;

        public string[] GetAsciiArt()
        {
            return asciiArt;
        }

        public void Show(){
            foreach (string line in asciiArt){
                Console.WriteLine(line);
            }
        }

    }


    class Wind : Shape{
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