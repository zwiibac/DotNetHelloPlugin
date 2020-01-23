using System;
using System.Threading.Tasks;

public class WheelTest : ITest
{
    public string Name => "WheelTest";

    public string Description => "Test the wings patience ..";

    public async Task<Result> Run(Action<string> print)
    {
        var rand = new Random();
        var num = rand.Next(100);

        if (num < 6) 
        {
            print("Error: object under test is not nearly a wheel");
            return Result.failure;
        }
        
        await Task.Delay(num);
               
        return Result.success;
    }
}
