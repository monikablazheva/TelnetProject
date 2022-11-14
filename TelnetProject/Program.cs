using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace TelnetProject
{
    class Program
    {
        static void Main(string[] args)
        {
            int lenght = 4096;
            int portNum = 8023;
            string menu = "Welcome to Telnet Server! Please choose a command: \r\ndate - Shows today's date \r\ntime - Shows current time \r\n \r\nEnter menu to show the Menu again";

            var tcpListener = new TcpListener(IPAddress.Any, portNum);
            tcpListener.Start();
            Console.WriteLine("Listening on port {0}...", portNum);

            byte[] buffer = new byte[lenght];
            using var stream = tcpListener.AcceptTcpClient().GetStream();
            SendToUser(menu);
            while (true)
            {
                int streamLenght = stream.Read(buffer);
                var command = Encoding.UTF8.GetString(buffer, 0, streamLenght);
                Console.WriteLine(command);
                DateTime today = DateTime.Now;
                command = command.ToLower();

                if(command == "date")
                {
                    var date = today.ToLongDateString();
                    SendToUser(date);
                }
                if(command == "time")
                {
                    string time = today.ToString("h:mm:ss tt");
                    SendToUser(time);
                }
                if(command == "year")
                {
                    string year = today.Year.ToString();
                    SendToUser(year);
                }
                if(command == "menu")
                {
                    SendToUser(menu);
                }
            }

            void SendToUser(string message)
            {
                var buffer = Encoding.UTF8.GetBytes(message);
                stream.Write(buffer, 0, message.Length);
            }
        }
    }
}
