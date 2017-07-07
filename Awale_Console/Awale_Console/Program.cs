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
				
				Console.Clear ();
				board.display (currentPlayer,gameManager.state);
				currentPlayer.ReadCoord (board);
				board.move (gameManager, currentPlayer.currentChoice);
				if (board.isCapturePossible (currentPlayer.currentChoice)) {
					currentPlayer.takeAllOpponentsSeeds (board, currentPlayer.currentChoice);

					if (board.isNyumba (currentPlayer.currentChoice)) {
						if (board.askToSpreadNyumba (board, currentPlayer) == true) {
							currentPlayer.takeAllMySeeds (board, currentPlayer.currentChoice);
						}
					} 



					board.hasCaptured = true;
				} 
				else 
				{
					currentPlayer.takeAllMySeeds (board, currentPlayer.currentChoice);
					board.hasCaptured = false;
				}

				if (board.hasCaptured == true) 
				{	
					currentPlayer.ReadCornerDirection (board);
					if (currentPlayer.currentChoice.corner == Player.Corner.Left) 
					{
						if (currentPlayer.currentChoice.direction == Player.Direction.ClockWise) 
						{
							currentPlayer.currentChoice.coord.X = 3;
							currentPlayer.currentChoice.coord.Y = 0;
						} 
						else if (currentPlayer.currentChoice.direction == Player.Direction.CounterClockWise) 
						{
							currentPlayer.currentChoice.coord.X = 2;
							currentPlayer.currentChoice.coord.Y = 1;
						}

					} 
					else if (currentPlayer.currentChoice.corner == Player.Corner.Right) 
					{
						if (currentPlayer.currentChoice.direction == Player.Direction.ClockWise) 
						{
							currentPlayer.currentChoice.coord.X = 2;
							currentPlayer.currentChoice.coord.Y = 6;
						} 
						else if (currentPlayer.currentChoice.direction == Player.Direction.CounterClockWise) 
						{
							currentPlayer.currentChoice.coord.X = 3;
							currentPlayer.currentChoice.coord.Y = 7;
						}

					} 
				} 

				board.disseminate (currentPlayer.currentChoice);
				board.returnBoard ();


				currentPlayer = (currentPlayer == player_1) ? player_2 : player_1;
				otherPlayer = (currentPlayer == player_1) ? player_2 : player_1;
				gameManager.update_Step_Round (board);
			}
		}

	}
	}