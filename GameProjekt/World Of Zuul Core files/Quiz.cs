//*****************************
//  Quiz.cs - Bruges til at rydde konsollen for et bedre overblik.
//  NEED: Som metode
//*****************************
using System;

public class Quiz
{
    // Array til spørgsmål
    public string[] qPrompt = {
        "Du skal nu skrive dit navn: <Poul>",
        "Du skal nu skrive dit fødselsår: <96>",
        "1. Hvad er hovedstaden i Danmark?",
        "2. Hvad er 2 + 2?",
        "3. Hvilket år blev Danmark medlem af EU?",
        "4. Hvad er Danmarks nationalret?",
        "5. Hvad er det danske ord for 'computer'?"
    };



    string userName="";
    string DOB="";

    // Array til svar
    public string[] ansPrompt={""};

    // Konstruktør, hvor vi undlader initQuiz for at undgå dobbeltkørsel
    public Quiz() { }

    public void initQuiz(){
        ansPrompt = new string[qPrompt.Length]; // Initialiser array til svar

        for (int i = 0; i < qPrompt.Length; i++)
        {
            Console.Clear(); // Ryd konsollen for hvert nyt spørgsmål for bedre overblik
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(qPrompt[i]);
            Console.ForegroundColor = ConsoleColor.White;

            string? answer;
            do
            {
                Console.Write("Dit svar: ");
                answer = Console.ReadLine();

                if (string.IsNullOrEmpty(answer))
                {
                    Console.WriteLine("Prøv igen, vi skal bruge noget tekst.");
                }
            } while (string.IsNullOrEmpty(answer)); // Gentag, hvis svaret er tomt eller null
            
            switch (i){
                case 0:
                    userName = answer;
                    break;
                case 1:
                    DOB = answer;
                    break;
                default:
                    ansPrompt[i] = answer; // Gem svaret i ansPrompt arrayet
                    break;
            }
        }
        if (userName != null && DOB != null)
        {
            QuizAnswer(userName,DOB);
        }


        Console.Clear();
    }

    public void QuizAnswer(string name, string dateOfBirth){


        // Definer stien til mappen, hvor filen skal gemmes
        string textPath = Path.Combine(Directory.GetCurrentDirectory(), "data");

        // Tjekker om DataMappen er oprettet hos brugeren
        if (!Directory.Exists(textPath))
        {
            Directory.CreateDirectory(textPath);
        }

        // Her angiver vi vores variable
        string fileName = $"{name.ToLower()}_{dateOfBirth}.txt";
        string filePath = Path.Combine(textPath, fileName);
        int attempt = 1;
        string[] DataFileSyntax = {
            "*************************************",
            $"  Tidspunkt: {DateTime.Now}",
            $"  Antal forsøg: {attempt}",
            "*************************************"
        };


        // Hvis der er flere forsøg, så vores bruegr kan tage quiz flere gange
        while (File.Exists(filePath))
        {
            fileName = $"{name}_{dateOfBirth}({attempt}).txt";
            filePath = Path.Combine(textPath, fileName);
            attempt++;
        }

        // Skriv svarene til den nye fil med det unikke navn
        using (StreamWriter outputFile = new StreamWriter(filePath))
        {
            foreach (string line in DataFileSyntax)
            {
                outputFile.WriteLine(line);
            }
            foreach (string answer in ansPrompt)
            {
                if(!string.IsNullOrWhiteSpace(answer)){
                    outputFile.WriteLine(answer);
                }
            }
        }

        // Vis en besked om, at filen er gemt
        Console.WriteLine($"Filen er gemt som: {fileName}");
    }

}
