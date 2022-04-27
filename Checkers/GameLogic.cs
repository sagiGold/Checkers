using System;
using System.Collections.Generic;

namespace Checkers
{
    public class GameLogic
    {
        private Board m_Board = null;
        private Player m_CurrentPlayer = null;
        private Player m_OpponentPlayer = null;
        private Player m_Winner = null;
        private int? m_CurrentMatchWinnerScore = null;

        public Board Board
        {
            get
            {
                return m_Board;
            }
        }

        internal Player Winner
        {
            get
            {
                return m_Winner;
            }
        }

        internal Player CurrentPlayer
        {
            get
            {
                return m_CurrentPlayer;
            }
        }

        internal Player OpponentPlayer
        {
            get
            {
                return m_OpponentPlayer;
            }
        }

        internal int CurrentWinnerScore
        {
            get
            {
                return m_CurrentMatchWinnerScore.Value;
            }
        }

        public bool InitPlayer(string i_Name)
        {
            bool update = Player.IsValidUserName(i_Name);

            if (update)
            {
                GameTool.eTeamSign sign = m_OpponentPlayer == null ? GameTool.eTeamSign.PlayerX : GameTool.eTeamSign.PlayerO;
                m_CurrentPlayer = new Player(i_Name, sign);
            }

            return update;
        }

        public bool InitBoard(string i_Size)
        {
            int size;
            bool update = Board.ValidSize(i_Size, out size);

            if (update)
            {
                m_Board = new Board(size);
            }

            return update;
        }

        public bool CheckOpponentType(string i_UserInput, ref string io_PlayerType)
        {
            Player.ePlayerType humanOrComputer;
            bool update = Player.ValidPlayerType(i_UserInput, out humanOrComputer);

            if (update)
            {
                io_PlayerType = humanOrComputer == Player.ePlayerType.Human ? "Human" : "Computer";
            }

            return update;
        }

        public bool IsAvailabeMove(Move i_Move)
        {
            bool isAvailabe = false;

            foreach (Move move in m_CurrentPlayer.ValidMoves)
            {
                if (Move.IsEquals(move, i_Move))
                {
                    isAvailabe = true;
                    break;
                }
            }

            return isAvailabe;
        }

        public void BulidMoveList()
        {
            m_CurrentPlayer.ValidMoves.Clear();

            foreach (GameTool tool in m_CurrentPlayer.PlayerTools)
            {
                tool.CheckOppurturnitiToEat(m_Board, m_CurrentPlayer.ValidMoves);
            }

            if (m_CurrentPlayer.ValidMoves.Count == 0)
            {
                foreach (GameTool tool in m_CurrentPlayer.PlayerTools)
                {
                    tool.AddValidMovesForTool(m_Board, m_CurrentPlayer.ValidMoves);
                }
            }
        }

        public void ExecutePlayerMove(Move i_Move)
        {
            i_Move.MakeMove(m_Board, m_OpponentPlayer.PlayerTools, m_CurrentPlayer.ValidMoves);
            m_CurrentPlayer.LastMove = i_Move.ToString();
        }

        public bool CheckForDoubleStrike(bool i_LastMoveEat)
        {
            return i_LastMoveEat && !(m_CurrentPlayer.ValidMoves.Count == 0);
        }

        public void SwapPlayers()
        {
            Player tempPlayer = m_CurrentPlayer;

            m_CurrentPlayer = m_OpponentPlayer;
            m_OpponentPlayer = tempPlayer;
        }

        public bool IsGameOver()
        {
            bool isGameOver = m_CurrentPlayer.ValidMoves.Count == 0;

            if (isGameOver)
            {
                updateWinnerScore(m_OpponentPlayer, m_CurrentPlayer);
            }

            return isGameOver;
        }

        public void ResetGame()
        {
            if (m_CurrentPlayer.Team == GameTool.eTeamSign.PlayerO)
            {
                SwapPlayers();
            }

            m_CurrentPlayer.ResetPlayerForNewGame();
            m_OpponentPlayer.ResetPlayerForNewGame();
            Board.InitializeBoard(m_CurrentPlayer, m_OpponentPlayer);
            m_CurrentMatchWinnerScore = 0;
        }

        public Move GetComputerMove()
        {
            Random random = new Random();

            System.Threading.Thread.Sleep(2500);
            return m_CurrentPlayer.ValidMoves[random.Next(m_CurrentPlayer.ValidMoves.Count - 1)];
        }

        public void CurrentPlayerQuitMatch()
        {
            SwapPlayers();
            m_CurrentPlayer.Score -= 3;
        }

        public bool IsComputerTurn()
        {
            return m_CurrentPlayer.IsComputer();
        }

        public string BoardToString()
        {
            return m_Board.ToString();
        }

        public KeyValuePair<bool, Move> TryDecodeUserInputToMove(string i_Move)
        {
            return Move.TryDecodeUserInputToMove(i_Move);
        }

        private void updateWinnerScore(Player i_Winner, Player i_Loser)
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

            m_CurrentMatchWinnerScore = Math.Abs(winnerToolCount - loserToolCount);
            i_Winner.Score += m_CurrentMatchWinnerScore.Value;
            m_Winner = i_Winner;
        }
    }
}
