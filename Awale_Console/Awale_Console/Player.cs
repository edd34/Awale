using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Awale_Console
{
	public class Player
	{
        public bool isIA = false;
        public bool NyumbaSpreaded = false;
		public string name;
        public bool canSpreadNyumba;
        public bool triggerSpreadNyumba;

        public Player()
        {
              //Console.WriteLine ("Enter the name for this player");
            this.name = "Unknown"; //Console.ReadLine ();
            this.isIA = false;
            this.canSpreadNyumba = false;
            triggerSpreadNyumba = false;
        }

        public Player(string player)
        {
            //  Console.WriteLine ("Enter the name for "+player);
            //  name = Console.ReadLine ();
            this.isIA = false;
            this.name = player;
            this.canSpreadNyumba = false;
            triggerSpreadNyumba = false;
        }

		//public int[] currentChoice = new int[4];
		public Choice currentChoice;
		public enum step 
		{
			UMUNIA,
			UTEZA_NA_NDRAZI
		}
			
		public struct Coord
		{
			public int X,Y;
		}

		public enum Direction
		{
			ClockWise,CounterClockWise
		}

		public enum Corner
		{
			Left,Right,Neither
		}

		public struct Choice
		{
			public Coord coord;
			public Direction direction;
			public Corner corner;

           
			public bool valid;

            public bool isPossibleToCaptureSomewhere(Board board)
            {
                for(int i = 0;i <8;i++)
                {
                    if (board.checkerBoard[2, i] > 0 && board.checkerBoard[1, i] > 0)
                        return true;
                }
                return false;
            }


            public bool isCoordValid(Board board)
            {
                


                if (board.checkerBoard[this.coord.X, this.coord.Y] == 0)
                {
                    return false;
                }
                else if (this.coord.X == 0 || this.coord.X == 1)
                {
                    return false;
                }
                else if (board.round <= 2 && board.isNyumba(this) && !board.isCapturePossible(this))
                {
                    return false; 
                }
                else
                    return true;
            }


		}

		
			
		public virtual void ReadCorner(Board board)
		{
			board.previousMove = "";
			ConsoleKey keyPressed = ConsoleKey.Spacebar;
			////////////////
			do {
				this.currentChoice.valid = false;
				Console.WriteLine ("\nChoose the corner with arrow left or right");
				keyPressed = Console.ReadKey(false).Key;//it is a blocking statement
				if(keyPressed == ConsoleKey.RightArrow)
				{
					this.currentChoice.valid = true;
					this.currentChoice.corner = Corner.Right;
				}
				else if(keyPressed == ConsoleKey.LeftArrow){
					this.currentChoice.valid = true;
					this.currentChoice.corner = Corner.Left;
				}
				else
					this.currentChoice.valid = false;
			}while(!this.currentChoice.valid);
			/////////////////////////////////////
			board.previousMove +=" corner = "+ this.currentChoice.corner;

		}

		public virtual void ReadCoord(Board board)
		{
            bool capturePossibleCurrentChoice = false;
            bool isPossibleToCaptureSomeWhere = false;
			board.previousMove = "";
			char charPressed = '0';
			////////////////
            do
            {
    			do {

    				Console.WriteLine ("\nChoose a letter[A;H] and press entrer");
    				charPressed = Console.ReadKey(false).KeyChar;//it is a blocking statement
    				if(charPressed >= 'A' && charPressed <= 'H' )
    				{
    					this.currentChoice.valid = true;
    					this.currentChoice.coord.Y = (int)charPressed-(int)'A';
    				}
    				else if(charPressed >= 'a' && charPressed <= 'h' ) 
    				{
    					this.currentChoice.coord.Y = (int)charPressed-(int)'a';
    					this.currentChoice.valid = true;
    				}
    				else
    				{
    					this.currentChoice.valid = false;
    				}

                } while(!this.currentChoice.valid );
    			board.previousMove += " "+ charPressed;
    			/////////////////
    			do {
    				this.currentChoice.valid = false;
    				Console.WriteLine ("\nChoose a cifer [1;4] and press entrer");
    				charPressed = Console.ReadKey(false).KeyChar;//it is a blocking statement
    				if(charPressed >= '1' && charPressed <= '4' )
    				{
    					this.currentChoice.valid = true;
    					this.currentChoice.coord.X = (int)charPressed - (int)'1';
    				}
    				else
    					this.currentChoice.valid = false;

    			} while(!this.currentChoice.valid);
    			board.previousMove +=" "+ charPressed;

                isPossibleToCaptureSomeWhere = this.currentChoice.isPossibleToCaptureSomewhere(board);
                capturePossibleCurrentChoice = board.isCapturePossible(this.currentChoice);
            } while(this.currentChoice.isCoordValid(board) == false || 
                (capturePossibleCurrentChoice == false && isPossibleToCaptureSomeWhere == true)==true );
		}

        public virtual void ReadDirection(Board board)
        {

            ConsoleKey keyPressed = ConsoleKey.Spacebar;
            do {
                this.currentChoice.valid = false;
                Console.WriteLine ("\nChoose the direction (clockWise or counterClockWise) with the arrow");
                keyPressed = Console.ReadKey(false).Key;
                if(keyPressed == ConsoleKey.LeftArrow )
                {
                    this.currentChoice.valid = true;
                    this.currentChoice.direction = Direction.CounterClockWise;
                }
                else if(keyPressed == ConsoleKey.RightArrow )
                {
                    this.currentChoice.valid = true;
                    this.currentChoice.direction = Direction.ClockWise;
                }
                else
                    this.currentChoice.valid = false;

            } while(!this.currentChoice.valid);
            board.previousMove +=" direction = " +this.currentChoice.direction;
        }


		public void takeAllMySeeds(Board board)
		{
            board.token += board.checkerBoard [this.currentChoice.coord.X, this.currentChoice.coord.Y];
            board.checkerBoard [this.currentChoice.coord.X, this.currentChoice.coord.Y] = 0;
		}

        public virtual void takeNumberMySeeds(Board board)
        {
            int ret = 0;
            ////////////////


            /////////////////
            do {
                Console.WriteLine ("\nEnter a number of seed between 0 and " + board.checkerBoard[2,5]);
            } while(Int32.TryParse(Console.ReadLine(),out ret));
            board.token += ret;
            board.checkerBoard [2,5] -= ret;
        }

		public int takeAllOpponentsSeeds(Board board)
		{
            int seedTaken = 0;
			if (currentChoice.coord.X == 2) 
			{
                seedTaken = board.checkerBoard[this.currentChoice.coord.X - 1, this.currentChoice.coord.Y];
				board.token += board.checkerBoard [this.currentChoice.coord.X - 1, this.currentChoice.coord.Y];
				board.checkerBoard [this.currentChoice.coord.X-1, this.currentChoice.coord.Y] = 0;
			}
            return seedTaken;
		}

	}
}

