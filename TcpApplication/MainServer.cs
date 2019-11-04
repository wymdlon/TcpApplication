using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Server
{
    public class MainServer
    {
        private TcpListener listener;
        private List<Connection> connections;

        public MainServer()
        {
            connections = new List<Connection>();
        }

        public void AddConnection(Connection client)
        {
            connections.Add(client);
        }

        public void RemoveConnection(string id)
        {
            Connection client = connections.FirstOrDefault(x => x.Id == id);

            if (client != null)
            {
                connections.Remove(client);
            }
        }

        public void BroadcastMessage(string message, string id)
        {
            byte[] data = Encoding.UTF8.GetBytes(message);
            foreach (Connection connection in connections)
            {
                if (connection.Id != id)
                {
                    connection.Stream.Write(data, 0, data.Length);
                }
            }
        }

        public void Listen()
        {
            listener = new TcpListener(IPAddress.Parse("127.0.0.1"), 8080);
            listener.Start();
            Console.WriteLine("Server was started");

            while (true)
            {
                TcpClient client = listener.AcceptTcpClient();

                if (connections.Count > 5)
                {
                    byte[] data = Encoding.UTF8.GetBytes("Server have already filled up");
                    client.GetStream().Write(data, 0, data.Length);
                    Console.WriteLine("New connection was rejected");
                    continue;
                }
                Connection connection = new Connection(client, this);
                Console.WriteLine("New connection was accepted");
                Thread thread = new Thread(new ThreadStart(connection.Process));
                thread.Start();
            }
        }

        public void Disconnect()
        {
            foreach(Connection connection in connections)
            {
                connection.Close();
            }

            listener.Stop();
        }
    }
}
