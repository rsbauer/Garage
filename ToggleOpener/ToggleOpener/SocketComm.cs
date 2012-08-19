using System;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using SecretLabs.NETMF.Hardware;
using SecretLabs.NETMF.Hardware.NetduinoPlus;
using Toolbox.NETMF.NET;
using System.Threading;

namespace ToggleOpener
{
    public class SocketComm
    {
        // private InputPort swPin = new InputPort(Pins.GPIO_PIN_D2, true, Port.ResistorMode.PullUp);
        public InputPort garageSensor { get; set; }
        public OutputPort garageRemote { get; set; }
        public IntegratedSocket socket { get; set; }
        private OutputPort led = new OutputPort(Pins.ONBOARD_LED, false);
        const bool ON = false;
        const bool OFF = true;

        public void Start()
        {
            bool garageStatus = false;
            string inbound = String.Empty;
            led.Write(false);

            int loop = 0;
            while (socket.IsConnected)
            {
                try
                {
                    if (garageStatus != garageSensor.Read() || loop == 0)
                    {
                        garageStatus = garageSensor.Read();
                        socket.Send((garageStatus ? "open" : "closed") + "\r\n");
                        loop = 1;
                    }

                    inbound = socket.Receive(false);
                    switch(inbound)
                    {
                        case "TOGGLE":
                            pressRemote();
                            break;
                        case "OPEN":
                            if(!garageStatus)
                                pressRemote();
                            break;
                        case "CLOSE":
                            if (garageStatus)
                                pressRemote();
                            break;
                    }
                }
                catch (Exception ex)
                {
                    // connection probably dropped
                    Debug.Print("Error: " + ex.Message);
                    Debug.Print("Disconnected");
                    socket.Close();
                    return;
                }

                Thread.Sleep(10);
            }
        }


        private void pressRemote()
        {
            led.Write(true);
            garageRemote.Write(ON);
            Thread.Sleep(1000);
            garageRemote.Write(OFF);
            led.Write(false);
            socket.Send("Command issued\r\n");
        }

    }
}
