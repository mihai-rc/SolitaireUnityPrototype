using System;
using UnityEngine;

namespace Solitaire.Cards
{
    [Serializable]
    public class CardConfig
    {
        [field: SerializeField]
        public Symbols Symbol { get; private set; }

        [field: SerializeField]
        public Color Color { get; private set; }

        [field: SerializeField]
        public Sprite LargeSprite { get; private set; }

        [field: SerializeField]
        public Sprite SmallSprite { get; private set; }
    }
}