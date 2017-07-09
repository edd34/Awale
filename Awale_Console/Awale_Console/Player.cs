using System;
using System.Diagnostics;

namespace Awale_Console
{
	public class Player
	{
        public Player player1;
        public bool NyumbaSpreaded = false;
		public readonly string name;
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

		public Player(string player)
		{
		//	Console.WriteLine ("Enter the name for "+player);
		//	name = Console.ReadLine ();
			name = player;
		}
			
		public void ReadCorner(Board board)
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

		public void ReadCoord(Board board)
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

        public void ReadDirection(Board board)
        {

            ConsoleKey keyPressed = ConsoleKey.Spacebar;
            do {
                this.currentChoice.valid = false;
                Console.WriteLine ("\nChoose the direction (clockWise or counterClockWise) with the arrow");
                keyPressed = Console.ReadKey(false).Key;//it is a blocking statement
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


		public void takeAllMySeeds(Board board,Player currentPlayer)
		{
            board.token += board.checkerBoard [currentPlayer.currentChoice.coord.X, currentPlayer.currentChoice.coord.Y];
            board.checkerBoard [currentPlayer.currentChoice.coord.X, currentPlayer.currentChoice.coord.Y] = 0;
		}

		public int takeAllOpponentsSeeds(Board board,Player.Choice currentChoice)
		{
            int seedTaken = 0;
			if (currentChoice.coord.X == 2) 
			{
                seedTaken = board.checkerBoard[currentChoice.coord.X - 1, currentChoice.coord.Y];
				board.token += board.checkerBoard [currentChoice.coord.X - 1, currentChoice.coord.Y];
				board.checkerBoard [currentChoice.coord.X-1, currentChoice.coord.Y] = 0;
			}
            return seedTaken;
		}

	}
}

