/* 
Class for quiz logic and handling user input for the quiz.
*/
public class Quiz
{
    private List<Question> questions = new List<Question>(); // Holds the list of questions
    private string userName = ""; // Stores the user's name
    private string dob = ""; // Stores the user's date of birth

    public Quiz()
    {
        try
        {
            LoadQuestions(); // Load the questions from the data file
        }
        catch (Exception ex)
        {
            Console.WriteLine($"DEBUG: Fejl under initialisering af Quiz: {ex.Message}"); // Error handling
        }
    }

    // Loads questions from the data file
    private void LoadQuestions()
    {
        questions.Clear(); // Clears any existing questions
        string[] sectionNames = TextDatabase.Instance.GetSectionArray("QuizSections"); // Get all section names

        foreach (var sectionName in sectionNames)
        {
            string[] lines = TextDatabase.Instance.GetSectionArray(sectionName); // Fetch section content
            if (lines.Length > 0) 
            { 
                string questionContent = string.Join(Environment.NewLine, lines); // Combine lines into one string
                questions.Add(new Question(sectionName, questionContent)); // Add new question to list
            }
        }
    }

    public void StartQuiz()
    {
        Console.Clear();

        // Handle name and date of birth separately
        var question1 = questions.Find(q => q.SectionName == "Question1");
        var question2 = questions.Find(q => q.SectionName == "Question2");

        if (question1 != null)
        {
            Console.WriteLine(question1.Text); 
            userName = GetNonEmptyInput("Dit navn: "); // Get the user's name
        }

        if (question2 != null)
        {
            Console.WriteLine(question2.Text);
            dob = GetNonEmptyInput("Dit fødselsår: "); // Get the user's date of birth
        }

        // Loop through remaining questions and collect answers
        int questionNumber = 1;
        foreach (var question in questions)
        {
            // Skip questions 1 and 2, already handled
            if (question.SectionName == "Question1" || question.SectionName == "Question2")
                continue;

            Console.Clear();
            Console.WriteLine($"Spørgsmål {questionNumber}:");
            Console.WriteLine(question.Text); // Display question

            int answer = GetValidOption(4); // Get valid answer (1-4)
            question.UserAnswer = answer; // Save the answer
            questionNumber++; // Increment question number
        }

        SaveResults(); // Save the quiz results
    }

    public string GetUserName()
    {
        return userName; // Return the user's name
    }

    // Saves the quiz results to a file
    private void SaveResults()
    {
        string path = System.IO.Path.Combine(System.IO.Directory.GetCurrentDirectory(), "data");
        if (!System.IO.Directory.Exists(path))
        {
            System.IO.Directory.CreateDirectory(path); // Create directory if it doesn't exist
        }

        string fileName = $"{userName.ToLower()}_{dob}.txt";
        string filePath = System.IO.Path.Combine(path, fileName);
        int attempt = 1;

        var fileContent = new List<string>();
        if (System.IO.File.Exists(filePath))
        {
            fileContent.AddRange(System.IO.File.ReadAllLines(filePath));
            for (int i = 0; i < fileContent.Count; i++)
            {
                if (fileContent[i].Contains("* Antal forsøg:"))
                {
                    int.TryParse(fileContent[i].Replace("* Antal forsøg:", "").Trim(), out attempt);
                    attempt++; // Increment attempt count
                    fileContent[i] = $"* Antal forsøg: {attempt}"; // Update attempt line
                }
            }
        }
        else
        {
            // Create a new file header if it doesn't exist
            fileContent.Add("****************************");
            fileContent.Add($"* Navn: {userName}");
            fileContent.Add($"* Fødselsår: {dob}");
            fileContent.Add($"* Antal forsøg: {attempt}");
            fileContent.Add("****************************");
            fileContent.Add(""); // Add extra line after header
        }

        // Prepare data for the current attempt
        var attemptData = new System.Text.StringBuilder();
        attemptData.AppendLine("_______________________________________________________");
        attemptData.AppendLine("************************");
        attemptData.AppendLine($"* Forsøg: {attempt}");
        attemptData.AppendLine($"* Tidspunkt: {DateTime.Now:dd-MM-yyyy HH:mm:ss}");
        attemptData.AppendLine("************************");

        foreach (var question in questions)
        {
            if (question.SectionName == "Question1" || question.SectionName == "Question2")
                continue; // Skip name and DOB question

            if (question.UserAnswer >= 1 && question.UserAnswer <= 4)
            {
                attemptData.AppendLine(question.UserAnswer.ToString()); // Save valid answers (1-4)
            }
        }

        // Write the content to the file
        using (System.IO.StreamWriter writer = new System.IO.StreamWriter(filePath))
        {
            foreach (var line in fileContent)
            {
                writer.WriteLine(line); // Write the header and existing content
            }
            writer.Write(attemptData.ToString()); // Add the current attempt data
        }

        Console.Clear(); // Clear the console after saving results
    }

    // Helper function to get non-empty input from user
    private string GetNonEmptyInput(string prompt)
    {
        string? input;
        do
        {
            Console.Write(prompt); 
            input = Console.ReadLine();
        } while (string.IsNullOrWhiteSpace(input)); // Retry if input is empty
        return input;
    }

    // Helper function to validate user's answer
    private int GetValidOption(int maxOption)
    {
        int choice;
        do
        {
            Console.Write("Vælg en mulighed: ");
            string? input = Console.ReadLine();

            if (int.TryParse(input, out choice) && choice >= 1 && choice <= maxOption)
            {
                return choice; // Return valid choice
            }

            Console.WriteLine("Ugyldigt valg. Prøv igen. (Vælg mellem 1 og 4)"); // Invalid choice message
        } while (true);
    }
}

public class Question
{
    public string SectionName { get; }
    public string Text { get; }
    public int UserAnswer { get; set; }

    public Question(string sectionName, string text)
    {
        SectionName = sectionName;
        Text = text;
        UserAnswer = -1; // Default answer is not selected
    }
}