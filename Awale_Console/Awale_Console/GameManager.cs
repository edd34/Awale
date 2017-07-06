using System;

namespace Awale_Console
{
	public class GameManager
	{
		public GameManager ()
		{
		}

		static void setTitle()
		{
			Console.Title = "Awalé Game";
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

	}
}

