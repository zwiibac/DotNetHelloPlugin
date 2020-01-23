using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace HelloPlugin
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var plugins = new List<string> 
            { 
                @"..\..\..\..\WingTest\bin\Debug\netcoreapp3.1\WingTest.dll",
                @"..\..\..\..\WheelTest\bin\Debug\netcoreapp3.1\WheelTest.dll"
            };

            Console.WriteLine("Search these plugins:");
            foreach (var path in plugins) 
            {
                Console.WriteLine(Path.GetFullPath(path));
            }

            Console.WriteLine("Load plugins:");

            foreach (var path in plugins)
            {
                Console.WriteLine(Path.GetFullPath(path));
            }

            IEnumerable<ITest> tests = plugins.SelectMany(
                pluginLocation => {
                    PluginLoadContext loadContext = new PluginLoadContext(Path.GetFullPath(pluginLocation));
                    Assembly assembly = loadContext.LoadFromAssemblyName(new AssemblyName(Path.GetFileNameWithoutExtension(pluginLocation)));
                    return assembly.GetTypes().Where(x => typeof(ITest).IsAssignableFrom(x)).Select(x => Activator.CreateInstance(x) as ITest);                    
                }
                );

            foreach (var test in tests) 
            {
                Console.WriteLine($"Run test: {test.Name} - {test.Description}");
                Console.WriteLine("Test ouput:");

                var results = Enumerable.Range(0, 10).Select(_ => test.Run((x) => Console.WriteLine(x))).ToArray();
                await Task.WhenAll(results);

                Console.WriteLine($"Test result: {results.Count(x => x.Result == Result.success)} success," +
                    $" {results.Count(x => x.Result == Result.failure)} failure");
                Console.WriteLine("--------------------------------------------------------------------------------");
            }


        }
    }
}
