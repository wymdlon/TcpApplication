﻿using Server.Models;
using Server.Utilities;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

namespace Server
{
    public class Connection
    {
        internal string Id { get; private set; }
        internal NetworkStream Stream { get; private set; }

        //private string userName;
        private TcpClient client;
        private MainServer server;
        private ISerializer serializer;
        public Circle Circle { get; private set; }

        public Connection(TcpClient tcpClient, MainServer mainServer)
        {
            Id = Guid.NewGuid().ToString();
            client = tcpClient;
            server = mainServer;
            server.AddConnection(this);
            
            Circle = new Circle();
            serializer = new Serializer();
        }

        public void Process()
        {
            try
            {
                Stream = client.GetStream();

                SendState();
                
                while (true)
                {
                    string message = GetMessage();

                    try
                    {


                        //server.CheckIntersection();
                    }
                    catch (Exception e)
                    {
                        Console.Error.WriteLine(e.Message);
                        break;
                    }
                }
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e.Message);
            }
            finally
            {
                server.RemoveConnection(Id);
                Close();
            }
        }

        private void SendState()
        {
            string json = serializer.SerializeConnection(this);

            byte[] data = Encoding.UTF8.GetBytes(json);
            Stream.Write(data, 0, data.Length);
        }

        private string GetMessage()
        {
            byte[] buffer = new byte[256];
            StringBuilder builder = new StringBuilder();
            do
            {
                int bytes = Stream.Read(buffer, 0, buffer.Length);
                builder.Append(Encoding.UTF8.GetString(buffer, 0, bytes));
            }
            while (Stream.DataAvailable);

            return builder.ToString();
        }

        public void Close()
        {
            Stream?.Close();
            client?.Close();
        }
    }
}