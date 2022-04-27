using System;
using System.Collections.Generic;
using System.Text;

namespace Checkers
{
    public class Move
    {
        private const int k_Jump2Squares = 2;
        private readonly bool m_EatMove;
        private Point m_CurrentPoint;
        private Point m_DestinationPoint;

        public Move(Point i_From, Point i_To)
        {
            m_CurrentPoint = i_From;
            m_DestinationPoint = i_To;
            m_EatMove = Math.Abs(m_CurrentPoint.X - m_DestinationPoint.X) == k_Jump2Squares;
        }

        public static bool IsEquals(Move i_FirstMove, Move i_SecondMove)
        {
            bool isEqual = Equals(i_FirstMove.m_CurrentPoint, i_SecondMove.m_CurrentPoint) &&
                Equals(i_FirstMove.m_DestinationPoint, i_SecondMove.m_DestinationPoint) && i_FirstMove.m_EatMove == i_SecondMove.m_EatMove;

            return isEqual;
        }

        public static KeyValuePair<bool, Move> TryDecodeUserInputToMove(string i_Move)
        {
            bool validInput = i_Move.Length == 5 && char.IsUpper(i_Move, 0) && char.IsUpper(i_Move, 3)
                && char.IsLower(i_Move, 1) && char.IsLower(i_Move, 4)
                && i_Move[2] == '>';    // Input built as the following template : Aa>Bb
            Move newMove = null;

            if (validInput)
            {
                newMove = decodeUserInputToMove(i_Move);
            }

            return new KeyValuePair<bool, Move>(validInput, newMove);
        }

        private static Move decodeUserInputToMove(string i_Move)
        {
            Point moveFrom = new Point((int)(i_Move[0] - 'A'), (int)(i_Move[1] - 'a'));
            Point moveTo = new Point((int)(i_Move[3] - 'A'), (int)(i_Move[4] - 'a'));

            return new Move(moveFrom, moveTo);
        }

        public bool IsAnEatingStep()
        {
            return m_EatMove;
        }

        public void MakeMove(Board io_GameBoard, List<GameTool> io_OpponentTools, List<Move> io_PlayerMoves)
        {
            GameTool toolToMove = io_GameBoard[m_CurrentPoint.Y, m_CurrentPoint.X];

            io_GameBoard.RemoveToolFromSquare(toolToMove.Location);
            io_GameBoard.AddToolToSquare(toolToMove, m_DestinationPoint);
            toolToMove.Location = m_DestinationPoint;
            if (m_EatMove)
            {
                skipOverTheOpponentTool(io_GameBoard, io_OpponentTools);
                io_PlayerMoves.Clear();
                toolToMove.CheckOppurturnitiToEat(io_GameBoard, io_PlayerMoves);
            }

            switchToKing(toolToMove, io_GameBoard);
        }

        public override string ToString()
        {
            StringBuilder moveInString = new StringBuilder();

            moveInString.Append(string.Format("{0}", (char)(m_CurrentPoint.X + 'A')));
            moveInString.Append(string.Format("{0}", (char)(m_CurrentPoint.Y + 'a')));
            moveInString.Append(">");
            moveInString.Append(string.Format("{0}", (char)(m_DestinationPoint.X + 'A')));
            moveInString.Append(string.Format("{0}", (char)(m_DestinationPoint.Y + 'a')));

            return moveInString.ToString();
        }

        private void switchToKing(GameTool io_Tool, Board i_GameBoard)
        {
            if (!io_Tool.IsKing() && i_GameBoard.ToolInEndLine(io_Tool))
            {
                io_Tool.MakeKing();
            }
        }

        private void skipOverTheOpponentTool(Board io_GameBoard, List<GameTool> io_OpponentTools)
        {
            GameTool toolToDelete = io_GameBoard[(m_CurrentPoint.Y + m_DestinationPoint.Y) / 2, (m_CurrentPoint.X + m_DestinationPoint.X) / 2];

            io_GameBoard.RemoveToolFromSquare(toolToDelete.Location);
            io_OpponentTools.Remove(toolToDelete);
        }
    }
}
