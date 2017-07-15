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
            ConsoleKey keyPressed;
            StartGame Run = new StartGame();

            do
            {
                Console.Clear();
                Console.WriteLine("Which game do you want to start ?");
                Console.WriteLine("[1] Player vs Player");
                Console.WriteLine("[2] Player vs Computer");
                Console.WriteLine("[3] Player vs Remote Player");
                //Console.WriteLine("Make your choice with the keyboard :");
                keyPressed = Console.ReadKey().Key;
            } while(keyPressed != ConsoleKey.D1 && keyPressed != ConsoleKey.D2 && keyPressed != ConsoleKey.D3);
            
            switch(keyPressed)
            {
                case ConsoleKey.D1:
                    Run.PlayerVsPlayer();    
                    break;

                case ConsoleKey.D2:
                    Run.PlayerVsComputer();    
                    break;

                case ConsoleKey.D3:
                    Run.PlayerVsRemotePlayer();    
                    break;
            }
		}

	}
}