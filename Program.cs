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

        string sendStr = "Hello client!";
        int bytesRead;
        while (true)
        {
            try
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

                server.Send(sendStr);
                Console.WriteLine(
                    $"[{DateTime.Now.ToString("dd/MM/yyyy HH:mm")}] " +
                    $"me: " +
                    $"{sendStr}");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            finally
            {
                Console.WriteLine(
                        $"[{DateTime.Now.ToString("dd/MM/yyyy HH:mm")}] " +
                        $"SYSTEM: " +
                        $"{server.Client.RemoteEndPoint} disconnected!\n");
            }
        }
    }
}