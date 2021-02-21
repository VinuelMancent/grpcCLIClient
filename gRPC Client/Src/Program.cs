using System;
using Logger;
using Logger.Src;

namespace gRPC_Client
{
    class Program
    {
        static void Main(string[] args)
        {
            var log = Manager.Instance;
            log.Info("default", "Starting client!");
            //Console.WriteLine("Starting client!");
            var myClient = new Client();
            myClient.Greeter();
            //myClient.Download("download.txt");
            //myClient.Upload("C#Upload.txt");
            myClient.GetFiles();
        }
    }
}