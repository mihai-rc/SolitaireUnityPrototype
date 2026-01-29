using UnityEngine;
using TMPro;

namespace Solitaire.Cards
{
    public class CardView : MonoBehaviour
    {
        [SerializeField] private CardData m_Data;
        [SerializeField] private SpriteRenderer m_SymbolRenderer;
        [SerializeField] private SpriteRenderer m_UpCornerRenderer;
        [SerializeField] private SpriteRenderer m_DownCornerRenderer;
        [SerializeField] private TMP_Text m_UpLetter;
        [SerializeField] private TMP_Text m_DownLetter;

        public CardData Data => m_Data;

        public void Init(string letter, CardConfig config)
        {
            m_Data = new CardData(letter, config.Symbol);
            SetLetter(letter);
            SetSymbolSprite(config.LargeSprite);
            SetColor(config.Color);
        }

        private void SetLetter(string letter)
        {
            m_UpLetter.text = letter;
            m_DownLetter.text = letter;
        }

        private void SetSymbolSprite(Sprite symbol)
        {
            m_SymbolRenderer.sprite = symbol;
            m_UpCornerRenderer.sprite = symbol;
            m_DownCornerRenderer.sprite = symbol;
        }

        private void SetColor(Color color)
        {
            m_SymbolRenderer.color = color;
            m_UpCornerRenderer.color = color;
            m_DownCornerRenderer.color = color;
            m_UpLetter.color = color;
            m_DownLetter.color = color;
        }
    }
}
