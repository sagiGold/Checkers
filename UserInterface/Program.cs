﻿using System;
using System.Text;
using Checkers;

namespace UserInterface
{
    public class Program
    {
        public static void Main()
        {
            GameManagement game = new GameManagement();
            Console.WriteLine(game.BoardToString());
            Console.ReadLine();

        }
    }
}
