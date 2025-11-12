
// Amir Moeini Rad
// September 2025

// Main Concept: Command Design Pattern
// With Help from ChatGPT

// In this pattern, we encapsulate a request or command as an object, thereby allowing for parameterization of clients
// with queues, requests, and operations.

// Parametrization means that we can pass different commands to the same method.
// - Because commands are objects, you can pass them around like data.
// - That means a client (like this program) can:
//   - Queue them → line up a list of actions to run later.
//   - Store them as requests → save what should be done, even if it’s not done immediately.
//   - Treat operations as objects → so you can easily switch, undo, or redo them.

// This pattern decouples the sender of a request from its receiver, enabling more flexible and reusable code.

namespace CommandDP
{
    // Command Interface
    // A general command
    internal interface ICommand
    {
        void Execute();
    }


    // ----------------------------------


    // Command Receiver
    // A Light class or object that does actions (the real work)
    internal class Light
    {
        public void TurnOn() => Console.WriteLine("Light is ON.");
        public void TurnOff() => Console.WriteLine("Light is OFF.");
    }

    
    // ----------------------------------


    // Concrete Commands
    // First Command
    internal class TurnOnCommand : ICommand
    {
        private Light _light;
        public TurnOnCommand(Light light) => _light = light;
        public void Execute() => _light.TurnOn();
    }


    // Second Command
    internal class TurnOffCommand : ICommand
    {
        private Light _light;
        public TurnOffCommand(Light light) => _light = light;
        public void Execute() => _light.TurnOff();
    }


    // ----------------------------------


    // Invoker
    // The device (remote control) that creates a command
    internal class RemoteControl
    {
        #pragma warning disable CS8618
        private ICommand _command;
        #pragma warning restore CS8618

        public void SetCommand(ICommand command) => _command = command;
        public void PressButton() => _command.Execute();
    }


    // ----------------------------------


    // Client
    // The person using the remote control to turn on/off the light.
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("---------------------------------");
            Console.WriteLine("Command Design Pattern in C#.NET.");
            Console.WriteLine("---------------------------------\n");


            Light light = new Light();

            ICommand turnon = new TurnOnCommand(light);
            ICommand turnoff = new TurnOffCommand(light);

            RemoteControl remoteControl = new RemoteControl();
            
            remoteControl.SetCommand(turnon);
            remoteControl.PressButton();

            remoteControl.SetCommand(turnoff);
            remoteControl.PressButton();


            Console.WriteLine("\nDone.");
        }
    }
}

/*

Final Recap:

(1) We define a general command interface.
(2) We define a class or object (in this example: a light) that will perform the real action.
(3) We define concrete commands for the light (turn on and turn off).
(4) We define a class or object for a device (in this example: a remote control) that will invoke commands.
(5) The client class creates a light object and a remote control object.
    Then, the client uses the remote control to send commands to the light.
    The light receives the commands and executes them.

The Main Components in the Command Pattern:
(1) Receiver (2) Command(s) (3) Invoker (4) Client
 
*/