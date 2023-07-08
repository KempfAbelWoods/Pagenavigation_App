using System.Collections.Generic;
using System.Diagnostics;
using System.Text.Json;
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
    private const int Port = 8080;

    public async static void SocketServer()
    {
        byte[] buffer = new byte[1024];
        byte[] responseData;
        int readReceived;
        string dataReceived;
        string freigabe;
        string setIpAddress = "";

        //IP Adresse auslesen
        var (list, err1) = Rw_Settings.ReadwithID("3", Paths.sqlite_path);
        if (list.Count == 1)
        {
            setIpAddress = list[0].Ressource;
        }

        if (err1 != null)
        {
            MessageBox.Show(err1.GetException().Message);
        }

        // IP-Adresse des Servers
        IPAddress ipAddress = IPAddress.Parse(setIpAddress);

        // Erstelle einen TCP/IP-Socket
        TcpListener listener = new TcpListener(ipAddress, Port);
        listener.Start();

        // Warte auf eine eingehende Verbindung
        TcpClient client = await listener.AcceptTcpClientAsync();

        // Erstelle ein NetworkStream-Objekt für die Kommunikation
        NetworkStream networkStream = client.GetStream();

        // Connection Code überprüfen und anschließend Freigabe senden
        readReceived = await networkStream.ReadAsync(buffer, 0, buffer.Length);
        dataReceived = Encoding.ASCII.GetString(buffer, 0, readReceived);
        if (dataReceived == Paths.ConnectionCode)
        {
            // Hier Freigabe schicken
            freigabe = "Freigabe";
            responseData = Encoding.ASCII.GetBytes(freigabe);
            await networkStream.WriteAsync(responseData, 0, responseData.Length);
        }
        else
        {
            var (list1, err2) = Rw_Users.ReadwithUserName(dataReceived, Paths.sqlite_path);
            if (list1.Count == 1)
            {
                // Hier Freigabe schicken
                freigabe = "Freigabe";
                responseData = Encoding.ASCII.GetBytes(freigabe);
                await networkStream.WriteAsync(responseData, 0, responseData.Length);
            }
            else if (list1.Count == 0)
            {
                freigabe = "Falscher User";
                responseData = Encoding.ASCII.GetBytes(freigabe);
                await networkStream.WriteAsync(responseData, 0, responseData.Length);
            }

            if (err2 != null)
            {
                MessageBox.Show(err2.GetException().Message);
            }
        }

        // Lese die Tasks Daten vom Client
        readReceived = await networkStream.ReadAsync(buffer, 0, buffer.Length);
        dataReceived = Encoding.ASCII.GetString(buffer, 0, readReceived);
        var Taskslist = new ConnectionHelper().Tasks;
        Taskslist = JsonSerializer.Deserialize<List<Db_Tasks>>(dataReceived);
        Rw_Tasks.Write(Taskslist, Paths.sqlite_path);
        
        //Empfangene Daten auswerten
        //TODO hier nicht Passwort von Settings lesen sondern in der DB nach User suchen und dem sein PW abfragen!!!!
        if (dataReceived != "")
        {
            // Sende Users und Tasks an den Client

            var (response, err) = ConnectionHelper.SendMessage(Paths.sqlite_path);
            if (err != null)
            {
                MessageBox.Show(err.GetException().Message);
            }

            ConnectionHelper.SendMessage(response);

            responseData = Encoding.ASCII.GetBytes(response);
            await networkStream.WriteAsync(responseData, 0, responseData.Length);

            // Schließe die Verbindung
            client.Close();
            listener.Stop();

            Trace.WriteLine("Verbindung geschlossen.");
            MessageBox.Show("Verbindung geschlossen.");
        }
        else if (dataReceived == "")
        {
            var (list1, err2) = Rw_Users.ReadwithUserName(dataReceived, Paths.sqlite_path);
            if (list1.Count == 1)
            {
                // Sende Users und Tasks an den Client

                var (response, err) = ConnectionHelper.SendMessage(Paths.sqlite_path);
                if (err != null)
                {
                    MessageBox.Show(err.GetException().Message);
                }

                ConnectionHelper.SendMessage(response);

                responseData = Encoding.ASCII.GetBytes(response);
                await networkStream.WriteAsync(responseData, 0, responseData.Length);

                // Schließe die Verbindung
                client.Close();
                listener.Stop();

                Trace.WriteLine("Verbindung geschlossen.");
                MessageBox.Show("Verbindung geschlossen.");
            }

            if (err2 != null)
            {
                MessageBox.Show(err2.GetException().Message);
            }
        }
    }
}