using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Solitaire.Board
{
    public class BoardView : MonoBehaviour
    {
        [CustomEditor(typeof(BoardView))]
        public class BoardViewEditor : Editor
        {
            private BoardView BoardView => target as BoardView;

            public override void OnInspectorGUI()
            {
                base.OnInspectorGUI();
                if (GUILayout.Button("Arrange"))
                {
                    BoardView.ArrangeBoard();
                }
            }
        }

        private const int k_StacksCount = 7;
        private const int k_CollectorsCount = 4;

        [Header("Editor Config"), Space(10)]
        [SerializeField] private Grid m_Grid;
        [SerializeField] private GameObject m_CardZonePrefab;

        [Header("View Config"), Space(10)]
        [SerializeField] private Transform m_DeckHolder;
        [SerializeField] private Transform m_CollectorsHolder;
        [SerializeField] private Transform m_StacksHolder;
        [SerializeField] private List<Transform> m_Collectors;
        [SerializeField] private List<Transform> m_Stacks;

        private void ArrangeBoard()
        {
            CreateStacks();
            CreateCollectors();
            FrameView();
        }

        private void CreateStacks()
        {
            foreach (var stack in m_Stacks)
            {
                DestroyImmediate(stack.gameObject);
            }

            m_Stacks.Clear();
            for (var i = 0; i < k_StacksCount; i++)
            {
                CreateZone(i, 0, m_StacksHolder, m_Stacks);
            }
        }

        private void CreateCollectors()
        {
            foreach (var collector in m_Collectors)
            {
                DestroyImmediate(collector.gameObject);
            }

            m_Collectors.Clear();
            for (var i = 0; i < k_CollectorsCount; i++)
            {
                CreateZone(i, 1, m_CollectorsHolder, m_Collectors);
            }
        }

        private void CreateZone(int x, int y, Transform parent, List<Transform> cache)
        {
            var position = m_Grid.GetCellCenterLocal(new Vector3Int(x, y, 0));
            var zone = Instantiate(m_CardZonePrefab, parent);
            zone.transform.localPosition = new Vector3(position.x, position.y, 0f);
            cache.Add(zone.transform);
        }

        private void FrameView()
        {
            transform.position = Vector3.zero;

            var firstStack = m_Stacks.First();
            var lastStack = m_Stacks.Last();
            var offsets = firstStack.position.x + (lastStack.position.x - firstStack.position.x) / 2;
            transform.position = new Vector3(-offsets, 0f, 0);
        }
    }
}
