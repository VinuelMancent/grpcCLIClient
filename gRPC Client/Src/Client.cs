using System;
using System.Buffers.Text;
using Cloud;
using Grpc.Core;
using Grpc.Net.Client;
using System.IO;
using System.Threading;
using Google.Protobuf;
using Microsoft.VisualBasic.FileIO;
using File = Cloud.File;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace gRPC_Client
{
    public class Client
    {
        private GrpcChannelOptions _myOptions;
        private GrpcChannel _channel;
        private readonly  Cloud.Cloud.CloudClient _client;
        private string _name = "";
        
        public Client()
        {
            this._myOptions = new GrpcChannelOptions
            {
                Credentials = ChannelCredentials.Insecure,
            };
            this._channel = GrpcChannel.ForAddress("http://localhost:9000", _myOptions);
            this._client = new Cloud.Cloud.CloudClient(_channel);
        }

        public Client(string name)
        {
            this._myOptions = new GrpcChannelOptions
            {
                Credentials = ChannelCredentials.Insecure,
            };
            this._channel = GrpcChannel.ForAddress("http://localhost:9000", _myOptions);
            this._client = new Cloud.Cloud.CloudClient(_channel);
            this._name = name;

        }
        //Greeter sends a simple message to the server and prints out its response
        public void Greeter()
        {
            //Send a simple request to the server and save the response in reply
            var reply = _client.SayHello(new Message
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
            var reply = _client.Download(new Message
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
            var reply = _client.Upload(new Cloud.File
            {
                File_ = ByteString.CopyFrom(file),
                Filename = filename,
            });
            Console.WriteLine(reply.Body);
        }

        public string[] GetFiles()
        {
            var reply = _client.GetFiles(new Message
            {
                Body = ".",
            });
            //translate the response into a bytearray containing the json string
            var responseByteArray = Convert.FromBase64String(reply.Body);
            //translate the bytearray into a string in json format
            var jsonString = System.Text.Encoding.Default.GetString(responseByteArray);
            JObject jsonObject = JObject.Parse(jsonString);
            //JsonConvert.DeserializeObject(jsonString);
            foreach (var item in jsonObject)
            {
                Console.WriteLine(item.Key +  ": " + item.Value);
            }
            return null;
        }
        
    }
}