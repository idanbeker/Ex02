using System;


namespace B16_Ex02_IdanBeker_305689507_IlanPeleg_312825227

    //TODO: check validity of input
{
    class Program
    {
        private static readonly int sr_MinimumBoardSize = 4;
        private static readonly int sr_MaximumBoardSize = 8;
        private static readonly string sr_RangeMessage = "Range: (" + sr_MinimumBoardSize + "-" + sr_MaximumBoardSize + ")";
        private static readonly string sr_WelcomeMessage = "Welcome to X-Drix \nGame by: Idan Beker and Ilan Peleg \nEnjoy!";
        private static readonly string sr_ChooseHeightMessage = "Please choose the board height " + sr_RangeMessage;
        private static readonly string sr_DimensionInvalidInputMessage = "Invalid input!\nPlease enter an integer";
        private static readonly string sr_ChooseLengthMessage = "Please choose the board length " + sr_RangeMessage;
        private static readonly string sr_ChooseNumberOfPlayerMessage = "Please chose the number of player (1 for single player, 2 for two players)";


        public static void Main()
        {
            int gameBoardHeight;
            int gameBoardLength;
            int numOfPlayers; //TODO: change it from int
            showWelcomeMessage();
            getNumberOfPlayers(out numOfPlayers);
            getGameBoardDimensions(out gameBoardHeight, out gameBoardLength);
            GameManager gm = new GameManager(numOfPlayers, gameBoardHeight, gameBoardLength);
            gm.StartGame();

            //System.Console.WriteLine("num of players:" + numOfPlayers + "dimensions" + gameBoardHeight + " " + gameBoardLength);
        }

        private static void getNumberOfPlayers(out int i_NumberOfPlayers)
        {
            bool checkInput = false;
            string userInput = "";
            
            do
            {
                System.Console.WriteLine(sr_ChooseNumberOfPlayerMessage);
                userInput = System.Console.ReadLine();
                checkInput = int.TryParse(userInput, out i_NumberOfPlayers);
            } while (!checkInput || i_NumberOfPlayers < 1 || i_NumberOfPlayers > 2);
        }

        /*
        *   Gets board dimension from the user.
        *  
                Param:
        *       i_GameBoardHeight - Height of desired board.
        *       i_GameBoardLength - Length of desired board.
        */

        private static void getGameBoardDimensions(out int i_GameBoardHeight, out int i_GameBoardLength)
        {
            //Assumes correct input untill proven other wise.
            bool validInputHeight = true;
            bool validInputLength = true;
            bool confirmChoice = true;
            string userInput = "";

            do
            {
                //Gets Height Dimension
                do
                {
                    if (!validInputHeight )
                    {
                        System.Console.WriteLine(sr_DimensionInvalidInputMessage);
                    }
                    System.Console.WriteLine(sr_ChooseHeightMessage);
                    userInput = System.Console.ReadLine();
                    validInputHeight = int.TryParse(userInput, out i_GameBoardHeight);
                } while (!validInputHeight || !validDimension(i_GameBoardHeight));

                //Gets Length Dimension
                do
                {
                    if (!validInputLength )
                    {
                        System.Console.WriteLine(sr_DimensionInvalidInputMessage);
                    }
                    System.Console.WriteLine(sr_ChooseLengthMessage);
                    userInput = System.Console.ReadLine();
                    validInputLength = int.TryParse(userInput, out i_GameBoardLength);
                } while (!validInputLength || !validDimension(i_GameBoardLength));

                //Confirm choice.
                do
                {
                    System.Console.Write("Your of board is {0} X {1}\nPress y to confirm or n to cancel\n", i_GameBoardHeight, i_GameBoardLength);
                    userInput = System.Console.ReadLine();
                    if(userInput.Equals("n"))
                    {
                        confirmChoice = false;
                    }
                    else if(userInput.Equals("y"))
                    {
                        confirmChoice = true;
                    }
                } while (!userInput.Equals("y") && !userInput.Equals("n"));
             
            } while (!confirmChoice);

        }

       /*
       *    Checks dimension of a given input
       *  
       *       Param:
       *       i_GameBoardDimension - Height of desired board.
       *    
       *       Returns :
       *       true if valid, false otherwise.   
       */
        private static bool validDimension(int i_GameBoardDimension)
        {
            return (i_GameBoardDimension >= sr_MinimumBoardSize) && (i_GameBoardDimension <= sr_MaximumBoardSize);
        }
       
       /*
       *   Prints the welcome message to the screen.
       */
        private static void showWelcomeMessage()
        {
            System.Console.WriteLine(sr_WelcomeMessage);
        }
    }
}
