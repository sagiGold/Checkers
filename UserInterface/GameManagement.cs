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

        public bool PressedQ
        {
            set
            {
                m_PressedQ = value;
            }
            get
            {
                return m_PressedQ;
            }
        }

        public void Run()
        {
            InputChecker.GetPreGameData(m_Game);

            do
            {
                m_PressedQ = false;
                m_Game.ResetGame();
                RunSingleMatch();
            } while (InputChecker.userWantsToPlay());

            Printer.GoodByeMsg();
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

        public void PlayerTurn()
        {
            Printer.PrintBoard(m_Game);
            Move nextMove = m_Game.IsComputerTurn() ? m_Game.GetComputerMove() : GetValidMove();

            if (!m_PressedQ)
            {
                m_Game.ExecutePlayerMove(nextMove);

                if (m_Game.CheckForDoubleStrike(nextMove.IsEatMove()))
                {
                    PlayerTurn();
                }
            }
        }

        private void handleGameOver()
        {
            Ex02.ConsoleUtils.Screen.Clear();

            if (m_PressedQ)
            {
                m_Game.CurrentPlayerQuitMatch();
                Printer.QuitGameMsg(m_Game);
            }
            else
            {
                Printer.WinningMsg(m_Game);
            }

            Printer.StatusMsg(m_Game);
        }
        public Move GetValidMove()
        {
            Printer.GetMoveMsg();
            string moveInput = Console.ReadLine();
            KeyValuePair<bool, Move> userNextMove;

            while ((!(userNextMove = TryDecodeUserInputToMove(moveInput)).Key ||
                !m_Game.IsAvailabeMove(userNextMove.Value)) && !checkForExitKeyInput(moveInput))
            {
                Printer.WrongInputMsg();
                Printer.GetMoveMsg();
                moveInput = Console.ReadLine();
            }

            return userNextMove.Value;
        }

        private KeyValuePair<bool, Move> TryDecodeUserInputToMove(string i_Move)
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

        private Move DecodeUserInputToMove(string i_Move)
        {
            Point moveFrom = new Point((i_Move[0] - 'A'), (i_Move[1] - 'a'));
            Point moveTo = new Point((i_Move[3] - 'A'), (i_Move[4] - 'a'));

            return new Move(moveFrom, moveTo);
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
    }
}
