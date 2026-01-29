using System.Collections.Generic;
using UnityEngine;

namespace Solitaire.Cards
{
    public class CardsDeckController
    {
        private const int k_CardTypes = 4;
        private const int k_CardsOfSameType = 13;

        private readonly CardsDeckView m_View;
        private readonly List<CardController> m_Cards;

        public CardsDeckController(CardsDeckView view)
        {
            m_View = view;
            m_Cards = InitCards();
        }

        private List<CardController> InitCards()
        {
            var cards = new List<CardController>();
            for (var typeIndex = 0; typeIndex < k_CardTypes; typeIndex++)
            {
                for (var cardIndex = 0; cardIndex < k_CardsOfSameType; cardIndex++)
                {
                    var cardLetter = GetLetterByIndex(cardIndex);
                    var cardConfig = m_View.Configs[typeIndex];
                    var cardView = InstantiateCardView(cardIndex + typeIndex * k_CardsOfSameType);
                    var cardController = new CardController(cardLetter, cardConfig, cardView);

                    m_View.CardViews.Add(cardView);
                    cards.Add(cardController);

                    Debug.Log(cardIndex + typeIndex * k_CardsOfSameType);
                }
            }

            return cards;
        }

        private string GetLetterByIndex(int index)
        {
            //TODO: Validate index
            return index switch
            {
                0  => "A",
                10 => "J",
                11 => "Q",
                12 => "K",
                _  => (index + 1).ToString()
            };
        }

        private CardView InstantiateCardView(int index)
        {
            var cardView = Object.Instantiate(m_View.CardPrefab, m_View.transform);
            cardView.transform.position = new Vector3
            {
                x = 0f,
                y = 0f,
                z = index * m_View.CardsSpacing
            };

            return cardView;
        }
    }
}