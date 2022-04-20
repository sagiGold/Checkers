namespace Checkers
{    
    public class GameTool
    {
        public enum eTeamSign
        {
            PlayerO = 'O',
            PlayerX = 'X',
            PlayerOKing = 'U',
            PlayerXKing = 'K',
        }

        private eTeamSign m_Sign;

        public eTeamSign Sign
        {
            get
            {
                return m_Sign;
            }

            set
            {
                m_Sign = value;
            }
        }
    }
}
