using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Checkers
{
    public class GameManagement
    {
        Board m_Board;
        Player m_PlayerOne;
        Player m_PlayerTwo;

        public GameManagement(int i_BoardSize, string i_PlayerOneName, string i_PlayerTwoName)
        {
            m_PlayerOne = new Player(i_PlayerOneName, GameTool.eTeamSign.PlayerX);
            m_PlayerTwo = new Player(i_PlayerTwoName, GameTool.eTeamSign.PlayerX);
            m_Board = new Board(i_BoardSize, i_BoardSize, m_PlayerOne, m_PlayerTwo);
        }

        public bool CheckSwitchToKing(GameTool i_Tool)
        {
            return !i_Tool.IsKing() && m_Board.ToolInEndLine(i_Tool);
        }

        public void GameTurn(Move i_CurrentMove)
        {
            /*if (i_CurrentMove.IsAvailabeMove())
            {
                i_CurrentMove.MakeMove(m_Board,)
            }*/
            // need to work on the events order of game turn
        }
    }
}
