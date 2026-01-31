using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PrimeTween;
using Solitaire.Cards;
using Solitaire.Deck;
using UnityEngine;

namespace Solitaire.Board
{
    public class BoardController : IDisposable
    {
        private const int k_Lanes = 7;

        private readonly GameConfig m_GameConfig;
        private readonly BoardView m_View;
        private readonly CardsDeckController m_Deck;
        private readonly LinkedList<CardView> m_DeckList;
        private readonly List<LinkedList<CardView>> m_LanesLists;

        public BoardController(GameConfig gameConfig, BoardView view, CardsDeckController deck)
        {
            m_GameConfig = gameConfig;
            m_View = view;
            m_Deck = deck;

            m_DeckList = new LinkedList<CardView>();
            m_LanesLists = new List<LinkedList<CardView>>();
            foreach (var _ in m_GameConfig.CardsPerLane)
            {
                m_LanesLists.Add(new LinkedList<CardView>());
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
            await m_Deck.ForEachAsync(this, async (controller, card) =>
            {
                controller.m_DeckList.AddLast(card);

                var deckSlot = m_View.DeckStack.GetNextPosition();
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
            });

            await Awaitable.WaitForSecondsAsync(0.5f);
        }

        private async ValueTask FillLanesAsync()
        {
            var placed = new int[k_Lanes];
            var done = false;

            do
            {
                done = true;
                for (var lane = 0; lane < k_Lanes; lane++)
                {
                    if (placed[lane] < m_GameConfig.CardsPerLane[lane])
                    {
                        // Place one element in this lane
                        placed[lane]++;

                        var card = m_DeckList.Last.Value;
                        m_DeckList.RemoveLast();
                        m_LanesLists[lane].AddLast(card);

                        var targetPosition = m_View.Stacks[lane].GetNextPosition();
                        Tween.Position(card.transform, targetPosition, 0.2f);
                        await Awaitable.WaitForSecondsAsync(0.1f);

                        done = false; // At least one lane still needed work
                    }
                }
            } while (!done);
        }

        public void Dispose()
        {
            m_Deck?.Dispose();
        }
    }
}