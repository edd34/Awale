using System;

namespace Awale_Console
{
	public class Player
	{
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
			Left,Right
		}

		public struct Choice
		{
			public Coord coord;
			public Direction direction;
			public Corner corner;

			public bool valid;
		}

		public Player(string player)
		{
		//	Console.WriteLine ("Enter the name for "+player);
		//	name = Console.ReadLine ();
			name = player;
		}
			
		public void ReadEntry(Board board)
		{
			board.previousMove = "";
			var keyPressed = ConsoleKey.Spacebar;
			char charPressed = '0';
			////////////////
			do {
				
				Console.WriteLine ("\nChoose a letter[A;H] and press entrer");
				charPressed = Console.ReadKey(false).KeyChar;//it is a blocking statement
				if(charPressed >= 'A' && charPressed <= 'H' )
				{
					this.currentChoice.valid = true;
					this.currentChoice.coord.X = (int)charPressed-(int)'A';
				}
				else if(charPressed >= 'a' && charPressed <= 'h' ) 
				{
					this.currentChoice.coord.X = (int)charPressed-(int)'a';
					this.currentChoice.valid = true;
				}
				else
				{
					this.currentChoice.valid = false;
				}

			} while(!this.currentChoice.valid);
			board.previousMove += charPressed;
			/////////////////
			do {
			this.currentChoice.valid = false;
				Console.WriteLine ("\nChoose a cifer [1;4] and press entrer");
				charPressed = Console.ReadKey(false).KeyChar;//it is a blocking statement
				if(charPressed >= '1' && charPressed <= '4' )
					{
					this.currentChoice.valid = true;
					this.currentChoice.coord.Y = (int)charPressed - (int)'1';
					}
				else
					this.currentChoice.valid = false;

			} while(!this.currentChoice.valid);
			board.previousMove += charPressed;
			/////////////////////////////
			do {
				this.currentChoice.valid = false;
				Console.WriteLine ("\nChoose the corner with arrow left or right");
				keyPressed = Console.ReadKey(false).Key;//it is a blocking statement
				if(keyPressed == ConsoleKey.LeftArrow )
				{
					this.currentChoice.valid = true;
					this.currentChoice.corner = Corner.Left;
				}
				else if(keyPressed == ConsoleKey.RightArrow )
				{
					this.currentChoice.valid = true;
					this.currentChoice.corner = Corner.Right;
				}
				else
					this.currentChoice.valid = false;

			} while(!this.currentChoice.valid);
			/////////////////////////////////////
			board.previousMove += this.currentChoice.corner;
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
			board.previousMove += this.currentChoice.direction;
		}
	}
}

