using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text.Json;
using System.Threading.Tasks;
using JsonCarExercise;

namespace JSONExerciseServer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("This is the server");

            TcpListener listener = new TcpListener(System.Net.IPAddress.Any, 10002);
           
            listener.Start();
            while (true)
            {
                TcpClient socket = listener.AcceptTcpClient();
                Console.WriteLine("Incoming client");
                Task.Run((() => { HandleClient(socket); }));

            }

        }

        private static void HandleClient(TcpClient socket)
        {
            NetworkStream ns = socket.GetStream();
            StreamReader reader = new StreamReader(ns);
            StreamWriter writer = new StreamWriter(ns);
            string message = reader.ReadLine();

            Car FromJsonCar = JsonSerializer.Deserialize<Car>(message);

            Console.WriteLine("Client said:" + FromJsonCar.Color + FromJsonCar.Model + FromJsonCar.registrationNumber);

            writer.WriteLine("Car received");
            writer.Flush();
            socket.Close();
        }
    }
}
