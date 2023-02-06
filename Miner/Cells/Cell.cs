namespace Miner.Cells
{
    public class Cell : ICell
    {
        public int NumberOfAdjacentBH { get; set; }

        public virtual string Label => NumberOfAdjacentBH.ToString();

        public bool IsOpenned { get; set; }

        public virtual void Open()
        {
            IsOpenned = true;
        }

        public override string ToString()
        {
            return IsOpenned ? Label : "?";
        }
    }
}
