﻿using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.IO;
using Core;
using ChatServer.Roles;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ChatServer
{
    public class ClientObject
    {
        public string Username { get; set; }
        public RoleBase Role { get; set; }
        private TcpClient Client;
        private NetworkStream Stream;
        private Thread WorkerThread;

        public delegate void handler(ClientObject sender, string message);
        public event handler MessageRecieved;

        public ClientObject(TcpClient tcpClient)
        {
            this.Client = tcpClient;
            Stream = tcpClient.GetStream();
            this.Role = new UnknownUser(this);
            this.MessageRecieved += Role.Handle;
        }

        public void Start()
        {
            WorkerThread = new Thread(new ThreadStart(Process));
            WorkerThread.Start();
        }

        private void Process()
        {
            //try
            //{
            while (true)
            {
                //try
                //{
                StreamReader sr = new StreamReader(Stream);
                string message = sr.ReadLine();

                if (message != null && message.Length > 0)
                {
                    Console.WriteLine(message);
                    MessageRecieved(this, message);
                }
                Thread.Sleep(10);
                //}
                //catch (Exception e)
                //{
                //    string message = String.Format("{0}: покинул чат", Person.userName);
                //    Console.WriteLine(message);
                //    Room.BroadcastMessage(this ,message);
                //    break;
                //}
            }
            //}
            //catch (Exception e)
            //{
            //     Console.WriteLine(e.Message);
            // }
            // finally
            // {
            //    Room.RemoveConnection(this);
            //     Close();
            // }
        }

        public void SendMessage(string message)
        {
            StreamWriter sw = new StreamWriter(Stream);
            sw.WriteLine(message + '\n');
            sw.Flush();
            Console.WriteLine(this.Username + ": " + message);
        }

        protected internal void Close()
        {
            WorkerThread?.Abort();
            if (Stream != null)
                Stream.Close();
            if (Client != null)
                Client.Close();
        }
    }
}