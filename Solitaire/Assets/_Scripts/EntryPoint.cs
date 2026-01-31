using UnityEngine;
using Solitaire.Deck;

namespace Solitaire
{
    public class EntryPoint : MonoBehaviour
    {
        [SerializeField] private CardsDeckView m_CardsDeckView;

        private CardsDeckController m_CardsDeckController;

        private void Start()
        {
            m_CardsDeckController = new CardsDeckController(m_CardsDeckView);
        }

        private void OnDestroy()
        {
            m_CardsDeckController.Dispose();
        }
    }
}