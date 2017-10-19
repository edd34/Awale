using System;
using Data_Struct;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Awale_Console
{
    public class Board
    {
        #region variables

        public int seed = 64;
        public int[,] checkerBoard = new int[4, 8];
        public bool game_end = false;
        public int currentPlayer = 1;
        public int round = 1;
        public int token;
        public string previousMove = "";
        public bool hasCaptured = false;
        private bool endMoveNonEmpty = false;

        public bool askDirection = false;

        #endregion

        public Board()
        {
            seed = 64;
            game_end = false;
        }

        public void initialize(int[,] checkerBoard, int seed, int round, int currentPlayer)//in order to initialize the first round
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    checkerBoard[i, j] = 0;
                }
            }
            checkerBoard[1, 3] = 6;
            checkerBoard[1, 2] = 2;
            checkerBoard[1, 1] = 2;
            checkerBoard[2, 4] = 6;
            checkerBoard[2, 5] = 2;
            checkerBoard[2, 6] = 2;
            seed = 44;
            round = 1;
            currentPlayer = 1;
            Debug.Assert(seed == 44);
        }

        public void initialize_test(int[,] checkerBoard, int seed)//in order to initialize the first round
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    checkerBoard[i, j] = 0;
                }
            }

            checkerBoard[0, 0] = 1;
            checkerBoard[1, 0] = 2;
            //checkerBoard[1, 2] = 1;
            checkerBoard[1, 6] = 1;

            //checkerBoard[2, 0] = 1;
            checkerBoard[2, 2] = 1;
            checkerBoard[2, 6] = 1;
            checkerBoard[2, 7] = 2;
            checkerBoard[3, 6] = 4;

            checkerBoard[3, 1] = 3;

            seed = 44;
            this.round = 5;
            seed = 44;
            this.round = 5;
        }



        //these two function shows the board when they are called
        public void display(string currentPlayerName, step state)//main display fonction for each round
        {
            this.showBoard(currentPlayerName, round, seed, this.checkerBoard);
            this.showMiscInfo(state);
        }

        public void showBoard(string currentPlayerName, int round, int seed, int[,] checkerBoard)
        {
            Debug.Assert((checkerBoard.GetLength(1) == 4));
            Debug.Assert((checkerBoard.GetLength(2) == 8));

            Console.Clear();
            Console.WriteLine();
            Console.WriteLine();
            Console.Write("     A   B   C   D   E   F   G   H");
            Console.WriteLine();
            Console.WriteLine(" ----------------------------------------");

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (j == 0)
                        Console.Write(" " + (i + 1) + " | ");
                    else if (j == 9)
                        Console.Write("| " + (i + 1));
                    else
                        Console.Write(checkerBoard[i, j - 1].ToString("##00") + "  ");

                }
                if (i == 1)
                {
                    Console.WriteLine();
                    Console.WriteLine(" ----------------------------------------  seed = " + seed);
                }
                else
                {
                    Console.WriteLine();
                }


            }
            Console.WriteLine(" ----------------------------------------");
            Console.Write("     A   B   C   D   E   F   G   H");
            Console.WriteLine();
            Console.WriteLine();
            Console.Write("    Player : " + currentPlayerName + "  |  Round : " + round);
        }

        public void showMiscInfo(step state)
        {
            Console.Write("  |  Step : " + state.ToString());
            Console.WriteLine();
        }


        public void takeAllMySeeds(int[,] checkerBoard, int token, Coord currentCoord)
        {
            token = checkerBoard[currentCoord.X, currentCoord.Y];
            checkerBoard[currentCoord.X, currentCoord.Y] = 0;
        }

        public virtual void takeNumberMySeeds(int[,] checkerboard, int token, Coord currentChoice)
        {
            int ret = 0;
            bool valid;

            do
            {
                Console.WriteLine("\nEnter a number of seed between 0 and " + checkerBoard[2, 4]);
                valid = Int32.TryParse(Console.ReadLine(), out ret);
            } while(!valid);
            token = ret;
            checkerBoard[2, 4] -= ret;
        }

        public void takeAllOpponentsSeeds(int[,] checkerBoard, Coord currentCoord, int token)
        {
            int seedTaken = 0;
            Debug.Assert(currentCoord.X == 2);

            seedTaken = checkerBoard[1, currentCoord.Y];
            checkerBoard[1, currentCoord.Y] = 0;
            token = seedTaken;
        }


        //This function return true if a move is playable

        /*
         a move is not playable if : 
            you want to play the nyumba during your first move (position 2;4 and round 1 or 2
            you want to place a seed in an empty hole
         
         * */
        public bool isPlayable(int[,] checkerBoard, Coord currentCoord, int round)
        {
            if (isEmpty(checkerBoard, currentCoord))
            {
                return false;    
            }
            else if (isFirstRound(round))
            {
                if (isNyumba(checkerBoard, currentCoord) && !isCapturePossible(checkerBoard, currentCoord) ||
                    checkerBoard[currentCoord.X, currentCoord.Y] == 0)
                    return false;
                else
                {
                    return true;
                }
               




            }
            else
            {
                if (checkerBoard[currentCoord.X, currentCoord.Y] > 0)
                    return true;
                else
                    return true;
            }
        }


        public bool isEmpty(int[,] checkerBoard, Coord currentCoord)
        {
            Debug.Assert(checkerBoard[currentCoord.X, currentCoord.Y] >= 0);
            return checkerBoard[currentCoord.X, currentCoord.Y] == 0;
        }

        public bool isNyumba(int[,] checkerBoard, Coord currentCoord)
        {
            Debug.Assert(checkerBoard[currentCoord.X, currentCoord.Y] >= 0);
            return currentCoord.X == 2 && currentCoord.Y == 4;
        }

        public bool isFirstRound(int round)
        {
            Debug.Assert(round >= 0);
            return (round == 1 || round == 2);
        }



        //this function return the board and let the other player play his turn
        public void returnBoard(int[,] checkerBoard)
        {
            int[,] Board2 = new int[4, 8];
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    Board2[3 - i, 7 - j] = checkerBoard[i, j];
                }
            }

            checkerBoard = Board2;
        }

        public void placeSeed(int[,] checkerBoard, int seed, Coord CurrentCoord, int token)
        {
            
        }

        public bool isPossibleToCaptureSomewhere(int[,] checkerBoard)
        {
            for (int i = 0; i < 8; i++)
            {
                if (checkerBoard[2, i] > 0 && checkerBoard[1, i] > 0)
                    return true;
            }
            return false;
        }


        public bool isCoordValid(int[,] checkerBoard, int round, Coord currentCoord)
        {



            if (checkerBoard[currentCoord.X, currentCoord.Y] == 0)
            {
                return false;
            }
            else if (currentCoord.X == 0 || currentCoord.X == 1)
            {
                return false;
            }
            else if (round <= 2 && isNyumba(checkerBoard, currentCoord) && !isCapturePossible(checkerBoard, currentCoord))
            {
                return false; 
            }
            else
                return true;
        }


        /*
        public void capture(Player currentPlayer)
        {
            int seedCaptured = 0;
            this.hasCaptured = false;
            this.askDirection = false;


            if (this.isCapturePossible(currentPlayer.currentChoice))
            {
                seedCaptured = currentPlayer.takeAllOpponentsSeeds(this);

                if (this.isNyumba(currentPlayer.currentChoice) && !currentPlayer.isNyumbaSpreaded)
                {
                    if (this.askToSpreadNyumba(currentPlayer) == true)
                    {
                        currentPlayer.takeAllMySeeds(this);
                        currentPlayer.isNyumbaSpreaded = true;
                    }
                }
                else
                {
                    currentPlayer.takeAllMySeeds(this);
                }



                this.hasCaptured = true;
            }
            else
            {
                currentPlayer.takeAllMySeeds(this);
                this.hasCaptured = false;
            }

            if (this.hasCaptured == true)
            {   
                switch (this.isCorner(currentPlayer.currentChoice))
                {
                    case Player.Corner.Left:
                        currentPlayer.currentChoice.corner = Player.Corner.Left;
                        break;
                    case Player.Corner.Right:
                        currentPlayer.currentChoice.corner = Player.Corner.Right;
                        break;
                    case Player.Corner.NotCorner:
                        showBoard(currentPlayer.name, round, seed, checkerBoard);


                        Console.BackgroundColor = ConsoleColor.Red;
                        Console.ForegroundColor = ConsoleColor.White;


                        Console.WriteLine("\nYou have captured at [{0};{1}] !",
                            (char)((int)'A' + currentPlayer.currentChoice.coord.Y), currentPlayer.currentChoice.coord.X + 1);
                        Console.ResetColor();
                        currentPlayer.ReadCorner(this);
                        break;
                }

                if (seedCaptured > 1)
                {
                    showBoard(currentPlayer.name, round, seed, checkerBoard);
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("\nYou have captured at [{0};{1}] !",
                        (char)((int)'A' + currentPlayer.currentChoice.coord.Y), currentPlayer.currentChoice.coord.X + 1);
                    Console.ResetColor();
                    currentPlayer.ReadDirection(this);
                }
                else if (this.token > 0 && (this.isCorner(currentPlayer.currentChoice) == Player.Corner.Right)
                         || this.isCorner(currentPlayer.currentChoice) == Player.Corner.Left || token > 0)
                {
                    currentPlayer.ReadDirection(this);
                }
                else if (seedCaptured == 1 &&
                         this.checkerBoard[currentPlayer.currentChoice.coord.X, currentPlayer.currentChoice.coord.Y] > 0
                         && this.checkerBoard[currentPlayer.currentChoice.coord.X - 1, currentPlayer.currentChoice.coord.Y] > 0)
                {
                    currentPlayer.takeAllMySeeds(this);
                    currentPlayer.takeAllOpponentsSeeds(this);
                    showBoard(currentPlayer.name, round, seed, checkerBoard);
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("\nYou have captured at [{0};{1}] !",
                        (char)((int)'A' + currentPlayer.currentChoice.coord.Y), currentPlayer.currentChoice.coord.X + 1);
                    Console.ResetColor();
                    currentPlayer.ReadDirection(this);

                }
                else
                    currentPlayer.currentChoice.direction = Player.Direction.ClockWise;

                if (currentPlayer.currentChoice.corner == Player.Corner.Left)
                {
                    if (currentPlayer.currentChoice.direction == Player.Direction.ClockWise)
                    {
                        currentPlayer.currentChoice.coord.X = 3;
                        currentPlayer.currentChoice.coord.Y = 0;
                    }
                    else if (currentPlayer.currentChoice.direction == Player.Direction.CounterClockWise)
                    {
                        currentPlayer.currentChoice.coord.X = 2;
                        currentPlayer.currentChoice.coord.Y = 1;
                    }

                }
                else if (currentPlayer.currentChoice.corner == Player.Corner.Right)
                {
                    if (currentPlayer.currentChoice.direction == Player.Direction.ClockWise)
                    {
                        currentPlayer.currentChoice.coord.X = 2;
                        currentPlayer.currentChoice.coord.Y = 6;
                    }
                    else if (currentPlayer.currentChoice.direction == Player.Direction.CounterClockWise)
                    {
                        currentPlayer.currentChoice.coord.X = 3;
                        currentPlayer.currentChoice.coord.Y = 7;
                    }

                } 

                askDirection = true;

            }
            else if (this.hasCaptured == false)
            {
                if (this.endMoveNonEmpty == true)
                {
                    showBoard(currentPlayer.name, round, seed, checkerBoard);
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("\nYou have earned a move because your ended in an non empty case !");
                    Console.WriteLine("\nYou have ended your move at [{0};{1}] !",
                        (char)((int)'A' + currentPlayer.currentChoice.coord.Y), currentPlayer.currentChoice.coord.X + 1);
                    Console.ResetColor();
                    this.endMoveNonEmpty = false;
                }
                if (this.askDirection == true || this.hasCaptured == false)
                {
                    currentPlayer.ReadDirection(this);
                    Console.WriteLine("Hello");
                    this.askDirection = false;
                }
                
                currentPlayer.takeAllMySeeds(this);

            }
        }
*/




        /*
        public void disseminate(Player currentPlayer)
        {
            if (this.token > 1)
            {
                this.nextChoice(currentPlayer);
                this.token--;
                this.checkerBoard[currentPlayer.currentChoice.coord.X, currentPlayer.currentChoice.coord.Y]++;
            }
            else if (this.token == 1)
            {
                this.nextChoice(currentPlayer);
                this.token--;
                this.checkerBoard[currentPlayer.currentChoice.coord.X, currentPlayer.currentChoice.coord.Y]++;
                if (this.checkerBoard[currentPlayer.currentChoice.coord.X, currentPlayer.currentChoice.coord.Y] > 1)
                {
                        
                    if (this.isNyumba(currentPlayer.currentChoice) && !currentPlayer.isNyumbaSpreaded && currentPlayer.canSpreadNyumba && !this.isCapturePossible(currentPlayer.currentChoice))
                    {
                        currentPlayer.takeNumberMySeeds(this);
                        if (this.token > 0)
                        {
                            currentPlayer.ReadDirection(this);
                            currentPlayer.isNyumbaSpreaded = true;
                            currentPlayer.canSpreadNyumba = false;
                        }
                            
                    }
                    else
                    {
                        this.capture(currentPlayer);
                    }
                            
                }
            }

            if (this.token > 0)
            {
                this.endMoveNonEmpty = true;
                this.disseminate(currentPlayer);
                this.endMoveNonEmpty = false;
            }

        }
*/


               
        public bool isCapturePossible(int[,] checkerBoard, Coord currentCoord)
        {
            if (currentCoord.X == 2)
            {
                if (checkerBoard[currentCoord.X - 1, currentCoord.Y] > 0)
                {
                    return true;
                }
                else
                    return false;
            }
            else
                return false;
        }

        public void nextChoice(Coord currentCoord, Direction currentDirection)
        {
            if (currentDirection == Direction.ClockWise)
            {
                if (currentCoord.X == 2)
                {

                    if (currentCoord.Y == 7)
                    {
                        currentCoord.X = 3;
                    }
                    else
                    {
                        currentCoord.Y++;
                    }
                }
                else if (currentCoord.X == 3)
                {

                    if (currentCoord.Y == 0)
                    {   
                        currentCoord.X = 2;

                    }
                    else
                    {
                        currentCoord.Y--;
                    }
                }
            }
            else if (currentDirection == Direction.CounterClockWise)
            {
                if (currentCoord.X == 2)
                {

                    if (currentCoord.Y == 0)
                    {
                        currentCoord.X = 3;
                    }
                    else
                    {
                        currentCoord.Y--;
                    }
                }
                else if (currentCoord.X == 3)
                {

                    if (currentCoord.Y == 7)
                    {   
                        currentCoord.X = 2;
                    }
                    else
                    {
                        currentCoord.Y++;

                    }

                }
            }

        }

        public void placeSeed(Coord currentCoord)
        {
          
            seed--;
            checkerBoard[currentCoord.X, currentCoord.Y]++;
            
        }


        public Corner isCorner(Coord currentCoord)
        {
            if (currentCoord.X == 2)
            {
                if (currentCoord.Y == 0 || currentCoord.Y == 1)
                    return Corner.Left;
                else if (currentCoord.Y == 6 || currentCoord.Y == 7)
                    return Corner.Right;
                else
                    return Corner.NotCorner;
            }
            else
                return Corner.NotCorner;
        }

        public bool askToSpreadNyumba(Player currentPlayer)
        {
            bool value = false;
            ConsoleKey keyPressed;
            if (currentPlayer.isIA == false)
            {
                do
                {
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("\nThis is your nyumba, do you want to spread it ? (Y/N)");
                    Console.ResetColor();
                    keyPressed = Console.ReadKey(false).Key;
                    if (keyPressed == ConsoleKey.Y)
                        value = true;
                    else if (keyPressed == ConsoleKey.N)
                        value = false;
                } while(!(keyPressed == ConsoleKey.Y || keyPressed == ConsoleKey.N));
            }
            else
            {
                value = (new Random().Next() % 100 < 7) ? true : false;
            }
            return value;
        }


    }
}

/*      public void capture_disseminate(Player currentPlayer, Board board)
        {
            this.hasCaptured = false;
            if (board.isCapturePossible (currentPlayer.currentChoice)) 
            {
                currentPlayer.takeAllOpponentsSeeds (board, currentPlayer.currentChoice);

                if (board.isNyumba (currentPlayer.currentChoice)) 
                {
                    if (board.askToSpreadNyumba (board, currentPlayer) == true) 
                    {
                        currentPlayer.takeAllMySeeds (board, currentPlayer);
                    }
                } 



                board.hasCaptured = true;
            } 
            else 
            {
                currentPlayer.takeAllMySeeds (board, currentPlayer);
                board.hasCaptured = false;
            }

            if (board.hasCaptured == true) 
            {   
                currentPlayer.ReadCorner (board);
                currentPlayer.ReadDirection(board);
                if (currentPlayer.currentChoice.corner == Player.Corner.Left) 
                {
                    if (currentPlayer.currentChoice.direction == Player.Direction.ClockWise) 
                    {
                        currentPlayer.currentChoice.coord.X = 3;
                        currentPlayer.currentChoice.coord.Y = 0;
                    } 
                    else if (currentPlayer.currentChoice.direction == Player.Direction.CounterClockWise) 
                    {
                        currentPlayer.currentChoice.coord.X = 2;
                        currentPlayer.currentChoice.coord.Y = 1;
                    }

                } 
                else if (currentPlayer.currentChoice.corner == Player.Corner.Right) 
                {
                    if (currentPlayer.currentChoice.direction == Player.Direction.ClockWise) 
                    {
                        currentPlayer.currentChoice.coord.X = 2;
                        currentPlayer.currentChoice.coord.Y = 6;
                    } 
                    else if (currentPlayer.currentChoice.direction == Player.Direction.CounterClockWise) 
                    {
                        currentPlayer.currentChoice.coord.X = 3;
                        currentPlayer.currentChoice.coord.Y = 7;
                    }

                } 
            }
            else if (board.hasCaptured == false) 
            {
               
            }
        }*/


/*
else 
{
    if (board.isNyumba(currentPlayer.currentChoice) && currentPlayer.NyumbaSpreaded == false)
        /* do
                    {
                        Console.WriteLine("How many seed do you want to spread ?");
                        try
                        {
                            read = Convert.ToInt32(Console.ReadLine());

                            if (read > 0
                            && read <= this.checkerBoard[currentPlayer.currentChoice.coord.X, currentPlayer.currentChoice.coord.Y])
                                isEntryOk = true;
                            else
                                isEntryOk = false;
                        }
                        catch
                        {
                            isEntryOk = false;
                        }
                    } while(!isEntryOk);
                
        currentPlayer.takeAllMySeeds (board, currentPlayer);
    board.hasCaptured = false;

}
*/