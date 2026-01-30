using System;
using System.Collections.Generic;
using UnityEngine.Pool;

namespace Solitaire.Cards
{
    public class CardsDeckController : IDisposable
    {
        private readonly CardsDeckView m_View;
        private readonly List<CardController> m_Cards;

        public CardsDeckController(CardsDeckView view)
        {
            m_View = view;
            m_Cards = ListPool<CardController>.Get();

            InitCards();
        }

        public void Dispose()
        {
            ListPool<CardController>.Release(m_Cards);
        }

        private void InitCards()
        {
            var configsMap = DictionaryPool<Symbols, CardConfig>.Get();
            foreach (var cardConfig in m_View.Configs)
            {
                configsMap[cardConfig.Symbol] = cardConfig;
            }

            foreach (var cardView in m_View.CardViews)
            {
                var cardConfig = configsMap[cardView.Data.Symbol];
                m_Cards.Add(new CardController(cardView, cardConfig));
            }

            DictionaryPool<Symbols, CardConfig>.Release(configsMap);
        }
    }
}