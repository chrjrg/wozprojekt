using System.Text;

class TextDatabase
{
    private Dictionary<string, string> dataSections;

    public TextDatabase()
    {
        dataSections = new Dictionary<string, string>();
    }

    // Load the file and parse it into sections
    public void LoadFile(string filePath)
    {
        try
        {
            string[] lines = File.ReadAllLines(filePath);
            StringBuilder sectionContent = new StringBuilder();
            string currentSection = null;

            foreach (var line in lines)
            {
                if (line.StartsWith("[") && line.EndsWith("]"))
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

            Console.WriteLine("File loaded successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading file: {ex.Message}");
        }
    }


    // Function to retrieve a specific section by name
    public string GetSection(string sectionName)
    {
        if (dataSections.TryGetValue(sectionName, out string sectionContent))
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



