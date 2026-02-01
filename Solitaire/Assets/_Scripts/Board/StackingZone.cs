using UnityEngine;

namespace Solitaire
{
    public class StackingZone : MonoBehaviour
    {
        [SerializeField] private Vector3 m_Spacing;

        private Vector3 m_CurrentPosition;
        private Vector3 m_NextPosition;

        public Vector3 Spacing => m_Spacing;

        private void Awake()
        {
            m_CurrentPosition = transform.position;
            m_NextPosition = transform.position;
        }

        // public Vector3 GetCurrentPosition() => m_CurrentPosition;

        public Vector3 GetNextPosition()
        {
            m_CurrentPosition = m_NextPosition;
            m_NextPosition += m_Spacing;

            return m_CurrentPosition;
        }

        public void DecreasePosition()
        {
            m_CurrentPosition -= m_Spacing;
            m_NextPosition -= m_Spacing;
        }
    }
}