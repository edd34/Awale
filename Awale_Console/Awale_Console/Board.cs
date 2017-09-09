using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

        public bool askDirectionEarnedMove = false;

        #endregion

        #region Initialization

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

        public void initialize_test()//in order to initialize the first round
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
            checkerBoard[1, 2] = 2;
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

        #endregion

        #region Display fonctions

        public void display(Player currentPlayer, GameManager.step state)//main display fonction for each round
        {
            
            this.showBoard(currentPlayer);
            this.showMiscInfo(state);
        }

        public void showBoard(Player currentPlayer)
        {
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
            Console.Write("    Player : " + currentPlayer.name + "  |  Round : " + round);
        }

        public void showMiscInfo(GameManager.step state)
        {
            Console.Write("  |  Step : " + state);
            Console.WriteLine();
        }

        #endregion

        public override string ToString()
        {
            string value = "";
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (i == 3 && j == 7)
                        value += checkerBoard[i, j].ToString();
                    else
                        value += checkerBoard[i, j].ToString() + ",";
                }
            }

            Console.WriteLine(value);
            return value;
        }

        public void ToBoard(string s)
        {
            int cpt = 0;
            //int count = 1000;
            string[] tmp = new string[10000];
            tmp = s.Split(',');
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    Int32.TryParse(tmp[cpt].ToString(), out this.checkerBoard[i, j]);
                    cpt++;
                }
            }
        }

        public bool isPlayable(Player.Choice currentChoice)
        {

            bool ret;
            if (this.checkerBoard[currentChoice.coord.X, currentChoice.coord.Y] == 0)
                ret = false;
            else if (round == 1 || round == 2)
            {
                if ((currentChoice.coord.X == 2 && currentChoice.coord.Y == 4 && !isCapturePossible(currentChoice)) ||
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

        public void capture(Player currentPlayer)
        {
            int seedCaptured = 0;
            this.hasCaptured = false;


            if (this.isCapturePossible(currentPlayer.currentChoice))
            {
                seedCaptured = currentPlayer.takeAllOpponentsSeeds(this);

                if (this.isNyumba(currentPlayer.currentChoice) && !currentPlayer.NyumbaSpreaded)
                {
                    if (this.askToSpreadNyumba(currentPlayer) == true)
                    {
                        currentPlayer.takeAllMySeeds(this);
                        currentPlayer.NyumbaSpreaded = true;
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
                    case Player.Corner.Neither:
                        showBoard(currentPlayer);


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
                    showBoard(currentPlayer);
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("\nYou have captured at [{0};{1}] !",
                        (char)((int)'A' + currentPlayer.currentChoice.coord.Y), currentPlayer.currentChoice.coord.X + 1);
                    Console.ResetColor();
                    currentPlayer.ReadDirection(this);
                }
                else if (this.token > 0 && (this.isCorner(currentPlayer.currentChoice) == Player.Corner.Right)
                         || this.isCorner(currentPlayer.currentChoice) == Player.Corner.Left)
                {
                    currentPlayer.ReadDirection(this);
                }
                else if (seedCaptured == 1 &&
                         this.checkerBoard[currentPlayer.currentChoice.coord.X, currentPlayer.currentChoice.coord.Y] > 0
                         && this.checkerBoard[currentPlayer.currentChoice.coord.X - 1, currentPlayer.currentChoice.coord.Y] > 0)
                {
                    currentPlayer.takeAllMySeeds(this);
                    currentPlayer.takeAllOpponentsSeeds(this);
                    showBoard(currentPlayer);
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

                askDirectionEarnedMove = true;

            }
            else if (this.hasCaptured == false)
            {
                if (this.endMoveNonEmpty == true)
                {
                    showBoard(currentPlayer);
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("\nYou have earned a move because your ended in an non empty case !");
                    Console.WriteLine("\nYou have ended your move at [{0};{1}] !",
                        (char)((int)'A' + currentPlayer.currentChoice.coord.Y), currentPlayer.currentChoice.coord.X + 1);
                    Console.ResetColor();
                    this.endMoveNonEmpty = false;
                }
                if (this.askDirectionEarnedMove == false)
                {
                    currentPlayer.ReadDirection(this);
                    Console.WriteLine("Hello");
                    this.askDirectionEarnedMove = true;
                }
                
                currentPlayer.takeAllMySeeds(this);

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
                        
                    if (this.isNyumba(currentPlayer.currentChoice) && !currentPlayer.NyumbaSpreaded && currentPlayer.canSpreadNyumba && !this.isCapturePossible(currentPlayer.currentChoice))
                    {
                        currentPlayer.takeNumberMySeeds(this);
                        if (this.token > 0)
                        {
                            currentPlayer.ReadDirection(this);
                            currentPlayer.NyumbaSpreaded = true;
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

        public void nextChoice(Player currentPlayer)
        {
            if (currentPlayer.currentChoice.direction == Player.Direction.ClockWise)
            {
                if (currentPlayer.currentChoice.coord.X == 2)
                {

                    if (currentPlayer.currentChoice.coord.Y == 7)
                    {
                        currentPlayer.currentChoice.coord.X = 3;
                    }
                    else
                    {
                        currentPlayer.currentChoice.coord.Y++;
                    }
                }
                else if (currentPlayer.currentChoice.coord.X == 3)
                {

                    if (currentPlayer.currentChoice.coord.Y == 0)
                    {   
                        currentPlayer.currentChoice.coord.X = 2;

                    }
                    else
                    {
                        currentPlayer.currentChoice.coord.Y--;
                    }
                }
            }
            else if (currentPlayer.currentChoice.direction == Player.Direction.CounterClockWise)
            {
                if (currentPlayer.currentChoice.coord.X == 2)
                {

                    if (currentPlayer.currentChoice.coord.Y == 0)
                    {
                        currentPlayer.currentChoice.coord.X = 3;
                    }
                    else
                    {
                        currentPlayer.currentChoice.coord.Y--;
                    }
                }
                else if (currentPlayer.currentChoice.coord.X == 3)
                {

                    if (currentPlayer.currentChoice.coord.Y == 7)
                    {   
                        currentPlayer.currentChoice.coord.X = 2;
                    }
                    else
                    {
                        currentPlayer.currentChoice.coord.Y++;

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

        public Player.Corner isCorner(Player.Choice currentChoice)
        {
            if (currentChoice.coord.X == 2)
            {
                if (currentChoice.coord.Y == 0 || currentChoice.coord.Y == 1)
                    return Player.Corner.Left;
                else if (currentChoice.coord.Y == 6 || currentChoice.coord.Y == 7)
                    return Player.Corner.Right;
                else
                    return Player.Corner.Neither;
            }
            else
                return Player.Corner.Neither;
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