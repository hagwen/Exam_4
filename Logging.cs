using System;
using System.IO;
using System.IO.Ports;

class SerialPortLogger
{
    static void Main()
    {

        using (SerialPort serialPort = new SerialPort("COM7", 921600)) // 921600 is default for debug
        {
            serialPort.Open();
            Console.WriteLine("Port opened");


            using (StreamWriter logger = new StreamWriter("Log.txt", append: true))
            {

                while (true)
                {

                    if (serialPort.BytesToRead > 0)
                    {

                        string logEntry = serialPort.ReadLine();
                        Console.WriteLine(logEntry);


                        logger.WriteLine($"{DateTime.Now}: {logEntry}");
                        logger.Flush();


                        if (logEntry.Contains("Button was pressed"))
                        {

                            logger.WriteLine($"{DateTime.Now}: {logEntry}");
                            logger.Flush();
                        }
                    }
                }
            }
        }
    }
}
