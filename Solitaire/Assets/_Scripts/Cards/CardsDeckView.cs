using System.Collections.Generic;
using UnityEngine;

namespace Solitaire.Cards
{
    public class CardsDeckView : MonoBehaviour
    {
        [SerializeField] private float m_CardsSpacing = 0.015f;
        [SerializeField] private CardView m_CardPrefab;
        [SerializeField] private List<CardConfig> m_Configs;
        [SerializeField] private List<CardView> m_CardViews;

        public float CardsSpacing => m_CardsSpacing;

        public CardView CardPrefab => m_CardPrefab;

        public List<CardConfig> Configs => m_Configs;

        public List<CardView> CardViews => m_CardViews;
    }
}