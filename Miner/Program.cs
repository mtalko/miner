using Miner;
using Miner.Logger;

while (true)
{
    Console.WriteLine("Please, enter size and black holes number, for example, '3,4' means board 3*3 with 4 black holes inside, or 'q' for exit:");
    var line = Console.ReadLine();

    if (line == null)
    {
        continue;
    }

    if (line == "q")
    {
        return 0;
    }

    var input = line.Split(',');

    if (input.Length < 2)
    {
        continue;
    }

    uint x = 0;
    uint y = 0;
    if (uint.TryParse(input[0], out x) && uint.TryParse(input[1], out y))
    {
        var runner = new Runner(new ConsoleLogger());
        runner.Run(x, y).GetAwaiter().GetResult();
    }
}