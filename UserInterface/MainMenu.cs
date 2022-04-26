using System;
using System.Collections.Generic;
using System.Text;
using Checkers;


namespace UserInterface
{
    public class MainMenu
    {
        public static void GetPreGameData(Game io_Game)
        {
            PrintMessage.GameIntro();
            GetNameFromUser(io_Game);
            io_Game.SwapPlayers();
            GetBoardSizeFromUser(io_Game);
            GetOpponent(io_Game);
            io_Game.SwapPlayers();
        }

        private static void GetNameFromUser(Game io_Game)
        {
            PrintMessage.NameMsg();
            string firstName = Console.ReadLine();

            while (!io_Game.InitPlayer(firstName))
            {
                PrintMessage.WrongInputMsg();
                PrintMessage.NameMsg();
                firstName = Console.ReadLine();
            }
        }

        private static void GetBoardSizeFromUser(Game io_Game)
        {
            PrintMessage.BoardSizeMsg();
            string boardSize = Console.ReadLine();

            while (!io_Game.InitBoard(boardSize))
            {
                PrintMessage.WrongInputMsg();
                PrintMessage.BoardSizeMsg();
                boardSize = Console.ReadLine();
            }
        }

        private static void GetOpponent(Game io_Game)
        {
            PrintMessage.ChooseOpponentMsg();
            string userInput = Console.ReadLine();
            string playerType = null;

            while (!io_Game.CheckOpponentType(userInput, ref playerType))
            {
                PrintMessage.WrongInputMsg();
                PrintMessage.ChooseOpponentMsg();
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
            PrintMessage.KeepPlayingMsg();
            string userChoice = Console.ReadLine();
            int userChoiceConverted;

            while ((!int.TryParse(userChoice, out userChoiceConverted)) ||
                (userChoiceConverted != 2 && userChoiceConverted != 1))
            {
                PrintMessage.WrongInputMsg();
                PrintMessage.KeepPlayingMsg();
                userChoice = Console.ReadLine();
            }

            return userChoiceConverted == 1;
        }
    }
}
