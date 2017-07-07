using System;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;
using Awale_Console;

namespace Awale_Console
{
	
	class MainClass
	{
		public static void Main (string[] args)
		{
			

			GameManager gameManager = new GameManager ();
			Board board = new Board ();
			Player player_1 = new Player ("Player 1");
			Player player_2 = new Player ("Player 2");
			Player currentPlayer = player_1;
			Player otherPlayer = player_2;

			GameManager.run ();
			board.initialize ();
			while (!board.game_end) 
			{
				
				//Console.Clear ();
				board.display (currentPlayer,gameManager.state);
				currentPlayer.ReadEntry (board,gameManager);
				if(board.isCapturePossible(currentPlayer.currentChoice))
				{
					currentPlayer.takeAllOpponentsSeeds (board, currentPlayer.currentChoice);

					if(board.spreadNyumba(board,currentPlayer))
						currentPlayer.takeAllMySeeds (board, currentPlayer.currentChoice);
				}
				board.disseminate (currentPlayer.currentChoice);
				//board.display (currentPlayer,gameManager.state);
				//Console.ReadLine ();
				board.returnBoard ();


				currentPlayer = (currentPlayer == player_1) ? player_2 : player_1;
				otherPlayer = (currentPlayer == player_1) ? player_2 : player_1;
				gameManager.update_Step_Round (board);
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