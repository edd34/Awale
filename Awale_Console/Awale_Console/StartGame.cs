using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace Awale_Console
{
    public class StartGame
    {
        public void PlayerVsPlayer()
        {
            GameManager gameManager = new GameManager();

            Board board = new Board();
            //Player player_1 = new Player ("Player 1");
            Player player_1 = new Player("Player 1");
            Player player_2 = new Player("Player 2");
            Player currentPlayer = player_1;
            Player otherPlayer = player_2;

            board.initialize();
            //board.initialize_test();
            while (!gameManager.isGameEnd(board, currentPlayer))
            {

                Console.Clear();
                board.display(currentPlayer, gameManager.state);
                gameManager.showCurrentPossibleMove(board);
                currentPlayer.ReadCoord(board);


                board.move(gameManager, currentPlayer.currentChoice);
                board.capture(currentPlayer);

                board.disseminate(currentPlayer);
                //board.display (currentPlayer,gameManager.state);
                //Console.ReadLine();
                board.returnBoard();


                currentPlayer = (currentPlayer == player_1) ? player_2 : player_1;
                otherPlayer = (currentPlayer == player_1) ? player_2 : player_1;
                gameManager.update_Step_Round(board);
            }
            board.display(currentPlayer, gameManager.state);

            Console.WriteLine("\n\nGame is end and winner is " + otherPlayer.name);
            Console.ReadLine();
        }


        public void PlayerVsComputer()
        {
            GameManager gameManager = new GameManager();

            Board board = new Board();
            //Player player_1 = new Player ("Player 1");
            Player player_1 = new Player("Player 1");
            PlayerIA player_2 = new PlayerIA("Player 2");
            Player currentPlayer = player_1;
            Player otherPlayer = player_2;

            board.initialize();
            while (!gameManager.isGameEnd(board, currentPlayer))
            {

                Console.Clear();
                board.display(currentPlayer, gameManager.state);
                gameManager.showCurrentPossibleMove(board);
                currentPlayer.ReadCoord(board);


                board.move(gameManager, currentPlayer.currentChoice);
                board.capture(currentPlayer);

                board.disseminate(currentPlayer);
                //board.display (currentPlayer,gameManager.state);
                //Console.ReadLine();
                board.returnBoard();


                currentPlayer = (currentPlayer == player_1) ? player_2 : player_1;
                otherPlayer = (currentPlayer == player_1) ? player_2 : player_1;
                gameManager.update_Step_Round(board);
            }
            board.display(currentPlayer, gameManager.state);

            Console.WriteLine("\n\nGame is end and winner is " + otherPlayer.name);
            Console.ReadLine();
        }

        public void PlayerVsRemotePlayer()
        {
            GameManager gameManager = new GameManager();

            Board board = new Board();
            //Player player_1 = new Player ("Player 1");
            PlayerNetwork player_1 = new PlayerNetwork();
            byte[] ba = new byte[10000];
            string rcpt = " ";
            board.initialize();
            player_1.initConnection();
            while (!gameManager.isGameEnd(board, player_1))
            {
                if (player_1.Host == true)
                {
                    player_1.asen = new ASCIIEncoding();
                    if (board.round > 1)
                    {
                        player_1.s.Receive(ba);
                        rcpt = System.Text.Encoding.UTF8.GetString(ba).Trim();
                        board.ToBoard(rcpt);    
                    }


                    Console.Clear();
                    board.display(player_1, gameManager.state);
                    gameManager.showCurrentPossibleMove(board);
                    player_1.ReadCoord(board);


                    board.move(gameManager, player_1.currentChoice);
                    board.capture(player_1);
                    board.display(player_1, gameManager.state);
                    board.disseminate(player_1);
                    board.returnBoard();
                    player_1.asen = new ASCIIEncoding();
                    //ba=player_1.asen.GetBytes(board.ToString());
                    player_1.s.Send(player_1.asen.GetBytes(board.ToString().Trim()));
                    gameManager.update_Step_Round(board);
                }
                else if (player_1.Host == false)
                {
                    ba = new byte[1000];
                    rcpt = System.Text.Encoding.UTF8.GetString(ba).Trim();
                    ;
                    board.ToBoard(rcpt);

                    Console.Clear();
                    board.display(player_1, gameManager.state);
                    gameManager.showCurrentPossibleMove(board);
                    player_1.ReadCoord(board);


                    board.move(gameManager, player_1.currentChoice);
                    board.capture(player_1);

                    board.disseminate(player_1);
                    board.display(player_1, gameManager.state);
                    board.returnBoard();
                    ba = new byte[1000];
                    player_1.asen = new ASCIIEncoding();
                    ba = player_1.asen.GetBytes(board.ToString().Trim());
                    player_1.stm.Write(ba, 0, ba.Length);
                    gameManager.update_Step_Round(board);

                }

                //Here I have to send data and also retrieve

                //
                //board.display (currentPlayer,gameManager.state);
                //Console.ReadLine();
                ///board.returnBoard ();

            }
            board.display(player_1, gameManager.state);
            player_1.s.Close();
            if (player_1.Host == true)
                player_1.tcpList.Stop();
            else
                player_1.tcpclnt.Close();


            //Console.WriteLine("\n\nGame is end and winner is " + otherPlayer.name);
            Console.ReadLine();

        }

        static public void Start()
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

            switch (keyPressed)
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

