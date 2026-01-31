using Solitaire.DataStructures;

namespace Solitaire.Cards
{
    public class StackableCard : IStackable<StackableCard>
    {
        public StackableCard Previous { get; private set; }

        public StackableCard Next { get; private set; }

        public CardView View { get; }

        public StackableCard(CardView cardView) => View = cardView;

        public bool TryAdd(StackableCard stackable)
        {
            if (Next != null)
            {
                return false;
            }

            Next = stackable;
            stackable.Previous = this;

            return true;
        }

        public void DetachSubstack()
        {
            if (Previous != null)
            {
                Previous.Next = null;
            }

            Previous = null;
        }
    }
}