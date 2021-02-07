using System;

namespace gRPC_Client
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting client!");
            var myClient = new Client();
            myClient.Greeter();
            //myClient.Download("download.txt");
            //myClient.Upload("C#Upload.txt");
            myClient.GetFiles();
        }
    }
}