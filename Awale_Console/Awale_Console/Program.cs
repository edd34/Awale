using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Drawing;

namespace Awale_Console
{
	
	class MainClass
	{
		public static void Main (string[] args)

        {
        
            /*ConsoleKey key;
            do
            {
                ShowCursorState();
                key = Console.ReadKey(true).Key;
                ProcessKey(key);
            } while (key != ConsoleKey.Escape);
            Console.SetCursorPosition(3, 6);
            Console.Write("Hello");
            Console.WriteLine("\nHello");
            Color color = Color.AliceBlue;
            Console.WriteLine("Color = "+color.R);
            Console.ReadLine();*/
            StartGame.Start();
		
        
        
        }


        private static void ShowCursorState()
        {
            Console.Clear();
            Console.ResetColor();
            Console.WriteLine("H to Hide, S to Show, X to Increase Height, Escape to Quit");
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(Console.CursorVisible ? "Visible" : "Hidden");
            Console.WriteLine("Size: {0}%", Console.CursorSize);
            Console.WriteLine("\nX");
            Console.SetCursorPosition(0, 5);
        }

        private static void ProcessKey(ConsoleKey key)
        {
            switch (key)
            {
                case ConsoleKey.H:
                    Console.CursorVisible = false;
                    break;

                case ConsoleKey.S:
                    Console.CursorVisible = true;
                    break;

                case ConsoleKey.X:
                    Console.CursorSize = Console.CursorSize == 100 ? 1 : Console.CursorSize + 1;
                    break;
            }
	}
    }}