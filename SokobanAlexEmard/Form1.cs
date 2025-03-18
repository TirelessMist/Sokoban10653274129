/*
 * Name: Alex Emard
 * Program: Sokoban
 * Due Date:
 * Class: Coding for IDM - P4
 * 
 * Program Description:
 *      Use your worker to push boxes into goals.
 * 
 * Extra features:
 *
 * Coding Requirements:
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

        int DRAW_OFFSET_Y;

        Size gridSize = new Size();
        Size cellSize = new Size(25, 25);

        Bitmap graphics = new Bitmap(typeof(Form1), "SokubanCells.png");

        Point workerLocation = new Point(0, 0);
        int level = 1;

        int imageIndex = -1;



        public Form1()
        {
            InitializeComponent();

            foreach (MenuStrip m in this.Controls.OfType<MenuStrip>())
            {
                DRAW_OFFSET_Y = m.Height;
                break;
            }
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

            try
            {
                StreamReader sr = new StreamReader("../../sokoban_maps.txt");


                while (!sr.EndOfStream) // runs until StreamReader reaches end of file
                {
                    lineOfText = sr.ReadLine();
                    if (lineOfText[0] == '_') continue;
                    if (lineOfText[0] == '@' && lineOfText[1] == Convert.ToChar(level.ToString())) // if you have more than 9 levels, you have to modify this code to check for more than just the next char after '@'.
                    {
                        lineOfText = sr.ReadLine();
                        string[] data = lineOfText.Split(',');
                        gridSize.Width = Convert.ToInt32(data[0]);
                        gridSize.Height = Convert.ToInt32(data[1]);
                        gameData = new GamePieces[gridSize.Width, gridSize.Height];

                        for (int row = 0; row < gridSize.Height; row++)
                        {
                            lineOfText = sr.ReadLine();
                            for (int col = 0; col < lineOfText.Length; col++)
                            {
                                SetCell(new Point(col, row), lineOfText[col]);
                            }
                        }
                    }
                }


                sr.Dispose();
                sr.Close();
                this.ClientSize = new Size(cellSize.Width * gridSize.Width, cellSize.Height * gridSize.Height + DRAW_OFFSET_Y);
                this.CenterToScreen();
                this.Invalidate();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            };
        }

        private void SetCell(Point point, char c)
        {
            // worker, workerOnDestination, baggage, baggageOnDestination, destination, blank
            if (c.Equals(' '))
            {
                gameData[point.X, point.Y] = new GamePieces(point, " ");
            }
            else if (c.Equals('#'))
            {
                gameData[point.X, point.Y] = new GamePieces(point, "wall");
            }
            else if (c.Equals('w'))
            {
                gameData[point.X, point.Y] = new GamePieces(point, "worker");
            }
            else if (c.Equals('W'))
            {
                gameData[point.X, point.Y] = new GamePieces(point, "workerOnDestination");
            }
            else if (c.Equals('b'))
            {
                gameData[point.X, point.Y] = new GamePieces(point, "baggage");
            }
            else if (c.Equals('B'))
            {
                gameData[point.X, point.Y] = new GamePieces(point, "baggageOnDestination");
            }
            else if (c.Equals('D'))
            {
                gameData[point.X, point.Y] = new GamePieces(point, "destination");
            }
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.TranslateTransform(0, DRAW_OFFSET_Y);

            for (int i = 0; i < gridSize.Width; i++) // vertical lines
            {
                e.Graphics.DrawLine(Pens.Black, i * cellSize.Width, 0, i * cellSize.Width, ClientRectangle.Bottom);
            }
            for (int i = 0; i < gridSize.Height; i++) // horizontal lines
            {
                e.Graphics.DrawLine(Pens.Black, 0, i * cellSize.Height, ClientRectangle.Right, i * cellSize.Height);
            }

            // Draw graphics to the screen.
            for (int i = 0; i < gridSize.Width; i++)//col
            {
                for (int j = 0; j < gridSize.Height; j++)//row
                {
                    Rectangle destRect = new Rectangle(i * cellSize.Width, j * cellSize.Height, cellSize.Width, cellSize.Height);
                    Rectangle srcRect;

                    switch (gameData[i, j].GetType())
                    {
                        case "wall":
                            {
                                imageIndex = 3;
                                break;
                            }
                        case "destination":
                            {
                                imageIndex = 2;
                                break;
                            }
                        case "baggage":
                            {
                                imageIndex = 1;
                                break;
                            }
                        case "worker":
                            {
                                imageIndex = 0;
                                break;
                            }
                        case "workerOnDestination":
                            {
                                imageIndex = 0;
                                break;
                            }
                        case "baggageOnDestination":
                            {
                                imageIndex = 1;
                                break;
                            }
                    }

                    srcRect = new Rectangle(imageIndex * cellSize.Width, 0, cellSize.Width, cellSize.Height);
                    e.Graphics.DrawImage(graphics, srcRect, destRect, GraphicsUnit.Pixel);
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ReadMap();
        }
    }
}
