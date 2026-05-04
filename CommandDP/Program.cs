
// Amir Moeini Rad
// September 2025

// Main Concept: The Command Design Pattern

// In this pattern, we encapsulate a request or command as an object,
// thereby allowing for parameterization of clients.

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
    internal interface ICommand
    {
        void Execute();
        void Undo();
    }


    // ----------------------------------


    // Command Receiver
    // A Light that does actual actions
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
        
        public void Undo() => _light.TurnOff();
    }


    // Second Command
    internal class TurnOffCommand : ICommand
    {
        private Light _light;
        
        public TurnOffCommand(Light light) => _light = light;
        
        public void Execute() => _light.TurnOff();
        
        public void Undo() => _light.TurnOn();
    }


    // ----------------------------------


    // Invoker
    // The device (remote control) that creates a command
    internal class RemoteControl
    {
        #pragma warning disable CS8618
        private ICommand _command;
        #pragma warning restore CS8618
        private Stack<ICommand> _undoStack = new();
        private Stack<ICommand> _redoStack = new();

        public void SetCommand(ICommand command) => _command = command;
        
        public void PressButton()
        {
            _command.Execute();
            _undoStack.Push(_command);
            _redoStack.Clear();
        }
        
        public void Undo()
        {
            if (_undoStack.Count == 0) return;
            var cmd = _undoStack.Pop();
            cmd.Undo();
            _redoStack.Push(cmd);
        }

        public void Redo() 
        {
            if (_redoStack.Count == 0) return;
            var cmd = _redoStack.Pop();
            cmd.Execute();
            _undoStack.Push(cmd);
        }
    }


    // ----------------------------------


    // Client
    // The person using the remote control to turn on/off the light.
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("-------------------------------------");
            Console.WriteLine("The Command Design Pattern in C#.NET.");
            Console.WriteLine("-------------------------------------\n");


            Light light = new(); // Receiver            

            ICommand turnOn = new TurnOnCommand(light); // Command 1
            ICommand turnOff = new TurnOffCommand(light); // Command 2            

            Console.WriteLine("A Remote Control...");
            RemoteControl remoteControl = new(); // Sender or Invoker            
            remoteControl.SetCommand(turnOn);
            remoteControl.PressButton();
            remoteControl.SetCommand(turnOff);
            remoteControl.PressButton();

            // A FIFO (First-In First-Out) Collection or Queue
            Console.WriteLine("\nA Command Queue...");
            Queue<ICommand> commandQueue = [];
            commandQueue.Enqueue(turnOn);
            commandQueue.Enqueue(turnOff);            
            while (commandQueue.Count > 0)
            {
                var cmd = commandQueue.Dequeue();
                cmd.Execute();
            }

            // A LIFO (Last-In First-Out) Collection
            Console.WriteLine("\nA Command Stack for Undo/Redo...");
            // Testing the Undo/Redo feature
            remoteControl.SetCommand(turnOn);
            remoteControl.PressButton();
            Console.Write("Undo... ");
            remoteControl.Undo();
            Console.Write("Redo... ");
            remoteControl.Redo();


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