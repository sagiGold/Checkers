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
            Console.WriteLine(GameMessages.GameIntro());
            GetNameFromUser(io_Game);
            GetBoardSizeFromUser(io_Game);
            GetOpponent(io_Game);
        }

        private static void GetNameFromUser(Game io_Game)
        {
            Console.WriteLine(GameMessages.NameMsg());
            string firstName = Console.ReadLine();

            while (!io_Game.InitPlayer(firstName))
            {
                Console.WriteLine(GameMessages.WrongInputMsg());
                Console.WriteLine(GameMessages.NameMsg());
                firstName = Console.ReadLine();
            }
        }

        private static void GetBoardSizeFromUser(Game io_Game)
        {
            Console.WriteLine(GameMessages.BoardSizeMsg());
            string boardSize = Console.ReadLine();

            while (!io_Game.InitBoard(boardSize))
            {
                Console.WriteLine(GameMessages.WrongInputMsg());
                Console.WriteLine(GameMessages.BoardSizeMsg());
                boardSize = Console.ReadLine();
            }
        }

        private static void GetOpponent(Game io_Game)
        {
            Console.WriteLine(GameMessages.ChooseOpponentMsg());
            string numOfPlayerType = Console.ReadLine();
            string playerType = null;

            while (!io_Game.OpponentId(numOfPlayerType, ref playerType))
            {
                Console.WriteLine(GameMessages.WrongInputMsg());
                Console.WriteLine(GameMessages.ChooseOpponentMsg());
                playerType = Console.ReadLine();
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
    }
}
