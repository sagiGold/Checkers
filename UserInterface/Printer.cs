using Checkers;
using System;

namespace UserInterface
{
    public class Printer
    {
        public static void GameIntro()
        {
            Console.WriteLine(GameMessages.GameIntro());
        }

        public static void NameMsg()
        {
            Console.WriteLine(GameMessages.NameMsg());
        }

        public static void BoardSizeMsg()
        {
            Console.WriteLine(GameMessages.BoardSizeMsg());
        }

        public static void ChooseOpponentMsg()
        {
            Console.WriteLine(GameMessages.ChooseOpponentMsg());
        }

        public static void WrongInputMsg()
        {
            Console.WriteLine(GameMessages.WrongInputMsg());
        }

        public static void WinningMsg(GameLogic i_Game)
        {
            Console.WriteLine(GameMessages.WinningMsg(i_Game));
        }

        public static void DrawMsg()
        {
            Console.WriteLine(GameMessages.DrawMsg());
        }
        public static void KeepPlayingMsg()
        {
            Console.WriteLine(GameMessages.KeepPlayingMsg());
        }

        public static void GoodByeMsg()
        {
            Ex02.ConsoleUtils.Screen.Clear();
            Console.WriteLine(GameMessages.GoodByeMsg());
            System.Threading.Thread.Sleep(4000);
        }

        public static void QuitGameMsg(GameLogic i_Game)
        {
            Console.WriteLine(GameMessages.QuitGameMsg(i_Game));
        }

        public static void StatusMsg(GameLogic i_Game)
        {
            Console.WriteLine(GameMessages.StatusMsg(i_Game));
        }

        public static void PrintBoard(GameLogic i_Game)
        {
            Ex02.ConsoleUtils.Screen.Clear();
            Console.WriteLine(i_Game.BoardToString());
            PrintPlayersData(i_Game);
        }

        private static void PrintPlayersData(GameLogic i_Game)
        {
            Console.WriteLine(GameMessages.PlayerTurnMsg(i_Game));
        }
    }
}
