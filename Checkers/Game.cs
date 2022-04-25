using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Checkers
{
    public class Game
    {
        Board m_Board = null;
        Player m_CurrentPlayer = null;
        Player m_NextPlayer = null;
        bool m_IsPlayerOneTurn = true;

        public Game()
        {
        }

        //public GameManagement(int i_BoardSize, string i_PlayerOneName, string i_PlayerTwoName)
        //{
        //    m_PlayerOne = new Player(i_PlayerOneName, GameTool.eToolSign.PlayerX);
        //    m_PlayerTwo = new Player(i_PlayerTwoName, GameTool.eToolSign.PlayerO);
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

        public bool IsAvailabeMove(Move i_Move)
        {
            return m_CurrentPlayer.ValidMoves.Contains(i_Move);
        }

        public bool InitPlayer(string i_Name)
        {
            bool updated = false;

            if (updated = Player.IsValidUserName(i_Name))
            {
                GameTool.eTeamSign sign = m_NextPlayer == null ? GameTool.eTeamSign.PlayerX : GameTool.eTeamSign.PlayerO;
                m_CurrentPlayer = new Player(i_Name, sign);
            }

            return updated;
        }

        public bool InitBoard(string i_Size)
        {
            Board.eBoardSize size;
            bool updated = false;

            if (updated = Player.ePLayerType.TryParse(i_Size, out size))
            {
                m_Board = new Board(size);
            }

            return updated;
        }

        public bool OpponentId(string i_NumOfPlayerType, ref string io_PlayerType )
        {
            Player.ePLayerType humanOrComputer;
            bool updated = false;

            if (updated = Player.ePLayerType.TryParse(i_NumOfPlayerType, out humanOrComputer))
            {
                io_PlayerType = humanOrComputer == Player.ePLayerType.Human ? "Human" : "Computer";
            }

            return updated;
        }

        public bool PlayerTurn(Move i_CurrentMove)
        {
            bool playerPlayed = false;

            if (playerPlayed = i_CurrentMove.IsAvailabeMove(m_CurrentPlayer))
            {
                i_CurrentMove.MakeMove(m_Board, m_NextPlayer.PlayerTools, m_CurrentPlayer.ValidMoves);
            }

            return playerPlayed;
        }

        public void BulidValidMoveListForPlayer()
        {
            m_CurrentPlayer.ValidMoves.Clear();

            foreach (GameTool tool in m_CurrentPlayer.PlayerTools)
            {
                tool.CheckOppurturnitiToEat(m_Board, m_CurrentPlayer.ValidMoves);
            }

            if (m_CurrentPlayer.ValidMoves.Count == 0) // Only if there is no piece to eat
            {
                foreach (GameTool tool in m_CurrentPlayer.PlayerTools)
                {
                    tool.AddValidMovesForTool(m_Board, m_CurrentPlayer.ValidMoves);
                }
            }
        }

        public void InitializeForNewGame()
        {
            m_CurrentPlayer.InitializePlayerForNewGame();
            m_NextPlayer.InitializePlayerForNewGame();
            Board.InitialBoardForNewGame(m_CurrentPlayer, m_NextPlayer);
            m_IsPlayerOneTurn = true;
        }

        public void SwapPlayers()
        {
            Player tempPlayer = m_CurrentPlayer;
            m_CurrentPlayer = m_NextPlayer;
            m_NextPlayer = tempPlayer;
        }

        public void ExecuteMove(Move i_Move)
        {
            i_Move.MakeMove(m_Board, m_NextPlayer.PlayerTools, m_CurrentPlayer.ValidMoves);
        }

        public bool CheckIfDoubleEat()
        {
            return !(m_CurrentPlayer.ValidMoves.Count == 0);
        }
    }
}
