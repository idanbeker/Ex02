
using System;
using Ex02.ConsoleUtils;


namespace B16_Ex02_IdanBeker_305689507_IlanPeleg_312825227
{
    //TODO: print informative msgs when the input is invalid
    //TODO: in the msg of asking the player to move, print which player we refers to
    //TODO: print the board correctly
    //TODO: think whether the program class shouldnt be shorter (specifically the main function)
    class GameManager
    {
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

        public Board getBoard()
        {
            return m_GameBoard;
        }

        public void StartGame()
        {
            int playerIndex = 1;//first playr starts the game

            while (true)
            {
                if (playerIndex == 1)
                {
                    manageTurn(playerIndex);
                    playerIndex++;
                    // Clear console
                    Ex02.ConsoleUtils.Screen.Clear();
                }
                else
                {
                    if (m_GameMode == eGameMode.MultyPlayer)
                    {
                        manageTurn(playerIndex);
                        // Clear console
                        Ex02.ConsoleUtils.Screen.Clear();
                    }
                    else //the game is single-player. the computer chooses randomly empty column
                    {
                        managComputersTurn();
                    }
                    playerIndex--;
                }

                System.Console.WriteLine(m_GameBoard.ToString());//print the board at teh end of the move
                //check if the board is full-meaning we have a tie
                handleTieCase();
            }

        }

        private void handleTieCase()
        {
            if (m_GameBoard.BoardIsFull())
            {
                //Clear console
                Ex02.ConsoleUtils.Screen.Clear();
                System.Console.WriteLine("we have a tie! ");
                System.Console.WriteLine("player one's score is: " + m_PlayerOneScore + " palyer two's score is: " + m_PlayerTwoScore);
                newGame();//start a new game if it is a tie(or let the player choose if he wants to exit)
            }
        }

        private void managComputersTurn()
        {
            bool isWon;//indicates if the last move was a winning move
            int playerIndex = 2;
            Random computersChoiceRnd = new Random();

            //chooses a random number between 0-(m_BoardLength-1)
            int computersChoiceCol = computersChoiceRnd.Next(m_BoardLength - 1);

            System.Console.WriteLine("the computer's move: ");
            //continue choosing random number while current one is invalid
            while (!m_GameBoard.EnterMove(computersChoiceCol, playerIndex, out isWon))
            {
                computersChoiceCol = computersChoiceRnd.Next(m_BoardLength - 1);
            }

            //if the computer won
            if (isWon)
            {
                m_PlayerTwoScore++;
                //Clear console
                Ex02.ConsoleUtils.Screen.Clear();
                System.Console.WriteLine("the computer has won! ");
                System.Console.WriteLine("player one's score is: " + m_PlayerOneScore + " computer's score is: " + m_PlayerTwoScore);
                newGame();
            }

        }

        //this function handles the logic of a player's move
        private void manageTurn(int i_playerIndex)
        {
            int currentPlayerInput;
            bool isWon;
            //this function returns the players choice of move
            currentPlayerInput = getPlayerInput(i_playerIndex);

            //-1 indicates that the input was "Q"
            if (currentPlayerInput != -1)
            {
                //continue getting input while current input is invalid
                while (!m_GameBoard.EnterMove(currentPlayerInput, i_playerIndex, out isWon))//isWon indicates whether the last move was a winning move
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
                        //Clear console
                        Ex02.ConsoleUtils.Screen.Clear();
                        System.Console.WriteLine("player one won! ");
                        System.Console.WriteLine("player one's score is: " + m_PlayerOneScore + " palyer two's score is: " + m_PlayerTwoScore);
                        newGame();
                    }
                    else
                    {
                        m_PlayerTwoScore++;
                        //Clear console
                        Ex02.ConsoleUtils.Screen.Clear();
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
            {   //keep asking for valid input if the current input isnt valid
                System.Console.Write("Do you want another game? press y or n ");
                userInput = System.Console.ReadLine();
                if (userInput.Equals("n"))
                {
                    wantsAnotherGame = false;
                }
                else if (userInput.Equals("y"))
                {
                    //Clear console if we want a new game
                    Ex02.ConsoleUtils.Screen.Clear();
                    wantsAnotherGame = true;
                }
            } while (!userInput.Equals("y") && !userInput.Equals("n"));
            //quit the game if the user has chosen "n"
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

            int parsedInput = -1;//if parseInput will not change, it will indicate that "Q" was pressed
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
                //check if the input is valid
                validParse = int.TryParse(userInput, out parsedInput);
                if (validParse)
                {
                    validNumber = isValidColumnInput(parsedInput);//if the number is in the right range
                }
                else
                {
                    System.Console.Write("you should enter only numbers" + Environment.NewLine);
                }
            } while (!validParse || !validNumber);

            return parsedInput;
        }



        //this function handles the case in which "Q" was entered and we need to end the current game
        private bool QEntered(string i_Input, int i_PlayerIndex)
        {
            bool isQEntered = (i_Input.Equals("Q"));
            if (isQEntered)
            {//we want to give a point to the enemy of the "Q presser"
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

        //check if the input was in the right range of the board
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
}