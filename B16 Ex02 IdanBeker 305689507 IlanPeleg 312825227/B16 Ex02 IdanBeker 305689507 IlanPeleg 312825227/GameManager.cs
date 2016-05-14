
using System;

class GameManager
{
    //TODO: organize the startgame func in functions
    private int m_PlayerOneScore;
    private int m_PlayerTwoScore;
    private Board m_GameBoard;
    private eGameMode m_GameMode;
    private int m_BoardHeight;
    private int m_BoardLength;


    public GameManager(int i_NumberOfPlayers, int i_BoardHeight, int i_BoardLength)
    {
        this.m_PlayerOneScore = 0;
        this.m_PlayerTwoScore = 0;
        m_BoardHeight = i_BoardHeight;
        m_BoardLength = i_BoardLength;
        this.m_GameBoard = new Board(i_BoardHeight, i_BoardLength);
        this.m_GameMode = i_NumberOfPlayers == 1 ? eGameMode.SinglePlayer : eGameMode.MultyPlayer;
    }

    public void StartGame()
    {
        int playerIndex = 1;
        
        while (true)
        {
            if (playerIndex == 1)
            {
                manageTurn(1);
                playerIndex++;
            }
            else
            {
                if (m_GameMode == eGameMode.MultyPlayer)
                {
                    manageTurn(2);
                }
                else //the game is single-player. the computer chooses randomly empty column
                {
                    managComputersTurn();
                }
                playerIndex--;
            }
            System.Console.WriteLine(m_GameBoard.ToString());
            //check if the board is full-meaning we have a tie
            handleTieCase();
        }

    }

    private void handleTieCase()
    {
        if (m_GameBoard.BoardIsFull())
        {
            System.Console.WriteLine("we have a tie! ");
            System.Console.WriteLine("player one's score is: " + m_PlayerOneScore + " palyer two's score is: " + m_PlayerTwoScore);
            newGame();
        }
    }

    private void managComputersTurn()
    {
        bool isWon;
        int playerIndex = 2;
        Random computersChoiceRnd = new Random();
    
        //chooses a random number between 0-(m_BoardLength-1)
        int computersChoiceCol = computersChoiceRnd.Next(m_BoardLength - 1);
        System.Console.WriteLine("the computer's move: ");
        while (!m_GameBoard.EnterMove(computersChoiceCol, playerIndex, out isWon))//continue choosing random number while current one is invalid
        {
            computersChoiceCol = computersChoiceRnd.Next(m_BoardLength - 1);
        }
        if (isWon)
        {
            m_PlayerTwoScore++;
            System.Console.WriteLine("the computer has won! ");
            System.Console.WriteLine("player one's score is: " + m_PlayerOneScore + " computer's score is: " + m_PlayerTwoScore);
            newGame();
        }

    }
    private void manageTurn(int i_playerIndex)
    {
        //  int playerIndex = 1;
        int currentPlayerInput;
        bool isWon;
        currentPlayerInput = getPlayerInput(i_playerIndex);
        if (currentPlayerInput != -1)//indicates whether the input was "Q" or not
        {
            //continue getting input while current input is invalid
            while (!m_GameBoard.EnterMove(currentPlayerInput, i_playerIndex, out isWon))//isWon recieves if the last move was a winning move
            {
                System.Console.WriteLine("this column is full ");
                currentPlayerInput = getPlayerInput(i_playerIndex);
                if (currentPlayerInput == -1)//means that he enters "Q"
                {
                    break;
                }
            }

            //we have a move- lets check if its a winning move!
            if (isWon)
            {
                if (i_playerIndex == 1)
                {
                    m_PlayerOneScore++;
                    System.Console.WriteLine("player one won! ");
                    System.Console.WriteLine("player one's score is: " + m_PlayerOneScore + " palyer two's score is: " + m_PlayerTwoScore);
                    newGame();
                }
                else
                {
                    m_PlayerTwoScore++;
                    System.Console.WriteLine("player two won! ");
                    System.Console.WriteLine("player one's score is: " + m_PlayerOneScore + " palyer two's score is: " + m_PlayerTwoScore);
                    newGame();
                }
            }
        }
    }



    //ask the user whether he wants to keep playing.
    //if not- exit the game
    //if yes- initialize the board
    private void newGame()
    {
        System.Console.WriteLine(m_GameBoard.ToString());//show the last-updated board
        bool wantsAnotherGame = false;
        string userInput;
        do
        {
            System.Console.Write("Do you want another game? press y or n ");
            userInput = System.Console.ReadLine();
            if (userInput.Equals("n"))
            {
                wantsAnotherGame = false;
            }
            else if (userInput.Equals("y"))
            {
                wantsAnotherGame = true;
            }
        } while (!userInput.Equals("y") && !userInput.Equals("n"));
        if (!wantsAnotherGame)
        {
            Environment.Exit(0);
        }
        this.m_GameBoard = new Board(m_BoardHeight, m_BoardLength);
    }



    private int getPlayerInput(int i_PlayerIndex)
    {
        bool isQEntered = false;
        bool validParse = false;
        bool validNumber = false;

        int parsedInput = -1;
        do
        {
            System.Console.Write("Enter the number of the cell you want to enter a coin" + Environment.NewLine);
            string userInput = System.Console.ReadLine();
            isQEntered = QEntered(userInput, i_PlayerIndex); //checks if Q was entered and handled the case in which it was
            if (isQEntered)
            {
                newGame();
                break;
            }
            validParse = int.TryParse(userInput, out parsedInput);
            if (validParse)
            {
                validNumber = isValidColumnInput(parsedInput);
            }
            else
            {
                System.Console.Write("you should enter only numbers" + Environment.NewLine);
            }
        } while (!validParse || !validNumber);

        return parsedInput;
    }

    private bool QEntered(string i_Input, int i_PlayerIndex)
    {
        bool isQEntered = (i_Input.Equals("Q"));
        if (isQEntered)
        {
            if (i_PlayerIndex == 1)
            {
                m_PlayerTwoScore++;
                System.Console.WriteLine("player one retired! player two won!");
                System.Console.WriteLine("player one's score is: " + m_PlayerOneScore + " palyer two's score is: " + m_PlayerTwoScore);
            }
            else
            {
                m_PlayerOneScore++;
                System.Console.WriteLine("player two retired! player one won!");
                System.Console.WriteLine("player one's score is: " + m_PlayerOneScore + " palyer two's score is: " + m_PlayerTwoScore);
            }
        }
        return isQEntered;
    }

    private bool isValidColumnInput(int i_Column)
    {
        bool isValidColumnInput = false;
        if (i_Column < m_BoardLength && i_Column >= 0)
        {
            isValidColumnInput = true;
        }
        else
        {
            isValidColumnInput = false;
            System.Console.Write("your number is not in the right range" + Environment.NewLine);
        }

        return isValidColumnInput;
    }

    public enum eGameMode
    {
        MultyPlayer,
        SinglePlayer,
    }


}
