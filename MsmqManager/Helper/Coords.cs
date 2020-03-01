using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MsmqManager.Helper
{
    public struct Coords
    {
        public (int X, int Y) Size { get; set; }
        public (int X, int Y) Position { get; set; }
        public Coords(int sizeX, int sizeY, int positionX, int positionY)
        {
            Size = (sizeX, sizeY);
            Position = (positionX, positionY);
        }

        public Coords((int,int) size, (int, int) positon)
        {
            Size = size;
            Position = positon;
        }
    }
}
