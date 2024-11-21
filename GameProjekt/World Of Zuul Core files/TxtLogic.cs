using System.Text;
/*
Syntax for use of database:
// Use "using static GameAssets" to access the singleton instance:
    using static GameAssets;

Use example:
    Console.WriteLine(db.GetSection("test"));

This would print the content of the section [Text1] from the loaded file, if it exists.
*/
public class TextDatabase
{
    private static readonly Lazy<TextDatabase> instance = new(() => new TextDatabase()); // Singleton instance. Lazy means it's only created when accessed, and only once.
    private Dictionary<string, string> dataSections; // Dictionary to store the sections
    

    // Private constructor to prevent external instantiation (Singleton pattern)
    private TextDatabase()
    {
        dataSections = new Dictionary<string, string>();
    }

    // Public property to access the singleton instance
    public static TextDatabase Instance => instance.Value;

    // Load the file and parse it into sections
    public void LoadFile(string filePath)
    {
        try 
        {
            string[] lines = File.ReadAllLines(filePath); // Read all lines from the file
            StringBuilder sectionContent = new StringBuilder(); // StringBuilder to store the content of the current section
            string? currentSection = null; // "?" means it can be null

            foreach (var line in lines)
            {
                if (line.StartsWith("[") && line.EndsWith("]"))
                {
                    if (currentSection != null)
                    {
                        dataSections[currentSection] = sectionContent.ToString().Trim(); // Save the previous section if we have one
                        sectionContent.Clear();
                    }
                    currentSection = line.Trim('[', ']');
                }
                else if (currentSection != null)
                {
                    sectionContent.AppendLine(line);
                }
            }

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

    // Method to retrieve a specific section by name
    public string GetSection(string sectionName)
    {
        if (dataSections.TryGetValue(sectionName, out string? sectionContent)) // Try to get the section content
        {
            return sectionContent;
        }
        else
        {
            Console.WriteLine($"Section '{sectionName}' not found.");
            return string.Empty;
        }
    }

    // Method to retrieve a section as a string array
    public string[] GetSectionArray(string sectionName)
    {
        if (dataSections.TryGetValue(sectionName, out string? sectionContent))
        {
            Console.WriteLine($"DEBUG: Sektionen '{sectionName}' fundet:");
            Console.WriteLine(sectionContent);
            return sectionContent.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
        }
        else
        {
            Console.WriteLine($"DEBUG: Sektionen '{sectionName}' ikke fundet.");
            return Array.Empty<string>();
        }
    }

}
