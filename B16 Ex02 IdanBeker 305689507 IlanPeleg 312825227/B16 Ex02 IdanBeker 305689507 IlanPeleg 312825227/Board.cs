

using System.Text;
//TODO: add namespace
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

    public bool EnterMove(int i_ColumnToMark, int i_Player, out bool isWon)
    {
        isWon = false; //indicates whether the last move was a winnig move
        bool moveSetFlag = m_Board[m_BoardHeight-1,i_ColumnToMark]==eBoardMarks.Empty;//check if the column is full
        if (moveSetFlag) //if there is a place for this coin - insert it
        {
            for (int i = 0; i < m_BoardHeight; i++)
            {
                if (m_Board[i, i_ColumnToMark] == eBoardMarks.Empty)
                {
                    m_Board[i, i_ColumnToMark] = i_Player == 1 ? eBoardMarks.PlayerOne : eBoardMarks.PlayerTwo;
                    if (GameIsWon(i_Player, i, i_ColumnToMark))
                    {
                        System.Console.WriteLine("there is a winner!");
                        isWon = true;
                        break;
                    }
                    m_BoardFreeSpace--;
                    moveSetFlag = true;
                    break;
                }
            }
        }
        return moveSetFlag;
    }
    

    private bool isThereWinningRow (int i_Player, int i_Row, int i_Col) //TODO: change type of player to enum
    {
        eBoardMarks valueToCheck= i_Player == 1 ? eBoardMarks.PlayerOne : eBoardMarks.PlayerTwo;
        int counterOfSequence = 0, currentCol = i_Col;
        
        //count the player's coins in the specified row - from the given cell to the right
        while (currentCol < m_BoardLength && m_Board[i_Row, currentCol] == valueToCheck)
        {
            counterOfSequence++;
            currentCol++;
        }
        currentCol = i_Col - 1; //now we want to count to the left
        //count the player's coins in the specified row - from the given cell to the left
        while (currentCol >= 0 && m_Board[i_Row, currentCol] == valueToCheck)
        {
            counterOfSequence++;
            currentCol--;
        }

        return (counterOfSequence>=4);
    }

    private bool isThereWinningCol(int i_Player, int i_Row, int i_Col) //TODO: change type of player to enum
    {
        eBoardMarks valueToCheck = i_Player == 1 ? eBoardMarks.PlayerOne : eBoardMarks.PlayerTwo;
        int counterOfSequence = 0, currentRow = i_Row;

        //count the player's coins in the specified col - from the given cell to the top
        while (currentRow < m_BoardHeight && m_Board[currentRow, i_Col] == valueToCheck)
        {
            counterOfSequence++;
            currentRow++;
        }
        currentRow = i_Row - 1; //now we want to count to the bottom
        //count the player's coins in the specified col - from the given cell to the bottom
        while (currentRow >= 0 && m_Board[currentRow, i_Col] == valueToCheck)
        {
            counterOfSequence++;
            currentRow--;
        }

        return (counterOfSequence >= 4);
    }

    private bool isThereWinningMainDiagonal(int i_Player, int i_Row, int i_Col) //TODO: change type of player to enum
    {
        eBoardMarks valueToCheck = i_Player == 1 ? eBoardMarks.PlayerOne : eBoardMarks.PlayerTwo;
        int counterOfSequence = 0, currentRow = i_Row, currentCol = i_Col;

        //count the player's coins in the specified main diagonal - from the given cell to the right-up
        while (currentRow < m_BoardHeight && currentCol < m_BoardLength && m_Board[currentRow, currentCol] == valueToCheck)
        {
            counterOfSequence++;
            currentRow++;
            currentCol++;
        }
        currentRow = i_Row - 1; //now we want to count to the left-down
        currentCol = i_Col - 1;
        //count the player's coins in the specified row - from the given cell to the left-down
        while (currentRow >= 0 && currentCol>=0 && m_Board[currentRow, currentCol] == valueToCheck)
        {
            counterOfSequence++;
            currentRow--;
            currentCol--;
        }

        return (counterOfSequence >= 4);
    }


    private bool isThereWinningSecondaryDiagonal(int i_Player, int i_Row, int i_Col) //TODO: change type of player to enum
    {
        eBoardMarks valueToCheck = i_Player == 1 ? eBoardMarks.PlayerOne : eBoardMarks.PlayerTwo;
        int counterOfSequence = 0, currentRow = i_Row, currentCol = i_Col;

        //count the player's coins in the specified main diagonal - from the given cell to the left-up
        while (currentRow < m_BoardHeight && currentCol >= 0 && m_Board[currentRow, currentCol] == valueToCheck)
        {
            counterOfSequence++;
            currentRow++;
            currentCol--;
        }
        currentRow = i_Row - 1; //now we want to count to the right-down
        currentCol = i_Col + 1;
        //count the player's coins in the specified row - from the given cell to the right-down
        while (currentRow >= 0 && currentCol < m_BoardLength && m_Board[currentRow, currentCol] == valueToCheck)
        {
            counterOfSequence++;
            currentRow--;
            currentCol++;
        }

        return (counterOfSequence >= 4);
    }

    public bool BoardIsFull()
    {
        return m_BoardFreeSpace == 0;
    }

    public bool GameIsWon(int i_Player, int i_Row, int i_Col)
    {
        return (isThereWinningRow(i_Player, i_Row,i_Col) ||
            isThereWinningCol(i_Player, i_Row, i_Col) ||
            isThereWinningMainDiagonal(i_Player, i_Row, i_Col) ||
            isThereWinningSecondaryDiagonal(i_Player, i_Row, i_Col));
    }

    public enum eBoardMarks
    {
        Empty,
        PlayerOne,
        PlayerTwo,
    }
}

