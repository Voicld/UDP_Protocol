using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

class SiriServer
{
    static void Main()
    {
        UdpClient udpServer = new UdpClient(11000);
        Console.WriteLine("Siri Server is running on port 11000...");

        Dictionary<string, string> responses = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            { "hello", "Hello, how can I help you?" },
            { "how are you", "I'm doing fine, thanks! What about you?" },
            { "how is the weather", "Unfortunately I can't show you the weather."},
            { "what is your name", "I am Siri, your voice assistant." },
            { "goodbye", "Bye! Have a nice day!" },
            { "what do you like", "I'm a machine, how can I even have preferences?" },
            { "amongus", "The impostor is sus ඞ" }
        };

        while (true)
        {
            IPEndPoint remoteEP = new IPEndPoint(IPAddress.Any, 0);
            byte[] receivedBytes = udpServer.Receive(ref remoteEP);
            string receivedText = Encoding.UTF8.GetString(receivedBytes);

            Console.WriteLine($"Client asked: {receivedText}");

            string response = responses.ContainsKey(receivedText.ToLower())
                ? responses[receivedText.ToLower()]
                : "Sorry I didn't understand your question.";

            byte[] responseBytes = Encoding.UTF8.GetBytes(response);
            udpServer.Send(responseBytes, responseBytes.Length, remoteEP);
        }
    }
}
