using Checkers;
using System;
using System.Text;

namespace UserInterface
{
    public class InputChecker
    {
        public static void GetPreGameData(GameLogic io_Game)
        {
            Printer.GameIntro();
            GetNameFromUser(io_Game);
            io_Game.SwapPlayers();
            GetBoardSizeFromUser(io_Game);
            GetOpponent(io_Game);
            io_Game.SwapPlayers();
        }

        private static void GetNameFromUser(GameLogic io_Game)
        {
            Printer.NameMsg();
            StringBuilder firstName = new StringBuilder(Console.ReadLine());

            while (!io_Game.InitPlayer(firstName.ToString()))
            {
                Printer.WrongInputMsg();
                Printer.NameMsg();
                firstName.Clear();
                firstName.Append(Console.ReadLine());
            }
        }

        private static void GetBoardSizeFromUser(GameLogic io_Game)
        {
            Printer.BoardSizeMsg();
            StringBuilder boardSize = new StringBuilder(Console.ReadLine());

            while (!io_Game.InitBoard(boardSize.ToString()))
            {
                Printer.WrongInputMsg();
                Printer.BoardSizeMsg();
                boardSize.Clear();
                boardSize.Append(Console.ReadLine());
            }
        }

        private static void GetOpponent(GameLogic io_Game)
        {
            Printer.ChooseOpponentMsg();
            StringBuilder userInput = new StringBuilder(Console.ReadLine());
            string playerType = null;

            while (!io_Game.CheckOpponentType(userInput.ToString(), ref playerType))
            {
                Printer.WrongInputMsg();
                Printer.ChooseOpponentMsg();
                userInput.Clear();
                userInput.Append(Console.ReadLine());
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
            StringBuilder userChoice = new StringBuilder(Console.ReadLine());
            int userChoiceConverted;

            while ((!int.TryParse(userChoice.ToString(), out userChoiceConverted)) ||
                (userChoiceConverted != 2 && userChoiceConverted != 1))             // should not stay here we need to move it to function in the logic that returns bool
            {
                Printer.WrongInputMsg();
                Printer.KeepPlayingMsg();
                userChoice.Clear();
                userChoice.Append(Console.ReadLine());
            }

            return userChoiceConverted == 1;
        }
    }
}
