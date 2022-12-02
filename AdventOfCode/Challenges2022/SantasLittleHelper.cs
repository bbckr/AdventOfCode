namespace AdventOfCode.Challenges2022;

/// <summary>
/// SantasLittleHelper helping Santa to solve to all the Challenges.
/// </summary>
public class SantasLittleHelper
{
    private static readonly string? ProjectDirectory = Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.Parent?.FullName;
    private static readonly string PathCalorieSheet = $"{ProjectDirectory}/AdventOfCode/Challenges2022/Input/elves_calorie_sheet.txt";
    private static readonly string PathRockPaperScissors = $"{ProjectDirectory}/AdventOfCode/Challenges2022/Input/rock_paper_scissors.txt";

    /// <summary>
    /// Challenge #1
    /// This list in `PathCalorieSheet` represents the Calories of the food carried by five Elves:
    ///
    /// The first Elf is carrying food with 1000, 2000, and 3000 Calories, a total of 6000 Calories.
    /// The second Elf is carrying one food item with 4000 Calories.
    /// The third Elf is carrying food with 5000 and 6000 Calories, a total of 11000 Calories.
    /// The fourth Elf is carrying food with 7000, 8000, and 9000 Calories, a total of 24000 Calories.
    /// The fifth Elf is carrying one food item with 10000 Calories.
    /// In case the Elves get hungry and need extra snacks, they need to know which Elf to ask: they'd like to know how
    /// many Calories are being carried by the Elf carrying the most Calories. In the example above, this is 24000
    /// (carried by the fourth Elf).
    ///
    /// Find the Elf carrying the most Calories. How many total Calories is that Elf carrying?
    /// </summary>
    /// <returns>The number of calories (int) held by the elf carrying the most calories.</returns>
    public static int Challenge1_GetHighestCalorieElf()
    {
        var summedCalories = 0;
        var highestCalories = 0;
        foreach (var line in System.IO.File.ReadLines(PathCalorieSheet))
        {
            if (string.IsNullOrEmpty(line))
            {
                if (summedCalories > highestCalories)
                {
                    highestCalories = summedCalories;
                }
                
                // Reset
                summedCalories = 0;
            }
            
            int.TryParse(line, out var calories);
            summedCalories += calories;
        }

        return highestCalories;
    }
    
    /// <summary>
    /// Challenge #2
    /// By the time you calculate the answer to Challenge #1, they've already realized that the Elf carrying the most
    /// Calories of food might eventually run out of snacks.
    /// 
    /// To avoid this unacceptable situation, the Elves would instead like to know the total Calories carried by the top
    /// three Elves carrying the most Calories. That way, even if one of those Elves runs out of snacks, they still have
    /// two backups.
    /// 
    /// In the example above, the top three Elves are the fourth Elf (with 24000 Calories), then the third Elf (with
    /// 11000 Calories), then the fifth Elf (with 10000 Calories). The sum of the Calories carried by these three elves
    /// is 45000.
    /// 
    /// Find the top three Elves carrying the most Calories. How many Calories are those Elves carrying in total?
    /// </summary>
    /// <param name="limit">The limit of highest calorie carrying Elves to sum together.</param>
    /// <returns>The sum (int) of all the calories of the top highest calorie carrying Elves.</returns>
    public static int Challenge2_GetHighestCalorieElves(int limit)
    {
        var summedCalories = 0;
        var totalCaloriesOfAllElves = new List<int>();
        foreach (var line in File.ReadLines(PathCalorieSheet))
        {
            if (string.IsNullOrEmpty(line))
            {
                totalCaloriesOfAllElves.Add(summedCalories);
                // Reset
                summedCalories = 0;
            }
            
            int.TryParse(line, out var calories);
            summedCalories += calories;
        }
        
        totalCaloriesOfAllElves.Sort();
        var totalElves = totalCaloriesOfAllElves.Count;
        var totalCaloriesOfHighestElves = 0;
        for (var i = totalElves - 1; i > -1 && i > totalElves - limit - 1; i--)
        {
            totalCaloriesOfHighestElves += totalCaloriesOfAllElves[i];
        }

        return totalCaloriesOfHighestElves;
    }

    public static Dictionary<Attack, RockPaperScissorsAttack> RockPaperScissorsAttackMap
        = new Dictionary<Attack, RockPaperScissorsAttack>
    {
        { Attack.Rock, new (Attack.Rock, 1, Attack.Paper, Attack.Scissors) },
        { Attack.Paper, new (Attack.Paper, 2, Attack.Scissors, Attack.Rock) },
        { Attack.Scissors, new (Attack.Scissors, 3, Attack.Rock, Attack.Paper) }
    };

    /// <summary>
    /// Challenge #3
    ///
    /// This strategy guide predicts and recommends the following:
    /// 
    /// In the first round, your opponent will choose Rock(A), and you should choose Paper(Y).
    /// This ends in a win for you with a score of 8 (2 because you chose Paper + 6 because you won).
    /// 
    /// In the second round, your opponent will choose Paper(B), and you should choose Rock(X).
    /// This ends in a loss for you with a score of 1 (1 + 0).
    /// The third round is a draw with both players choosing Scissors, giving you a score of 3 + 3 = 6.
    /// In this example, if you were to follow the strategy guide, you would get a total score of 15
    /// (8 + 1 + 6).
    /// 
    /// What would your total score be if everything goes exactly according to your strategy guide?
    /// </summary>
    /// <returns>The total score (int) based on the strategy guide.</returns>
    public static int Challenge3_RockPaperScissors()
    {
        var strategyMap = new Dictionary<string, RockPaperScissorsAttack>
        {
            ["A"] = RockPaperScissorsAttackMap[Attack.Rock],
            ["B"] = RockPaperScissorsAttackMap[Attack.Paper],
            ["C"] = RockPaperScissorsAttackMap[Attack.Scissors],
            ["X"] = RockPaperScissorsAttackMap[Attack.Rock],
            ["Y"] = RockPaperScissorsAttackMap[Attack.Paper],
            ["Z"] = RockPaperScissorsAttackMap[Attack.Scissors]
        };

        var finalScore = 0;

        foreach (var line in File.ReadLines(PathRockPaperScissors))
        {
            var moves = line.Split(" ");
            var player1Attack = strategyMap[moves[0]];
            var player2Attack = strategyMap[moves[1]];
            finalScore += player2Attack.Score(player1Attack);
        }

        return finalScore;
    }

    /// <summary>
    /// Challenge #4
    ///
    /// The total score is still calculated in the same way, but now you need to figure out what shape
    /// to choose so the round ends as indicated. The example above now goes like this:
    /// 
    /// In the first round, your opponent will choose Rock(A), and you need the round to end in a draw(Y),
    /// so you also choose Rock.This gives you a score of 1 + 3 = 4.
    /// In the second round, your opponent will choose Paper(B), and you choose Rock so you lose(X) with
    /// a score of 1 + 0 = 1.
    /// In the third round, you will defeat your opponent's Scissors with Rock for a score of 1 + 6 = 7.
    /// 
    /// Now that you're correctly decrypting the ultra top secret strategy guide, you would get a total
    /// score of 12.
    /// 
    /// Following the Elf's instructions for the second column, what would your total score be if
    /// everything goes exactly according to your strategy guide?
    /// </summary>
    /// <returns>The total score (int) based on the new strategy guide.</returns>
    public static int Challenge4_RockPaperScissors()
    {
        var strategyMap = new Dictionary<string, RockPaperScissorsAttack>
        {
            ["A"] = RockPaperScissorsAttackMap[Attack.Rock],
            ["B"] = RockPaperScissorsAttackMap[Attack.Paper],
            ["C"] = RockPaperScissorsAttackMap[Attack.Scissors]
        };

        var outcomeMap = new Dictionary<string, Outcome>
        {
            ["X"] = Outcome.Loss,
            ["Y"] = Outcome.Draw,
            ["Z"] = Outcome.Win
        };

        var finalScore = 0;

        foreach (var line in File.ReadLines(PathRockPaperScissors))
        {
            var moves = line.Split(" ");
            var player1Attack = strategyMap[moves[0]];
            var outcome = outcomeMap[moves[1]];

            finalScore += player1Attack.ScoreOpponent(outcome);
        }

        return finalScore;
    }
}

public class RockPaperScissorsAttack
{
    /// <summary>
    /// Self is the type of attack.
    /// </summary>
    private readonly Attack Self;
    
    /// <summary>
    /// Value is the points for the attack, no matter win or lose.
    /// </summary>
    private readonly int Value;
    
    /// <summary>
    /// Weakness is the type of attack this attack will lose against.
    /// </summary>
    private readonly Attack Weakness;
    
    /// <summary>
    /// Strength is the type of attach this attack with win against.
    /// </summary>
    private readonly Attack Strength;

    public RockPaperScissorsAttack(Attack self, int value, Attack weakness, Attack strength)
    {
        Self = self;
        Value = value;
        Weakness = weakness;
        Strength = strength;
    }

    /// <summary>
    /// Shoot finds the outcome of the round for the given opponent.
    /// </summary>
    /// <param name="opponent">The opponent to shoot against.</param>
    /// <returns></returns>
    private Outcome Shoot(RockPaperScissorsAttack opponent)
    {
        if (Weakness == opponent.Self)
        {
            return Outcome.Loss;
        }

        if (Self == opponent.Self)
        {
            return Outcome.Draw;
        }

        return Outcome.Win;
    }

    /// <summary>
    /// If returns the opponent you would have to face for a given outcome to happen.
    /// </summary>
    /// <param name="outcome">The desired outcome of the round.</param>
    /// <param name="reverse">If you want the outcome of the round for the opponent.</param>
    /// <returns>The opponent (RockPaperScissorsAttack) to face.</returns>
    private RockPaperScissorsAttack If(Outcome outcome, bool reverse = false)
    {
        if (outcome == Outcome.Draw)
        {
            return SantasLittleHelper.RockPaperScissorsAttackMap[Self];
        }
        
        if ((outcome == Outcome.Loss && !reverse) || (outcome == Outcome.Win && reverse))
        {
            return SantasLittleHelper.RockPaperScissorsAttackMap[Weakness];
        }

        return SantasLittleHelper.RockPaperScissorsAttackMap[Strength];
    }

    /// <summary>
    /// Score shoots against an opponent and tallies the score for the round.
    /// </summary>
    /// <param name="opponent">The opponent to shoot against.</param>
    /// <returns>The round score (int).</returns>
    public int Score(RockPaperScissorsAttack opponent)
    {
        var outcome = this.Shoot(opponent);
        return Score(outcome);
    }

    /// <summary>
    /// Score tallies the score for the given outcome of the round.
    /// </summary>
    /// <param name="outcome">The outcome to simulate.</param>
    /// <returns>The round score (int).</returns>
    private int Score(Outcome outcome)
    {
        return (int)outcome + Value;
    }

    /// <summary>
    /// ScoreOpponent tallies the score for the round based on the opponent outcome.
    /// </summary>
    /// <param name="outcome">The opponent outcome.</param>
    /// <returns>The round score (int) of the opponent.</returns>
    public int ScoreOpponent(Outcome outcome)
    {
        var roundScore = (int)outcome;

        var player2Attack = If(outcome, true);
        roundScore += player2Attack.Value;

        return roundScore;
    }
}

/// <summary>
/// Attack is the type of attack for a rock paper scissors game.
/// </summary>
public enum Attack
{
    Rock,
    Paper,
    Scissors
}

/// <summary>
/// Outcome is the round result with the score of the outcome.
/// </summary>
public enum Outcome
{
    Win = 6,
    Draw = 3,
    Loss = 0
}
