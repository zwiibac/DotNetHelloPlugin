using System;
using System.Threading.Tasks;

public enum Result 
{
    success,
    failure
}

public interface ITest
{
    public string Name { get; }
    
    public string Description { get; }

    public Task<Result> Run(Action<string> print);
}

