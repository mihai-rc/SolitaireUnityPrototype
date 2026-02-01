using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using PrimeTween;
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
            card.SetDroppedOverHandler(HandleCardDropOver);

            m_HiddenCards.AddLast(card);
            return m_StackingZone.GetNextPosition();
        }

        public async ValueTask ShowFirstHiddenCard()
        {
            var card = m_HiddenCards.Last.Value;
            m_HiddenCards.RemoveLast();
            
            var currentPosition = card.transform.position;
            var popPosition = new Vector3(currentPosition.x, currentPosition.y, currentPosition.z - 1f);

            await Sequence.Create()
                .Group(Tween.Position(card.transform, popPosition, 0.2f, Ease.OutQuart))
                .Group(Tween.Scale(card.transform, new Vector3(1.2f, 1.2f, 1.2f), 0.2f, Ease.OutQuart))
                .Group(Tween.Rotation(card.transform, Quaternion.Euler(0f, 0f, 0f), 0.2f, Ease.OutQuart))
                .Chain(Sequence.Create()
                .Group(Tween.Position(card.transform, currentPosition, 0.2f, Ease.OutQuart))
                .Group(Tween.Scale(card.transform, Vector3.one, 0.2f, Ease.OutQuart)));

            BindCard(card);
        }

        private void BindCard(Card card)
        {
            m_ShownCards.AddLast(card);
            card.SetOwningLane(this);

            card.SetBeginDragHandler(HandleCardBeginDrag);
            card.SetDragHandler(HandleCardDrag);
            card.SetDragEndHandler(HandleCardEndDrag);
            card.SetDroppedOverHandler(HandleCardDropOver);
            // card.SetDetachHandler(HandleDetachCard);
        }

        private void HandleCardBeginDrag(Card card)
        {
            if (m_ShownCards.Last.Value == card)
            {
                card.Draggable = false;
                m_StackingZone.DecreasePosition();

                return;
            }

            var node = m_ShownCards.Find(card);
            while (node != null)
            {
                node.Value.Draggable = false;
                m_StackingZone.DecreasePosition();
                node = node.Next;
            }
        }

        private void HandleCardDrag(Card card, Vector3 position)
        {
            if (m_ShownCards.Last.Value == card)
            {
                card.transform.position = position;
                return;
            }

            var node = m_ShownCards.Find(card);
            var spacing = Vector3.zero;
            while (node != null)
            {
                node.Value.transform.position = position + spacing;
                spacing += m_StackingZone.Spacing;
                node = node.Next;
            }
        }

        private void HandleCardEndDrag(Card card)
        {
            if (m_ShownCards.Last.Value == card)
            {
                card.Draggable = true;
                var retreatSlot = m_StackingZone.GetNextPosition();
                Tween.Position(card.transform, retreatSlot, 0.2f);

                return;
            }

            var node = m_ShownCards.Find(card);
            while (node != null)
            {
                node.Value.Draggable = true;
                var retreatSlot = m_StackingZone.GetNextPosition();
                Tween.Position(node.Value.transform, retreatSlot, 0.2f);
                node = node.Next;
            }
        }

        private void HandleCardDropOver(Card card, Card droppedCard)
        {
            if (!card.TryGetOwningLane(out var receiverLane) ||
                !droppedCard.TryGetOwningLane(out var senderLane))
            {
                return;
            }

            if (senderLane.m_ShownCards.Last.Value == droppedCard)
            {
                droppedCard.Draggable = true;
                senderLane.m_ShownCards.Remove(droppedCard);
                receiverLane.BindCard(droppedCard);

                var newSlot = receiverLane.m_StackingZone.GetNextPosition();
                Tween.Position(droppedCard.transform, newSlot, 0.2f);

                return;
            }

            var node = senderLane.m_ShownCards.Find(droppedCard);
            while (node != null)
            {
                var nextNode = node.Next;
                node.Value.Draggable = true;
                receiverLane.BindCard(node.Value);
                senderLane.m_ShownCards.Remove(node.Value);

                var newSlot = receiverLane.m_StackingZone.GetNextPosition();
                Tween.Position(node.Value.transform, newSlot, 0.2f);
                node = nextNode;
            }
        }

        // private void HandleDetachCard(Card card) => m_ShownCards.Remove(card); 
    }
}