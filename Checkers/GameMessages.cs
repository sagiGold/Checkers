using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Checkers
{
    public class GameMessages
    {
        public static string GameIntro()
        {
            return "HELLO AND WELCOME TO CHECKERS, ENJOY!";
        }
        
        public static string NameMsg()
        {
            return "Please enter your name (up to 20 characters without spaces):";
        }

        public static string BoardSizeMsg()
        {
            string msg = @"Please choose board size:
for 6X6 please press 6.
for 8X8 please press 8. 
for 10X10 please press 10.";

            return msg;
        }

        public static string ChooseOpponentMsg()
        {
            string msg = @"Please choose your opponent:
1. vs computer please press 1.
2. vs human please press 2.";

            return msg;
        }

        public static string WrongInputMsg()
        {
            return "Wrong input, please try again";
        }

        public static string GetMoveMsg()
        {
            return "Please enter your next move";
        }

        public static string WinningMsg(Player i_Winner)
        {
            return $"{i_Winner.Name} wins with {i_Winner.Score} points, Congrats !!!";
        }

        public static string DrawMsg()
        {
            return "It's a draw !";
        }
        public static string KeepPlayingMsg()
        {
            string msg = @"Would you like to play another round ?:
1. Yes.
2. No.";

            return msg;
        }

        public static string GoodByeMsg()
        {
            return "Bye Bye :)";
        }
    }
}
