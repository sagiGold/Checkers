using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using Checkers;

namespace UserInterface
{
    public class GameManagement
    {
        private Game m_Game = new Game();
       
        public void Run()
        {
            MainMenu.GetPreGameData(m_Game);

            while (true /*keepPlaying*/)
            {
                m_Game.ResetGame();
                RunSingleMatch();
            }
        }

        public void RunSingleMatch()
        {
            while (true /* something that checks if end game in game logic*/)
            {
                PlayerTurn();
                m_Game.SwapPlayers();
            }
        }

        public void PlayerTurn()
        {
            PrintBoard();
            Move nextMove = GetValidMove();
            m_Game.ExecuteMove(nextMove);

            if (m_Game.CheckForDoubleStrike())
            {
                PlayerTurn();
            }
        }

        private Move GetValidMove()
        {
            GameMessages.GetMoveMsg();
            string moveInput = Console.ReadLine();
            KeyValuePair<bool, Move> userNextMove;

            while (!(userNextMove = TryDecodeUserInputToMove(moveInput)).Key && !m_Game.IsAvailabeMove(userNextMove.Value))
            {
                GameMessages.WrongInputMsg();
                GameMessages.GetMoveMsg();
                moveInput = Console.ReadLine();
            }

            return userNextMove.Value;
        }

        public KeyValuePair<bool, Move> TryDecodeUserInputToMove(string i_Move)
        {
            bool validInput = char.IsUpper(i_Move, 0) && char.IsUpper(i_Move, 3)
                && char.IsLower(i_Move, 1) && char.IsLower(i_Move, 4)
                && i_Move[2] == '>';    // Input built as the following template : Aa>Bb
            Move newMove = null;

            if (validInput)
            {
                newMove = DecodeUserInputToMove(i_Move);
            }

            return new KeyValuePair<bool, Move>(validInput, newMove);
        }

        public Move DecodeUserInputToMove(string i_Move)
        {
            Point moveFrom = new Point((i_Move[0] - 'A'), (i_Move[1] - 'a'));
            Point moveTo = new Point((i_Move[3] - 'A'), (i_Move[4] - 'a'));

            return new Move(moveFrom, moveTo);
        }

        private void PrintBoard()
        {
            Ex02.ConsoleUtils.Screen.Clear();
            Console.WriteLine(BoardToString());
        }

        private string BoardToString()
        {
            StringBuilder boardInString = new StringBuilder();
            string horizontalEqualsLine = createEqualsLine(m_Game.Board.Size);

            boardInString.Append(" ");

            for (char c = 'A'; c < m_Game.Board.Size + 'A'; c++)
            {
                boardInString.Append(string.Format("  {0} ", c));
            }

            boardInString.Append(Environment.NewLine);

            for (int i = 0; i < m_Game.Board.Size; i++)
            {
                boardInString.Append(horizontalEqualsLine);
                boardInString.Append(string.Format("{0}", (char)(i + 'a')));

                for (int j = 0; j < m_Game.Board.Size; j++)
                {
                    if (m_Game.Board[i, j] == null)
                    {
                        boardInString.Append("| " + " " + " ");

                    }
                    else
                    {
                        boardInString.Append("| " + (char)m_Game.Board[i, j].ToolSign + " ");
                    }
                }

                boardInString.Append("|");
                boardInString.Append(Environment.NewLine);
            }

            boardInString.Append(horizontalEqualsLine);
            return boardInString.ToString();
        }

        private string createEqualsLine(int i_Size)
        {
            StringBuilder equalLine = new StringBuilder();

            equalLine.Append(" ");

            for (int j = 0; j < i_Size; j++)
            {
                equalLine.Append("====");
            }

            equalLine.Append("=" + Environment.NewLine);
            return equalLine.ToString();
        }
    }
}
