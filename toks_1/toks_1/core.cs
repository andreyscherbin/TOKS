using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;
using System.Threading;




namespace toks_1
{
     class core
    {
        private SerialPort serialPort;
        private BitStuffing stuffingObject = new BitStuffing();
        private Boolean isPortOpened = false;
        private StringBuilder message = new StringBuilder();

        public  String[] speeds = {"110","300","600","1200","4800","9600","14400","19200","38400",
                                     "57600","115200","128000","256000"};
        public string[] getPortNames()
        {
            return SerialPort.GetPortNames();
        }

        public Boolean connect(String portName, String speedName)
        {
            if (isPortOpened)
            {
                Program.print("Port is already opened!");
                return true;
            }

            try
            {
                Program.print("Trying to open por" + portName + "...");     

                switch (speedName)
                {
                    case "110":
                        serialPort = new SerialPort(portName, 110, Parity.None, 8, StopBits.One);
                        serialPort.Open();
                        break;
                    case "300":
                        serialPort = new SerialPort(portName, 300, Parity.None, 8, StopBits.One);
                        serialPort.Open();
                        break;
                    case "600":
                        serialPort = new SerialPort(portName, 600, Parity.None, 8, StopBits.One);
                        serialPort.Open();
                        break;
                    case "1200":
                        serialPort = new SerialPort(portName, 1200, Parity.None, 8, StopBits.One);
                        serialPort.Open();
                        break;
                    case "4800":
                        serialPort = new SerialPort(portName, 4800, Parity.None, 8, StopBits.One);
                        serialPort.Open();
                        break;
                    case "9600":
                        serialPort = new SerialPort(portName, 9600, Parity.None, 8, StopBits.One);
                        serialPort.Open();
                        break;
                    case "14400":
                        serialPort = new SerialPort(portName, 14400, Parity.None, 8, StopBits.One);
                        serialPort.Open();
                        break;
                    case "19200":
                        serialPort = new SerialPort(portName, 19200, Parity.None, 8, StopBits.One);
                        serialPort.Open();
                        break;
                    case "38400":
                        serialPort = new SerialPort(portName, 38400, Parity.None, 8, StopBits.One);
                        serialPort.Open();
                        break;
                    case "57600":
                        serialPort = new SerialPort(portName, 57600, Parity.None, 8, StopBits.One);
                        serialPort.Open();
                        break;
                    case "115200":
                        serialPort = new SerialPort(portName, 115200, Parity.None, 8, StopBits.One);
                        serialPort.Open();
                        break;
                    case "128000":
                        serialPort = new SerialPort(portName, 128000, Parity.None, 8, StopBits.One);
                        serialPort.Open();
                        break;
                    case "256000":
                        serialPort = new SerialPort(portName, 256000, Parity.None, 8, StopBits.One);
                        serialPort.Open();
                        break;
                    default:
                        Program.print("Error: wrong speed.");
                        return false;
                }

                serialPort.ErrorReceived += new SerialErrorReceivedEventHandler(serialPort_ErrorReceived);
                serialPort.DataReceived += new SerialDataReceivedEventHandler(serialPort_DataReceived);

                isPortOpened = true;
                Program.print("Port has been opened. " + "\nBaud: " + speedName);

            }
            catch (Exception e)
            {

                if (e.GetType().Equals("Port busy"))
                {
                    Program.print("Cannot open port: port is busy.");
                }
                else
                {
                    Program.print("Cannot open port: " + e.GetType());
                }
                return false;
            }

            return true;
        }

        void serialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            byte[] data = new byte[serialPort.BytesToRead];
            serialPort.Read(data, 0, data.Length);
            var message = stuffingObject.Decode(data);
            Program.print(message);
        }

        void serialPort_ErrorReceived(object sender, SerialErrorReceivedEventArgs e)
        {
            Program.print("ErrorReceived");
        }

        public Boolean stop()
        {

            if (!isPortOpened)
            {
                Program.print("Port is already closed!");
                return true;
            }

            try
            {
                Program.print("Trying to close port " + serialPort.PortName + "...");
                serialPort.Close();
                Program.print("Port has been closed.");
                isPortOpened = false;
                return true;

            }
            catch (Exception e)
            {
                Program.print("Cannot close port!");
                return false;
            }
        }

        public Boolean sendMessage(String s)
        {

            Thread myThread = new Thread(() =>
                {
                    try
                    {
                        serialPort.RtsEnable = true;
                        var encoded = stuffingObject.Encode(s);                       
                        serialPort.Write(encoded, 0, encoded.Length);
                        Thread.Sleep(100);
                        serialPort.RtsEnable = false;
                        Program.print("Message was sent");

                    }
                    catch (Exception e)
                    {
                        Program.print("Cannot send message 1: " + e.GetType());
                    }
                });
            myThread.Start();
            return true;
        }

    }
}
