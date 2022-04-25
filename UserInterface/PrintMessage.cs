﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UserInterface
{
    class PrintMessage
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
    }
}