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
            StartGame Run = new StartGame();
            //Run.PlayerVsPlayer();
            //Run.PlayerVsComputer();
            Run.PlayerVsRemotePlayer();
            Console.ReadLine();


		}

	}
}