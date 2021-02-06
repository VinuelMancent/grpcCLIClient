using System;

namespace gRPC_Client
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var myClient = new Client();
            myClient.Greeter();
        }
    }
}