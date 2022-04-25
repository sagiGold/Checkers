using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Checkers
{
    public class Game
    {
        private Board m_Board = null;
        private Player m_CurrentPlayer = null;
        private Player m_NextPlayer = null;
        private Player m_Winner = null;

        public Board Board
        {
            get
            {
                return m_Board;
            }
        }

        public Player Winner
        {
            get
            {
                return m_Winner;
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
            int size;
            bool updated = false;

            if (updated = Board.ValidSize(i_Size, out size))
            {
                m_Board = new Board(size);
            }

            return updated;
        }

        public bool CheckOpponentType(string i_UserInput, ref string io_PlayerType )
        {
            Player.ePLayerType humanOrComputer;
            bool updated = false;

            if (updated = Player.ePLayerType.TryParse(i_UserInput, out humanOrComputer))
            {
                io_PlayerType = humanOrComputer == Player.ePLayerType.Human ? "Human" : "Computer";
            }

            return updated;
        }

        public void BulidMoveList()
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

        public void ResetGame()
        {
            m_CurrentPlayer.ResetPlayerForNewGame();
            m_NextPlayer.ResetPlayerForNewGame();
            Board.InitializeBoard(m_CurrentPlayer, m_NextPlayer);
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

        public bool CheckForDoubleStrike()
        {
            return !(m_CurrentPlayer.ValidMoves.Count == 0);
        }

        public bool IsGameOver()
        {
            bool isGameOver = false;

            if (m_CurrentPlayer.ValidMoves.Count != 0 && m_NextPlayer.ValidMoves.Count == 0)
            {
                updateWinnerData(m_CurrentPlayer, m_NextPlayer);
                isGameOver = true;
            }
            else if (m_CurrentPlayer.ValidMoves.Count == 0 && m_NextPlayer.ValidMoves.Count != 0)
            {
                updateWinnerData(m_NextPlayer, m_CurrentPlayer);
                isGameOver = true;
            }
            else if (m_CurrentPlayer.ValidMoves.Count == 0 && m_NextPlayer.ValidMoves.Count == 0)
            {
                isGameOver = true; // Draw
            }

            return isGameOver;
        }

        private void updateWinnerData(Player i_Winner, Player i_Loser)
        {
            int winnerToolCount = 0;
            int loserToolCount = 0;

            foreach (GameTool tool in i_Winner.PlayerTools)
            {
                winnerToolCount += (int)tool.Rank;
            }

            foreach (GameTool tool in i_Loser.PlayerTools)
            {
                loserToolCount += (int)tool.Rank;
            }

            if (loserToolCount < winnerToolCount) 
            {
                i_Winner.Score += winnerToolCount - loserToolCount;
            }
            else // Our own bunus to player in case of winning and having smaller countdown
            {
                i_Winner.Score += 5;
            }

            m_Winner = i_Winner;
        }




        //public bool PlayerTurn(Move i_CurrentMove)
        //{
        //    bool playerPlayed = false;

        //    if (playerPlayed = i_CurrentMove.IsAvailabeMove(m_CurrentPlayer))
        //    {
        //        i_CurrentMove.MakeMove(m_Board, m_NextPlayer.PlayerTools, m_CurrentPlayer.ValidMoves);
        //    }

        //    return playerPlayed;
        //}
    }
}
