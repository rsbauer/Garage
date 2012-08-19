using System;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using SecretLabs.NETMF.Hardware;
using SecretLabs.NETMF.Hardware.NetduinoPlus;
using Toolbox.NETMF.NET;
using System.Threading;
using System.Net.Sockets;
using System.Net;
using Socket = System.Net.Sockets.Socket;

namespace ToggleOpener
{
    public class Program
    {

        public static void Main()
        {
            /*
            OutputPort garageOpener = new OutputPort(Pins.GPIO_PIN_D3, false);
            OutputPort led = new OutputPort(Pins.ONBOARD_LED, false);

            // using pull down resistor (ground connected to resistor vs gpio pin connected to resistor first)
            // doing so causes true/false to flip
            garageOpener.Write(true);

            for (int a = 0; a < 5; a++)
            {
                led.Write(true);
                Thread.Sleep(500);
                led.Write(false);
                Thread.Sleep(500);
            }

            while (true)
            {
                garageOpener.Write(false);
                led.Write(true);
                Thread.Sleep(5000);
                garageOpener.Write(true);
                led.Write(false);
                Thread.Sleep(5000);
            }
             */

            // startSocketListener();
            OutputPort garageRemote = new OutputPort(Pins.GPIO_PIN_D3, false);
            // using pull down resistor (ground connected to resistor vs gpio pin connected to resistor first)
            // doing so causes true/false to flip
            garageRemote.Write(true);

            SocketComm socketComm = new SocketComm();
            InputPort swPin = new InputPort(Pins.GPIO_PIN_D2, true, Port.ResistorMode.PullUp);
            IntegratedSocket socket = new IntegratedSocket("", 23);

            while (true)
            {
                socketComm.garageSensor = swPin;
                socketComm.garageRemote = garageRemote;
                socketComm.socket = socket;
                socket.Listen();
                socketComm.Start();
                socket.Close();
            }

            /*
            Socket server = new Socket(AddressFamily.InterNetwork,
                SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint localEndPoint = new IPEndPoint(IPAddress.Any, 23);
            server.Bind(localEndPoint);
            server.Listen(Int32.MaxValue);

            while (true)
            {
                // Wait for a client to connect.
                Socket clientSocket = server.Accept();

                // Process the client request.  true means asynchronous.
                Debug.Print("Connected");
            }
            */
        }
    }
}
