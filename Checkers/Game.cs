using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Checkers
{
    public class Game
    {
        Board m_Board = null;
        Player m_PlayerOne = null;
        Player m_PlayerTwo = null;

        //public GameManagement(int i_BoardSize, string i_PlayerOneName, string i_PlayerTwoName)
        //{
        //    m_PlayerOne = new Player(i_PlayerOneName, GameTool.eTeamSign.PlayerX);
        //    m_PlayerTwo = new Player(i_PlayerTwoName, GameTool.eTeamSign.PlayerO);
        //    m_Board = new Board(i_BoardSize, m_PlayerOne, m_PlayerTwo);
        //}

        public Board Board
        {
            get
            {
                return m_Board;
            }
        }

        public bool CheckSwitchToKing(GameTool i_Tool)
        {
            return !i_Tool.IsKing() && m_Board.ToolInEndLine(i_Tool);
        }

        public bool InitPlayer(string i_Name)
        {
            Player playerToUpdate = m_PlayerOne == null ? m_PlayerOne : m_PlayerTwo;
            bool updated = false;

            if (updated = Player.IsValidUserName(i_Name))
            {
                GameTool.eTeamSign sign = m_PlayerOne == null ? GameTool.eTeamSign.PlayerX : GameTool.eTeamSign.PlayerO;
                playerToUpdate = new Player(i_Name, sign);
            }

            return updated;
        }

        public bool InitBoard(string i_Size)
        {
            int size;
            bool updated = false;

            if (updated = Board.ValidSize(i_Size, out size))
            {
                m_Board = new Board(size);
            }

            return updated;
        }

        public bool OpponentId(string i_NumOfPlayerType, out string o_PlayerType )
        {
            Player.ePLayerType humanOrComputer;


            if (Player.ePLayerType.TryParse(i_NumOfPlayerType, out humanOrComputer))
            {
                o_PlayerType = humanOrComputer == Player.ePLayerType.Human ? "Human" : "Computer";
            }

            return ;
        }    
    }
}
