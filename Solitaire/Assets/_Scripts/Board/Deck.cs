using System.Collections.Generic;
using UnityEngine;
using Solitaire.Cards;

using UnityEditor;
using UnityEngine.Pool;

namespace Solitaire
{
    public class Deck : MonoBehaviour
    {
        [CustomEditor(typeof(Deck))]
        public class CardsDeckViewEditor : Editor
        {
            private Deck Deck => target as Deck;

            public override void OnInspectorGUI()
            {
                base.OnInspectorGUI();
                if (GUILayout.Button("Create Cards"))
                {
                    Deck.CreateCards();
                }
            }
        }

        private const int k_CardSymbols = 4;
        private const int k_CardRanks = 13;

        [SerializeField] private Card m_CardPrefab;
        [SerializeField] private List<CardConfig> m_Configs;
        [SerializeField] private List<Card> m_Cards;

        public List<Card> Cards => m_Cards;

        public void Init()
        {
            var configsMap = DictionaryPool<Symbols, CardConfig>.Get();
            foreach (var cardConfig in m_Configs)
            {
                //TODO: Validate and handle config file misconfigurations.
                configsMap[cardConfig.Symbol] = cardConfig;
            }

            foreach (var cardView in Cards)
            {
                var cardConfig = configsMap[cardView.Data.Symbol];
                cardView.Configure(cardConfig);
            }

            DictionaryPool<Symbols, CardConfig>.Release(configsMap);
        }

        public void Shuffle()
        {
            for (var i = 0; i < Cards.Count; i++)
            {
                var randomIndex = Random.Range(0, Cards.Count);
                (Cards[i], Cards[randomIndex]) = (Cards[randomIndex], Cards[i]);
            }
        }

        private void CreateCards()
        {
            foreach (var card in m_Cards)
            {
                DestroyImmediate(card.gameObject);
            }

            m_Cards.Clear();
            for (var symbol = 0; symbol < k_CardSymbols; symbol++)
            {
                for (var rank = 0; rank < k_CardRanks; rank++)
                {
                    var cardLetter = GetLetterByIndex(rank);
                    var cardData = new CardData(rank, cardLetter, (Symbols)symbol);
                    var card = Instantiate(m_CardPrefab, transform);

                    card.InitData(cardData);
                    card.transform.localPosition = Vector3.zero;
                    card.transform.localScale = Vector3.one;

                    m_Cards.Add(card);
                }
            }
        }

        private string GetLetterByIndex(int rank)
        {
            //TODO: Validate index
            return rank switch
            {
                0  => "A",
                10 => "J",
                11 => "Q",
                12 => "K",
                _  => (rank + 1).ToString()
            };
        }
    }
}