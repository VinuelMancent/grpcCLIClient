using System;
using Cloud;
using Grpc.Core;
using Grpc.Net.Client;
using System.IO;
using Microsoft.VisualBasic.FileIO;

namespace gRPC_Client
{
    public class Client
    {
        private GrpcChannelOptions myOptions;
        private GrpcChannel channel;
        private Cloud.Cloud.CloudClient client;
        
        public Client()
        {
            this.myOptions = new GrpcChannelOptions
            {
                Credentials = ChannelCredentials.Insecure,
            };
            this.channel = GrpcChannel.ForAddress("http://localhost:9000", myOptions);
            this.client = new Cloud.Cloud.CloudClient(channel);
        }
        //Greeter sends a simple message to the server and prints out its response
        public void Greeter()
        {
            //Send a simple request to the server and save the response in reply
            var reply = client.SayHello(new Message
            {
                Body = "Greetings to the server",
            });
            //Print the response body
            Console.WriteLine(reply.Body);
        }

        public void Download(string filename)
        {
            var reply = client.Download(new Message
            {
                Body = filename,
            });
            FileSystem.WriteAllBytes(filename, reply.File_.ToByteArray(), false);
        }
    }
}