// Form1.cs
using System;
using System.Drawing;
using System.Windows.Forms;
using HieuCaro;

namespace WindowsFormCaro
{
    public partial class Form1 : Form
    {
        private GameEngine engine;
        private Button[,] btns;
        private int cellSize = 30;
        private Panel panelBoard;
        private Button btnAIMove;
        private Button btnClear;
        private Label lblMsg;

        public Form1()
        {
            this.Text = "WindowsFormCaro - uses hieucaro.dll";
            this.ClientSize = new Size(800, 600);
            engine = new GameEngine();
            engine.BoardSize = 11;
            InitializeComponents();
            BuildBoard();
        }

        private void InitializeComponents()
        {
            panelBoard = new Panel();
            panelBoard.Location = new Point(10, 10);
            panelBoard.AutoScroll = true;
            panelBoard.Size = new Size(600, 500);
            this.Controls.Add(panelBoard);

            btnAIMove = new Button();
            btnAIMove.Text = "AI Move";
            btnAIMove.Location = new Point(630, 30);
            btnAIMove.Click += new EventHandler(btnAIMove_Click);
            this.Controls.Add(btnAIMove);

            btnClear = new Button();
            btnClear.Text = "Clear";
            btnClear.Location = new Point(630, 70);
            btnClear.Click += new EventHandler(btnClear_Click);
            this.Controls.Add(btnClear);

            lblMsg = new Label();
            lblMsg.Text = "Message";
            lblMsg.Location = new Point(630, 110);
            lblMsg.Size = new Size(150, 300);
            this.Controls.Add(lblMsg);
        }

        private void BuildBoard()
        {
            int n = engine.BoardSize;
            btns = new Button[n, n];
            panelBoard.Controls.Clear();
            for (int r = 0; r < n; r++)
            {
                for (int c = 0; c < n; c++)
                {
                    Button b = new Button();
                    b.Size = new Size(cellSize, cellSize);
                    b.Location = new Point(c * cellSize, r * cellSize);
                    b.Tag = r + "," + c;
                    b.Click += new EventHandler(Cell_Click);
                    b.Text = ".";
                    panelBoard.Controls.Add(b);
                    btns[r, c] = b;
                }
            }
            RefreshBoardUI();
        }

        private void Cell_Click(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            string tag = (string)b.Tag;
            string[] parts = tag.Split(',');
            int r = int.Parse(parts[0]);
            int c = int.Parse(parts[1]);
            bool ok = engine.SetCell(r, c, engine.CurrentPlayer);
            if (!ok)
            {
                lblMsg.Text = "Cannot place at (" + r + "," + c + ").";
                return;
            }
            RefreshBoardUI();
            int winner = engine.CheckWin();
            if (winner != 0)
            {
                lblMsg.Text = "Player " + winner + " wins! " + engine.LastMessage;
                return;
            }
            // switch
            engine.CurrentPlayer = (engine.CurrentPlayer == 1) ? 2 : 1;
            lblMsg.Text = "Placed at (" + r + "," + c + "). " + engine.LastMessage;
        }

        private void btnAIMove_Click(object sender, EventArgs e)
        {
            int r, c;
            bool ok = engine.ComputeAIMove(out r, out c);
            lblMsg.Text = engine.LastMessage;
            RefreshBoardUI();
            int winner = engine.CheckWin();
            if (winner != 0)
            {
                MessageBox.Show("Player " + winner + " wins!", "Game Over", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                // switch player (AI played currentPlayer)
                engine.CurrentPlayer = (engine.CurrentPlayer == 1) ? 2 : 1;
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            engine.ClearBoard();
            RefreshBoardUI();
            lblMsg.Text = "Board cleared.";
        }

        private void RefreshBoardUI()
        {
            int n = engine.BoardSize;
            int[,] b = engine.Board;
            for (int r = 0; r < n; r++)
            {
                for (int c = 0; c < n; c++)
                {
                    Button btn = btns[r, c];
                    if (b[r, c] == 0) btn.Text = ".";
                    else if (b[r, c] == 1) btn.Text = "X";
                    else btn.Text = "O";
                }
            }
        }
        
    }
}
