using System;
using System.Threading;
using System.Threading.Tasks;
using Awale_Console;

namespace Awale_Console
{
	
	class MainClass
	{
		public static int Main (string[] args)
		{
			GameManager gameManager = new GameManager ();
			Board board = new Board ();
			Player player_1 = new Player ("Player 1");
			Player player_2 = new Player ("Player 2");
			Player currentPlayer = player_1;
			Player otherPlayer = player_2;

			GameManager.run ();
			board.initialize ();
			while (true) 
			{
				
				gameManager.updateStep (board);
				board.display (currentPlayer,gameManager.state);
					currentPlayer.ReadEntry (board);
					//}while(board.isPlayable(currentPlayer.currentChoice));
					board.returnBoard ();
					board.round++;
					currentPlayer = (currentPlayer == player_1) ? player_2 : player_1;
					otherPlayer = (currentPlayer == player_1) ? player_2 : player_1;
			}
			/*setTitle();
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
			}*/
		}

	}
	}