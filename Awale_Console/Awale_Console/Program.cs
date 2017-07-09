using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Awale_Console
{
	
	class MainClass
	{
		public static void Main (string[] args)
		{
           

			GameManager gameManager = new GameManager ();

			Board board = new Board ();
			//Player player_1 = new Player ("Player 1");
            PlayerIA player_1 = new PlayerIA ("Player 1");
            PlayerIA player_2 = new PlayerIA ("Player 2");
			PlayerIA currentPlayer = player_1;
			PlayerIA otherPlayer = player_2;
            
			board.initialize ();
            while (!gameManager.isGameEnd(board,currentPlayer)) 
			{
				
				Console.Clear ();
				board.display (currentPlayer,gameManager.state);
                gameManager.showCurrentPossibleMove(board);
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
            board.display (currentPlayer,gameManager.state);

            Console.WriteLine("\n\nGame is end and winner is " + otherPlayer.name);
            Console.ReadLine();
		}

	}
	}