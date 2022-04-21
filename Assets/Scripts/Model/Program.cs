using System;

namespace Labyrinth
{
    class Program
    {
        static void Main(string[] args)
        {
            Lobby lobby = new Lobby();

            lobby.OpenLobby();

            do
            {
                Game game = new Game();
                game.StartNewRound();

                lobby.EndRound();
            } while (!lobby.IsEndGame);
        }
    }
}
