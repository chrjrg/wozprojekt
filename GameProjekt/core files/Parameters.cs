/* 
Class for parameter management and resource handling in the game.
*/
using static GameAssets;

// Class representing a Resource with its properties, methods, and statuses.
public class Resource {
    private string name;  // Name of the resource
    private double value; // Current value of the resource
    private string unit;  // Unit of the resource (e.g., currency, amount)

    // Static variables to store the amounts of different energy types
    public static int WindAmount = 0;
    public static int WaterAmount = 0;
    public static int SolarAmount = 0;
    public static int AtomAmount = 0;

    // Constructor to initialize the resource with a name, value, and unit
    public Resource(string name, double initialValue, string unit) {
        this.name = name;
        this.value = initialValue;
        this.unit = unit;
    }

    // Getter method to return the current value of the resource
    public double GetValue() => value;

    // Getter method to return the name of the resource
    public string GetName() => name;

    // Method to adjust the value of the resource by a certain amount
    public void Adjust(double amount) => value += amount;  

    // Method to get the formatted value of the resource depending on its unit
    public string GetFormattedValue() {
        switch (unit) {
            // Case for handling currency units with different thresholds (billion, million, thousand)
            case var curreny when curreny == db.GetSection("MoneyUnit"):
                if (value >= 1_000_000_000) return $"{value / 1_000_000_000:0.##} {db.GetSection("BillionUnit")} {unit}";
                else if (value >= 1_000_000) return $"{value / 1_000_000:0.##} {db.GetSection("MillionUnit")} {unit}";
                else if (value >= 1_000) return $"{value / 1_000:0.##} {db.GetSection("ThousandUnit")} {unit}";
                else return $"{value:0.##} {unit}";
            default:
                return $"{value:0.##} {unit}";  // Default formatting if the unit is not money
        }
    }

    // Method to print the status of the resource to the console
    public void GetStatus() {
        var originalColor = Console.ForegroundColor;  // Save the original console color

        // Change color to green for the resource name
        Console.ForegroundColor = ConsoleColor.Green;
        Console.Write(name);  // Print the resource name

        // Change color back to white for the value
        Console.ForegroundColor = ConsoleColor.White;

        // Switch statement to format and display the resource's value based on its unit
        switch (unit) {
            case var curreny when curreny == db.GetSection("MoneyUnit"):
                if (value >= 1_000_000_000) {  // Billion
                    Console.WriteLine($": {value / 1_000_000_000:0.##} {db.GetSection("BillionUnit")} {unit}");
                }
                else if (value >= 1_000_000) {  // Million
                    Console.WriteLine($": {value / 1_000_000:0.##} {db.GetSection("MillionUnit")} {unit}");
                }
                else if (value >= 1_000) {  // Thousand
                    Console.WriteLine($": {value / 1_000:0.##} {db.GetSection("ThousandUnit")} {unit}");
                }
                else {  // Less than thousand
                    Console.WriteLine($": {value:0.##} {unit}");
                }
                break;

            default:  // Default case for any other units
                Console.WriteLine($": {value:0.##} {unit}");
                break;
        }

        // Restore the original color of the console after displaying the resource
        Console.ForegroundColor = originalColor;
    }

    // Method to return the value of the resource (used for calculation or comparison)
    public double GetStatusValue() {
        double result = value;  // Store the current value of the resource
        return result;  // Return the value
    }

    // Static method to display the status of multiple resources at once
    public static void DisplayAllStatuses(params Resource[] resources) {
        Console.Clear();  // Clear the console for fresh output
        Console.ForegroundColor = ConsoleColor.White;  // Set text color to white for the headings
        Console.WriteLine("______________________________________________");
        Console.WriteLine("");  

        // Set color to green for the status heading
        Console.ForegroundColor = ConsoleColor.Green;
        Console.Write(db.GetSection("StatusParameters") + " ");  // Print the section heading
        Console.ForegroundColor = ConsoleColor.White;  // Set text color back to white for "current" label
        Console.Write(db.GetSection("StatusCurrent"));
        Console.WriteLine("");  
        Console.WriteLine("______________________________________________");

        // Loop through each resource to display its name and value
        foreach (var resource in resources) {
            Console.ForegroundColor = ConsoleColor.Green;  // Set color to green for the resource name
            Console.WriteLine("");
            Console.Write(" - " + resource.GetName() + ": ");  // Print the resource name
            Console.ForegroundColor = ConsoleColor.White;  // Set color to white for the value
            Console.WriteLine(resource.GetFormattedValue());  // Print the formatted value
        }

        // Reset the console color to default after displaying all statuses
        Console.ResetColor();
        Console.WriteLine("______________________________________________");
        Console.WriteLine("");

        // Call method to show the inventory (assumes this method exists elsewhere in the code)
        EnergyStore.ShowInventory();
        context.ClickNext();  // Move to the next part of the game
        secretary.UserChoiceSecretary();  // Allow the secretary to interact with the player
    }
}