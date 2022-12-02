namespace AdventOfCode.Challenges2022;

/// <summary>
/// SantasLittleHelper helping Santa to solve to all the Challenges.
/// </summary>
public class SantasLittleHelper
{
    private const string PathCalorieSheet = 
        @"/Users/briannabecker/RiderProjects/AdventOfCode/AdventOfCode/Challenges2022/Input/elves_calorie_sheet.txt";
    
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
        foreach (var line in System.IO.File.ReadLines(PathCalorieSheet))
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
}
