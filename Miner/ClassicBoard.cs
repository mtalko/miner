using Miner.Cells;
using Miner.Logger;

namespace Miner
{
    public class ClassicBoard
    {
        private readonly ILogger _logger;
        private readonly ICell[,] _board;

        private readonly uint _holesNumber;
        private readonly uint _size;

        private uint _shouldBeOppened;

        public ClassicBoard(uint size, uint holesNumber, ILogger logger)
        {
            _size = size;
            _board = new ICell[_size, _size];
            _holesNumber = holesNumber;
            _shouldBeOppened = (_size * _size) - _holesNumber;
            _logger = logger;
        }

        public bool IsFinished => _shouldBeOppened == 0;

        public void PrintBoard(bool shouldOpen = false)
        {
            for (var i = 0; i < _board.GetLength(0); i++)
            {
                for (var j = 0; j < _board.GetLength(1); j++)
                {
                    if (shouldOpen)
                    {
                        _board[i, j].IsOpenned = true;
                    }

                    _logger.Info($"{_board[i, j]} ");
                }

                _logger.Info(Environment.NewLine);
            }
        }

        public void InitializeBoard()
        {
            InitializeBlackHoles();
            CalculateBoard();
            PrintBoard();
        }

        public void InitializeBlackHoles()
        {
            var rnd = new Random();

            for (var i = 0; i < _holesNumber; i++)
            {
                var x = rnd.Next((int)_size);
                var y = rnd.Next((int)_size);

                if (_board[x, y] == null)
                {
                    _board[x, y] = new BlackHoleCell();
                }
                else
                {
                    i--;
                }
            }
        }

        // Set the values for cells in the board
        public void CalculateBoard()
        {
            for (uint i = 0; i < _board.GetLength(0); i++)
            {
                for (uint j = 0; j < _board.GetLength(1); j++)
                {
                    if (_board[i, j] == null)
                    {
                        _board[i, j] = new Cell();
                    }
                    else if (_board[i, j] is BlackHoleCell)
                    {
                        MarkAdjacent(i, j);
                    }
                }
            }
        }

        public void OpenCell(uint x, uint y)
        {
            var cell = _board[x, y];
            if (!cell.IsOpenned)
            {
                cell.Open();
                _shouldBeOppened--;

                if (cell.NumberOfAdjacentBH == 0)
                {
                    OpenAdjacent(x, y);
                }
            }
        }

        // Opens all adjacent cells (in case cell value is '0')
        private void OpenAdjacent(uint x, uint y)
        {
            ActionAdjacentCells(x, y, (i, j) => OpenCell(i, j));
        }

        // Adds 1 to all adjacent cells 
        private void MarkAdjacent(uint x, uint y)
        {
            ActionAdjacentCells(x, y, (i, j) =>
            {
                if (_board[i, j] == null)
                {
                    _board[i, j] = new Cell()
                    {
                        NumberOfAdjacentBH = 1
                    };
                }
                else
                {
                    _board[i, j].NumberOfAdjacentBH++;
                }
            });
        }

        private void ActionAdjacentCells(uint x, uint y, Action<uint, uint> action)
        {
            var lowX = x == 0 ? x : x - 1;
            var highX = x == (_size - 1) ? x : x + 1;
            var lowY = y == 0 ? y : y - 1;
            var hihgY = y == (_size - 1) ? y : y + 1;

            for (var i = lowX; i <= highX; i++)
            {
                for (var j = lowY; j <= hihgY; j++)
                {
                    if (x != i || y != j)
                    {
                        action(i, j);
                    }
                }
            }
        }
    }
}
