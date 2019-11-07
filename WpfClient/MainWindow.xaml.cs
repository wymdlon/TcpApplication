using Infrastructure.Models;
using Infrastructure.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfClient
{
    public partial class MainWindow : Window
    {
        private const string host = "127.0.0.1";
        private const int port = 8080;


        private Ellipse myEllipse;
        private TcpClient tcpClient;
        private NetworkStream stream;
        private Dictionary<string, Client> players;


        public MainWindow()
        {
            InitializeComponent();
            tcpClient = new TcpClient();
            players = new Dictionary<string, Client>();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                tcpClient.Connect(host, port);
                stream = tcpClient.GetStream();

                Thread receiveThread = new Thread(new ThreadStart(HandleOtherPlayers));
                receiveThread.Start();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }


            myEllipse = CreateEllipseMargin(1.0, 2.0);
            Canvas.Children.Add(myEllipse);
        }

        private void HandleOtherPlayers()
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

        private void Disconnect()
        {
            if (stream != null)
                stream.Close();
            if (tcpClient != null)
                tcpClient.Close();
        }

        Ellipse CreateEllipse()
        {
            Ellipse myEllipse = new Ellipse();

            SolidColorBrush mySolidColorBrush = new SolidColorBrush
            {
                Color = Color.FromArgb(255, 255, 255, 0)
            };

            myEllipse.Fill = mySolidColorBrush;
            myEllipse.StrokeThickness = 1;
            myEllipse.Stroke = Brushes.Black;

            myEllipse.Width = 50;
            myEllipse.Height = 50;

            return myEllipse;
        }


        Ellipse CreateEllipseMargin(double desiredLeft, double desiredTop)
        {
            Ellipse ellipse = CreateEllipse();
            Canvas.SetLeft(ellipse, desiredLeft);
            Canvas.SetBottom(ellipse, desiredTop);

            return ellipse;
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            var left = Canvas.GetLeft(myEllipse);
            var bottom = Canvas.GetBottom(myEllipse);


            Activity activity = new Activity();
            switch (e.Key)
            {
                case Key.Up:
                    activity.Type = Activity.Types.Up;
                    break;
                case Key.Down:
                    activity.Type = Activity.Types.Down;
                    break;
                case Key.Right:
                    activity.Type = Activity.Types.Right;
                    break;
                case Key.Left:
                    activity.Type = Activity.Types.Left;
                    break;

                default:
                    return;
            }

            ISerializer serializer = new Serializer();
            string str = serializer.Serialize(activity);

            byte[] data = Encoding.UTF8.GetBytes(str);
            stream.Write(data, 0, data.Length);
        }
    }
}
