using System;
using System.IO;
using System.IO.Ports;

class SerialPortLogger
{
    static void Main()
    {
        // Open the serial port
        using (SerialPort serialPort = new SerialPort("COM7", 921600)) // 921600 is default for debug
        {
            serialPort.Open();
            Console.WriteLine("Port opened");

            // Open the log file
            using (StreamWriter logger = new StreamWriter("Log.txt", append: true))
            {
                // Loop indefinitely
                while (true)
                {
                    // Check if data is available to read from the serial port
                    if (serialPort.BytesToRead > 0)
                    {
                        // Read a line of data from the serial port
                        string logEntry = serialPort.ReadLine();
                        Console.WriteLine(logEntry); // Print the received data to the console

                        // Write the received data to the log file along with timestamp
                        logger.WriteLine($"{DateTime.Now}: {logEntry}");
                        logger.Flush();

                        // Check if the received data contains information from the Blinky program
                        if (logEntry.Contains("Button was pressed"))
                        {
                            // Log the received data to the file
                            logger.WriteLine($"{DateTime.Now}: {logEntry}");
                            logger.Flush();
                        }
                    }
                }
            }
        }
    }
}
