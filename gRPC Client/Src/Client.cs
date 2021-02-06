using System;
using Cloud;
using Grpc.Core;
using Grpc.Net.Client;
using System.IO;
using Google.Protobuf;
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
        //tries to download the given file
        //ToDo add a error message in proto when the file cant be found
        public void Download(string filename)
        {
            var reply = client.Download(new Message
            {
                Body = filename,
            });
            FileSystem.WriteAllBytes(filename, reply.File_.ToByteArray(), false);
        }
        
        //upload gets the filename and uploads it to the server
        //ToDo add a error message when the file cant be uploaded
        public void Upload(string filename)
        {
            var file = FileSystem.ReadAllBytes(filename);
            var reply = client.Upload(new Cloud.File
            {
                File_ = ByteString.CopyFrom(file),
                Filename = filename,
            });
            Console.WriteLine(reply.Body);
        }
        
    }
}