using UnityEngine;

namespace Solitaire
{
    [CreateAssetMenu(menuName = "Solitaire/GameConfig", fileName = "GameConfig")]
    public class GameConfig : ScriptableObject
    {
        [field: SerializeField]
        public int[] CardsPerLane { get; private set; }
    }
}