using System;

namespace Labyrinth
{
    public class Converting
    {
        public static (int, int) GetDirection(string directionString)
        {
            int i = 0, j = 0;
            if (directionString == "W")
                j = 1;
            else if (directionString == "S")
                j = -1;
            else if (directionString == "D")
                i = 1;
            else if (directionString == "A")
                i = -1;
            return (i, j);
        }
        public static (int, int) GetNewPosition((int i, int j) OldPosition, (int i, int j) direction) =>
            (OldPosition.i + direction.i, OldPosition.j + direction.j);
    }
}