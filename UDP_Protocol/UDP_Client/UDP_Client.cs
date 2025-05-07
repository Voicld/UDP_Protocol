using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

class SiriClient
{
    static void Main()
    {
        UdpClient udpClient = new UdpClient();
        udpClient.Connect("localhost", 11000);

        Console.WriteLine("Siri Client is running. Enter your question here ( If you want to leave, type (exit) ):");

        while (true)
        {
            Console.Write("You: ");
            string message = Console.ReadLine();
            if (message.ToLower() == "exit")
                break;

            byte[] sendBytes = Encoding.UTF8.GetBytes(message);
            udpClient.Send(sendBytes, sendBytes.Length);

            IPEndPoint remoteEP = new IPEndPoint(IPAddress.Any, 0);
            byte[] receivedBytes = udpClient.Receive(ref remoteEP);
            string response = Encoding.UTF8.GetString(receivedBytes);

            Console.WriteLine("Siri: " + response);
        }

        udpClient.Close();
    }
}
