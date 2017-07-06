using System;

namespace Awale_Console
{
	public class Player
	{
		private string name;
		private string previousMove = "";
		public Player (string name)
		{
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
			//playerChoice == 
			return new int[4] {i,j,d,c};
		}
	}
}

