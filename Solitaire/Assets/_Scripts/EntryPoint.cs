using Solitaire.Board;
using UnityEngine;
using Solitaire.Deck;

namespace Solitaire
{
    public class EntryPoint : MonoBehaviour
    {
        [SerializeField] private GameConfig m_GameConfig;
        [SerializeField] private CardsDeckView m_CardsDeckView;
        [SerializeField] private BoardView m_BoardView;

        private CardsDeckController m_CardsDeckController;
        private BoardController m_BoardController;

        private void Start()
        {
            m_CardsDeckController = new CardsDeckController(m_CardsDeckView);
            m_BoardController = new BoardController(m_GameConfig, m_BoardView, m_CardsDeckController);

            m_BoardController.SetupAsync();
        }

        private void OnDestroy()
        {
            m_CardsDeckController.Dispose();
        }
    }
}