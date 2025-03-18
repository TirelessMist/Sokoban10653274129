/*
* Name: Alex Emard
* Class: Grade 11 Coding (DMIC30S)
* Program Name: Sokoban
* Due Date:
* Program Description:
* Extra Features:
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
