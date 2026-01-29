using UnityEngine;
using Solitaire.Cards;

namespace Solitaire
{
    public class EntryPoint : MonoBehaviour
    {
        [SerializeField] private CardsDeckView m_CardsDeckView;

        private void Start()
        {
            new CardsDeckController(m_CardsDeckView);
        }
    }
}