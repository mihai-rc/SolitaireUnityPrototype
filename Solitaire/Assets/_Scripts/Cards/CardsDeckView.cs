using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Solitaire.Cards
{
    public class CardsDeckView : MonoBehaviour
    {
        [CustomEditor(typeof(CardsDeckView))]
        public class CardsDeckViewEditor : Editor
        {
            private CardsDeckView CardsDeckView => target as CardsDeckView;

            public override void OnInspectorGUI()
            {
                base.OnInspectorGUI();
                if (GUILayout.Button("Create Cards"))
                {
                    CardsDeckView.CreateCards();
                }
            }
        }

        private const int k_CardSymbols = 4;
        private const int k_CardRanks = 13;

        [SerializeField] private CardView m_CardPrefab;
        [SerializeField] private List<CardConfig> m_Configs;
        [SerializeField] private List<CardView> m_CardViews;

        public List<CardConfig> Configs => m_Configs;

        public List<CardView> CardViews => m_CardViews;

        private void CreateCards()
        {
            foreach (var cardView in m_CardViews)
            {
                DestroyImmediate(cardView.gameObject);
            }

            m_CardViews.Clear();
            for (var symbol = 0; symbol < k_CardSymbols; symbol++)
            {
                for (var rank = 0; rank < k_CardRanks; rank++)
                {
                    var cardLetter = GetLetterByIndex(rank);
                    var cardView = Instantiate(m_CardPrefab, transform);
                    var cardData = new CardData(rank, cardLetter, (Symbols)symbol);

                    cardView.InitData(cardData);
                    cardView.transform.localPosition = Vector3.zero;
                    cardView.transform.localScale = Vector3.one;

                    m_CardViews.Add(cardView);
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

        private CardView InstantiateCardView()
        {
            var cardView = Instantiate(m_CardPrefab, transform);
            cardView.transform.localPosition = Vector3.zero;

            return cardView;
        }
    }
}