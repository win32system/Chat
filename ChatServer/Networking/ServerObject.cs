﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading;
using System.IO;
using Core;

namespace ChatServer
{
    public class ServerObject
    {
        private TcpListener tcpListener; 

        protected internal void Listen()
        {
            try
            {
                tcpListener = new TcpListener(IPAddress.Any, 8888);
                tcpListener.Start();
                Console.WriteLine("Сервер запущен. Ожидание подключений...");

                while (true)
                {
                    TcpClient tcpClient = tcpListener.AcceptTcpClient();

                    ClientObject clientObject = new ClientObject(tcpClient);
                    Manager.Clients.AddLast(clientObject);
                    clientObject.Start();
                    
                    Console.WriteLine("Connected");//////////////////
                }
            }
            catch (Exception ex)
            {
              //  Console.WriteLine(ex.Message);
                //    Console.WriteLine(ex.Message);
                Disconnect();
            }
        }

        protected internal void Disconnect()
        {
            tcpListener.Stop();
        }
    }
}
