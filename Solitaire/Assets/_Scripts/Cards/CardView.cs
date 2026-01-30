using UnityEngine;
using TMPro;

namespace Solitaire.Cards
{
    public class CardView : MonoBehaviour
    {
        [SerializeField] private CardData m_Data;
        [SerializeField] private SpriteRenderer m_SymbolRenderer;
        [SerializeField] private SpriteRenderer m_UpIconRenderer;
        [SerializeField] private SpriteRenderer m_DownIconRenderer;
        [SerializeField] private TMP_Text m_UpLetter;
        [SerializeField] private TMP_Text m_DownLetter;

        public CardData Data => m_Data;

        public void InitData(CardData data) => m_Data = data;

        public void Configure(CardConfig config)
        {
            SetLetter(m_Data.Letter);
            SetSymbolSprite(config.SymbolSprite);
            SetIconSprite(config.IconSprite);
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
        }

        private void SetIconSprite(Sprite icon)
        {
            m_UpIconRenderer.sprite = icon;
            m_DownIconRenderer.sprite = icon;
        }

        private void SetColor(Color color)
        {
            m_SymbolRenderer.color = color;
            m_UpIconRenderer.color = color;
            m_DownIconRenderer.color = color;
            m_UpLetter.color = color;
            m_DownLetter.color = color;
        }
    }
}
