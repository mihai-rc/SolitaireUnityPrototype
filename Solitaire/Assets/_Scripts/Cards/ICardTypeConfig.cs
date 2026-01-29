using UnityEngine;

namespace Solitaire.Cards
{
    public interface ICardTypeConfig
    {
        Symbols Symbol { get; }

        Color Color { get; }

        Sprite LargeSprite { get; }

        Sprite SmallSprite { get; }
    }
}