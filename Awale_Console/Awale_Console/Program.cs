using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Awale_Console
{
	
	class MainClass
	{
		public static void Main (string[] args)
		{

            Console.SetCursorPosition(3, 6);
            Console.Write("Hello");
            Console.WriteLine("\nHello");
            Color color = Color.AliceBlue;
            Console.WriteLine("Color = "+color.R);
            Console.ReadLine();
            StartGame.Start();
		}

	}
}