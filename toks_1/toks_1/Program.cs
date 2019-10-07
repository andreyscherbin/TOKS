using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace toks_1
{
    public class Program
    {
        public static void print(String s)
        {
            Console.WriteLine(s);
        }
        static void Main(string[] args)
        {
            core messengerCore = new core();

            String baudName = "9600";
            String portName;

            while (true){

                string[] portNames = messengerCore.getPortNames();

                portName = portNames[0];

                Boolean portIsOpened = false;
                while(!portIsOpened) {
                    print("1 - Connect, 2 - Change port, 3 - Change baud, 0 - Exit");
                    print("Port: " + portName);
                    print("Baud: " + baudName);

                    int t;
                    

                        string input = Console.ReadLine();
                        Scanner scanner = new Scanner(input);
                        t = scanner.nextInt();
                    switch(t) {
                        case 1:
                            if(messengerCore.connect(portName,baudName)) {
                                portIsOpened = true;
                            }
                            break;
                        case 2: {
                            print("Available ports: ");
                            foreach (String el in portNames)
                                print(el);
                            print("Print a name of port:");
                            string nameOfPort = Console.ReadLine();
                            if(portNames.Contains(nameOfPort)) {
                                portName = nameOfPort;
                                print("Port name has been changed.");
                            } else {
                                print("Wrong name of a port!");
                            }
                            break;
                        }
                        case 3: {
                            print("Available bauds: " + String.Join(",",messengerCore.speeds));
                            print("Print a baud: ");
                            string nameOfBaud = Console.ReadLine();
                            Boolean search = messengerCore.speeds.Contains(nameOfBaud);
                            if( search == true) {
                                baudName = nameOfBaud;
                                print("The baud has been chaned.");
                            } else {
                                print("Wrong name of a baud!");
                            }break;
                        }
                        case 0:
                            return;
                    }
                }
                while (portIsOpened)
                {
                    print("1 - Print message, 2 - Close port, 0 - Exit");
                    int t;
                    string input = Console.ReadLine();
                    Scanner scanner = new Scanner(input);
                    t = scanner.nextInt();
                    switch (t)
                    {
                        case 1:
                            print("Print a message:");
                            string message = Console.ReadLine();
                            messengerCore.sendMessage(message);
                            break;
                        case 2:
                            portIsOpened = false;
                            break;
                        case 0:
                            portIsOpened = false;
                            if (messengerCore.stop())
                            {
                                return;
                            }
                            break;
                        default:
                            {
                                print("Wrong input!");
                                break;
                            }
                    }
                }

                    
            }
        }
    }
}
