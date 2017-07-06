using System;

namespace Awale_Console
{
	public class Board
	{
		static int seed = 64;
		static int [,] checkerBoard = new int[4,8];
		static bool game_end = false;
		static int currentPlayer = 1;
		static int round = 0;
		static int token;

		static int[] PlayerChoice = new int[4] ;


		public Board ()
		{
			seed = 64;
			game_end = false;
		}

		static void initialize()//in order to initialize the first round
		{
			for (int i = 0; i < 4; i++) {
				for (int j = 0; j < 8; j++) 
				{
					Board [i, j] = 0;
				}
			}
			Board [1, 3] = 6;
			Board [1, 2] = 2;
			Board [1, 1] = 2;
			Board [2, 4] = 6;
			Board [2, 5] = 2;
			Board [2, 6] = 2;
			seed = 44;
		}

		static void display()//main display fonction for each round
		{

			Console.WriteLine ();
			Console.WriteLine ();
			Console.Write ("    A  B  C  D  E  F  G  H");
			Console.WriteLine ();
			Console.WriteLine ("--------------------------------");

			for(int i = 0; i < 4 ; i++)
			{
				for (int j = 0; j < 10; j++) 
				{
					if (j == 0)
						Console.Write ((i+1) + " | ");
					else if(j==9)
						Console.Write ("| " + (i+1));
					else 

						Console.Write (Board [i, j-1] + "  ");

				}
				if (i == 1) {
					Console.WriteLine ();
					Console.WriteLine ("--------------------------------  seed = "+seed);
				} else {
					Console.WriteLine ();
				}


			}
			Console.WriteLine ("--------------------------------");
			Console.Write ("    A  B  C  D  E  F  G  H");
			Console.WriteLine ();
			Console.WriteLine ();
			Console.WriteLine ("    Player : " + currentPlayer + "    Round : " + round);
			if (round > 0)
				Console.WriteLine ("Previous move = "+previousMove);
			else
				Console.WriteLine ();

		}

		static void disseminate(int x, int y, int token,bool Left, bool clockWise)
		{
			bool counterClockWise = !clockWise;
			while (token > 0) 
			{

				if (clockWise) 
				{
					if (x == 2) 
					{

						if (y == 7) 
						{
							x = 3;
							Board [x, y]++;
							token--;
						}
						else 
						{
							y++;
							Board [x, y]++;
							token--;
						}
					} 
					else if (x == 3) 
					{

						if (y == 0) 
						{	
							x = 2;
							Board [x, y]++;
							token--;

						}
						else 
						{
							y--;
							Board [x, y]++;
							token--;
						}

					}
				} 
				else if (counterClockWise) 
				{
					if (x == 2) 
					{

						if (y == 0) 
						{
							x = 3;
							Board [x, y]++;
							token--;
						}
						else 
						{
							y--;
							Board [x, y]++;
							token--;
						}
					} 
					else if (x == 3) 
					{

						if (y == 7) 
						{	
							x = 2;
							Board [x, y]++;
							token--;
						}
						else 
						{
							y++;
							Board [x, y]++;
							token--;

						}

					}
				}

				if (token == 0) 
				{
					if (x == 2) 
					{
						if (Board [1, y] > 0) 
						{
							token += Board [1, y];
							Board [1, y] = 0;
						}
					}
				}

			}

		}

		static bool isPlayable(int[] Choice)
		{

			bool ret;
			if(Choice[0]==0 || Choice[0]==1)
				ret = false;	
			else if (round == 0  || round == 1)
			{
				if (Choice [0] == 2 && Choice [1] == 4 || Board [Choice [0], Choice [1]] == 0) {
					ret = false;
				} else
					ret = true;

			}
			else
			{
				if (Board [Choice [0], Choice [1]] > 0)
					ret = true;
				else
					ret = false;
			}
			return ret;
		}

		static void returnBoard()
		{
			int [,] Board2 = new int[4,8];
			for (int i = 0; i < 4; i++) {
				for (int j = 0; j < 8; j++) 
				{
					Board2 [3-i, 7-j] = Board[i,j];
				}
			}

			for (int i = 0; i < 4; i++) {
				for (int j = 0; j < 8; j++) 
				{
					Board [i, j] = Board2[i,j];
				}
			}
		}
	}
}

