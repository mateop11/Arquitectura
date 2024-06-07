using Experimental.System.Messaging;
using System;
class Producer
{
    public void SendMessage(string queuePath, string message)
    {
        if (!MessageQueue.Exists(queuePath))
        {
            MessageQueue.Create(queuePath);
        }

        using (MessageQueue queue = new MessageQueue(queuePath))
        {
            queue.Formatter = new XmlMessageFormatter(new String[] { "System.String,mscorlib" });
            queue.Send(message);
            Console.WriteLine("Mensaje enviado a la cola: " + message);
        }
    }
}
