using System;
using UnityEngine;

namespace Solitaire.Board
{
    public class StackingZone : MonoBehaviour
    {
        // [SerializeField] private int m_Capacity;
        [SerializeField] private Vector3 m_Spacing;
        private Vector3 m_NextPosition;

        private void Awake()
        {
            m_NextPosition = transform.position;
        }

        public Vector3 GetNextPosition()
        {
            var nextPosition = m_NextPosition;
            m_NextPosition += m_Spacing;

            return nextPosition;
        }
    }
}