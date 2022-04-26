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
        bool m_PressedQ; //option for nullable
       
        public void Run()
        {
            MainMenu.GetPreGameData(m_Game);

            while (true /*keepPlaying*/)
            {
                m_PressedQ = false;
                m_Game.ResetGame();
                RunSingleMatch();
            }
        }

        public void RunSingleMatch()
        {
            m_Game.BulidMoveList();

            while (!m_Game.IsGameOver() && !m_PressedQ)
            {
                PlayerTurn();
                m_Game.SwapPlayers();
                m_Game.BulidMoveList();
            } 

            handleGameOver();    
        }

        private void handleGameOver()
        {
            if (!m_PressedQ)
            {
                if (m_Game.Winner != null)
                {
                    PrintMessage.WinningMsg(m_Game.Winner);
                }
                else
                {
                    PrintMessage.DrawMsg();
                }
            }
            else
            {
                //Quit game message** >> loser and score penalty
            }
        }

        public void PlayerTurn()
        {
            PrintBoard();
            Move nextMove = GetValidMove();

            if (!m_PressedQ)
            {
                m_Game.ExecutePlayerMove(nextMove);

                if (m_Game.CheckForDoubleStrike(nextMove.IsEatMove()))
                {
                    PlayerTurn();
                }
            }
        }

        private Move GetValidMove()
        {
            PrintMessage.GetMoveMsg();
            string moveInput = Console.ReadLine();
            KeyValuePair<bool, Move> userNextMove;

            while ((!(userNextMove = TryDecodeUserInputToMove(moveInput)).Key ||
                !m_Game.IsAvailabeMove(userNextMove.Value)) && !checkForExitKeyInput(moveInput))
            {
                PrintMessage.WrongInputMsg();
                PrintMessage.GetMoveMsg();
                moveInput = Console.ReadLine();
            }

            return userNextMove.Value;
        }

        private bool checkForExitKeyInput(string i_UserInput)
        {
            string exitKey = "Q";

            if (string.Compare(i_UserInput, exitKey) == 0)
            {
                m_PressedQ = true;
            }

            return m_PressedQ;
        }

        public KeyValuePair<bool, Move> TryDecodeUserInputToMove(string i_Move)
        {
            bool validInput = i_Move.Length == 5 && char.IsUpper(i_Move, 0) && char.IsUpper(i_Move, 3)
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
            Console.WriteLine(m_Game.BoardToString());
        }
    }
}
