/*
Endgame class responsible for evaluating the player's performance in terms of energy, CO2 emissions, budget, and stability
*/
using static GameAssets;

public class Endgame {
    
    // Maximum possible values for energy output, CO2 emissions, stability, and budget
    const double maxEnergyOutput = 94500;
    const double idealCO2 = 1350000;
    const double maxCO2Emission = 10000000;
    const double maxStability = 92.5;
    const double maxBudget = 100_000_000_000;

    // Weights assigned to each factor when calculating the total score
    const double EnergyWeight = 0.25;
    const double CO2Weight = 0.25;
    const double StabilityWeight = 0.30;
    const double BudgetWeight = 0.20;

    // Player's current values for each factor
    double playerEnergyOutput;
    double playerCO2Emission;
    double playerBudgetUsage;
    double playerStability;

    // Updates the player's values based on current game data
    public void UpdatePlayerValues() {
        playerEnergyOutput = energi.GetStatusValue(); // Fetch current energy output
        playerCO2Emission = co2.GetStatusValue();  // Fetch current CO2 emissions
        playerStability = inventory.CalculateOverallStability(AtomType, SolarType, WindType, WaterType);  // Calculate overall stability
        playerBudgetUsage = budget.GetStatusValue();  // Fetch current budget usage
    }

    // Calculates the energy score as a fraction of the maximum energy output
    public double EnergyScore() {
        double energyScore = Math.Min(playerEnergyOutput / maxEnergyOutput, 1) * 10; // Max score of 10
        return energyScore;
    }

    // Calculates the CO2 score based on how close the emissions are to the ideal value
    public double Co2Score() {
        double co2Score = Math.Max(0, 1 - (playerCO2Emission - idealCO2) / (maxCO2Emission - idealCO2)) * 10; // Max score of 10
        return co2Score;
    }

    // Calculates the budget score based on how much of the budget has been used
    public double BudgetScore() {
        double budgetScore = Math.Max(0, 1 - playerBudgetUsage / maxBudget) * 10; // Max score of 10
        return budgetScore;
    }

    // Calculates the stability score based on the current overall stability
    public double StabiltyScore() {
        double stabilityScore = playerStability / maxStability * 10; // Max score of 10
        return stabilityScore;
    }

    // Evaluates and displays the player's performance in each category and overall
    public void Evaluate() {
        Console.Clear();
        UpdatePlayerValues(); // Update the player's values before evaluation
        
        // Evaluate energy score and display appropriate message
        double energyScore = EnergyScore();
        if (energyScore <= 2) {
            Console.WriteLine(db.GetSection("EndGameEnergyScorePoor") + "\n");
        }
        else if (energyScore <= 4) {
            Console.WriteLine(db.GetSection("EndGameEnergyScoreBelowAverage") + "\n");
        }
        else if (energyScore <= 6) {
            Console.WriteLine(db.GetSection("EndGameEnergyScoreAverage") + "\n");
        }
        else if (energyScore <= 8) {
            Console.WriteLine(db.GetSection("EndGameEnergyScoreGood") + "\n");
        } else {
            Console.WriteLine(db.GetSection("EndGameEnergyScoreExcellent" + "\n"));
        }
  
        // Evaluate CO2 score and display appropriate message
        double co2Score = Co2Score();
        if (co2Score <= 2) {
            Console.WriteLine(db.GetSection("EndGameCO2ScorePoor") + "\n");
        }
        else if (co2Score <= 4) {
            Console.WriteLine(db.GetSection("EndGameCO2ScoreBelowAverage") + "\n");
        }
        else if (co2Score <= 6) {
            Console.WriteLine(db.GetSection("EndGameCO2ScoreAverage") + "\n");
        }
        else if (co2Score <= 8) {
            Console.WriteLine(db.GetSection("EndGameCO2ScoreGood") + "\n");
        } else {
            Console.WriteLine(db.GetSection("EndGameCO2ScoreExcellent") + "\n");
        }

        // Evaluate budget score and display appropriate message
        double budgetScore = BudgetScore();
        if (budgetScore <= 2) {
            Console.WriteLine(db.GetSection("EndGameBudgetScorePoor") + "\n");
        }
        else if (budgetScore <= 4) {
            Console.WriteLine(db.GetSection("EndGameBudgetScoreBelowAverage") + "\n");
        }
        else if (budgetScore <= 6) {
            Console.WriteLine(db.GetSection("EndGameBudgetScoreAverage") + "\n");
        }
        else if (budgetScore <= 8) {
            Console.WriteLine(db.GetSection("EndGameBudgetScoreGood") + "\n");
        } else {
            Console.WriteLine(db.GetSection("EndGameBudgetScoreExcellent") + "\n");
        }

        // Evaluate stability score and display appropriate message
        double stabilityScore = StabiltyScore();
        if (stabilityScore <= 2) {
            Console.WriteLine(db.GetSection("EndGameStabilityScorePoor") + "\n");
        }
        else if (stabilityScore <= 4) {
            Console.WriteLine(db.GetSection("EndGameStabilityScoreBelowAverage") + "\n");
        }
        else if (stabilityScore <= 6) {
            Console.WriteLine(db.GetSection("EndGameStabilityScoreAverage") + "\n");
        }
        else if (stabilityScore <= 8) {
            Console.WriteLine(db.GetSection("EndGameStabilityScoreGood") + "\n");
        } else {
            Console.WriteLine(db.GetSection("EndGameStabilityScoreExcellent") + "\n");
        }

        // Calculate the total score based on weighted average of all category scores
        double totalScore = (energyScore * EnergyWeight) + (co2Score * CO2Weight) + (stabilityScore * StabilityWeight) + (budgetScore * BudgetWeight); // Max score of 10
        if (totalScore <= 2) {
            Console.WriteLine(db.GetSection("EndGamePoor"));
        }
        else if (totalScore <= 4) {
            Console.WriteLine(db.GetSection("EndGameBelowAverage"));
        }
        else if (totalScore <= 6) {
            Console.WriteLine(db.GetSection("EndGameAverage"));
        }
        else if (totalScore <= 8) {
            Console.WriteLine(db.GetSection("EndGameGood"));
        } else {
            Console.WriteLine(db.GetSection("EndGameExcellent"));
        }

        // Finalize the endgame and prompt the player for the next step
        context.ClickNext();
        context.GetCurrent().Goodbye();
        quiz.StartQuiz(); // Start a quiz for the player to continue
        context.MakeDone(); // Mark the game as finished
    }
}