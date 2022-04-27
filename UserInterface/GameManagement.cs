using System;
using System.Collections.Generic;
using Checkers;

namespace UserInterface
{
    public class GameManagement
    {
        private readonly GameLogic r_Game = new GameLogic();
        private bool m_PressedQ = false;

        public void Run()
        {
            InputChecker.GetPreGameData(r_Game);
            do
            {
                m_PressedQ = false;
                r_Game.ResetGame();
                RunSingleMatch();
            } while (InputChecker.UserWantsToPlay());

            Printer.GoodByeMsg();
        }

        public void RunSingleMatch()
        {
            r_Game.BulidMoveList();
            while (!r_Game.IsGameOver() && !m_PressedQ)
            {
                PlayerTurn();
                r_Game.SwapPlayers();
                r_Game.BulidMoveList();
            } 

            handleGameOver();    
        }

        public void PlayerTurn()
        {
            Printer.PrintBoard(r_Game);
            Move nextMove = r_Game.IsComputerTurn() ? r_Game.GetComputerMove() : GetValidMove();

            if (!m_PressedQ)
            {
                r_Game.ExecutePlayerMove(nextMove);

                if (r_Game.CheckForDoubleStrike(nextMove.IsAnEatingStep()))
                {
                    PlayerTurn();
                }
            }
        }

        public Move GetValidMove()
        {
            string moveInput = Console.ReadLine();
            KeyValuePair<bool, Move> userNextMove;

            while ((!(userNextMove = r_Game.TryDecodeUserInputToMove(moveInput)).Key ||
                !r_Game.IsAvailabeMove(userNextMove.Value)) && !checkForExitKeyInput(moveInput))
            {
                Printer.WrongInputMsg();
                moveInput = Console.ReadLine();
            }

            return userNextMove.Value;
        }

        private void handleGameOver()
        {
            Ex02.ConsoleUtils.Screen.Clear();
            if (m_PressedQ)
            {
                r_Game.CurrentPlayerQuitMatch();
                Printer.QuitGameMsg(r_Game);
            }
            else
            {
                Printer.WinningMsg(r_Game);
            }

            Printer.StatusMsg(r_Game);
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
