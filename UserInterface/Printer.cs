using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UserInterface
{
    public class Printer
    {
        public static void GameIntro()
        {
            Console.WriteLine(Checkers.GameMessages.GameIntro());
        }

        public static void NameMsg()
        {
            Console.WriteLine(Checkers.GameMessages.NameMsg());
        }

        public static void BoardSizeMsg()
        {
            Console.WriteLine(Checkers.GameMessages.BoardSizeMsg());
        }

        public static void ChooseOpponentMsg()
        {
            Console.WriteLine(Checkers.GameMessages.ChooseOpponentMsg());
        }

        public static void WrongInputMsg()
        {
            Console.WriteLine(Checkers.GameMessages.WrongInputMsg());
        }

        public static void GetMoveMsg()
        {
            Console.WriteLine(Checkers.GameMessages.GetMoveMsg());
        }

        public static void WinningMsg(Checkers.Game i_Game)
        {
            Console.WriteLine(Checkers.GameMessages.WinningMsg(i_Game));
        }

        public static void DrawMsg()
        {
            Console.WriteLine(Checkers.GameMessages.DrawMsg());
        }
        public static void KeepPlayingMsg()
        {
            Console.WriteLine(Checkers.GameMessages.KeepPlayingMsg());
        }

        public static void GoodByeMsg()
        {
            Ex02.ConsoleUtils.Screen.Clear();
            Console.WriteLine(Checkers.GameMessages.GoodByeMsg());
            System.Threading.Thread.Sleep(4000);
        }

        public static void QuitGameMsg(Checkers.Game i_Game)
        {
            Console.WriteLine(Checkers.GameMessages.QuitGameMsg(i_Game));
        }

        public static void StatusMsg(Checkers.Game i_Game)
        {
            Console.WriteLine(Checkers.GameMessages.StatusMsg(i_Game));
        }

        public static void PrintBoard(Checkers.Game i_Game)
        {
            Ex02.ConsoleUtils.Screen.Clear();
            Console.WriteLine(i_Game.BoardToString());
            PrintPlayersData(i_Game); // dani's last turn : Aa> Bb
                                              // sagi's turn: 
        }
    }
}
