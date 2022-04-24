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
            return "HELLO AND WELCOME, YOU ARE GOING TO PLAY CHECKERS, ENJOY!";
        }

        public static string NameMsg()
        {
            return "Please enter your nickname (until 20 characters without spaces):";
        }

        public static string BoardSizeMsg()
        {
            string msg = @"Please choose the size of the board:
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
    }
}
