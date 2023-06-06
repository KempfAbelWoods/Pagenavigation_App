namespace Page_Navigation_App.Connection;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

public class Server
{
    private const int Port = 8888;

    public static void SocketServer()
    {
        byte[] buffer = new byte[1024];

        // IP-Adresse des Servers
        IPAddress ipAddress = IPAddress.Parse("192.168.2.110");

        // Erstelle einen TCP/IP-Socket
        TcpListener listener = new TcpListener(ipAddress, Port);
        listener.Start();

        Console.WriteLine("Warte auf Verbindung...");

        // Warte auf eine eingehende Verbindung
        TcpClient client = listener.AcceptTcpClient();

        Console.WriteLine("Client verbunden.");

        // Erstelle ein NetworkStream-Objekt für die Kommunikation
        NetworkStream networkStream = client.GetStream();

        // Lese die Daten vom Client
        int bytesRead = networkStream.Read(buffer, 0, buffer.Length);
        string dataReceived = Encoding.ASCII.GetString(buffer, 0, bytesRead);

        Console.WriteLine("Daten empfangen: " + dataReceived);

        // Sende eine Antwort an den Client
        string response = "Hallo von Server!";
        byte[] responseData = Encoding.ASCII.GetBytes(response);
        networkStream.Write(responseData, 0, responseData.Length);

        // Schließe die Verbindung
        client.Close();
        listener.Stop();

        Console.WriteLine("Verbindung geschlossen.");
    }
}