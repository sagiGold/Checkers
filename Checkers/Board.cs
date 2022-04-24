using System.Drawing;

namespace Checkers
{
    public class Board
    {
        private const GameTool k_Empty = null;
        private readonly GameTool[,] m_GameBoard;
        private int m_Size;

        public enum eBoardSize
        {
            Small = 6,
            Medium = 8,
            Large = 10,
        }

        //public Board(int i_BoardSize, Player io_PlayerOne, Player io_PlayerTwo)
        //{
        //    m_Height = i_BoardSize;
        //    m_Width = i_BoardSize;
        //    m_GameBoard = new GameTool[i_BoardSize, i_BoardSize];
        //    InitialBoardForNewGame(io_PlayerOne, io_PlayerTwo);
        //}

        public Board(eBoardSize i_BoardSize)
        {
            m_Size = (int)i_BoardSize;
            m_GameBoard = new GameTool[m_Size, m_Size];
        }

        public GameTool this[int i_Row, int i_Colum]
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

        public int Size
        {
            get
            {
                return m_Size;
            }

            set
            {
                m_Size = value;
            }
        }

        public void InitialBoardForNewGame(Player io_Player1, Player io_Player2)
        {
            initializeBufferZone();
            initializePlayersTools(io_Player1, io_Player2);
        }

        private void initializeBufferZone()
        {
            int startLoopIndex = (m_Size / 2) - 1;
            int endLoopIndex = (m_Size / 2) + 1;

            for (int i = startLoopIndex; i < endLoopIndex; i++)
            {
                for (int j = 0; j < m_Size; j++)
                {
                    m_GameBoard[i, j] = k_Empty;
                }
            }
        }

        private void initializePlayersTools(Player io_Player1, Player io_Player2)
        {
            int startLine = 0;
            int endLine = (m_Size / 2) - 1;
            arrangePlayerToolsOnBoard(io_Player2, startLine, endLine);

            startLine = (m_Size / 2) + 1;
            endLine = m_Size;
            arrangePlayerToolsOnBoard(io_Player1, startLine, endLine);
        }

        private void arrangePlayerToolsOnBoard(Player io_Player, int i_StartLine, int i_EndLine)
        {
            for (int i = i_StartLine; i < i_EndLine; i++)
            {
                for (int j = 0; j < m_Size; j++)
                {
                    if ((i % 2 == 0 && j % 2 != 0) || (i % 2 != 0 && j % 2 == 0))
                    {
                        GameTool newMember = new GameTool(io_Player.Team, new Point(j, i));
                        io_Player.PlayerTools.Add(newMember);
                        m_GameBoard[i, j] = newMember;
                    }
                    else
                    {
                        m_GameBoard[i, j] = k_Empty;
                    }
                }
            }
        }

        public void AddToolToSquare(GameTool i_AddTool, Point i_NewLocation)
        {
            m_GameBoard[i_NewLocation.Y, i_NewLocation.X] = i_AddTool;
        }

        public void RemoveToolFromSquare(Point i_ToolLocation)
        {
            m_GameBoard[i_ToolLocation.Y, i_ToolLocation.X] = k_Empty;
        }

        public bool ToolInEndLine(GameTool i_Tool)
        {
            int endLine = i_Tool.GetToolDirection() == GameTool.eDirection.Up ? 0 : m_Size - 1;

            return i_Tool.Location.Y == endLine;
        }

        public bool IsPointInBoard(Point i_SquareLocation)
        {
            return i_SquareLocation.X < m_Size && i_SquareLocation.Y < m_Size;
        }

        public bool IsSquareEmpty(Point i_SquareLocation)
        {
            return m_GameBoard[i_SquareLocation.Y, i_SquareLocation.X] == k_Empty;
        }

        public bool IsOpponentInSquare(Point i_SquareLocation, GameTool.eTeamSign i_ToolTeam)
        {
            return !IsSquareEmpty(i_SquareLocation) && m_GameBoard[i_SquareLocation.Y, i_SquareLocation.X].TeamSign != i_ToolTeam;
        }

        //public void Clear()/// Dont know if we need it
        //{
        //    for (int i = 0; i < Height; i++)
        //    {
        //        for (int j = 0; j < Width; j++)
        //        {
        //            this[i, j] = k_Empty;
        //        }
        //    }
        //}

        public static bool ValidSize(string i_BoardSize, out int o_ValidBoardSize)
        {
            bool isNumeric = false;

            isNumeric = int.TryParse(i_BoardSize, out o_ValidBoardSize);
            return isNumeric && legalSize(o_ValidBoardSize);
        }

        private static bool legalSize(int i_ValidSize)
        {
            return i_ValidSize == (int)eBoardSize.Small || i_ValidSize == (int)eBoardSize.Medium || i_ValidSize == (int)eBoardSize.Large;
        }
    }
}