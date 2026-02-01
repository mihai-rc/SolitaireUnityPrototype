using System.Collections.Generic;
using System.Threading.Tasks;
using PrimeTween;
using Solitaire.Cards;
using UnityEngine;

namespace Solitaire
{
    public class Dealer
    {
        private readonly Deck m_Deck;
        private readonly StackingZone m_StackingZone;
        private readonly LinkedList<Card> m_Cards = new();

        public Dealer(Deck deck, StackingZone stackingZone)
        {
            m_Deck = deck;
            m_StackingZone = stackingZone;

            m_Deck.Init();
        }

        public async ValueTask FillDeckAsync()
        {
            m_Deck.Shuffle();
            foreach (var card in m_Deck.Cards)
            {
                m_Cards.AddLast(card);

                var deckSlot = m_StackingZone.GetNextPosition();
                MoveCardToDeck(card, deckSlot);

                await Awaitable.WaitForSecondsAsync(0.01f);
            }

            await Awaitable.WaitForSecondsAsync(0.5f);
        }

        public Card Deal()
        {
            var card = m_Cards.Last.Value;
            m_Cards.RemoveLast();
            m_StackingZone.DecreasePosition();

            return card;
        }

        private void MoveCardToDeck(Card card, Vector3 deckSlot)
        {
            var midpoint = (card.transform.position + deckSlot) / 2f;
            var offset = deckSlot - midpoint;
            var axis = card.transform.up * -1;

            Tween.Custom(0, 180, 0.2f, angle =>
            {
                card.transform.position = midpoint + Quaternion.AngleAxis(180 - angle, axis) * offset;
                card.transform.eulerAngles = new Vector3(0f, angle, 0f);
            },
            Ease.OutQuad);
        }
    }
}