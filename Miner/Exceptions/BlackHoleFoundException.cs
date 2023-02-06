namespace Miner.Exceptions
{
    public class BlackHoleFoundException : Exception
    {
        public BlackHoleFoundException()
        {
        }

        public BlackHoleFoundException(string message)
            : base(message)
        {
        }

        public BlackHoleFoundException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
