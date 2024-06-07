using System;

class Program
{
    static void Main()
    {
        string queuePath = @".\Private$\TestQueue";
        Producer producer = new Producer();
        Subscriber subscriber = new Subscriber("smtp.gmail.com", 587, "tu-email@gmail.com", "tu-contraseña");
        QueueManager queueManager = new QueueManager(queuePath, subscriber);

        // Enviar un mensaje a la cola
        producer.SendMessage(queuePath, "Hola, este es un mensaje de prueba.");

        // Comenzar a escuchar mensajes en la cola
        queueManager.StartListening();

        Console.WriteLine("Presiona Enter para salir.");
        Console.ReadLine();
    }
}
