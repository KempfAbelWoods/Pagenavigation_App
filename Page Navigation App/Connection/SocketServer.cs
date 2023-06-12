using System.Windows.Forms;
using Page_Navigation_App.Configs;
using Page_Navigation_App.DB;

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
        
        string SetIpAddress = "";
        //IP Adresse auslesen
        var (list, err1) = Rw_Settings.ReadwithID("3", Paths.sqlite_path);
        SetIpAddress = Paths.sqlite_path;
        if (err1 != null)
        {
            MessageBox.Show(err1.GetException().Message);
        }
        
        // IP-Adresse des Servers
        IPAddress ipAddress = IPAddress.Parse(SetIpAddress);

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
        int passwordRead = networkStream.Read(buffer, 0, buffer.Length);
        string dataReceived = Encoding.ASCII.GetString(buffer, 0, passwordRead);

        string Password = "";
        //IP Adresse auslesen
        var (list1, err2) = Rw_Settings.ReadwithID("2", Paths.sqlite_path);
        Password = Paths.sqlite_path;
        if (err2 != null)
        {
            MessageBox.Show(err1.GetException().Message);
        }

        if (dataReceived == Password)
        {
            Console.WriteLine("Passwort korrekt! ");
            
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
}