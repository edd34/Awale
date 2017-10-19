using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace Awale_Console
{
    public class PlayerNetwork
        :Player
    {
        public bool Host;
        public bool YourTurn;
        public IPAddress ipAd;
        public TcpListener tcpList;
        public Socket s;
        public TcpClient tcpclnt = new TcpClient();
        public Stream stm;
        public ASCIIEncoding asen = new ASCIIEncoding();

        public PlayerNetwork()
        {

            Console.WriteLine("\nWhat is your name ?");
            base.name = Console.ReadLine();
            base.isIA = false;
        }

        public void initConnection()
        {
            ConsoleKey keyPressed;
            do
            {
                Console.WriteLine("Are Server (Y/N) ?");
                keyPressed = Console.ReadKey().Key;                
            } while(!(keyPressed != ConsoleKey.N || keyPressed != ConsoleKey.Y));

            if(keyPressed == ConsoleKey.Y)
            {
                string hostName = Dns.GetHostName(); // Retrive the Name of HOST  
                Console.WriteLine("\nhostName is : ");  
                Console.WriteLine(hostName);  
                // Get the IP  
                string myIP = Dns.GetHostAddresses(hostName)[0].ToString();  
                Console.WriteLine("Your IP Address is : "+myIP+" provide it to your friend");
                this.ipAd = IPAddress.Parse(myIP);
                this.tcpList=new TcpListener(ipAd,8080);
                this.tcpList.Start();
                Console.WriteLine("The server is running at port 8080...");    
                Console.WriteLine("The local End point is  :" + 
                    tcpList.LocalEndpoint );
                Console.WriteLine("Waiting for a connection.....");
                //this.s = new Socket(SocketType.Stream, ProtocolType.Tcp);
                this.s=tcpList.AcceptSocket();
                Console.WriteLine("Connection accepted from " + s.RemoteEndPoint);
                this.YourTurn = true;
                this.Host = true;
            }
            else if(keyPressed == ConsoleKey.N)
            {
                
                Console.WriteLine("Write down the IPv4 adress given by your friend and type enter");
                Console.WriteLine("(It should be something like 'XXX.XXX.XXX.XXX')");
                this.ipAd = IPAddress.Parse(Console.ReadLine()); 
                Console.WriteLine("ok");
                this.tcpclnt = new TcpClient();
                this.tcpclnt.Connect(this.ipAd, 8080);
                Console.WriteLine("Connected");
                this.stm = tcpclnt.GetStream();
                this.YourTurn = false;
                this.Host = false;

            }
        }
            

    }
}

