namespace AdventOfCodeTests.Challenges2022;

public class SantasLittleHelperTests
{
    [Test]
    public void Test_Challenge1_GetHighestCalorieElf()
    {
        var value = AdventOfCode.Challenges2022.SantasLittleHelper.Challenge1_GetHighestCalorieElf();
        Console.WriteLine($"Highest calories carried by an elf: {value}");
        Assert.That(value, Is.EqualTo(74394));
    }
    
    [Test]
    public void Test_Challenge2_GetHighestCalorieElves()
    {
        var limit = 3;
        var value = AdventOfCode.Challenges2022.SantasLittleHelper.Challenge2_GetHighestCalorieElves(limit);
        Console.WriteLine($"Total calories carried by the top {limit} elves: {value}");
        Assert.That(value, Is.EqualTo(212836));
    }

    [Test]
    public void Test_Challenge3_RockPaperScissors()
    {
        var value = AdventOfCode.Challenges2022.SantasLittleHelper.Challenge3_RockPaperScissors();
        Console.WriteLine($"Total score from strategy guide: {value}");
        Assert.That(value, Is.EqualTo(11906));
    }

    [Test]
    public void Test_Challenge4_RockPaperScissors()
    {
        var value = AdventOfCode.Challenges2022.SantasLittleHelper.Challenge4_RockPaperScissors();
        Console.WriteLine($"Total score from new strategy guide: {value}");
        Assert.That(value, Is.EqualTo(11186));
    }
}
