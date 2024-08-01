using System.Diagnostics;

namespace ConsoleServer;
internal class Program
{
    public static void Main(string[] args)
    {
        string ip = "127.0.0.1";
        int port = 1024;
        Model.Server server = new Model.Server(ip, port);

        Console.WriteLine(
            $"[{DateTime.Now.ToString("dd/MM/yyyy HH:mm")}] " +
            $"SYSTEM: Server was setted up. Waiting for client...");

        string sendStr;
        int bytesRead;
        while (true)
        {
            try
            {
                if (!server.Client.Connected)
                {
                    server.ClientAccept();
                    Console.WriteLine(
                            $"[{DateTime.Now.ToString("dd/MM/yyyy HH:mm")}] " +
                            $"SYSTEM: " +
                            $"{server.Client.RemoteEndPoint} connected!");

                    bytesRead = server.Receive();
                    Console.WriteLine(
                            $"[{DateTime.Now.ToString("dd/MM/yyyy HH:mm")}] " +
                            $"{server.Client.RemoteEndPoint}: " +
                            $"{System.Text.Encoding.UTF8.GetString(server.Buffer, 0, bytesRead)}");

                    if (System.Text.Encoding.UTF8.GetString(server.Buffer, 0, bytesRead).ToLower() == "getdate")
                    {
                        sendStr = DateTime.Now.ToString("dd/MM/yyyy");
                        server.Send(sendStr);
                        Console.WriteLine(
                            $"[{DateTime.Now.ToString("dd/MM/yyyy HH:mm")}] " +
                            $"me: " +
                            $"{sendStr}");
                    }
                    else if (System.Text.Encoding.UTF8.GetString(server.Buffer, 0, bytesRead).ToLower() == "gettime")
                    {
                        sendStr = DateTime.Now.ToString("HH:mm:ss");
                        server.Send(sendStr);
                        Console.WriteLine(
                            $"[{DateTime.Now.ToString("dd/MM/yyyy HH:mm")}] " +
                            $"me: " +
                            $"{sendStr}");
                    }
                    else
                    {
                        sendStr = "Unrecognized command!";
                        server.Send(sendStr);
                        Console.WriteLine(
                            $"[{DateTime.Now.ToString("dd/MM/yyyy HH:mm")}] " +
                            $"me: " +
                            $"{sendStr}");
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Console.WriteLine(
                        $"[{DateTime.Now.ToString("dd/MM/yyyy HH:mm")}] " +
                        $"SYSTEM: " +
                        $"{server.Client.RemoteEndPoint} disconnected!\n");
            }
        }
    }
}