using System;

namespace Awale_Console
{
	public class GameManager
	{

		public enum step 
		{
			UMUNIA,
			UTEZA_NA_NDRAZI
		}

		public step state = step.UMUNIA;

		public void updateStep(Board board)
		{
			if (board.seed > 0) 
				state = step.UMUNIA;
			else
				state = step.UTEZA_NA_NDRAZI;
		}

		public static void run()
		{

		}

		public GameManager ()
		{
			setTitle ();
		}

		static void setTitle()
		{
			Console.Title = "Awalé Game";
		}
		/*
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
		}*/

	}
}

