using System;
using System.Threading;
using System.Net.Sockets;
using System.Text;
using Infrastructure.Utilities;
using Infrastructure.Models;

namespace Client
{
    class Program
    {
        static string userName;
        private const string host = "127.0.0.1";
        private const int port = 8080;
        static TcpClient client;
        static NetworkStream stream;

        static void Main(string[] args)
        {

            client = new TcpClient();
            try
            {
                client.Connect(host, port);
                stream = client.GetStream();

                Thread receiveThread = new Thread(new ThreadStart(ReceiveMessage));
                receiveThread.Start();

                Process();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Disconnect();
            }

            //ISerializer serializer = new Serializer();
            //IDeserializer deserializer = new Deserializer();

            //Activity activity = new Activity();
            //activity.Type = Activity.Types.Left;

            //string str = serializer.Serialize(activity);
            //Console.WriteLine(str);
            //Activity activity1 = deserializer.Deserialize<Activity>(str);
            //Console.WriteLine(activity1);

        }
        
        static void Process()
        {
            while (true)
            {
                var ch = Console.ReadKey(false).Key;
                Activity activity = new Activity();
                switch (ch)
                {
                    case ConsoleKey.UpArrow:
                        activity.Type = Activity.Types.Up;
                        break;
                    case ConsoleKey.DownArrow:
                        activity.Type = Activity.Types.Down;
                        break;
                    case ConsoleKey.RightArrow:
                        activity.Type = Activity.Types.Right;
                        break;
                    case ConsoleKey.LeftArrow:
                        activity.Type = Activity.Types.Left;
                        break;

                    default:
                        continue;
                }
                ISerializer serializer = new Serializer();
                string str = serializer.Serialize(activity);

                byte[] data = Encoding.UTF8.GetBytes(str);
                stream.Write(data, 0, data.Length);
            }
        }
        
        static void ReceiveMessage()
        {
            while (true)
            {
                try
                {
                    byte[] data = new byte[64]; 
                    StringBuilder builder = new StringBuilder();
                    int bytes = 0;
                    do
                    {
                        bytes = stream.Read(data, 0, data.Length);
                        builder.Append(Encoding.UTF8.GetString(data, 0, bytes));
                    }
                    while (stream.DataAvailable);

                    string message = builder.ToString();
                    Console.WriteLine(message);
                }
                catch
                {
                    Console.WriteLine("Connection refused!"); 
                    Console.ReadLine();
                    Disconnect();
                }
            }
        }

        static void Disconnect()
        {
            if (stream != null)
                stream.Close();
            if (client != null)
                client.Close();
        }
    }
}