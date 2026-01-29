using UnityEngine;

namespace Solitaire.Cards
{
    [CreateAssetMenu(menuName = "Solitaire/CardConfigCollection", fileName = "CardConfigCollection")]
    public class CardConfigCollection : ScriptableObject
    {
        [field: SerializeField]
        public CardTypeConfig[] Configs { get; private set; }
    }
}