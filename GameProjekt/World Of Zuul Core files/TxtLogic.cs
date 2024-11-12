using System.Text;

/*
Syntax for use of database:
Initialize the database (for each file you want to load):
    TextDatabase db = new TextDatabase();
    db.LoadFile(Path.Combine(Directory.GetCurrentDirectory(), "World Of Zuul Core files/data.txt").ToString());
Use example:
    Console.WriteLine(db.GetSection("test"));
This would print the content of the section [Text1] from the loaded file.
*/

class TextDatabase
{
    
    private Dictionary<string, string> dataSections; // Dictionary to store the sections

    public TextDatabase()
    {
        dataSections = new Dictionary<string, string>(); // Initialize the dictionary
    }

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
                if (line.StartsWith("[") && line.EndsWith("]")) // checks for section headers
                {
                    // Save the previous section if we have one
                    if (currentSection != null)
                    {
                        dataSections[currentSection] = sectionContent.ToString().Trim();
                        sectionContent.Clear();
                    }
                    // Set the new section name
                    currentSection = line.Trim('[', ']');
                }
                else if (currentSection != null)
                {
                    // Append line to the current section
                    sectionContent.AppendLine(line);
                }
            }

            // Save the last section
            if (currentSection != null)
            {
                dataSections[currentSection] = sectionContent.ToString().Trim();
            }

            //Console.WriteLine("File loaded successfully.");
        }
        catch (Exception ex) 
        {
            Console.WriteLine($"Error loading file: {ex.Message}");
        }
    }

    // Method to retrieve a specific section by name
    public string GetSection(string sectionName)
    {
        if (dataSections.TryGetValue(sectionName, out string? sectionContent))
        {
            return sectionContent;
        }
        else
        {
            Console.WriteLine($"Section '{sectionName}' not found.");
            return string.Empty;
        }
    }
}



