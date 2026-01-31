using System.Collections.Generic;
using Solitaire.Cards;
using UnityEngine;

namespace Solitaire.Board
{
    public class CardsStackView : MonoBehaviour
    {
        [SerializeField] private int m_Capacity;
        [SerializeField] private float m_Spacing;
        [SerializeField] private List<CardView> m_CardViews;

        public bool TryAdd()
        {
            return false;
        }
    }
}