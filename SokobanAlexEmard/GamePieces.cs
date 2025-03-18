using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SokobanAlexEmard
{
    internal class GamePieces
    {
        private Point gridPos;
        private string type;

        public GamePieces()
        {
            Point gridPos = new Point(-1, -1);
            type = " ";
        }

        public GamePieces(Point gridPos, string type)
        {
            this.gridPos = gridPos;
            this.type = type;
        }

        public Point GridPos { get { return gridPos; } set { this.gridPos = value; } }

        public string Type { get { return type; } set { type = value; } }
        public string GetType() { return type; }
        public void SetType(string type)
        {
            this.type = type;
        }
    }
}
