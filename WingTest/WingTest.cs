using System;
using System.Threading.Tasks;

public class WingTest : ITest
{
    public string Name => "WingTest";

    public string Description => "Test lift and weight";

    public async Task<Result> Run(Action<string> print)
    {
        var rand = new Random();
        var num = rand.Next(1000);
        if (num < 500)
            return Result.success;
        else if (num < 750)
            print("Failure: Wing is too heavy");
        else
            print("Failure: Lift is too low");
        
        return Result.failure;
    }
}

public class WingWaitTest : ITest 
{
    public string Name => "WingWaitTest";

    public string Description => "Test the wings patience ..";

    public async Task<Result> Run(Action<string> print)
    {
        var rand = new Random();
        var num = rand.Next(10000);
        print($"Wait for {num} millisecons");
        await Task.Delay(num);
        print($"at least {num} millisecons later ...");

        return Result.success;
    }
}

