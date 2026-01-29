namespace Solitaire.Cards
{
    public class CardController
    {
        private readonly CardView m_CardView;

        public CardData CardData => m_CardView.Data;

        public CardController(string letter, CardConfig config, CardView cardView)
        {
            m_CardView = cardView;
            m_CardView.Init(letter, config);
        }
    }
}