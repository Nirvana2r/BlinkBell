using System.Device.Gpio;
using System;
using System.Threading;

namespace BlinkBell
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Blinking LED. Press Ctrl+C to end.");
            int pin = 17;
            int button = 27;

            using var controller = new GpioController();
            controller.OpenPin(pin, PinMode.Output);
            controller.OpenPin(button, PinMode.InputPullUp);

            bool ledOn = false;
            bool previousButtonState = true;

            while (true)
            {
                bool currentButtonState = controller.Read(button) == PinValue.Low;

                if (currentButtonState && !previousButtonState)
                {
                    ledOn = !ledOn;
                    controller.Write(pin, ledOn ? PinValue.High : PinValue.Low);
                    Console.WriteLine($"LED turned {(ledOn ? "on" : "off")}");
                }

                previousButtonState = currentButtonState;
                Thread.Sleep(100);
            }
        }
    }
}
