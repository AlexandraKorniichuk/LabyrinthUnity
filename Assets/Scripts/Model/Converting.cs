using System;

namespace Labyrinth
{
    public class Converting
    {
        public static (int, int) GetDirection(string directionString)
        {
            int i = 0, j = 0;
            if (directionString == "W")
                i = -1;
            else if (directionString == "S")
                i = 1;
            else if (directionString == "D")
                j = 1;
            else if (directionString == "A")
                j = -1;
            return (i, j);
        }
        public static (int, int) GetNewPostion((int i, int j) OldPosition, (int i, int j) direction) =>
            (OldPosition.i + direction.i, OldPosition.j + direction.j);
    }
}