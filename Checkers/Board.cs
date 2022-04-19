using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Checkers
{
    public class Board
    {
        public enum eBoradSize
        {
            Small = 6,
            Medium = 8,
            Large = 10
        }

        private readonly char[,] m_GameBoard;
        private int m_Height;
        private int m_Width;

        public Board(int height, int width)
        {
            m_Height = height;
            m_Width = width;
            m_GameBoard = new char[height, width];
        }

        public char this[int i_Row, int i_Colum]
        {
            get
            {
                return m_GameBoard[i_Row, i_Colum];
            }

            set
            {
                m_GameBoard[i_Row, i_Colum] = value;
            }
        }

        public int Height
        {
            get
            {
                return m_Height;
            }

            set
            {
                m_Height = value;
            }
        }

        public int Width
        {
            get
            {
                return m_Width;
            }

            set
            {
                m_Width = value;
            }
        }

    }
}
