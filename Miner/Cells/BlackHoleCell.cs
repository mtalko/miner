using Miner.Exceptions;

namespace Miner.Cells
{
    public class BlackHoleCell : Cell
    {
        private readonly string BlackHoleName = "H";

        public override string Label => BlackHoleName;

        public override void Open()
        {
            base.Open();

            throw new BlackHoleFoundException();
        }
    }
}
