using Cloud;
using Grpc.Core;
using Grpc.Net.Client;

namespace gRPC_Client
{
    public class Client
    {
        public void Greeter()
        {
            var myOptions = new GrpcChannelOptions
            {
                Credentials = ChannelCredentials.Insecure,
            };
            var channel = GrpcChannel.ForAddress("http://localhost:9000");
            var client = new Cloud.Cloud.CloudClient(channel);

            var reply = client.SayHello((new Message
            {
                Body = "Greetings to the server",
            }));
        }
    }
}