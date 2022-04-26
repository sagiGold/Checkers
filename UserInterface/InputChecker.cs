using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using Checkers;


namespace UserInterface
{
    public class InputChecker
    {
        public static void GetPreGameData(Game io_Game)
        {
            Printer.GameIntro();
            GetNameFromUser(io_Game);
            io_Game.SwapPlayers();
            GetBoardSizeFromUser(io_Game);
            GetOpponent(io_Game);
            io_Game.SwapPlayers();
        }

        private static void GetNameFromUser(Game io_Game)
        {
            Printer.NameMsg();
            string firstName = Console.ReadLine();

            while (!io_Game.InitPlayer(firstName))
            {
                Printer.WrongInputMsg();
                Printer.NameMsg();
                firstName = Console.ReadLine();
            }
        }

        private static void GetBoardSizeFromUser(Game io_Game)
        {
            Printer.BoardSizeMsg();
            string boardSize = Console.ReadLine();

            while (!io_Game.InitBoard(boardSize))
            {
                Printer.WrongInputMsg();
                Printer.BoardSizeMsg();
                boardSize = Console.ReadLine();
            }
        }

        private static void GetOpponent(Game io_Game)
        {
            Printer.ChooseOpponentMsg();
            string userInput = Console.ReadLine();
            string playerType = null;

            while (!io_Game.CheckOpponentType(userInput, ref playerType))
            {
                Printer.WrongInputMsg();
                Printer.ChooseOpponentMsg();
                userInput = Console.ReadLine();
            }

            bool isComputer = playerType == "Computer";

            if (isComputer)
            {
                io_Game.InitPlayer(playerType);
            }
            else 
            {
                GetNameFromUser(io_Game);
            }

        }
        public static bool userWantsToPlay()
        {
            Printer.KeepPlayingMsg();
            string userChoice = Console.ReadLine();
            int userChoiceConverted;

            while ((!int.TryParse(userChoice, out userChoiceConverted)) ||
                (userChoiceConverted != 2 && userChoiceConverted != 1))
            {
                Printer.WrongInputMsg();
                Printer.KeepPlayingMsg();
                userChoice = Console.ReadLine();
            }

            return userChoiceConverted == 1;
        }
    }
}
