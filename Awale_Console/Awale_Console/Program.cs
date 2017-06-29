using System;
using System.Threading;
using System.Threading.Tasks;
using Awale_Console;

namespace Awale_Console
{
	
	class MainClass
	{
		

		static int seed = 64;
		static int [,] Board = new int[4,8];
		static bool game_end = false;
		static int currentPlayer = 1;
		static int round = 0;
		static string previousMove = "";
		public static void Main (string[] args)
		{
			int[] PlayerChoice = new int[4] ;
			setTitle();
			initialize ();

			while (game_end == false)
			{
				Console.Clear ();
				Console.WriteLine ("       Awalé game");
				display ();
				do {
					PlayerChoice = ReadEntry ();
				} while(!isPlayable (PlayerChoice));
				Move (PlayerChoice);
				returnBoard ();
				changeCurentPlayer ();
				round++;
			}

		}

		static bool isGameEnd()
		{
			bool state = true;
			if (seed == 0) 
			{
				for (int j=0; j < 8; j++) 
				{
					if (Board [2, j] == 1)
					{
						state = false;
					} 
					else 
					{
						state = true;
						break;
					}
				}
			} 
			else 
			{
				for (int j=0; j < 8; j++) 
				{
					if (Board [2, j] > 1) {
						state = false;
					} 
					else 
					{
						state = true;
						break;
					}
				}
			}

			return state;
		}

		static void Move(int[] Choice)
		{
			int token = Board [Choice [0], Choice [1]];
			Board [Choice [0], Choice [1]] = 0;
			bool Left = (Choice [2] == 0);
			bool Right = !Left;
			bool clockWise = (Choice [3] == 0);
			bool counterClockWise = !clockWise;


			int x=Choice[0], y=Choice[1];
			if (seed > 0)
			{
				if (x == 2) 
				{
					if (Board [1, Choice [1]] > 0) 
					{
						
						token += Board [1, Choice [1]];
						Board [1, Choice [1]] = 0;
					}

				}
				seed--;
				token++;
				if (y == 6 || y == 7 && x == 2)
					disseminate (x, 7, token, Left, clockWise);
				else
					disseminate (x, y, token, Left, clockWise);
			} 
			else 
			{

			}


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

		static int[] ReadEntry()
		{
			string Choice;
			int i,j,d,c;
			do{i=-1;
				j=-1;
				d=-1;
				c=-1;
				Console.WriteLine ("\n"+"Make a choice then press Enter key : [A:H] [1:4] {L,R} {C,U} ");
				Choice = Console.ReadLine ();

				try{	
			

			switch (Choice[0]) {//choose the letter
			case ('A'):
				j = 0;
				break;

			case ('B'):
				j = 1;
				break;

			case ('C'):
				j = 2;
				break;

			case ('D'):
				j = 3;
				break;

			case ('E'):
				j = 4;
				break;

			case ('F'):
				j = 5;
				break;

			case ('G'):
				j = 6;
				break;

			case ('H'):
				j = 7;
				break;
			
			default:
				j = -1;
				break;
			}
				

		
				switch (Choice[1]) {//choose the number
			case ('1'):
				i = 0;
				break;

			case ('2'):
				i = 1;
				break;

			case ('3'):
				i = 2;
				break;

			case ('4'):
				i = 3;
				break;

			default:
				i = -1;
				break;

			}

			
			switch (Choice[2]) //choose the corner left or right (if possible)
			{
			case ('L'):
				d = 0;
				break;

			case ('R'):
				d = 1;
				break;

			default:
				d=-1;
				break;
			}

			switch (Choice[3]) //choose wether you play clockwise (C) or counter clockwise (U)
			{
			case ('C'):
				c = 0;
				break;

			case ('U'):
				c = 1;
				break;
			default:
				c=-1;
				break;
			}
				}
				catch{
					Console.WriteLine("error type (something) again !");
				}

        
			}while(j==-1 || i ==-1 ||d==-1||c==-1);
			previousMove = Choice;
			return new int[4] {i,j,d,c};
		}

		static void changeCurentPlayer()
		{
			switch (currentPlayer) 
			{
			case 1:
				currentPlayer = 2;
				break;

			case 2:
				currentPlayer = 1;
				break;
			}
		}

		static void setTitle()
		{
			Console.Title = "Awalé Game";
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
	}




}
