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
        [SerializeField] private CardsStackView m_CardsStackPrefab;

        [Header("View Config"), Space(10)]
        [SerializeField] private Transform m_DeckHolder;
        [SerializeField] private Transform m_CollectorsHolder;
        [SerializeField] private Transform m_StacksHolder;
        [SerializeField] private CardsStackView m_DeckStack;
        [SerializeField] private List<CardsStackView> m_Collectors;
        [SerializeField] private List<CardsStackView> m_Stacks;

        private void ArrangeBoard()
        {
            CreateRearrangeableStacks();
            CreateCollectorsStacks();
            CreateDeckStack();
            FrameView();
        }

        private void CreateRearrangeableStacks()
        {
            foreach (var stack in m_Stacks)
            {
                DestroyImmediate(stack.gameObject);
            }

            m_Stacks.Clear();
            for (var i = 0; i < k_StacksCount; i++)
            {
                var stack = CreateStack(i, 0, m_StacksHolder);
                m_Stacks.Add(stack);
            }
        }

        private void CreateCollectorsStacks()
        {
            foreach (var collector in m_Collectors)
            {
                DestroyImmediate(collector.gameObject);
            }

            m_Collectors.Clear();
            for (var i = 0; i < k_CollectorsCount; i++)
            {
                var stack = CreateStack(i, 1, m_CollectorsHolder);
                m_Collectors.Add(stack);
            }
        }

        private void CreateDeckStack()
        {
            if (m_DeckStack != null)
            {
                DestroyImmediate(m_DeckStack.gameObject);
            }

            m_DeckStack = CreateStack(k_StacksCount - 1, 1, m_DeckHolder);
        }

        private CardsStackView CreateStack(int x, int y, Transform parent)
        {
            var position = m_Grid.GetCellCenterLocal(new Vector3Int(x, y, 0));
            var stack = Instantiate(m_CardsStackPrefab, parent);
            stack.transform.localPosition = new Vector3(position.x, position.y, 0f);

            return stack;
        }

        private void FrameView()
        {
            transform.position = Vector3.zero;

            var firstStackPosition = m_Stacks.First().transform.position;
            var lastStackPosition = m_Stacks.Last().transform.position;
            var offsets = firstStackPosition.x + (lastStackPosition.x - firstStackPosition.x) / 2;
            transform.position = new Vector3(-offsets, 0f, 0);
        }
    }
}
