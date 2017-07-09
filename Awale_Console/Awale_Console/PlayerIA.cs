using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Awale_Console
{
    public class PlayerIA
        :Player
    {
        public PlayerIA()
        {
           base.name = "Computer";
           base.isIA = true;
        }

        public PlayerIA(string name)
        {
            base.name = name;
            base.isIA = true;
        }
            
        List<Player.Choice> availableMove = new List<Player.Choice>();

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
                        if (!(tmpChoice.isPossibleToCaptureSomewhere(board) && !board.isCapturePossible(tmpChoice)))
                        {
                            Console.Write(" " + (char)(y+(int)'A') + (x+1));
                            this.availableMove.Add(tmpChoice);
                        }
                            

                    }
                }
            }
        }

        public override void ReadCorner(Board board)
        {
            if((new Random().Next()%2)==0)
                base.currentChoice.corner = Corner.Left;
            else
                base.currentChoice.corner = Corner.Right;


        }

        public override void ReadCoord(Board board)
        {
           this.showCurrentPossibleMove(board);

            base.currentChoice = availableMove[new Random().Next(availableMove.Count-1)];

        }
        public override void ReadDirection(Board board)
        {
            if((new Random().Next()%2)==0)
                base.currentChoice.direction = Direction.ClockWise;
            else
                base.currentChoice.direction = Direction.CounterClockWise;
        }

    }
}

