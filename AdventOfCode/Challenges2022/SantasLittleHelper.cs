namespace AdventOfCode.Challenges2022;

public class SantasLittleHelper
{
    private const string PATH_CALORIE_SHEET = @"/Users/briannabecker/RiderProjects/AdventOfCode/AdventOfCode/Challenges2022/Input/elves_calorie_sheet.txt";
        
    public static int Challenge1_GetHighestCalorieElf()
    {
        var summedCalories = 0;
        var highestCalories = 0;
        foreach (var line in System.IO.File.ReadLines(PATH_CALORIE_SHEET))
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
    
    public static int Challenge2_GetHighestCalorieElves(int limit)
    {
        var summedCalories = 0;
        var totalCaloriesOfAllElves = new List<int>();
        foreach (var line in System.IO.File.ReadLines(PATH_CALORIE_SHEET))
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
