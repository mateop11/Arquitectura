using Experimental.System.Messaging;
using System;
class QueueManager
{
    private string queuePath;
    private Subscriber subscriber;

    public QueueManager(string path, Subscriber sub)
    {
        queuePath = path;
        subscriber = sub;
    }

    public void StartListening()
    {
        if (!MessageQueue.Exists(queuePath))
        {
            MessageQueue.Create(queuePath);
        }

        using (MessageQueue queue = new MessageQueue(queuePath))
        {
            queue.Formatter = new XmlMessageFormatter(new String[] { "System.String,mscorlib" });
            queue.ReceiveCompleted += new ReceiveCompletedEventHandler(OnMessageReceived);
            queue.BeginReceive();
            Console.WriteLine("Escuchando mensajes en la cola...");
        }
    }

    private void OnMessageReceived(object sender, ReceiveCompletedEventArgs e)
    {
        MessageQueue queue = (MessageQueue)sender;
        Message message = queue.EndReceive(e.AsyncResult);
        string body = message.Body.ToString();

        // Procesar el mensaje
        Console.WriteLine("Mensaje recibido: " + body);

        // Enviar el mensaje por correo
        subscriber.SendEmail(body);

        // Reiniciar la escucha
        queue.BeginReceive();
    }
}
