namespace Solitaire.Cards
{
    public class CardController
    {
        private readonly CardView m_CardView;

        public CardData CardData => m_CardView.Data;

        public CardController(CardView cardView, CardConfig config)
        {
            m_CardView = cardView;
            m_CardView.Configure(config);
        }
    }
}