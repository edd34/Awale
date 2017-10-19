using System;
using Data_Struct;


namespace Awale_Console
{
    public class Player
    {
        public bool isIA = false;
        public bool isNyumbaSpreaded = false;
        public string name;
        public bool canSpreadNyumba;
        public bool triggerSpreadNyumba;

        public Player()
        {
            //Console.WriteLine ("Enter the name for this player");
            this.name = "Unknown"; //Console.ReadLine ();
            this.isIA = false;
            this.canSpreadNyumba = true;
            triggerSpreadNyumba = false;
        }

        public Player(string player)
        {
            //  Console.WriteLine ("Enter the name for "+player);
            //  name = Console.ReadLine ();
            this.isIA = false;
            this.name = player;
            this.canSpreadNyumba = true;
            triggerSpreadNyumba = false;
        }

        //public int[] currentChoice = new int[4];
        public Choice currentChoice;






		
        #region reading input

        public virtual void ReadCorner()
        {
            bool valid;
            ConsoleKey keyPressed = ConsoleKey.Spacebar;
			
            do
            {
                valid = false;
                Console.WriteLine("\nChoose the corner with arrow left or right");
                keyPressed = Console.ReadKey(false).Key;//it is a blocking statement
                if (keyPressed == ConsoleKey.RightArrow)
                {
                    valid = true;
                    currentChoice.corner = Corner.Right;
                }
                else if (keyPressed == ConsoleKey.LeftArrow)
                {
                    valid = true;
                    currentChoice.corner = Corner.Left;
                }
                else
                    valid = false;
            } while(!valid);
			

        }

        public virtual void ReadCoord(Board board, Choice currentChoice)
        {
            
            bool capturePossibleCurrentChoice = false;
            bool isPossibleToCaptureSomeWhere = false;
            bool valid = false;
            char charPressed = '0';
			
            do
            {
                do
                {

                    Console.WriteLine("\nChoose a letter[A;H] and press entrer");
                    charPressed = Console.ReadKey(false).KeyChar;//it is a blocking statement
                    if (charPressed >= 'A' && charPressed <= 'H')
                    {
                        valid = true;
                        currentChoice.coord.Y = (int)charPressed - (int)'A';
                    }
                    else if (charPressed >= 'a' && charPressed <= 'h')
                    {
                        currentChoice.coord.Y = (int)charPressed - (int)'a';
                        valid = true;
                    }
                    else
                    {
                        valid = false;
                    }

                } while(!valid);
                board.previousMove += " " + charPressed;
    			
                do
                {
                    valid = false;
                    Console.WriteLine("\nChoose a cifer [1;4] and press entrer");
                    charPressed = Console.ReadKey(false).KeyChar;//it is a blocking statement
                    if (charPressed >= '1' && charPressed <= '4')
                    {
                        valid = true;
                        currentChoice.coord.X = (int)charPressed - (int)'1';
                    }
                    else
                        valid = false;

                } while(!valid);
                board.previousMove += " " + charPressed;

                isPossibleToCaptureSomeWhere = this.currentChoice.isPossibleToCaptureSomewhere(board);
                capturePossibleCurrentChoice = board.isCapturePossible(this.currentChoice);
            } while(this.currentChoice.isCoordValid(board) == false ||
                    (capturePossibleCurrentChoice == false && isPossibleToCaptureSomeWhere == true) == true);
        }

        public virtual void ReadDirection(Board board)
        {

            ConsoleKey keyPressed = ConsoleKey.Spacebar;
            do
            {
                this.currentChoice.valid = false;
                Console.WriteLine("\nChoose the direction (clockWise or counterClockWise) with the arrow");
                keyPressed = Console.ReadKey(false).Key;
                if (keyPressed == ConsoleKey.LeftArrow)
                {
                    this.currentChoice.valid = true;
                    this.currentChoice.direction = Direction.CounterClockWise;
                }
                else if (keyPressed == ConsoleKey.RightArrow)
                {
                    this.currentChoice.valid = true;
                    this.currentChoice.direction = Direction.ClockWise;
                }
                else
                    this.currentChoice.valid = false;

            } while(!this.currentChoice.valid);
            board.previousMove += " direction = " + this.currentChoice.direction;
        }

        #endregion


    }
}

