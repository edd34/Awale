using System;
using System.Diagnostics;

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

        public void update_Step_Round(Board board)
        {
            board.round++;
            if (board.seed > 0)
                state = step.UMUNIA;
            else
                state = step.UTEZA_NA_NDRAZI;
        }

        public GameManager()
        {
            setTitle();
        }

        public bool isGameEnd(Board board,Player currentPlayer)
        {
            for(int i = 0;i <7;i++)
            {
                if (board.checkerBoard[2, i] > 0)
                    return false;
            }
            return true;
        }

        public void showCurrentPossibleMove(Board board)
        {
            
            Player.Choice tmpChoice = new Player.Choice();
            Console.Write("Possible Move are : ");
            for(int x = 2; x < 4; x++)
            {
                for(int y = 0; y<8; y++)
                {
                    tmpChoice.coord.X = x;
                    tmpChoice.coord.Y = y;
                    if(board.isPlayable(tmpChoice))
                    {
                        if(!(tmpChoice.isPossibleToCaptureSomewhere(board) && !board.isCapturePossible(tmpChoice)))
                                Console.Write(" " + (char)(y+(int)'A') + (x+1));
                          
                            
                    }
                }
            }
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