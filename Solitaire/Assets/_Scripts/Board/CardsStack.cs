using System;
using System.Threading.Tasks;
using Solitaire.Cards;

namespace Solitaire.Board
{
    public class CardsStack
    {
        private readonly StackingZone m_Zone;
        // private StackableCard m_FirstCard;
        // private StackableCard m_LastCard;

        // public CardsStack(StackingZone zone) => m_Zone = zone;
        //
        // public void AddCard(StackableCard card)
        // {
        //     m_LastCard = card;
        //     if (m_FirstCard == null)
        //     {
        //         m_FirstCard = card;
        //         return;
        //     }
        //
        //     m_FirstCard.TryAdd(card);
        // }
        //
        // public async ValueTask ForEachAsync<T>(T context, Func<T, StackableCard, ValueTask> iteratorFn)
        // {
        //     var card = m_FirstCard;
        //     while (card != null)
        //     {
        //         if (iteratorFn != null)
        //         {
        //             await iteratorFn(context, card);
        //         }
        //
        //         card = card.Next;
        //     }
        // }
    }
}