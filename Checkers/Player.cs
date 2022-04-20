using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Checkers
{
    class Player
    {
        private const int m_MaxUserNameSize = 20;

        public enum ePLayerType
        {
            Computer,
            Human,
        }

        private int m_Score = 0;
        private char m_ToolShape;
        private string m_Name;
        private ePLayerType m_PlayerType;
        private List<GameTool> m_PlayerTools = new List<GameTool>();
        private List<Move> m_ValidMovesList = new List<Move>();

    }
}
