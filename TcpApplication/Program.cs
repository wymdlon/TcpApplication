using System;
using System.Threading;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            MainServer server = new MainServer();
            Thread listenThread = new Thread(new ThreadStart(server.Listen));

            try
            {
                listenThread.Start();
            }
            catch (Exception e)
            {
                server.Disconnect();
                Console.WriteLine(e.Message);
            }
        }
    }
}
