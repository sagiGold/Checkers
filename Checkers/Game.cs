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
        private Player m_OpponentPlayer = null;
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
            bool isAvailabe = false;

            foreach (Move move in m_CurrentPlayer.ValidMoves)
            {
                if (move.Equals(i_Move))
                {
                    isAvailabe = true;
                    break;
                }
            }

            return isAvailabe;
           // return m_CurrentPlayer.ValidMoves.Contains(i_Move);
        }

        public bool InitPlayer(string i_Name)
        {
            bool updated = false;

            if (updated = Player.IsValidUserName(i_Name))
            {
                GameTool.eTeamSign sign = m_OpponentPlayer == null ? GameTool.eTeamSign.PlayerX : GameTool.eTeamSign.PlayerO;
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
            Player.ePlayerType humanOrComputer;
            bool updated = false;

            if (updated = Player.ValidPlayerType(i_UserInput, out humanOrComputer))
            {
                io_PlayerType = humanOrComputer == Player.ePlayerType.Human ? "Human" : "Computer";
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
            m_OpponentPlayer.ResetPlayerForNewGame();
            Board.InitializeBoard(m_CurrentPlayer, m_OpponentPlayer);
        }

        public void SwapPlayers()
        {
            Player tempPlayer = m_CurrentPlayer;
            m_CurrentPlayer = m_OpponentPlayer;
            m_OpponentPlayer = tempPlayer;
        }

        public void ExecutePlayerMove(Move i_Move)
        {
            i_Move.MakeMove(m_Board, m_OpponentPlayer.PlayerTools, m_CurrentPlayer.ValidMoves);
        }

        public bool CheckForDoubleStrike(bool i_LastMoveEat)
        {
            return i_LastMoveEat && !(m_CurrentPlayer.ValidMoves.Count == 0);
        }

        public bool IsGameOver()
        {
            bool isGameOver = false;

            if (m_CurrentPlayer.ValidMoves.Count == 0)
            {
                if (m_OpponentPlayer.ValidMoves.Count != 0)
                {
                updateWinnerData(m_OpponentPlayer, m_CurrentPlayer);
                }

                isGameOver = true;
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

            i_Winner.Score += Math.Abs(winnerToolCount - loserToolCount);
            m_Winner = i_Winner;
        }

        public string BoardToString()
        {
            return m_Board.ToString();
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
