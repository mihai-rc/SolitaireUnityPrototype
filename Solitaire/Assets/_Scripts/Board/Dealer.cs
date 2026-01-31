using System.Collections.Generic;
using Solitaire.Cards;

namespace Solitaire
{
    public class Dealer
    {
        private readonly LinkedList<Card> m_DeckList = new();

        public void Add(Card card) => m_DeckList.AddLast(card);

        public Card Deal()
        {
            var card = m_DeckList.Last.Value;
            m_DeckList.RemoveLast();

            return card;
        }
    }
}