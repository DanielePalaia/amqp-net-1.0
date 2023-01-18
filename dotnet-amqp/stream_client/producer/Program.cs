using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Amqp;
using Amqp.Framing;
using Amqp.Types;

Console.WriteLine("Hello, World!");
/// message ---> plugin --- convert amqp 1.0 to 091 -> 091 to amqp 1.0

var address = new Address("amqp://guest:guest@localhost:5672");
var connection = new Connection(address);
var session = new Session(connection);

Message message = new Message("Message from .NET 1.0");
message.Properties = new Properties()
{
    MessageId = "1",
    Subject = "test",
    ContentType = "text/plain"
};
message.ApplicationProperties = new ApplicationProperties()
{
    Map = { { "key1", "value1" }, { "key2", 2 } }
};
var sender = new SenderLink(session, "mixing", "/amq/queue/test_amqp1.0");
sender.Send(message);
Console.WriteLine("Sent Hello AMQP!");

sender.Close();
session.Close();
connection.Close();