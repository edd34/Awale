using System;
using System.Threading;
using System.Threading.Tasks;
using Awale_Console;

namespace Awale_Console
{
	
	class MainClass
	{
		


		public static void Main (string[] args)
		{
			
			setTitle();
			initialize ();

			while (game_end == false)
			{
				Console.Clear ();
				Console.WriteLine ("       Awalé game");
				display ();
				Move ();
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

		static void Move()
		{
			string NewChoice;
			int[] Choice = new int[4] ;
			Choice = ReadEntry ();
			token =   Board [Choice [0], Choice [1]];
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
			else if(seed == 0 &&  x == 2)
			{
				Console.WriteLine ("\n"+"Make a choice then press Enter key : [A:H] [1:4] {L,R} {C,U} ");
				//NewChoice = Console.ReadLine ();
				Move ();
			}


		}
















	}




}
