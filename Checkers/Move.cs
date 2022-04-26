﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

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

        public bool IsEatMove()
        {
            return m_EatMove;
        }

        private void switchToKing(GameTool io_Tool, Board i_GameBoard)
        {
            if (!io_Tool.IsKing() && i_GameBoard.ToolInEndLine(io_Tool))
            {
                io_Tool.MakeKing();
            }
        }

        public void MakeMove(Board io_GameBoard, List<GameTool> io_OpponentTools, List<Move> io_PlayerMoves)
        {
            GameTool toolToMove = io_GameBoard[m_CurrentPoint.Y, m_CurrentPoint.X];

            io_GameBoard.RemoveToolFromSquare(toolToMove.Location);
            io_GameBoard.AddToolToSquare(toolToMove, m_MoveTo);
            toolToMove.Location = m_MoveTo;
            if (m_EatMove)
            {
                skipOverTheOpponentTool(io_GameBoard, io_OpponentTools);
                io_PlayerMoves.Clear();
                toolToMove.CheckOppurturnitiToEat(io_GameBoard, io_PlayerMoves);
            }

            switchToKing(toolToMove, io_GameBoard);
        }

        private void skipOverTheOpponentTool(Board io_GameBoard, List<GameTool> io_OpponentTools)
        {
            GameTool toolToDelete = io_GameBoard[(m_CurrentPoint.Y + m_MoveTo.Y) / 2, (m_CurrentPoint.X + m_MoveTo.X) / 2];

            io_GameBoard.RemoveToolFromSquare(toolToDelete.Location);
            io_OpponentTools.Remove(toolToDelete);
        }

        public bool Equals(Move i_Move)
        {
            bool isEqual = m_CurrentPoint == i_Move.m_CurrentPoint && m_MoveTo == i_Move.m_MoveTo && m_EatMove == i_Move.m_EatMove;

            return isEqual;
        }

        public string ToString()
        {
            StringBuilder moveInString = new StringBuilder();

            moveInString.Append(string.Format("{0}", (char)(m_CurrentPoint.X + 'A')));
            moveInString.Append(string.Format("{0}", (char)(m_CurrentPoint.Y + 'a')));
            moveInString.Append(">");
            moveInString.Append(string.Format("{0}", (char)(m_MoveTo.X + 'A')));
            moveInString.Append(string.Format("{0}", (char)(m_MoveTo.Y + 'a')));

            return moveInString.ToString();
        }
    }
}
