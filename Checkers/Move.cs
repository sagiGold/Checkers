using System;
using System.Drawing;
using System.Collections.Generic;

namespace Checkers
{
    public class Move
    {
        private const int k_Jump2Squares = 2;
        private Point m_CurrentPoint;
        private Point m_MoveTo;
        private bool m_EatMove;

        public Move(Point i_From, Point i_To)
        {
            m_CurrentPoint = i_From;
            m_MoveTo = i_To;
            m_EatMove = Math.Abs(m_CurrentPoint.X - m_MoveTo.X) == k_Jump2Squares;
        }

        public bool IsAvailabeMove(Player i_PlayerTurn)
        {
            return i_PlayerTurn.MovesList.Contains(this);
        }

        public void MakeMove(Board io_GameBoard, List<GameTool> io_OpponentTools)
        {
            GameTool toolToMove = io_GameBoard[m_CurrentPoint.Y, m_CurrentPoint.X];

            io_GameBoard[m_CurrentPoint.Y, m_CurrentPoint.X] = null;
            if (m_EatMove)
            {
                eatOpponentTool(io_GameBoard, io_OpponentTools);
            }

            toolToMove.Location = m_MoveTo;
            io_GameBoard[m_MoveTo.Y, m_MoveTo.X] = toolToMove;
        }

        private void eatOpponentTool(Board io_GameBoard, List<GameTool> io_OpponentTools)
        {
            GameTool toolToDelete = io_GameBoard[(m_CurrentPoint.Y + m_MoveTo.Y) / 2, (m_CurrentPoint.X + m_MoveTo.X) / 2];

            io_GameBoard[(m_CurrentPoint.Y + m_MoveTo.Y) / 2, (m_CurrentPoint.X + m_MoveTo.X) / 2] = null;
            io_OpponentTools.Remove(toolToDelete);
        }
    }
}
