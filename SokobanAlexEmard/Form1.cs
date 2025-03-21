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
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Security.Policy;
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
        int BOTTOM_OFFSET_Y;

        int BORDER_SIZE;

        Size gridSize = new Size();
        Size cellSize = new Size(25, 25);

        Bitmap graphics = new Bitmap(typeof(Form1), "SokubanCells.png");

        Point workerLocation = new Point(0, 0);
        int level = 1;
        int maxLevels = 5;
        int boxesLeft;

        int imageIndex = -1;

        static string PROGRAM_NAME = "Sokoban";

        bool isControlDown;
        bool isShiftDown;

        Stack<GamePieces[,]> savedStateHistory = new Stack<GamePieces[,]>(); // first in, last off data structure type
        Stack<Point> savedWorkerLocation = new Stack<Point>();
        Stack<int> savedBoxesLeft = new Stack<int>();
        Stack<GamePieces[,]> savedStateRedo = new Stack<GamePieces[,]>();
        Stack<Point> savedWorkerLocRedo = new Stack<Point>();
        Stack<int> savedBoxesLeftRedo = new Stack<int>();


        public Form1()
        {
            InitializeComponent();
            BORDER_SIZE = 100;
            foreach (MenuStrip m in this.Controls.OfType<MenuStrip>())
            {
                DRAW_OFFSET_Y = m.Height;
                AssignMenuClickEvents(m.Items);
                break;
            }
            foreach (StatusStrip s in this.Controls.OfType<StatusStrip>())
            {
                BOTTOM_OFFSET_Y = s.Height;
                break;
            }
            foreach (ToolStripMenuItem item in fileToolStripMenuItem.DropDownItems)
            {
                if (item.Tag != null && item.Tag.Equals("openLevel"))
                {
                    for (int i = 1; i <= maxLevels; i++)
                    {
                        ToolStripMenuItem levelItem = new ToolStripMenuItem("Level " + i);
                        levelItem.Tag = i;
                        levelItem.Click += new EventHandler(OpenLevel);
                        item.DropDownItems.Add(levelItem);
                    }
                }
            }

            this.Text = $"{PROGRAM_NAME} - Level " + level;
        }

        private void OpenLevel(object sender, EventArgs e)
        {
            ToolStripMenuItem item = (ToolStripMenuItem)sender;
            level = (int)item.Tag;
            this.Text = $"{PROGRAM_NAME} - Level " + level;
            ReadMap();
        }
        private void AssignMenuClickEvents(ToolStripItemCollection menuItems)
        {
            foreach (ToolStripItem item in menuItems)
            {
                if (item is ToolStripMenuItem menuItem)
                {
                    menuItem.Click += menuStrip1_ItemClicked;
                    if (menuItem.DropDownItems.Count > 0)
                    {
                        AssignMenuClickEvents(menuItem.DropDownItems);
                    }
                }
            }
        }

        private void menuStrip1_ItemClicked(object sender, EventArgs e)
        {
            if (sender is ToolStripMenuItem t)
            {
                switch (t.Tag)
                {
                    case "restart":
                        {
                            ReadMap();
                            break;
                        }
                    case "quit":
                        {
                            Application.Exit();
                            break;
                        }
                    case "undo":
                        {
                            UndoMove();
                            break;
                        }
                    case "redo":
                        {
                            RedoMove();
                            break;
                        }
                }
            }
        }

        private void ReadMap()
        {
            savedStateHistory.Clear();
            savedWorkerLocation.Clear();
            savedBoxesLeft.Clear();
            savedStateRedo.Clear();
            savedWorkerLocRedo.Clear();
            savedBoxesLeftRedo.Clear();
            // Read the text file, create and fill the gameData array.
            string lineOfText = null;
            boxesLeft = 0;
            try
            {
                StreamReader sr = new StreamReader("../../sokoban_maps.txt");


                while (!sr.EndOfStream) // runs until StreamReader reaches end of file
                {
                    lineOfText = sr.ReadLine();
                    if (lineOfText[0] == '_') continue;
                    if (lineOfText[0] == '@' && lineOfText[1] == Convert.ToChar(level.ToString())) // if you have more than 9 levels, you have to modify this code to check for more than just the next char after '@'.
                    { // if currently-scanned level is same as level we want to load, load it
                        lineOfText = sr.ReadLine();
                        string[] data = lineOfText.Split(',');
                        gridSize.Width = Convert.ToInt16(data[0]);
                        gridSize.Height = Convert.ToInt16(data[1]);
                        gameData = new GamePieces[gridSize.Width, gridSize.Height];

                        for (int row = 0; row < gridSize.Height; row++)
                        {
                            lineOfText = sr.ReadLine();
                            for (int col = 0; col < lineOfText.Length; col++)
                            {
                                if (lineOfText[col] == 'b' || lineOfText[col] == 'B')
                                {
                                    boxesLeft++;
                                }
                                SetCell(new Point(col, row), lineOfText[col]);
                                scoreToolStripStatusLabel.Text = "Score: " + boxesLeft;
                            }
                        }
                    }
                }


                UpdateScore();
                sr.Dispose();
                sr.Close();
                this.ClientSize = new Size(cellSize.Width * gridSize.Width + BORDER_SIZE, cellSize.Height * gridSize.Height + DRAW_OFFSET_Y + BOTTOM_OFFSET_Y + BORDER_SIZE);
                this.CenterToScreen();
                this.Invalidate();
            }
            catch (Exception ex)
            {
                Console.WriteLine("(ReadMap Function) - Error: " + ex.Message);
            }
            ;
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
                workerLocation = point;
            }
            else if (c.Equals('W'))
            {
                gameData[point.X, point.Y] = new GamePieces(point, "workerOnDestination");
                workerLocation = point;
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
                e.Graphics.DrawLine(Pens.Black, i * cellSize.Width + BORDER_SIZE, 0 + BORDER_SIZE, i * cellSize.Width + BORDER_SIZE, ClientRectangle.Bottom + BORDER_SIZE);
            }
            for (int i = 0; i < gridSize.Height; i++) // horizontal lines
            {
                e.Graphics.DrawLine(Pens.Black, 0 + BORDER_SIZE, i * cellSize.Height + BORDER_SIZE, ClientRectangle.Right + BORDER_SIZE, i * cellSize.Height + BORDER_SIZE);
            }

            // Draw graphics to the screen.
            for (int i = 0; i < gridSize.Width; i++)//col
            {
                for (int j = 0; j < gridSize.Height; j++)//row
                {
                    if (gameData[i, j].GetType().Equals(" ")) // fix code to support non-rectangular maps
                    {
                        continue; // skip drawing blank cells. Continue makes it skip over the rest of the code in the loop and go to the next iteration.
                    }
                    Rectangle destRect = new Rectangle(i * cellSize.Width + BORDER_SIZE, j * cellSize.Height + BORDER_SIZE, cellSize.Width, cellSize.Height);
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
                    e.Graphics.DrawImage(graphics, destRect, srcRect, GraphicsUnit.Pixel);

                }
            }
        }


        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            bool hasMoved = false;
            switch (e.KeyCode)
            {
                case Keys.Up:
                case Keys.W:

                    hasMoved = MoveWorker(0, -1);
                    break;

                case Keys.Down:
                case Keys.S:

                    hasMoved = MoveWorker(0, 1);
                    break;

                case Keys.Left:
                case Keys.A:

                    hasMoved = MoveWorker(-1, 0);
                    break;

                case Keys.Right:
                case Keys.D:

                    hasMoved = MoveWorker(1, 0);
                    break;
                case Keys.Z:
                    if (isControlDown && isShiftDown)
                    {
                        RedoMove();
                    }
                    else if (isControlDown && !isShiftDown)
                    {
                        UndoMove();
                    }
                    break;
                case Keys.Y:
                    if (isControlDown)
                    {
                        RedoMove();
                    }
                    break;
                case Keys.ControlKey:
                    {
                        isControlDown = true;
                        break;
                    }
                case Keys.ShiftKey:
                    {
                        isShiftDown = true;
                        break;
                    }

            }
            if (hasMoved)
            {
                Invalidate();
            }
        }
        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.ControlKey:
                    isControlDown = false;
                    break;
                case Keys.ShiftKey:
                    isShiftDown = false;
                    break;
            }
        }
        private bool MoveWorker(int dirX, int dirY)
        {
            SaveForUndo();
            bool validMove = false;
            int x = workerLocation.X;
            int y = workerLocation.Y;

            if (gameData[x + dirX, y + dirY].GetType() == " ") //if worker is moving to blank, set type to worker
            {
                gameData[x + dirX, y + dirY].SetType("worker");
                WorkerCameFrom();
                workerLocation.X += dirX;
                workerLocation.Y += dirY;
                validMove = true;
            }
            else if (gameData[x + dirX, y + dirY].GetType() == "destination") //if worker is moving to destination, set type to workerOnDestination
            {
                gameData[x + dirX, y + dirY].SetType("workerOnDestination");
                WorkerCameFrom();
                workerLocation.X += dirX;
                workerLocation.Y += dirY;
                validMove = true;
            }
            else if (gameData[x + dirX, y + dirY].GetType() == "baggage")
            {

                int bX = x + dirX;
                int bY = y + dirY;
                if (gameData[bX + dirX, bY + dirY].GetType() == " ")
                {
                    gameData[x + dirX, y + dirY].SetType("worker");
                    WorkerCameFrom();
                    BaggageCameFrom(bX, bY);
                    gameData[bX + dirX, bY + dirY].SetType("baggage");
                    workerLocation.X += dirX;
                    workerLocation.Y += dirY;
                    validMove = true;
                }
                else if (gameData[bX + dirX, bY + dirY].GetType() == "destination")
                {
                    gameData[bX, bY].SetType(" ");
                    gameData[bX + dirX, bY + dirY].SetType(" ");
                    gameData[x + dirX, y + dirY].SetType("worker");
                    WorkerCameFrom();
                    workerLocation.X += dirX;
                    workerLocation.Y += dirY;
                    Score();
                    validMove = true;
                }

            }
            else if (gameData[x + dirX, y + dirY].GetType() == "baggageOnDestination")
            {
                int bX = x + dirX;
                int bY = y + dirY;
                if (gameData[bX + dirX, bY + dirY].GetType() == " ")
                {
                    gameData[x + dirX, y + dirY].SetType("worker");
                    WorkerCameFrom();
                    BaggageCameFrom(bX, bY);
                    gameData[bX + dirX, bY + dirY].SetType("baggage");
                    workerLocation.X += dirX;
                    workerLocation.Y += dirY;
                    validMove = true;
                }
                else if (gameData[bX + dirX, bY + dirY].GetType() == "destination")
                {
                    gameData[bX, bY].SetType(" ");
                    gameData[bX + dirX, bY + dirY].SetType(" ");
                    gameData[x + dirX, y + dirY].SetType("worker");
                    WorkerCameFrom();
                    workerLocation.X += dirX;
                    workerLocation.Y += dirY;
                    Score();
                    validMove = true;
                }
            }

            return validMove;


        }
        private void WorkerCameFrom()
        {
            if (gameData[workerLocation.X, workerLocation.Y].GetType() == "worker") // if worker came from worker
            {
                gameData[workerLocation.X, workerLocation.Y].SetType(" ");
            }
            else if (gameData[workerLocation.X, workerLocation.Y].GetType() == "workerOnDestination")
            {
                gameData[workerLocation.X, workerLocation.Y].SetType("destination");
            }
        }
        private void BaggageCameFrom(int bX, int bY)
        {
            if (gameData[bX, bY].GetType() == "baggage") // if worker came from worker
            {
                gameData[bX, bY].SetType("worker");
            }
            else if (gameData[bX, bY].GetType() == "baggageOnDestination")
            {
                gameData[bX, bY].SetType("workerOnDestination");
            }
        }
        private void Score()
        {
            if (boxesLeft - 1 == 0)
            {
                boxesLeft--;
                UpdateScore();
                EndGame("win");
            }
            else
            {
                boxesLeft--;
                UpdateScore();
            }
            UpdateScore();
        }

        private void Form1_HelpButtonClicked(object sender, CancelEventArgs e)
        {
            Process.Start(new ProcessStartInfo("chrome.exe", "https://www.google.com"));
        }

        private void SaveForUndo()
        {
            GamePieces[,] saveData = new GamePieces[gridSize.Width, gridSize.Height];
            Point saveWorkerLoc = new Point(workerLocation.X, workerLocation.Y);
            int saveScore = boxesLeft;
            for (int i = 0; i < gridSize.Width; i++)
            {
                for (int j = 0; j < gridSize.Height; j++)
                {
                    saveData[i, j] = new GamePieces(gameData[i, j].GridPos, gameData[i, j].Type.ToString());
                }
            }
            savedStateHistory.Push(saveData);
            savedWorkerLocation.Push(saveWorkerLoc);
            savedBoxesLeft.Push(saveScore);
        }

        private void UndoMove()
        {
            if (savedStateHistory.Count > 0)
            {
                SaveForRedo();
                GamePieces[,] savedState = savedStateHistory.Pop();
                for (int i = 0; i < gridSize.Width; i++)
                {
                    for (int j = 0; j < gridSize.Height; j++)
                    {
                        gameData[i, j].SetType(savedState[i, j].GetType());
                    }
                }
                workerLocation = savedWorkerLocation.Pop();
                boxesLeft = savedBoxesLeft.Pop();
                UpdateScore();
                Invalidate();
            }
        }
        private void SaveForRedo()
        {
            GamePieces[,] saveData = new GamePieces[gridSize.Width, gridSize.Height];
            Point saveWorkerLoc = new Point(workerLocation.X, workerLocation.Y);
            int saveScore = boxesLeft;
            for (int i = 0; i < gridSize.Width; i++)
            {
                for (int j = 0; j < gridSize.Height; j++)
                {
                    saveData[i, j] = new GamePieces(gameData[i, j].GridPos, gameData[i, j].Type.ToString());
                }
            }
            savedStateRedo.Push(saveData);
            savedWorkerLocRedo.Push(saveWorkerLoc);
            savedBoxesLeftRedo.Push(saveScore);
        }
        private void RedoMove()
        {
            if (savedStateRedo.Count > 0)
            {
                SaveForUndo();
                GamePieces[,] savedState = savedStateRedo.Pop();
                for (int i = 0; i < gridSize.Width; i++)
                {
                    for (int j = 0; j < gridSize.Height; j++)
                    {
                        gameData[i, j].SetType(savedState[i, j].GetType());
                    }
                }
                workerLocation = savedWorkerLocRedo.Pop();
                boxesLeft = savedBoxesLeftRedo.Pop();
                UpdateScore();
                Invalidate();
            }
        }
        private void UpdateScore()
        {
            scoreToolStripStatusLabel.Text = "Boxes Left: " + boxesLeft;
        }
        private void EndGame(string reason)
        {
            switch (reason)
            {
                case "win":
                    {
                        break;
                    }
                case "lose":
                    {
                        break;
                    }
            }
            ShowEndDialog(reason);
        }
        private void ShowEndDialog(string reason)
        {
            endScreenTableLayoutPanel.Visible = true;
            endScreenLevelNoLabel.Text = "Level " + level;
            switch (reason)
            {
                case "win":
                    {
                        endScreenTitleLabel.Text = "You Win!";
                        nextLevelButton.Enabled = true;
                        break;
                    }
                case "lose":
                    {
                        endScreenTitleLabel.Text = "You Lost!";
                        nextLevelButton.Enabled = false;
                        break;
                    }
            }

        }
    }
}
