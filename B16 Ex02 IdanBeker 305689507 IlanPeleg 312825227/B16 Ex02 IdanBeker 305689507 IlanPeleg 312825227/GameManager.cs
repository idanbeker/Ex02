
using System;

class GameManager
{
    private int m_PlayerOneScore;
    private int m_PlayerTwoScore;
    private Board m_GameBoard;
    private eGameMode m_GameMode;


    public GameManager(int i_NumberOfPlayers, int i_BoardHeight, int i_BoardLength)
    {
        this.m_PlayerOneScore = 0;
        this.m_PlayerTwoScore = 0;
        this.m_GameBoard = new Board(i_BoardHeight, i_BoardLength);
        this.m_GameMode = i_NumberOfPlayers == 1 ? eGameMode.SinglePlayer : eGameMode.MultyPlayer;
    }

    public void StartGame()
    {
        //TODO Implemented for testing.
        int playerIndex = 1;
        int currentPlayerInput;

        while (!m_GameBoard.BoardIsFull())
        {
            if (playerIndex == 1)
            {
                currentPlayerInput = getPlayerInput();
                m_GameBoard.EnterMove(currentPlayerInput, playerIndex);
                playerIndex++;
            }
            else
            {
                currentPlayerInput = getPlayerInput();
                m_GameBoard.EnterMove(currentPlayerInput, playerIndex);
                playerIndex--;
            }
            System.Console.WriteLine(m_GameBoard.ToString());

        }
    }

    private int getPlayerInput()
    {
        //TODO Implemented for testing.
        int parsedInput;
        System.Console.Write("Enter input bitch");
        string userInput = System.Console.ReadLine();
        int.TryParse(userInput, out parsedInput);
        return parsedInput;
    }

    public enum eGameMode
    {
        MultyPlayer,
        SinglePlayer,
    }


}
