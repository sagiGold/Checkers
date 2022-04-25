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
            Player currentPlayer = m_IsPlayerOneTurn ? m_PlayerOne : m_PlayerTwo;
            Player nextPlayer = m_IsPlayerOneTurn ? m_PlayerTwo : m_PlayerOne;
            bool playerPlayed = false;

            if (playerPlayed = i_CurrentMove.IsAvailabeMove(currentPlayer))
            {
                i_CurrentMove.MakeMove(m_Board, nextPlayer.PlayerTools, currentPlayer.ValidMoves);
            }

            return playerPlayed;
        }

        public void BulidValidMoveListForPlayer()
        {
            Player currentPlayer = m_IsPlayerOneTurn ? m_PlayerOne : m_PlayerTwo;

            currentPlayer.ValidMoves.Clear();
            foreach (GameTool tool in currentPlayer.PlayerTools)
            {
                tool.CheckOppurturnitiToEat(m_Board, currentPlayer.ValidMoves);
            }

            if (currentPlayer.ValidMoves.Count == 0) // Only if there is no piece to eat
            {
                foreach (GameTool tool in currentPlayer.PlayerTools)
                {
                    tool.AddValidMovesForTool(m_Board, currentPlayer.ValidMoves);
                }
            }
        }

        public void InitializeForNewGame()
        {
            m_PlayerOne.InitializePlayerForNewGame();
            m_PlayerTwo.InitializePlayerForNewGame();
            Board.InitialBoardForNewGame(m_PlayerOne, m_PlayerTwo);
            m_IsPlayerOneTurn = true;
        }
    }
}
