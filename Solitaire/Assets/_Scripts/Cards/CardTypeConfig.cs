using UnityEngine;

namespace Solitaire.Cards
{
    [CreateAssetMenu(menuName = "Solitaire/Card Type Config", fileName = "config_card_Config")]
    public class CardTypeConfig : ScriptableObject, ICardTypeConfig
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