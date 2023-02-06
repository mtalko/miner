namespace Miner.Cells
{
    public interface ICell
    {
        bool IsOpenned { get; set; }

        int NumberOfAdjacentBH { get; set; }

        void Open();
    }
}
