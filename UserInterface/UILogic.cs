using Checkers;
using System;
using System.Text;

namespace UserInterface
{
    public class UILogic
    {
        public static void GetPreGameData(GameLogic io_Game)
        {
            Printer.GameIntro();
            getNameFromUser(io_Game);
            io_Game.SwapPlayers();
            getBoardSizeFromUser(io_Game);
            getOpponent(io_Game);
            io_Game.SwapPlayers();
        }

        private static void getNameFromUser(GameLogic io_Game)
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

        private static void getBoardSizeFromUser(GameLogic io_Game)
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

        private static void getOpponent(GameLogic io_Game)
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
                getNameFromUser(io_Game);
            }

        }
        public static bool UserWantsToPlay()
        {
            Printer.KeepPlayingMsg();
            StringBuilder userChoice = new StringBuilder(Console.ReadLine());
            int anotherRound;

            while (checkForAnotherRound(userChoice.ToString(), out anotherRound))
            {
                Printer.WrongInputMsg();
                Printer.KeepPlayingMsg();
                userChoice.Clear();
                userChoice.Append(Console.ReadLine());
            }

            return anotherRound == 1;
        }

        private static bool checkForAnotherRound(string i_UserChoice, out int o_AnotherRound)
        {
            int.TryParse(i_UserChoice, out o_AnotherRound);

            return o_AnotherRound == 2 && o_AnotherRound == 1;
        }
    }
}
