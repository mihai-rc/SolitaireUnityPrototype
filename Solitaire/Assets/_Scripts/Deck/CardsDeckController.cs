using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine.Pool;
using Solitaire.Cards;

namespace Solitaire.Deck
{
    public class CardsDeckController : IDisposable
    {
        private readonly CardsDeckView m_View;
        private readonly List<CardView> m_Cards;

        public CardsDeckController(CardsDeckView view)
        {
            m_View = view;
            m_Cards = ListPool<CardView>.Get();

            InitCardsStack();
        }

        public void Shuffle()
        {
            for (var i = 0; i < m_Cards.Count; i++)
            {
                var randomIndex = UnityEngine.Random.Range(0, m_Cards.Count);
                (m_Cards[i], m_Cards[randomIndex]) = (m_Cards[randomIndex], m_Cards[i]);
            }

            // StackableCard previous = null;
            // foreach (var card in m_StackableCards)
            // {
            //     if (previous != null)
            //     {
            //         card.TryAdd(previous);
            //     }
            //
            //     previous = card;
            // }
            //
            // return previous;
        }

        public async ValueTask ForEachAsync<T>(T context, Func<T, CardView, ValueTask> iteratorFn)
        {
            foreach (var card in m_Cards)
            {
                if (iteratorFn != null)
                {
                    await iteratorFn(context, card);
                }
            }
        }

        public void Dispose()
        {
            ListPool<CardView>.Release(m_Cards);
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
                m_Cards.Add(cardView);
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