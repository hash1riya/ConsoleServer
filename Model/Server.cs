using System.Diagnostics;
using System.Net.Sockets;
using System.Net;

namespace ConsoleServer.Model;
public class Server
{
    IPEndPoint endp;
    Socket socket;

    public Socket Client;
    public byte[] Buffer = new byte[1024];

    public Server(string ip, int port)
    {
        this.endp = new IPEndPoint(IPAddress.Parse(ip), port);
        this.socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
        this.Client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
        this.socket.Bind(endp);
        this.socket.Listen(10);
    }
    public void ClientAccept() => this.Client = this.socket.Accept();

    public void Stop()
    {
        try
        {
            this.socket.Close();
            this.Client.Close();
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }
    }
    public int Receive() => this.Client.Receive(this.Buffer);
    public void Send(string strSend)
    {
        if (this.socket != null)
            this.Client.Send(System.Text.Encoding.UTF8.GetBytes(strSend));
    }
}