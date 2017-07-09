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
            
			board.initialize ();
            while (!gameManager.isGameEnd(board,currentPlayer)) 
			{
				
				Console.Clear ();
				board.display (currentPlayer,gameManager.state);
                currentPlayer.ReadCoord (board);


				board.move (gameManager, currentPlayer.currentChoice);
                board.capture(currentPlayer,board);

                board.disseminate (currentPlayer,board);
                //board.display (currentPlayer,gameManager.state);
                //Console.ReadLine();
				board.returnBoard ();


				currentPlayer = (currentPlayer == player_1) ? player_2 : player_1;
				otherPlayer = (currentPlayer == player_1) ? player_2 : player_1;
				gameManager.update_Step_Round (board);
			}
		}

	}
	}