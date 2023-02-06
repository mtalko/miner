using Miner.Exceptions;
using Miner.Logger;

namespace Miner
{
    public class Runner
    {
        private readonly ILogger _logger;

        private uint _size;

        public Runner(ILogger logger)
        {
            _logger = logger;
        }

        public async Task Run(uint size, uint holesNumber)
        {
            _size = size;

            if (ValidateInput(size, holesNumber))
            {
                await Task.Run(() =>
                {
                    var board = new ClassicBoard(size, holesNumber, _logger);
                    board.InitializeBoard();
                    var shouldContinue = true;
                    uint x = 0;
                    uint y = 0;

                    while (shouldContinue)
                    {
                        shouldContinue = ProcessNextInput(out x, out y);

                        if (shouldContinue)
                        {
                            try
                            {
                                board.OpenCell(x, y);
                            }
                            catch (BlackHoleFoundException)
                            {
                                _logger.Error($"You have found the black hole.{Environment.NewLine}");
                                break;
                            }

                            if (board.IsFinished)
                            {
                                _logger.Info($"You have successfully finished game.{Environment.NewLine}");
                                break;
                            }

                            board.PrintBoard();
                        }
                    }

                    board.PrintBoard(true);
                });
            }
        }

        private bool ProcessNextInput(out uint x, out uint y)
        {
            while (true)
            {
                Console.WriteLine("Please, enter cell you gonna open in format 'x,y' (starting from 1), or 'q' for exit:");
                var line = Console.ReadLine();

                if (line == null)
                {
                    continue;
                }

                if (line == "q")
                {
                    x = 0;
                    y = 0;
                    return false;
                }

                var args = line.Split(',');

                if (args.Length < 2)
                {
                    continue;
                }

                if (uint.TryParse(args[0], out x) &&
                    uint.TryParse(args[1], out y))
                {
                    if (_size < x)
                    {
                        _logger.Error("x couldn't be greater than size");
                    }

                    if (_size < y)
                    {
                        _logger.Error("y couldn't be greater than size");
                    }

                    x -= 1; // since user enters the indexes starting from '1', not from '0'
                    y -= 1;
                    return true;
                }
            }
        }

        private bool ValidateInput(uint size, uint holesNumber)
        {
            if ((size * size) < holesNumber)
            {
                _logger.Error($"Holes number should be less than {(size * size)}");
                return false;
            }

            return true;
        }
    }
}
