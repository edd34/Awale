using System;

namespace Awale_Console
{
    public class Board
    {
        public int seed = 64;
        public int[,] checkerBoard = new int[4, 8];
        public bool game_end = false;
        public int currentPlayer = 1;
        public int round = 1;
        public int token;
        public string previousMove = "";
        public bool hasCaptured = false;



        public Board()
        {
            seed = 64;
            game_end = false;
        }

        public void initialize()//in order to initialize the first round
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
        }



        public void display(Player currentPlayer, GameManager.step state)//main display fonction for each round
        {

            //Console.Clear ();
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
                        Console.Write(this.checkerBoard[i, j - 1].ToString("##00") + "  ");

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
            Console.WriteLine("    Player : " + currentPlayer.name + "  |  Round : " + round + "  |  Step : " + state);
            if (round == 1)
                Console.WriteLine();
            else
                Console.WriteLine("Previous move = " + this.previousMove);


        }

        public bool isPlayable(Player.Choice currentChoice)
        {

            bool ret;
            if (this.checkerBoard[currentChoice.coord.X, currentChoice.coord.Y] == 0)
                ret = false;
            else if (round == 1 || round == 2)
            {
                if (currentChoice.coord.X == 2 && currentChoice.coord.Y == 4 ||
                this.checkerBoard[currentChoice.coord.X, currentChoice.coord.Y] == 0)
                {
                    ret = false;
                }
                else
                    ret = true;

            }
            else
            {
                if (this.checkerBoard[currentChoice.coord.X, currentChoice.coord.Y] > 0)
                    ret = true;
                else
                    ret = false;
            }
            return ret;
        }

        public void returnBoard()
        {
            int[,] Board2 = new int[4, 8];
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    Board2[3 - i, 7 - j] = this.checkerBoard[i, j];
                }
            }

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    this.checkerBoard[i, j] = Board2[i, j];
                }
            }
        }

        public void capture(Player currentPlayer, Board board)
        {
            this.hasCaptured = false;
            if (board.isCapturePossible (currentPlayer.currentChoice)) 
            {
                currentPlayer.takeAllOpponentsSeeds (board, currentPlayer.currentChoice);

                if (board.isNyumba (currentPlayer.currentChoice)) 
                {
                    if (board.askToSpreadNyumba (board, currentPlayer) == true) 
                    {
                        currentPlayer.takeAllMySeeds (board, currentPlayer.currentChoice);
                    }
                } 



                board.hasCaptured = true;
            } 
            else 
            {
                currentPlayer.takeAllMySeeds (board, currentPlayer.currentChoice);
                board.hasCaptured = false;
            }

            if (board.hasCaptured == true) 
            {   
                currentPlayer.ReadCorner (board);
                currentPlayer.ReadDirection(board);
                Console.WriteLine("Hello");
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
                Console.WriteLine("Hello2");
                currentPlayer.ReadDirection(board);
            }
        }

        public void capture_disseminate(Player currentPlayer, Board board)
        {
            this.hasCaptured = false;
            if (board.isCapturePossible (currentPlayer.currentChoice)) 
            {
                currentPlayer.takeAllOpponentsSeeds (board, currentPlayer.currentChoice);

                if (board.isNyumba (currentPlayer.currentChoice)) 
                {
                    if (board.askToSpreadNyumba (board, currentPlayer) == true) 
                    {
                        currentPlayer.takeAllMySeeds (board, currentPlayer.currentChoice);
                    }
                } 



                board.hasCaptured = true;
            } 
            else 
            {
                currentPlayer.takeAllMySeeds (board, currentPlayer.currentChoice);
                board.hasCaptured = false;
            }

            if (board.hasCaptured == true) 
            {   
                currentPlayer.ReadCorner (board);
                currentPlayer.ReadDirection(board);
                Console.WriteLine("Hello");
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
        }

        public bool isCapturePossible(Player.Choice currentChoice)
        {
            if (currentChoice.coord.X == 2)
            {
                if (this.checkerBoard[currentChoice.coord.X - 1, currentChoice.coord.Y] > 0)
                {
                    return true;
                }
                else
                    return false;
            }
            else
                return false;
        }


        public void disseminate(Player.Choice currentChoice, Player currentPlayer)
        {


            while (this.token > 0)
            {
                
                if (currentChoice.direction == Player.Direction.ClockWise)
                {
                    if (currentChoice.coord.X == 2)
                    {

                        if (currentChoice.coord.Y == 7)
                        {
                            currentChoice.coord.X = 3;
                            this.checkerBoard[currentChoice.coord.X, currentChoice.coord.Y]++;
                            this.token--;
                        }
                        else
                        {
                            currentChoice.coord.Y++;
                            this.checkerBoard[currentChoice.coord.X, currentChoice.coord.Y]++;
                            this.token--;
                        }
                    }
                    else if (currentChoice.coord.X == 3)
                    {

                        if (currentChoice.coord.Y == 0)
                        {	
                            currentChoice.coord.X = 2;
                            this.checkerBoard[currentChoice.coord.X, currentChoice.coord.Y]++;
                            this.token--;

                        }
                        else
                        {
                            currentChoice.coord.Y--;
                            this.checkerBoard[currentChoice.coord.X, currentChoice.coord.Y]++;
                            this.token--;
                        }
                    }
                }
                else if (currentChoice.direction == Player.Direction.CounterClockWise)
                {
                    if (currentChoice.coord.X == 2)
                    {

                        if (currentChoice.coord.Y == 0)
                        {
                            currentChoice.coord.X = 3;
                            this.checkerBoard[currentChoice.coord.X, currentChoice.coord.Y]++;
                            this.token--;
                        }
                        else
                        {
                            currentChoice.coord.Y--;
                            this.checkerBoard[currentChoice.coord.X, currentChoice.coord.Y]++;
                            this.token--;
                        }
                    }
                    else if (currentChoice.coord.X == 3)
                    {

                        if (currentChoice.coord.Y == 7)
                        {	
                            currentChoice.coord.X = 2;
                            this.checkerBoard[currentChoice.coord.X, currentChoice.coord.Y]++;
                            this.token--;
                        }
                        else
                        {
                            currentChoice.coord.Y++;
                            this.checkerBoard[currentChoice.coord.X, currentChoice.coord.Y]++;
                            token--;

                        }

                    }
                }
                if(this.token == 1)
                {
                    if(this.checkerBoard[currentChoice.coord.X, currentChoice.coord.Y]>0)
                    {
                        if(this.isCapturePossible(currentChoice))
                        {
                            this.capture_disseminate(currentPlayer, this);
                        }
                        else
                        {
                            currentPlayer.takeAllMySeeds(this, currentChoice);
                        }
                    }
                }
               
            }

        }

        public void move(GameManager gameManager, Player.Choice currentChoice)
        {
            if (gameManager.state == GameManager.step.UMUNIA)
            {
                this.seed--;
                this.checkerBoard[currentChoice.coord.X, currentChoice.coord.Y]++;
            }
            else
            {

            }
        }

        public bool isNyumba(Player.Choice currentChoice)
        {
            return currentChoice.coord.X == 2 && currentChoice.coord.Y == 4;
        }

        public bool isCorner(Player.Choice currentChoice) 
        {
            if (currentChoice.coord.X == 2)
            {
                if (currentChoice.coord.Y == 0 || currentChoice.coord.Y == 1 || currentChoice.coord.Y == 6 || currentChoice.coord.Y == 7)
                    return true;
                else
                    return false;
            }
            else
                return false;
        }

        public bool askToSpreadNyumba(Board board, Player currentPlayer)
        {
            bool value = false;
            ConsoleKey keyPressed;
            do
            {
                Console.WriteLine("\nThis is the nyumba, do you want tp spread it ? (Y/N)");
                keyPressed = Console.ReadKey(false).Key;
                if (keyPressed == ConsoleKey.Y)
                    value = true;
                else if (keyPressed == ConsoleKey.N)
                    value = false;
            } while(!(keyPressed == ConsoleKey.Y || keyPressed == ConsoleKey.N));
            return value;
        }


    }
}

