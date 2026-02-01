using UnityEngine;

namespace Solitaire
{
    public class EntryPoint : MonoBehaviour
    {
        [SerializeField] private GameConfig m_GameConfig;
        [SerializeField] private Deck m_Deck;
        [SerializeField] private Board m_Board;

        private Dealer m_Dealer;
        private BoardController m_BoardController;

        private void Start()
        {
            m_BoardController = new BoardController(m_GameConfig, m_Board, m_Deck);
            m_BoardController.SetupAsync();
        }
    }
}