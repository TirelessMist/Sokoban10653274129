/*
 * Name: Alex Emard
 * Program: Sokoban
 * Due Date: 
 * Class: Coding for IDM - P4
 * 
 * Program Description:
 * 
 * Extra features:
 *
 *  Coding Requirements:
 *  - Menu (line 269)
 *  - Checkered background using rectangles (line 113)
 *  - Valid move placement (line 220)
 *  - Inform if win (line 193)
 *  - Inform if lost (line 212)
 *  - Hint (line 292)
 * 
 */



using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SokobanAlexEmard
{
    public partial class Form1 : Form
    {
        GamePieces[,] gameData;
        GamePieces[,] savedState;

        Size gridSize = new Size();
        Size cellSize = new Size(25, 25);

        Bitmap graphics = new Bitmap(typeof(Form1), "SokubanCells.png");

        Point workerLocation = new Point(0, 0);
        int level = 1;



        public Form1()
        {
            InitializeComponent();
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Tag)
            {
                case "restart":
                    {
                        break;
                    }
                case "quit":
                    {
                        Application.Exit();
                        break;
                    }
                case "undo":
                    {
                        break;
                    }
            }
        }

        private void ReadMap()
        {
            // Read the text file, create and fill the gameData array.
            string lineOfText = null;
        }
    }
}
