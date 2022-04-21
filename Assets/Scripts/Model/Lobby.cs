using System;

namespace Labyrinth
{
    public class Lobby
    {
        public bool IsEndGame = true;
        private ConsoleKey StartGameKey = ConsoleKey.Enter;
        private ConsoleKey PlayAgainKey = ConsoleKey.Spacebar;
        public void OpenLobby()
        {
            ShowGreating();
            InputEnterKey();
            Console.Clear();
        }

        private void ShowGreating()
        {
            Console.BackgroundColor = ConsoleColor.Red;
            Console.WriteLine("Welcome to the labyrinth");
            Console.BackgroundColor = ConsoleColor.Black;

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Rules are simple: ");

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Get out of Labyrynth before time runs out");
            Console.WriteLine("Take the key and find THE RIGHT exist");
            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Use WASD for movement");
            Console.WriteLine($"Your character is '{CellSymbol.PlayerSymbol}', key is '{CellSymbol.KeySymbol}', exits are '{CellSymbol.ExitSymbol}'");
            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Press '{StartGameKey}' to start");
            Console.ForegroundColor = ConsoleColor.White;
        }

        private void InputEnterKey() =>
            InputController.InputKey(StartGameKey);

        public void EndRound()
        {
            WriteResultMessage();
            WriteOfferMessage();
            IsEndGame = !HavePlayAgainKeyInput();
            Console.Clear();
        }

        private void WriteResultMessage()
        {
            if (Game.IsWin)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Congratulations, you made it out");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Your time is up");
            }
            Console.ForegroundColor = ConsoleColor.White;
        }

        private void WriteOfferMessage() => 
            Console.WriteLine($"If you want to play again - press '{PlayAgainKey}'");

        private bool HavePlayAgainKeyInput() =>
            InputController.GetInputKey() == PlayAgainKey;
    }
}