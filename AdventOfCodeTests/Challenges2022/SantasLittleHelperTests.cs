namespace AdventOfCodeTests.Challenges2022;

public class SantasLittleHelperTests
{
    [Test]
    public void Test_Challenge1_GetHighestCalorieElf()
    {
        var value = AdventOfCode.Challenges2022.SantasLittleHelper.Challenge1_GetHighestCalorieElf();
        Console.WriteLine($"Highest calories consumed by an elf: {value}");
        Assert.Pass();
    }
    
    [Test]
    public void Test_Challenge2_GetHighestCalorieElves()
    {
        var limit = 3;
        var value = AdventOfCode.Challenges2022.SantasLittleHelper.Challenge2_GetHighestCalorieElves(limit);
        Console.WriteLine($"Highest calories consumed by top {limit} elves: {value}");
        Assert.Pass();
    }
}
