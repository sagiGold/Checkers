using System.Collections.Generic;

namespace Checkers
{
    public class Player
    {
        private const int m_MaxUserNameSize = 20;

        public enum ePlayerType
        {
            Computer = 1,
            Human,
        }

        private string m_Name;
        private string m_LastMove = null;
        private ePlayerType m_PlayerType;
        private GameTool.eTeamSign m_Team;
        private List<GameTool> m_PlayerTools = new List<GameTool>();
        private List<Move> m_ValidMovesList = new List<Move>();
        private int m_Score = 0;

        public Player(string i_PlayerName, GameTool.eTeamSign i_Team)
        {
            m_Name = i_PlayerName;
            m_Team = i_Team;
            m_PlayerType = i_PlayerName == "Computer" ? ePlayerType.Computer : ePlayerType.Human;
        }

        public int Score
        {
            get
            {
                return m_Score;
            }

            set
            {
                m_Score = value;
            }
        }

        public GameTool.eTeamSign Team
        {
            get
            {
                return m_Team;
            }

            set
            {
                m_Team = value;
            }
        }

        public string Name
        {
            get
            {
                return m_Name;
            }

            set
            {
                m_Name = value;
            }
        }

        public string LastMove
        {
            get
            {
                return m_LastMove;
            }

            set
            {
                m_LastMove = value;
            }
        }

        public List<GameTool> PlayerTools
        {
            get
            {
                return m_PlayerTools;
            }

            set
            {
                m_PlayerTools = value;
            }
        }

        public List<Move> ValidMoves
        {
            get
            {
                return m_ValidMovesList;
            }

            set
            {
                m_ValidMovesList = value;
            }
        }

        public static bool IsValidUserName(string i_UserName)
        {
            bool nameContainSpaces = i_UserName.Contains(" ");

            return i_UserName.Length <= m_MaxUserNameSize && !nameContainSpaces;
        }

        public static bool ValidPlayerType(string i_UserInput, out ePlayerType o_PlayerType)
        {
            bool isNumeric = false;

            isNumeric = ePlayerType.TryParse(i_UserInput, out o_PlayerType);
            return isNumeric && legalPlayerType(o_PlayerType);
        }

        private static bool legalPlayerType(ePlayerType i_ValidPlayerType)
        {
            return i_ValidPlayerType == ePlayerType.Computer || i_ValidPlayerType == ePlayerType.Human;
        }

        public bool IsComputer()
        {
            return m_PlayerType == ePlayerType.Computer;
        }

        public void ResetPlayerForNewGame()
        {
            m_LastMove = null;
            m_PlayerTools.Clear();
            m_ValidMovesList.Clear();
        }
    }
}