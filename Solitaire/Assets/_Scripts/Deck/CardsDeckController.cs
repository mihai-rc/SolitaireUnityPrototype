using System;
using System.Collections.Generic;
using UnityEngine.Pool;
using Solitaire.Cards;

namespace Solitaire.Deck
{
    public class CardsDeckController : IDisposable
    {
        private readonly CardsDeckView m_View;
        private readonly List<StackableCard> m_StackableCards;

        public CardsDeckController(CardsDeckView view)
        {
            m_View = view;
            m_StackableCards = ListPool<StackableCard>.Get();

            InitCardsStack();
        }

        public StackableCard Shuffle()
        {
            for (var i = 0; i < m_StackableCards.Count; i++)
            {
                var randomIndex = UnityEngine.Random.Range(0, m_StackableCards.Count);
                (m_StackableCards[i], m_StackableCards[randomIndex]) = (m_StackableCards[randomIndex], m_StackableCards[i]);
            }

            StackableCard previous = null;
            foreach (var card in m_StackableCards)
            {
                if (previous != null)
                {
                    card.TryAdd(previous);
                }

                previous = card;
            }

            return previous;
        }

        public void ForEach(Action<StackableCard> iteratorFn)
        {
            // var card = m_FirstCard;
            // while (card != null)
            // {
            //     iteratorFn?.Invoke(card);
            //     card = card.Next;
            // }

            foreach (var card in m_StackableCards)
            {
                iteratorFn?.Invoke(card);
            }
        }

        public void Dispose()
        {
            ListPool<StackableCard>.Release(m_StackableCards);
        }

        private void InitCardsStack()
        {
            var configsMap = DictionaryPool<Symbols, CardConfig>.Get();
            foreach (var cardConfig in m_View.Configs)
            {
                //TODO: Validate and handle config file misconfigurations.
                configsMap[cardConfig.Symbol] = cardConfig;
            }

            foreach (var cardView in m_View.CardViews)
            {
                var cardConfig = configsMap[cardView.Data.Symbol];
                cardView.Configure(cardConfig);

                var current = new StackableCard(cardView);
                m_StackableCards.Add(current);
            }

            DictionaryPool<Symbols, CardConfig>.Release(configsMap);
        }

        // private void AddCardToStack(CardView cardView, ref StackableCard previous)
        // {
        //     if (previous != null)
        //     {
        //         if (!current.TryAdd(current))
        //         {
        //             //TODO: Handle error
        //             return;
        //         }
        //     }
        //
        //     previous = current;
        // }
    }
}