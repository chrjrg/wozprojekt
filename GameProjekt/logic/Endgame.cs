using static GameAssets;

public class Endgame {
    
    const double maxEnergyOutput = 0;
    const double idealCO2 = 0;
    const double maxCO2Emission = 0;
    const double maxStability = 0;
    const double maxBudget = 0;

    double playerEnergyOutput = 0;
    double playerCO2Emission = 0;
    double playerStability = 0;
    double playerBudgetUsage = 0;

    public void UpdatePlayerValues() {
        playerEnergyOutput = 0; //Get status
        playerCO2Emission = 0;  //Get status
        playerStability = 0;  //Get status
        playerBudgetUsage = 0;  //Get status
    }

    public int Evaluate()
    {
    // Energy Score
    double energyScore = Math.Min(playerEnergyOutput / maxEnergyOutput, 1) * 10; // 10 is the max score

    // CO2 Score
    double co2Score = Math.Max(0, 1 - (playerCO2Emission - idealCO2) / (maxCO2Emission - idealCO2)) * 10; // 10 is the max score

    // Stability Score
    double stabilityScore = (playerStability / maxStability) * 10; // 10 is the max score

    // Budget Score
    double budgetScore = Math.Max(0, 1 - playerBudgetUsage / maxBudget) * 10; // 10 is the max score

    // Total Score (vægtet gennemsnit)
    double totalScore = (energyScore * 0.3) + (co2Score * 0.3) + (stabilityScore * 0.2) + (budgetScore * 0.2); // 10 is the max score

    // Evaluate based on cutoff
    if (totalScore <= 3) return 1;      // Poor
    else if (totalScore <= 5) return 2; // Below Average
    else if (totalScore <= 7) return 3; // Average
    else if (totalScore <= 9) return 4; // Good
    else return 5;                      // Excellent
    }
}