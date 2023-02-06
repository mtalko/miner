namespace Miner.Logger
{
    public class ConsoleLogger : ILogger
    {
        public void Info(string message)
        {
            Console.Write(message);
        }

        public void Error(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Info(message);
            Console.ResetColor();
        }
    }
}
