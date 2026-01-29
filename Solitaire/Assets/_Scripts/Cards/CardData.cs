using System;
using UnityEngine;

namespace Solitaire.Cards
{
    [Serializable]
    public class CardData
    {
        [field: SerializeField]
        public string Letter { get; private set; }

        [field: SerializeField]
        public Symbols Symbol { get; private set; }

        public CardData(string letter, Symbols symbol)
        {
            Letter = letter;
            Symbol = symbol;
        }
    }
}