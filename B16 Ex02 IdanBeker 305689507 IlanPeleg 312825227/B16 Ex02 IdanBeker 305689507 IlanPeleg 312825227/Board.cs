

using System.Text;

class Board
{
    private static readonly string sr_EmptyCell = "[ ]";
    private static readonly string sr_PlayerOneCell = "[X]";
    private static readonly string sr_PlayerTwoCell = "[O]";

    private int m_BoardHeight;
    private int m_BoardLength;
    private int m_BoardFreeSpace;
    private eBoardMarks[,] m_Board;

    public Board(int i_BoardHeight, int i_BoardLength)
    {
        this.m_BoardFreeSpace = i_BoardHeight * i_BoardLength;
        this.m_BoardHeight = i_BoardHeight;
        this.m_BoardLength = i_BoardLength;
        this.m_Board = new eBoardMarks[m_BoardHeight, m_BoardLength];
       
    }

    override
    public string ToString()
    {
        StringBuilder stringBuilder = new StringBuilder();
        for (int i = m_BoardHeight - 1; i >= 0; i--)
        {
            for (int j = 0; j < m_BoardLength; j++)
            {
                switch (m_Board[i, j])
                {
                    case eBoardMarks.Empty:
                        stringBuilder.Append(sr_EmptyCell);
                        break;
                    case eBoardMarks.PlayerOne:
                        stringBuilder.Append(sr_PlayerOneCell);
                        break;
                    case eBoardMarks.PlayerTwo:
                        stringBuilder.Append(sr_PlayerTwoCell);
                        break;
                }

            }
            stringBuilder.Append("\n");
        }

        return stringBuilder.ToString();
    }

    public bool EnterMove(int i_ColumnToMark, int i_Player)
    {
        bool moveSetFlag = false;

        for (int i = 0; i < m_BoardHeight; i++)
        {
            if (m_Board[i, i_ColumnToMark] == eBoardMarks.Empty)
            {
                m_Board[i, i_ColumnToMark] = i_Player == 1 ? eBoardMarks.PlayerOne : eBoardMarks.PlayerTwo;
                m_BoardFreeSpace--;
                moveSetFlag = true;
                break;
            }
        }
        return moveSetFlag;
    }

    public bool BoardIsFull()
    {
        return m_BoardFreeSpace == 0;
    }

    public bool GameIsWon()
    {
        //TODO Implement.
        return false;
    }

    public enum eBoardMarks
    {
        Empty,
        PlayerOne,
        PlayerTwo,
    }
}

