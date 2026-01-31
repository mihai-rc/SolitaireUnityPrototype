using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Solitaire
{
    public class Board : MonoBehaviour
    {
        [CustomEditor(typeof(Board))]
        public class BoardViewEditor : Editor
        {
            private Board Board => target as Board;

            public override void OnInspectorGUI()
            {
                base.OnInspectorGUI();
                if (GUILayout.Button("Arrange"))
                {
                    Board.ArrangeBoard();
                }
            }
        }

        private const int k_StacksCount = 7;
        private const int k_CollectorsCount = 4;

        [Header("Editor Config"), Space(10)]
        [SerializeField] private Grid m_Grid;
        [SerializeField] private StackingZone m_CardsStackPrefab;

        [Header("View Config"), Space(10)]
        [SerializeField] private Transform m_CollectorsHolder;
        [SerializeField] private Transform m_LanesHolder;
        [SerializeField] private StackingZone m_DeckZone;
        [SerializeField] private List<StackingZone> m_CollectorZones;
        [SerializeField] private List<StackingZone> m_LaneZones;

        public StackingZone DeckZone => m_DeckZone;

        public List<StackingZone> CollectorZones => m_CollectorZones;

        public List<StackingZone> LaneZones => m_LaneZones;

        private void ArrangeBoard()
        {
            CreateDeckZone();
            CreateCollectorsZones();
            CreateLaneZones();
            FrameView();
        }

        private void CreateDeckZone()
        {
            if (m_DeckZone != null)
            {
                DestroyImmediate(m_DeckZone.gameObject);
            }

            m_DeckZone = CreateStackingZone(k_StacksCount - 1, 1, transform);
            m_DeckZone.name = "DeckZone";
        }

        private void CreateLaneZones()
        {
            foreach (var zone in m_LaneZones)
            {
                DestroyImmediate(zone.gameObject);
            }

            m_LaneZones.Clear();
            for (var i = 0; i < k_StacksCount; i++)
            {
                var zone = CreateStackingZone(i, 0, m_LanesHolder);
                zone.name = $"LaneZone_{i}";

                m_LaneZones.Add(zone);
            }
        }

        private void CreateCollectorsZones()
        {
            foreach (var collector in m_CollectorZones)
            {
                DestroyImmediate(collector.gameObject);
            }

            m_CollectorZones.Clear();
            for (var i = 0; i < k_CollectorsCount; i++)
            {
                var zone = CreateStackingZone(i, 1, m_CollectorsHolder);
                zone.name = $"CollectorZone_{i}";

                m_CollectorZones.Add(zone);
            }
        }

        private StackingZone CreateStackingZone(int x, int y, Transform parent)
        {
            var zone = Instantiate(m_CardsStackPrefab, parent);
            var position = m_Grid.GetCellCenterLocal(new Vector3Int(x, y, 0));
            zone.transform.localPosition = new Vector3(position.x, position.y, 0f);

            return zone;
        }

        private void FrameView()
        {
            transform.position = Vector3.zero;

            var firstStackPosition = m_LaneZones.First().transform.position;
            var lastStackPosition = m_LaneZones.Last().transform.position;
            var offsets = firstStackPosition.x + (lastStackPosition.x - firstStackPosition.x) / 2;
            transform.position = new Vector3(-offsets, 0f, 0);
        }
    }
}
