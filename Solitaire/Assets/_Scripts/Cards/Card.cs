using System;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

namespace Solitaire.Cards
{
    public class Card : MonoBehaviour, 
        IBeginDragHandler,
        IDragHandler,
        IEndDragHandler,
        IDropHandler
    {
        [SerializeField] private CardData m_Data;
        [SerializeField] private BoxCollider2D m_Collider;
        [SerializeField] private SpriteRenderer m_SymbolRenderer;
        [SerializeField] private SpriteRenderer m_UpIconRenderer;
        [SerializeField] private SpriteRenderer m_DownIconRenderer;
        [SerializeField] private TMP_Text m_UpLetter;
        [SerializeField] private TMP_Text m_DownLetter;

        private static Vector3 k_NullFingerPosition => new (-1, -1, -1);
        private Vector3 m_FirstPosition = k_NullFingerPosition;
        private Vector3 m_FingerOffset = Vector3.zero;
        private bool m_WasDropped;

        private Action<Card> m_OnBeginDragFn;
        private Action<Card, Vector3> m_OnDragFn;
        private Action<Card> m_OnEndDragFn;
        private Action<Card, Card> m_OnDroppedOverFn;
        // private Action<Card> m_OnDetachFn;
        private Lane m_OwningLane;

        public CardData Data => m_Data;

        public bool Draggable 
        {
            set => m_Collider.enabled = value;
        }

        public void InitData(CardData data) => m_Data = data;

        public void Configure(CardConfig config)
        {
            SetLetter(m_Data.Letter);
            SetSymbolSprite(config.SymbolSprite);
            SetIconSprite(config.IconSprite);
            SetColor(config.Color);
        }

        public bool TryGetOwningLane(out Lane lane)
        {
            if (m_OwningLane == null)
            {
                lane = null;
                return false;
            }

            lane = m_OwningLane;
            return true;
        }

        // public void Detach() => m_OnDetachFn?.Invoke(this);
        public void SetOwningLane(Lane lane) => m_OwningLane = lane;

        public void SetBeginDragHandler(Action<Card> onBeginDragFn) => m_OnBeginDragFn = onBeginDragFn;

        public void SetDragHandler(Action<Card, Vector3> onDragFn) => m_OnDragFn = onDragFn;

        public void SetDragEndHandler(Action<Card> onEndDragFn) => m_OnEndDragFn = onEndDragFn;

        public void SetDroppedOverHandler(Action<Card, Card> onDroppedOverFn) => m_OnDroppedOverFn = onDroppedOverFn;

        // public void SetDetachHandler(Action<Card> onDetachFn) => m_OnDetachFn = onDetachFn;

        public void OnBeginDrag(PointerEventData eventData)
        {
            // m_Collider.enabled = false;
            m_FirstPosition = Camera.main.ScreenToWorldPoint(eventData.position);
            m_FingerOffset = transform.position - m_FirstPosition;
            m_OnBeginDragFn?.Invoke(this);
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (m_FirstPosition == k_NullFingerPosition)
            {
                return;
            }

            var finger = Camera.main.ScreenToWorldPoint(eventData.position);
            finger += m_FingerOffset;

            var position = new Vector3(finger.x, finger.y, - 1f);
            m_OnDragFn?.Invoke(this, position);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            m_FirstPosition = k_NullFingerPosition;
            // m_Collider.enabled = true;

            if (!m_WasDropped)
            {
                m_OnEndDragFn?.Invoke(this);
            }

            m_WasDropped = false;
        }

        public void OnDrop(PointerEventData eventData)
        {
            if (eventData.pointerDrag == null)
            {
                return;
            }

            var droppedCard = eventData.pointerDrag.GetComponent<Card>();
            droppedCard.m_WasDropped = true;
            m_OnDroppedOverFn?.Invoke(this, droppedCard);
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
