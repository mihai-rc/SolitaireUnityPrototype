using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using PrimeTween;

namespace Solitaire
{
    public class BoardController
    {
        private const int k_Lanes = 7;

        private readonly GameConfig m_GameConfig;
        private readonly Board m_Board;
        private readonly Dealer m_Dealer;
        private readonly List<Lane> m_Lanes = new();

        public BoardController(GameConfig gameConfig, Board board, Deck deck)
        {
            m_GameConfig = gameConfig;
            m_Board = board;
            m_Dealer = new Dealer(deck, board.DeckZone);

            for (var lane = 0; lane < k_Lanes; lane++)
            {
                m_Lanes.Add(new Lane(m_Board.LaneZones[lane]));
            }
        }

        public async ValueTask SetupAsync()
        {
            await m_Dealer.FillDeckAsync();
            await FillLanesAsync();
            
            await Awaitable.WaitForSecondsAsync(1f);
            foreach (var lane in m_Lanes)
            {
                lane.ShowFirstHiddenCard();
                await Awaitable.WaitForSecondsAsync(0.1f);
            }
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
                    var targetPosition = m_Lanes[lane].AddHiddenCard(card);

                    Tween.Position(card.transform, targetPosition, 0.2f);
                    await Awaitable.WaitForSecondsAsync(0.1f);

                    done = false; // At least one lane still needed work
                }
            } 
            while (!done);
        }
    }
}