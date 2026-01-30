using System;
using UnityEngine;

namespace Solitaire.Cards
{
    [Serializable]
    public class CardData
    {
        [field: SerializeField]
        public int Rank { get; private set; }

        [field: SerializeField]
        public string Letter { get; private set; }

        [field: SerializeField]
        public Symbols Symbol { get; private set; }

        public CardData(int rank, string letter, Symbols symbol)
        {
            Rank = rank;
            Letter = letter;
            Symbol = symbol;
        }
    }
}