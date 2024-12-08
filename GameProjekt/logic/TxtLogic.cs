using System.Text;
/* 
Syntax for use of database:
To access the singleton instance of the TextDatabase class:
    using static GameAssets;
Example:
    Console.WriteLine(db.GetSection("test"));
This would print the content of the section [test] from the loaded file, if it exists.
*/

public class TextDatabase
{
    private static readonly Lazy<TextDatabase> instance = new(() => new TextDatabase()); // Singleton instance, created lazily when accessed
    private Dictionary<string, string> dataSections; // Stores sections from the file as key-value pairs

    // Private constructor to enforce the Singleton pattern
    private TextDatabase()
    {
        dataSections = new Dictionary<string, string>();
    }

    // Public property to get the singleton instance
    public static TextDatabase Instance => instance.Value;

    /* 
    Loads the file at the given path and parses it into sections.
    Each section is stored in the dataSections dictionary.
    */
    public void LoadFile(string filePath)
    {
        try 
        {
            string[] lines = File.ReadAllLines(filePath); // Read all lines from the file
            StringBuilder sectionContent = new StringBuilder(); // To accumulate lines for the current section
            string? currentSection = null; // Holds the name of the current section

            foreach (var line in lines)
            {
                if (line.StartsWith("[") && line.EndsWith("]")) // Detects section headers
                {
                    if (currentSection != null)
                    {
                        dataSections[currentSection] = sectionContent.ToString().Trim(); // Save previous section
                        sectionContent.Clear(); // Clear for next section
                    }
                    currentSection = line.Trim('[', ']'); // Set the new section name
                }
                else if (currentSection != null)
                {
                    sectionContent.AppendLine(line); // Add line to the current section's content
                }
            }

            // Save the last section
            if (currentSection != null) 
            {
                dataSections[currentSection] = sectionContent.ToString().Trim();
            }
        }
        catch (Exception ex) 
        {
            Console.WriteLine($"Error loading file: {ex.Message}");
        }
    }

    /* 
    Retrieves the content of a section by its name.
    @param sectionName The name of the section to retrieve.
    @return The content of the section or an empty string if not found.
    */
    public string GetSection(string sectionName)
    {
        if (dataSections.TryGetValue(sectionName, out string? sectionContent))
        {
            return sectionContent; // Return the content of the found section
        }
        else
        {
            Console.WriteLine($"Section '{sectionName}' not found.");
            return string.Empty; // Return an empty string if section is not found
        }
    }

    /* 
    Retrieves the content of a section as an array of strings (split by lines).
    @param sectionName The name of the section to retrieve.
    @return An array of strings representing each line in the section.
    */
    public string[] GetSectionArray(string sectionName)
    {
        if (dataSections.TryGetValue(sectionName, out string? sectionContent))
        {
            return sectionContent.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries); // Split by lines
        }
        else
        {
            Console.WriteLine($"Section '{sectionName}' not found.");
            return Array.Empty<string>(); // Return an empty array if section is not found
        }
    }

    /* 
    Language selector for loading the appropriate text file based on user input.
    This method prompts the user to choose a language (DAN, ENG, DE) and loads the corresponding file.
    */
    public class LanguageSelector
    {
        public static void SelectLanguageAndLoadFile(TextDatabase db)
        {
            Console.Clear();
            bool validInput = false;
            while (!validInput)
            {
                Console.WriteLine($"Which language? (DAN/ENG)\nWrite 'DAN' for Danish\nWrite 'ENG' for English");
                string userInput = Console.ReadLine()!.ToLower(); // Read and convert input to lowercase
                
                string filePath = userInput switch
                {
                    "dan" => Path.Combine(Directory.GetCurrentDirectory(), "txtfiles/DAN.txt"),
                    "eng" => Path.Combine(Directory.GetCurrentDirectory(), "txtfiles/ENG.txt"),
                    _ => string.Empty // If input is invalid, return an empty string
                };

                if (!string.IsNullOrEmpty(filePath))
                {
                    db.LoadFile(filePath); // Load the selected file
                    validInput = true; // Set validInput to true to exit the loop
                    Console.Clear();
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Wrong input, try again." + "\n"); // Prompt the user to try again
                }
            }
        }
    }
}