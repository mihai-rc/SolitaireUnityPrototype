using System.Collections.Generic;
using System.Threading.Tasks;
using PrimeTween;
using UnityEngine;
using Solitaire.Cards;

namespace Solitaire
{
    public class Lane
    {
        private readonly StackingZone m_StackingZone;
        private readonly LinkedList<Card> m_HiddenCards = new();
        private readonly LinkedList<Card> m_ShownCards = new();

        public Lane(StackingZone stackingZone) => m_StackingZone = stackingZone;

        public Vector3 AddHiddenCard(Card card)
        {
            m_HiddenCards.AddLast(card);
            return m_StackingZone.GetNextPosition();
        }

        public async ValueTask ShowNextCard()
        {
            var card = m_HiddenCards.Last.Value;
            m_HiddenCards.RemoveLast();
            m_ShownCards.AddLast(card);
            
            var currentPosition = card.transform.position;
            var popPosition = new Vector3(currentPosition.x, currentPosition.y, currentPosition.z - 1f);

            await Sequence.Create()
                .Group(Tween.Position(card.transform, popPosition, 0.2f, Ease.OutQuart))
                .Group(Tween.Scale(card.transform, new Vector3(1.2f, 1.2f, 1.2f), 0.2f, Ease.OutQuart))
                .Group(Tween.Rotation(card.transform, Quaternion.Euler(0f, 0f, 0f), 0.2f, Ease.OutQuart))
                .Chain(Sequence.Create()
                .Group(Tween.Position(card.transform, currentPosition, 0.2f, Ease.OutQuart))
                .Group(Tween.Scale(card.transform, Vector3.one, 0.2f, Ease.OutQuart)));
        }
    }
}