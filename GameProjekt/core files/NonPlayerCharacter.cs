/* 
Class for NON-Player Characters (NPCs) in the game.
*/
using static GameAssets;
using static Anim;


// Abstract base class for NPCs in the game. All NPCs will inherit from this class.
public abstract class NPC {
    public string name;  // Name of the NPC
    protected bool exitInteraction;  // Flag to track if interaction should be exited


    // Constructor to initialize NPC with a name.
    public NPC(string name) {
        this.name = name;
        this.exitInteraction = false;
    }


    // Gets the name of the NPC.
    public string GetName() {
        return name;
    }


    // Handles the interaction with the NPC.
    public void Interact(Context context) {
        Console.Clear();
        string currentLocation = context.GetCurrentName().ToString();  // Get current location
        PerformAction(context);   // Perform the NPC's action
    }

    // Abstract methods to be implemented by derived classes
    public abstract void DisplayMandatoryIntro(Context context);
    public abstract void PerformAction(Context context);
}


// Expert class inherits from NPC.
public class Expert : NPC {
    private string current;  // Current location of the Expert NPC

    // Constructor to initialize Expert NPC with a name.
    public Expert(string name) : base(name) {
        current = string.Empty;  // Initialize current location as empty
    }

    // Displays the mandatory introduction message based on the current location.
    public override void DisplayMandatoryIntro(Context context) {
        current = context.GetCurrentName().ToString();  // Get current location
        switch (current)
        {
            case var atom when atom == db.GetSection("EnergyAtomName"):  // If current location is Atom room
                DisplaySection("AtomExpertIntro");
                break;
            case var water when water == db.GetSection("EnergyWaterName"):  // If current location is Water room
                DisplaySection("WaterExpertIntro");
                break;
            case var solar when solar == db.GetSection("EnergySolarName"):  // If current location is Solar room
                DisplaySection("SolarExpertIntro");
                break;
            case var wind when wind == db.GetSection("EnergyWindName"):  // If current location is Wind room
                DisplaySection("WindExpertIntro");
                break;
            default:  
                break;
        }
    }

    // Performs the action of interacting with the Expert NPC.
    public override void PerformAction(Context context) {
        Space currentSpace = context.GetCurrent();  // Get current space

        // Check if the player has already interacted with this space
        if (!currentSpace.alreadyBeenHere) {
            DisplayMandatoryIntro(context);  // Display the introduction message
            context.ClickNext();  // Move to the next context
            currentSpace.alreadyBeenHere = true;  // Mark the space as interacted
        } 

        UserChoiseExpert();  // Allow the user to choose further interaction
    }

    // Displays information about the energy type based on the current location.
    public void DisplayInfo (Context context) {
        current = context.GetCurrentName().ToString();  // Get current location
        switch (current)
        {
            case var atom when atom == db.GetSection("EnergyAtomName"):  // If current location is Atom room
                Console.WriteLine(db.GetSection("AtomExpertShortInfo"));
                Console.WriteLine("");
                break;
            case var water when water == db.GetSection("EnergyWaterName"):  // If current location is Water room
                Console.WriteLine(db.GetSection("WaterExpertShortInfo"));
                Console.WriteLine("");
                break;
            case var solar when solar == db.GetSection("EnergySolarName"):  // If current location is Solar room
                Console.WriteLine(db.GetSection("SolarExpertShortInfo"));
                Console.WriteLine("");
                break;
            case var wind when wind == db.GetSection("EnergyWindName"):  // If current location is Wind room
                Console.WriteLine(db.GetSection("WindExpertShortInfo"));
                Console.WriteLine("");
                break;
            default:  
                break;
        }
    }

    // Displays a section of text with animated character split effect.
    private void DisplaySection(string section) {
        Anim.CharSplit(db.GetSection(section) + "\n", 10);  // Use animation for text display
    }

    // Handles the user's choice when interacting with the Expert NPC.
    public void UserChoiseExpert() {
        Console.Clear();
        Space currentSpace = context.GetCurrent();  // Get current space
        DisplayInfo(context);  // Display relevant information

        // If information hasn't been selected yet, show a lock message
        if (!currentSpace.selectedInfo) {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(db.GetSection("ExpertInfoLock") + "\n");
            Console.ForegroundColor = ConsoleColor.White;
        }

        // Present choices to the user for further interaction
        Console.WriteLine($"{db.GetSection("ExpertInfoOption1")} '{db.GetSection("ExpertInfoChoice")}'");
        Console.WriteLine($"{db.GetSection("ExpertInfoOption2")} '{db.GetSection("ExpertBuyChoice")}'");
        Console.WriteLine($"{db.GetSection("ExpertInfoOption3")} '{db.GetSection("ExpertBackChoice")}'");

        Console.Write("> ");
        string userInput = Console.ReadLine()!.ToLower();  // Get user input

        // Handle the user's choice
        if (userInput == db.GetSection($"ExpertInfoChoice")) {
            Console.Clear();
            switch (current) {
                case var atom when atom == db.GetSection("EnergyAtomName"):  // Display more info about Atom energy
                    Console.WriteLine(db.GetSection("AtomExpertMoreInfo"));
                    break;
                case var water when water == db.GetSection("EnergyWaterName"):  // Display more info about Water energy
                    Console.WriteLine(db.GetSection("WaterExpertMoreInfo"));
                    break;
                case var solar when solar == db.GetSection("EnergySolarName"):  // Display more info about Solar energy
                    Console.WriteLine(db.GetSection("SolarExpertMoreInfo"));
                    break;
                case var wind when wind == db.GetSection("EnergyWindName"):  // Display more info about Wind energy
                    Console.WriteLine(db.GetSection("WindExpertMoreInfo"));
                    break;
                default:  
                    break;
            }
            currentSpace.selectedInfo = true;  // Mark the info as selected
            context.ClickNext();  // Move to the next context
            UserChoiseExpert();  // Recursively call the method to allow another choice
        } else if (userInput == db.GetSection("ExpertBuyChoice")) {
            Console.Clear();
            if (currentSpace.selectedInfo) {
                Contract.CreateContract(context);  // Create a contract if info has been selected
            } else {
                UserChoiseExpert();  // Otherwise, let the user choose again
            }
        } else if (userInput == db.GetSection("ExpertBackChoice")) {
            Console.Clear();
            context.TransitionBackHere();  // Transition the player back to the previous space
        } else {
            Console.Clear();
            Console.WriteLine(db.GetSection("InputError"));  // Display input error if the choice is invalid
            UserChoiseExpert();  // Ask the user to choose again
        }
    }
}

// The Secretary class represents an NPC with actions related to managing resources and submitting data
public class Secretary : NPC {
    public Secretary(string name) : base(name) { }

    // Performs the action of interacting with the Secretary NPC.
    public override void PerformAction(Context context) {
        Space currentSpace = context.GetCurrent();  // Get current space
        if (!currentSpace.alreadyBeenHere) {
            DisplayMandatoryIntro(context);  // Display the intro if not already interacted
            currentSpace.alreadyBeenHere = true;
            context.ClickNext();  // Transition to the next context
            Console.Clear();
            UserChoiceSecretary();  // Proceed with user's choice interaction
        } else {
            UserChoiceSecretary();  // Otherwise, just proceed with choice
        }
    }

    // Displays the mandatory introduction message for the Secretary NPC.
    public override void DisplayMandatoryIntro(Context context) {
        DisplaySection("SecretaryIntro");  // Display the intro section for Secretary
    }


    // Handles the user's choices when interacting with the Secretary NPC.
    public void UserChoiceSecretary() {
        Console.Clear();
        Console.WriteLine(db.GetSection("SecretaryOptionIntro") + "\n");
        Console.WriteLine($"{db.GetSection("SecretaryInfoOption1")} '{db.GetSection("SecretaryInfoStatus")}'");
        Console.WriteLine($"{db.GetSection("SecretaryInfoOption2")} '{db.GetSection("SecretarySubmitChoice")}'");
        Console.WriteLine($"{db.GetSection("SecretaryInfoOption3")} '{db.GetSection("SecretaryBackChoice")}'");

        Console.Write("> ");
        string userInput = Console.ReadLine()!.ToLower();  // Get user input

        // Handle the user's input choices
        if (userInput == db.GetSection("SecretaryInfoStatus")) {
            Resource.DisplayAllStatuses(budget, energi, co2);  // Display resource statuses
        } else if (userInput == db.GetSection("SecretarySubmitChoice")) {
            Console.Clear();
            Submit();  // Call the submit function to handle resource evaluation
        } else if (userInput == db.GetSection("SecretaryBackChoice")) {
            Console.Clear();
            context.TransitionBackHere();  // Transition back to the previous context
        } else {
            Console.Clear();
            Console.WriteLine(db.GetSection("InputError") + "\n");  // Show input error if invalid choice
            UserChoiceSecretary();  // Allow the user to choose again
        }
    }


    // Submits the current resource and budget status for evaluation.
    public void Submit() {
        budget.GetStatus(); 
        energi.GetStatus();  
        co2.GetStatus();
        Console.WriteLine($"\nStabilitet: {inventory.CalculateOverallStability(AtomType, SolarType, WindType, WaterType)}%");  // Calculate and display overall stability

        // Offer endgame or back choices
        Console.WriteLine($"\n{db.GetSection("EndGameOption1")} '{db.GetSection("EndGameSubmitChoice")}'");
        Console.WriteLine($"{db.GetSection("EndGameOption2")} '{db.GetSection("EndGameBackChoice")}'");

        Console.Write("> ");
        string userInput = Console.ReadLine()!.ToLower();  // Get user input for endgame choices
        if (userInput == db.GetSection("EndGameSubmitChoice")) {
            endgameInstance.Evaluate();  // Evaluate the endgame if the user chooses to submit
        } else if (userInput == db.GetSection("EndGameBackChoice")) {
            Console.Clear();
            context.TransitionBackHere();  // Otherwise, transition back to the previous context
        } else {
            Console.Clear();
            Console.WriteLine(db.GetSection("InputError"));  // Display input error if invalid choice
            Submit();  // Let the user try again
        }
    }


    // Displays a section of text with animated character split effect.
    private void DisplaySection(string section) {
        Anim.CharSplit(db.GetSection(section) + "\n", 1);  // Use animation for text display
    }

    // Handles the Secretary's introduction sequence, displaying initial messages.
    public void SecretaryIntro(){
        Console.Clear();
        string[] initMessage = db.GetSectionArray("SecIntro");

        foreach (string text in initMessage) {
            CharSplit(text, 15);
            Thread.Sleep(100);  // Add delay between each line for effect
            System.Console.WriteLine();
        }

        // Prompt the user to start the quiz
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("\n" + db.GetSection("QuizPressKey"));
        Console.ReadKey(true);  // Wait for user input to proceed
        Console.ForegroundColor = ConsoleColor.White;

        // Start the quiz if it's initialized
        if (quiz != null) {
            quiz.StartQuiz();
        } else {
            Console.WriteLine("Quiz-object not initialized!");  // Handle the case if quiz is not initialized
        }
    }

    // Handles the Secretary's outro sequence, thanking the player.
    public void SecretaryOutro() {
        Console.WriteLine("Tak fordi du spillede spillet!");
        quiz.StartQuiz();  // Restart the quiz at the end if necessary
    }
}