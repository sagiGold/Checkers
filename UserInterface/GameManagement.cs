using System;
using System.Collections.Generic;
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
            InitiateNewGame();

        }
        public void InitiateNewGame()
        {
            MainMenu.GetPreGameData(m_Game);
            //m_Game.Board.InitialBoardForNewGame(m_Game.pl)

        }



        public Move TranslateUserInputToMove(string i_MoveInput)
        {
            return null;
        }

        public string BoardToString()
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
                    boardInString.Append("| " + (char)m_Game.Board[i, j].ToolSign + " ");
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
