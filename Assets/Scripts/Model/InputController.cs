using System;

namespace Labyrinth
{
    public class InputController
    {
        public static ConsoleKey GetInputMovementKey()
        {
            ConsoleKey inputKey;
            do
            {
                inputKey = Console.ReadKey(true).Key;
            } while (inputKey != ConsoleKey.W && inputKey != ConsoleKey.A && inputKey != ConsoleKey.S && inputKey != ConsoleKey.D);
            return inputKey;
        }

        public static void InputKey(ConsoleKey desiredKey)
        {
            ConsoleKey inputKey;
            do
            {
                inputKey = Console.ReadKey(true).Key;
            } while (inputKey != desiredKey);
        }

        public static ConsoleKey GetInputKey() =>
            Console.ReadKey(true).Key;
    }
}