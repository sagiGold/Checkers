using System.Collections.Generic;

namespace Checkers
{
    public class Player
    {
        public enum ePlayerType
        {
            Computer = 1,
            Human,
        }

        private const int k_MaxUserNameSize = 20;
        private readonly string r_Name;
        private readonly ePlayerType r_PlayerType;
        private readonly GameTool.eTeamSign r_Team;
        private int m_Score = 0;
        private string m_LastMove;
        private List<GameTool> m_PlayerTools = new List<GameTool>();
        private List<Move> m_ValidMovesList = new List<Move>();

        public Player(string i_PlayerName, GameTool.eTeamSign i_Team)
        {
            r_Name = i_PlayerName;
            r_Team = i_Team;
            r_PlayerType = i_PlayerName == "Computer" ? ePlayerType.Computer : ePlayerType.Human;
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
                return r_Team;
            }
        }

        public string Name
        {
            get
            {
                return r_Name;
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
        }

        public List<Move> ValidMoves
        {
            get
            {
                return m_ValidMovesList;
            }
        }

        public static bool IsValidUserName(string i_UserName)
        {
            bool nameContainSpaces = i_UserName.Contains(" ");

            return i_UserName.Length <= k_MaxUserNameSize && !nameContainSpaces;
        }

        public static bool ValidPlayerType(string i_UserInput, out ePlayerType o_PlayerType)
        {
            bool isNumeric = ePlayerType.TryParse(i_UserInput, out o_PlayerType);

            return isNumeric && legalPlayerType(o_PlayerType);
        }

        private static bool legalPlayerType(ePlayerType i_ValidPlayerType)
        {
            return i_ValidPlayerType == ePlayerType.Computer || i_ValidPlayerType == ePlayerType.Human;
        }

        public bool IsComputer()
        {
            return r_PlayerType == ePlayerType.Computer;
        }

        public void ResetPlayerForNewGame()
        {
            m_LastMove = null;
            m_PlayerTools.Clear();
            m_ValidMovesList.Clear();
        }
    }
}