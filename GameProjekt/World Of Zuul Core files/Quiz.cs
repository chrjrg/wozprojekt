using System;
using System.Collections.Generic;

public class Quiz
{
    private List<Question> questions = new List<Question>();
    private string userName = "";
    private string dob = "";

    // Constructoren - Dvs. At når vi initiliaserer quiz objektet, så skal den load spørgsmålene fra data filen. 
    public Quiz()
    {
        try
        {
            LoadQuestions();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"DEBUG: Fejl under initialisering af Quiz: {ex.Message}");
        }
    }

    // Metode til at indlæse spørgsmål fra vores datafil.
private void LoadQuestions()
{
    Console.WriteLine("DEBUG: Indlæser spørgsmål...");
    questions.Clear();

    // Hent quizsektioner fra "QuizSections"
    string[] sectionNames = TextDatabase.Instance.GetSectionArray("QuizSections");

    foreach (var sectionName in sectionNames)
    {
        // Hent indholdet af sektionen
        string[] lines = TextDatabase.Instance.GetSectionArray(sectionName);

        if (lines.Length > 0)
        { 
            string questionContent = string.Join(Environment.NewLine, lines);
            questions.Add(new Question(sectionName, questionContent));
            Console.WriteLine($"DEBUG: Spørgsmål tilføjet: {sectionName}");
        }
    }

    Console.WriteLine($"DEBUG: {questions.Count} spørgsmål indlæst.");
}



    public void StartQuiz()
    {
        Console.Clear();

        // Håndter navn og fødselsår separat
        var question1 = questions.Find(q => q.SectionName == "Question1");
        var question2 = questions.Find(q => q.SectionName == "Question2");

        if (question1 != null)
        {
            Console.WriteLine(question1.Text);
            userName = GetNonEmptyInput("Dit navn: "); // Gem navnet
        }

        if (question2 != null)
        {
            Console.WriteLine(question2.Text);
            dob = GetNonEmptyInput("Dit fødselsår: "); // Gem fødselsåret
        }

        // Start nummereringen fra 1 for de resterende spørgsmål
        int questionNumber = 1;

        foreach (var question in questions)
        {
            // Spring spørgsmål 1 og 2 over, da de allerede er håndteret
            if (question.SectionName == "Question1" || question.SectionName == "Question2")
                continue;

            Console.Clear();
            Console.WriteLine($"Spørgsmål {questionNumber}:");
            Console.WriteLine(question.Text); // Vis hele spørgsmålet

            // Få brugerens svar (kun mellem 1 og 4)
            int answer = GetValidOption(4);
            question.UserAnswer = answer;
            questionNumber++; // Incrementer nummer
        }

        // Gem resultaterne
        SaveResults();
    }

public string GetUserName()
{
    return userName;
}


private void SaveResults()
{
    string path = System.IO.Path.Combine(System.IO.Directory.GetCurrentDirectory(), "data");
    if (!System.IO.Directory.Exists(path))
    {
        System.IO.Directory.CreateDirectory(path);
    }

    string fileName = $"{userName.ToLower()}_{dob}.txt";
    string filePath = System.IO.Path.Combine(path, fileName);
    int attempt = 1;

    // Læs eksisterende fil, hvis den findes
    var fileContent = new List<string>();
    if (System.IO.File.Exists(filePath))
    {
        fileContent.AddRange(System.IO.File.ReadAllLines(filePath));
        for (int i = 0; i < fileContent.Count; i++)
        {
            if (fileContent[i].Contains("* Antal forsøg:"))
            {
                int.TryParse(fileContent[i].Replace("* Antal forsøg:", "").Trim(), out attempt);
                attempt++; // Inkrementér
                fileContent[i] = $"* Antal forsøg: {attempt}"; // Opdater linjen
            }
        }
    }
    else
    {
        // Hvis filen ikke findes, opret en ny header
        fileContent.Add("****************************");
        fileContent.Add($"* Navn: {userName}");
        fileContent.Add($"* Fødselsår: {dob}");
        fileContent.Add($"* Antal forsøg: {attempt}");
        fileContent.Add("****************************");
        fileContent.Add(""); // Ekstra linje efter headeren
    }

    // Byg data for det aktuelle forsøg
    var attemptData = new System.Text.StringBuilder();
    attemptData.AppendLine("_______________________________________________________");
    attemptData.AppendLine("************************");
    attemptData.AppendLine($"* Forsøg: {attempt}");
    attemptData.AppendLine($"* Tidspunkt: {DateTime.Now:dd-MM-yyyy HH:mm:ss}");
    attemptData.AppendLine("************************");
    foreach (var question in questions)
    {
        // Spring navn og fødselsår over (de gemmes ikke som rå data)
        if (question.SectionName == "Question1" || question.SectionName == "Question2")
            continue;

        // Gem kun gyldige svar (1-4)
        if (question.UserAnswer >= 1 && question.UserAnswer <= 4)
        {
            attemptData.AppendLine(question.UserAnswer.ToString());
        }
    }

    // Skriv hele filen tilbage
    using (System.IO.StreamWriter writer = new System.IO.StreamWriter(filePath))
    {
        foreach (var line in fileContent)
        {
            writer.WriteLine(line); // Skriv eksisterende linjer (inkl. opdateret header)
        }
        writer.Write(attemptData.ToString()); // Tilføj det aktuelle forsøg
    }

    Console.Clear();
}




    // Hjælpefunktion til at få ikke-tom input
    private string GetNonEmptyInput(string prompt)
    {
        string? input;
        do
        {
            Console.Write(prompt);
            input = Console.ReadLine();
        } while (string.IsNullOrWhiteSpace(input));
        return input;
    }

    // Hjælpefunktion til at validere brugerens valg
    private int GetValidOption(int maxOption)
    {
        int choice;
        do
        {
            Console.Write("Vælg en mulighed: ");
            string? input = Console.ReadLine();

            if (int.TryParse(input, out choice) && choice >= 1 && choice <= maxOption)
            {
                return choice;
            }

            Console.WriteLine("Ugyldigt valg. Prøv igen. (Vælg mellem 1 og 4)");
        } while (true);
    }
}

// Klasse til at holde spørgsmål og brugerens svar
public class Question
{
    public string SectionName { get; }
    public string Text { get; }
    public int UserAnswer { get; set; }

    public Question(string sectionName, string text)
    {
        SectionName = sectionName;
        Text = text;
        UserAnswer = -1; // Ingen svar som standard
    }
}