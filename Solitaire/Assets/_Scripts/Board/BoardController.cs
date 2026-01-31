using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using PrimeTween;
using Solitaire.Cards;
using Unity.VisualScripting;

namespace Solitaire
{
    public class BoardController
    {
        private const int k_Lanes = 7;

        private readonly GameConfig m_GameConfig;
        private readonly Board m_Board;
        private readonly Deck m_Deck;
        private readonly Dealer m_Dealer = new();
        private readonly List<LinkedList<Card>> m_LanesLists = new();

        public BoardController(GameConfig gameConfig, Board board, Deck deck)
        {
            m_GameConfig = gameConfig;
            m_Board = board;
            m_Deck = deck;

            m_Deck.Init();
            foreach (var _ in m_GameConfig.CardsPerLane)
            {
                m_LanesLists.Add(new LinkedList<Card>());
            }
        }

        public async ValueTask SetupAsync()
        {
            m_Deck.Shuffle();

            await FillDeckAsync();
            await FillLanesAsync();
        }

        private async ValueTask FillDeckAsync()
        {
            foreach (var card in m_Deck.Cards)
            {
                m_Dealer.Add(card);

                var deckSlot = m_Board.DeckZone.GetNextPosition();
                var midpoint = (card.transform.position + deckSlot) / 2f;
                var offset = deckSlot - midpoint;
                var axis = card.transform.up * -1;

                Tween.Custom(0, 180, 0.2f, angle =>
                {
                    card.transform.position = midpoint + Quaternion.AngleAxis(180 - angle, axis) * offset;
                    card.transform.eulerAngles = new Vector3(0f, angle, 0f);
                },
                Ease.InOutQuad);

                await Awaitable.WaitForSecondsAsync(0.01f);
            }

            await Awaitable.WaitForSecondsAsync(0.5f);
        }

        private async ValueTask FillLanesAsync()
        {
            var placedPerLane = new int[k_Lanes];
            var done = false;

            do
            {
                done = true;
                for (var lane = 0; lane < k_Lanes; lane++)
                {
                    if (placedPerLane[lane] >= m_GameConfig.CardsPerLane[lane]) continue;

                    // Place one element in this lane
                    placedPerLane[lane]++;

                    var card = m_Dealer.Deal();
                    m_LanesLists[lane].AddLast(card);

                    var targetPosition = m_Board.LaneZones[lane].GetNextPosition();
                    Tween.Position(card.transform, targetPosition, 0.2f);
                    await Awaitable.WaitForSecondsAsync(0.1f);

                    done = false; // At least one lane still needed work
                }
            } 
            while (!done);
        }
    }
}