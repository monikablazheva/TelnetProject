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

            var tcpListener = new TcpListener(IPAddress.Any, portNum);
            tcpListener.Start();
            Console.WriteLine("Listening on port {0}...", portNum);

            byte[] buffer = new byte[lenght];
            using var stream = tcpListener.AcceptTcpClient().GetStream();
            while (true)
            {
                SendToUser("Welcome to Telnet Server!");
                int streamLenght = stream.Read(buffer);
                var command = Encoding.UTF8.GetString(buffer, 0, streamLenght);
                //Process command
                //Response to user
            }

            void SendToUser(string message)
            {
                var buffer = Encoding.UTF8.GetBytes(message);
                stream.Write(buffer, 0, message.Length);
            }
        }
    }
}
