/*
 * Name: Alex Emard
 * Program: Sokoban
 * Due Date: Friday, March 28, 2025
 * Class: Coding for IDM - P4
 * 
 * Program Description:
 *      A game where you use your worker to push boxes into goals.
 * 
 * Extra features:
 *      - You can use Ctrl+Z and Ctrl+Y / Ctrl+Shift+Z to undo and redo moves.
 *      - Score counter
 *      - Moves counter
 *      - Modified readmap code to support non-rectangular maps
 *
 * Coding Requirements:
 *      - The game
 *      - Movement
 *      - Undo/redo
 *      - A winner
 *      - Get to next level
 *      - Restart, Quit, About, and Help menu items
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
        public struct GameState
        {
            public GamePieces[,] GamePieces;
            public Point WorkerLocation;
            public int Moves;
            public int BoxesLeft;

            public GameState(GamePieces[,] gamePieces, Point workerLocation, int moves, int boxesLeft)
            {
                GamePieces = gamePieces;
                WorkerLocation = workerLocation;
                Moves = moves;
                BoxesLeft = boxesLeft;
            }
        }

        int DRAW_OFFSET_Y;
        int BOTTOM_OFFSET_Y;

        int BORDER_SIZE;

        Size gridSize = new Size();
        Size cellSize = new Size(25, 25);

        Bitmap graphics = new Bitmap(typeof(Form1), "SokubanCells.png");

        int level = 1;
        int maxLevels = 5;

        int imageIndex = -1;

        static string PROGRAM_NAME = "Sokoban";

        bool isControlDown;
        bool isShiftDown;

        string readmeLines;

        GameState currentState = new GameState();
        Stack<GameState> undoStack = new Stack<GameState>();
        Stack<GameState> redoStack = new Stack<GameState>();

        public Form1()
        {
            InitializeComponent();
            BORDER_SIZE = 100;

            try
            {
                StreamReader reader = new StreamReader("../../../readme.md");
                readmeLines = reader.ReadToEnd();
                Console.WriteLine("Loaded readme.md");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                throw;
            }
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
            level = Convert.ToInt16(item.Tag);
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
        private void RestartGame()
        {
            ReadMap();
            currentState.Moves = 0;
            this.Focus();
        }

        private void menuStrip1_ItemClicked(object sender, EventArgs e)
        {
            if (sender is ToolStripMenuItem t)
            {
                switch (t.Tag)
                {
                    case "restart":
                        {
                            RestartGame();
                            break;
                        }
                    case "quit":
                        {
                            var result = MessageBox.Show("Are you sure you want to quit?", "Quit", buttons: MessageBoxButtons.YesNo);
                            if (result == DialogResult.Yes)
                            {
                                Application.Exit();
                            }
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
                    case "about":
                        MessageBox.Show(readmeLines);
                        break;
                    case "page":
                        {
                            var result = MessageBox.Show("This will open in the default browser", "Help", buttons: MessageBoxButtons.OKCancel);
                            if (result == DialogResult.OK)
                            {
                                Process.Start("explorer", "https://github.com/TirelessMist/Sokoban/wiki/Sokoban-Help-Page");
                            }
                            break;
                        }
                    case "privPol":
                        MessageBox.Show("Privacy Policy: We probably won't sell your personal information.", "Privacy Policy", buttons: MessageBoxButtons.OK);
                        break;
                }
            }
        }

        private void ReadMap()
        {
            this.Text = $"{PROGRAM_NAME} - Level " + level;
            undoStack.Clear();
            redoStack.Clear();
            currentState.Moves = 0;
            // Read the text file, create and fill the gameData array.
            string lineOfText = null;
            currentState.BoxesLeft = 0;
            try
            {
                StreamReader sr = new StreamReader("../../sokoban_maps.txt");


                while (!sr.EndOfStream) // runs until StreamReader reaches end of file
                {
                    lineOfText = sr.ReadLine(); //each index of lineOfText is a single char of that line
                    if (lineOfText[0] == '_') continue;
                    if (lineOfText.Equals(level.ToString())) // if you have more than 9 levels, you have to modify this code to check for more than just the next char after '@'.
                    { // if currently-scanned level is same as level we want to load, load it
                        lineOfText = sr.ReadLine();
                        string[] data = lineOfText.Split(',');
                        gridSize.Width = Convert.ToInt16(data[0]);
                        gridSize.Height = Convert.ToInt16(data[1]);
                        currentState.GamePieces = new GamePieces[gridSize.Width, gridSize.Height];

                        for (int row = 0; row < gridSize.Height; row++)
                        {
                            lineOfText = sr.ReadLine();
                            for (int col = 0; col < lineOfText.Length; col++)
                            {
                                if (lineOfText[col] == 'b' || lineOfText[col] == 'B')
                                {
                                    currentState.BoxesLeft++;
                                }
                                SetCell(new Point(col, row), lineOfText[col]);
                            }
                        }
                    }
                }


                UpdateScore();
                sr.Dispose();
                sr.Close();
                ClientSize = new Size(cellSize.Width * gridSize.Width + BORDER_SIZE * 2, cellSize.Height * gridSize.Height + DRAW_OFFSET_Y + BOTTOM_OFFSET_Y + BORDER_SIZE * 2);
                CenterToScreen();
                Invalidate();
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
                currentState.GamePieces[point.X, point.Y] = new GamePieces(point, " ");
            }
            else if (c.Equals('#'))
            {
                currentState.GamePieces[point.X, point.Y] = new GamePieces(point, "wall");
            }
            else if (c.Equals('w'))
            {
                currentState.GamePieces[point.X, point.Y] = new GamePieces(point, "worker");
                currentState.WorkerLocation = point;
            }
            else if (c.Equals('W'))
            {
                currentState.GamePieces[point.X, point.Y] = new GamePieces(point, "workerOnDestination");
                currentState.WorkerLocation = point;
            }
            else if (c.Equals('b'))
            {
                currentState.GamePieces[point.X, point.Y] = new GamePieces(point, "baggage");
            }
            else if (c.Equals('B'))
            {
                currentState.GamePieces[point.X, point.Y] = new GamePieces(point, "baggageOnDestination");
            }
            else if (c.Equals('D'))
            {
                currentState.GamePieces[point.X, point.Y] = new GamePieces(point, "destination");
            }
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.TranslateTransform(0, DRAW_OFFSET_Y);
            for (int i = -1; i < gridSize.Height; i++)
            { // horizontal lines
                e.Graphics.DrawLine(Pens.Black, BORDER_SIZE, i * cellSize.Height + DRAW_OFFSET_Y + BORDER_SIZE, ClientRectangle.Right - BORDER_SIZE - 1, i * cellSize.Height + DRAW_OFFSET_Y + BORDER_SIZE);
            }
            for (int i = 0; i < gridSize.Width + 1; i++)
            { // vertical lines
                e.Graphics.DrawLine(Pens.Black, i * cellSize.Width + BORDER_SIZE, BORDER_SIZE, i * cellSize.Width + BORDER_SIZE, ClientRectangle.Bottom - BORDER_SIZE - BOTTOM_OFFSET_Y - 25);
            }

            // Draw graphics to the screen.
            for (int i = 0; i < gridSize.Width; i++)//col
            {
                for (int j = 0; j < gridSize.Height; j++)//row
                {
                    if (currentState.GamePieces[i, j] == null)
                    {
                        continue;
                    }

                    if (currentState.GamePieces[i, j].GetType().Equals(" ")) // fix code to support non-rectangular maps
                    {
                        continue; // skip drawing blank cells. Continue makes it skip over the rest of the code in the loop and go to the next iteration.
                    }
                    Rectangle destRect = new Rectangle(i * cellSize.Width + BORDER_SIZE, j * cellSize.Height + BORDER_SIZE, cellSize.Width, cellSize.Height);
                    Rectangle srcRect;

                    switch (currentState.GamePieces[i, j].GetType())
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
                currentState.Moves++;
                UpdateMovesCounter();
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
            int x = currentState.WorkerLocation.X;
            int y = currentState.WorkerLocation.Y;

            if (currentState.GamePieces[x + dirX, y + dirY].GetType() == " ") //if worker is moving to blank, set type to worker
            {
                currentState.GamePieces[x + dirX, y + dirY].SetType("worker");
                WorkerCameFrom();
                currentState.WorkerLocation.X += dirX;
                currentState.WorkerLocation.Y += dirY;
                validMove = true;
            }
            else if (currentState.GamePieces[x + dirX, y + dirY].GetType() == "destination") //if worker is moving to destination, set type to workerOnDestination
            {
                currentState.GamePieces[x + dirX, y + dirY].SetType("workerOnDestination");
                WorkerCameFrom();
                currentState.WorkerLocation.X += dirX;
                currentState.WorkerLocation.Y += dirY;
                validMove = true;
            }
            else if (currentState.GamePieces[x + dirX, y + dirY].GetType() == "baggage")
            {

                int bX = x + dirX;
                int bY = y + dirY;
                if (currentState.GamePieces[bX + dirX, bY + dirY].GetType() == " ")
                {
                    currentState.GamePieces[x + dirX, y + dirY].SetType("worker");
                    WorkerCameFrom();
                    BaggageCameFrom(bX, bY);
                    currentState.GamePieces[bX + dirX, bY + dirY].SetType("baggage");
                    currentState.WorkerLocation.X += dirX;
                    currentState.WorkerLocation.Y += dirY;
                    validMove = true;
                }
                else if (currentState.GamePieces[bX + dirX, bY + dirY].GetType() == "destination")
                {
                    currentState.GamePieces[bX, bY].SetType(" ");
                    currentState.GamePieces[bX + dirX, bY + dirY].SetType(" ");
                    currentState.GamePieces[x + dirX, y + dirY].SetType("worker");
                    WorkerCameFrom();
                    currentState.WorkerLocation.X += dirX;
                    currentState.WorkerLocation.Y += dirY;
                    Score();
                    validMove = true;
                }

            }
            else if (currentState.GamePieces[x + dirX, y + dirY].GetType() == "baggageOnDestination")
            {
                int bX = x + dirX;
                int bY = y + dirY;
                if (currentState.GamePieces[bX + dirX, bY + dirY].GetType() == " ")
                {
                    currentState.GamePieces[x + dirX, y + dirY].SetType("worker");
                    WorkerCameFrom();
                    BaggageCameFrom(bX, bY);
                    currentState.GamePieces[bX + dirX, bY + dirY].SetType("baggage");
                    currentState.WorkerLocation.X += dirX;
                    currentState.WorkerLocation.Y += dirY;
                    validMove = true;
                }
                else if (currentState.GamePieces[bX + dirX, bY + dirY].GetType() == "destination")
                {
                    currentState.GamePieces[bX, bY].SetType(" ");
                    currentState.GamePieces[bX + dirX, bY + dirY].SetType(" ");
                    currentState.GamePieces[x + dirX, y + dirY].SetType("worker");
                    WorkerCameFrom();
                    currentState.WorkerLocation.X += dirX;
                    currentState.WorkerLocation.Y += dirY;
                    Score();
                    validMove = true;
                }
            }

            return validMove;


        }
        private void WorkerCameFrom()
        {
            if (currentState.GamePieces[currentState.WorkerLocation.X, currentState.WorkerLocation.Y].GetType() == "worker") // if worker came from worker
            {
                currentState.GamePieces[currentState.WorkerLocation.X, currentState.WorkerLocation.Y].SetType(" ");
            }
            else if (currentState.GamePieces[currentState.WorkerLocation.X, currentState.WorkerLocation.Y].GetType() == "workerOnDestination")
            {
                currentState.GamePieces[currentState.WorkerLocation.X, currentState.WorkerLocation.Y].SetType("destination");
            }
        }
        private void BaggageCameFrom(int bX, int bY)
        {
            if (currentState.GamePieces[bX, bY].GetType() == "baggage") // if worker came from worker
            {
                currentState.GamePieces[bX, bY].SetType("worker");
            }
            else if (currentState.GamePieces[bX, bY].GetType() == "baggageOnDestination")
            {
                currentState.GamePieces[bX, bY].SetType("workerOnDestination");
            }
        }
        private void Score()
        {
            if (currentState.BoxesLeft - 1 == 0)
            {
                currentState.BoxesLeft--;
                UpdateScore();
                EndGame("win");
            }
            else
            {
                currentState.BoxesLeft--;
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
            for (int i = 0; i < gridSize.Width; i++)
            {
                for (int j = 0; j < gridSize.Height; j++)
                {
                    if (currentState.GamePieces[i, j] != null)
                    {
                        saveData[i, j] = new GamePieces(currentState.GamePieces[i, j].GridPos, currentState.GamePieces[i, j].Type.ToString());
                    }
                }
            }
            undoStack.Push(new GameState(saveData, currentState.WorkerLocation, currentState.Moves, currentState.BoxesLeft));
        }

        private void UndoMove()
        {
            if (undoStack.Count > 0)
            {
                SaveForRedo();
                GameState temp = undoStack.Pop();
                GamePieces[,] savedState = temp.GamePieces;
                for (int i = 0; i < gridSize.Width; i++)
                {
                    for (int j = 0; j < gridSize.Height; j++)
                    {
                        if (savedState[i, j] != null)
                        {
                            currentState.GamePieces[i, j].SetType(savedState[i, j].GetType());
                        }
                    }
                }
                currentState.Moves = temp.Moves;
                currentState.WorkerLocation = temp.WorkerLocation;
                currentState.BoxesLeft = temp.BoxesLeft;
                UpdateScore();
                UpdateMovesCounter();
                Invalidate();
            }
        }
        private void SaveForRedo()
        {
            GamePieces[,] saveData = new GamePieces[gridSize.Width, gridSize.Height];
            for (int i = 0; i < gridSize.Width; i++)
            {
                for (int j = 0; j < gridSize.Height; j++)
                {
                    if (currentState.GamePieces[i, j] != null)
                    {
                        saveData[i, j] = new GamePieces(currentState.GamePieces[i, j].GridPos, currentState.GamePieces[i, j].Type.ToString());
                    }
                }
            }
            redoStack.Push(new GameState(saveData, currentState.WorkerLocation, currentState.Moves, currentState.BoxesLeft));
        }
        private void RedoMove()
        {
            if (redoStack.Count > 0)
            {
                SaveForUndo();
                GameState temp = redoStack.Pop();
                GamePieces[,] savedState = temp.GamePieces;
                for (int i = 0; i < gridSize.Width; i++)
                {
                    for (int j = 0; j < gridSize.Height; j++)
                    {
                        if (savedState[i, j] != null)
                        {
                            currentState.GamePieces[i, j].SetType(savedState[i, j].GetType());
                        }
                    }
                }
                currentState.Moves = temp.Moves;
                currentState.WorkerLocation = temp.WorkerLocation;
                currentState.BoxesLeft = temp.BoxesLeft;
                UpdateScore();
                UpdateMovesCounter();
                Invalidate();
            }
        }
        private void UpdateScore()
        {
            scoreToolStripStatusLabel.Text = "Boxes Left: " + currentState.BoxesLeft;
        }

        private void UpdateMovesCounter()
        {
            movesCounterToolStripStatusLabel.Text = "Moves: " + currentState.Moves;
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

        private void endScreenButton_Click(object sender, EventArgs e)
        {
            endScreenTableLayoutPanel.Visible = false;
            Button b = (Button)sender;
            switch (b.Tag)
            {
                case "mainMenu":
                    Application.Restart();
                    break;
                case "restart":
                    RestartGame();
                    break;
                case "nextLevel":
                    level++;
                    RestartGame();
                    break;
            }
        }
    }
}
