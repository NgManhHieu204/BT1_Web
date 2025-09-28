using System;

namespace HieuCaro
{
    public class GameEngine
    {
        private int boardSize;
        private int[,] board;
        private int currentPlayer;
        private int lastMoveRow = -1;
        private int lastMoveCol = -1;
        private string lastMessage = "";

        private string engineSignature = "HieuCaroEngine v1.0 - by Hieu";

        public GameEngine()
        {
            boardSize = 15;
            InitializeBoard(boardSize);
            currentPlayer = 1;
        }

        public void InitializeBoard(int size)
        {
            if (size < 5) size = 5;
            boardSize = size;
            board = new int[boardSize, boardSize];
            lastMoveRow = -1;
            lastMoveCol = -1;
            lastMessage = "Board initialized. " + engineSignature;
        }

        public int BoardSize
        {
            get { return boardSize; }
            set { InitializeBoard(value); }
        }

        public int[,] Board
        {
            get { return board; }
        }

        public int CurrentPlayer
        {
            get { return currentPlayer; }
            set
            {
                if (value == 1 || value == 2) currentPlayer = value;
            }
        }

        public int LastMoveRow
        {
            get { return lastMoveRow; }
        }

        public int LastMoveCol
        {
            get { return lastMoveCol; }
        }

        public string LastMessage
        {
            get { return lastMessage; }
        }

        public string Signature
        {
            get { return engineSignature; }
        }

        public bool SetCell(int r, int c, int player)
        {
            if (r < 0 || r >= boardSize || c < 0 || c >= boardSize) return false;
            if (player != 1 && player != 2) return false;
            if (board[r, c] != 0) return false;
            board[r, c] = player;
            lastMoveRow = r;
            lastMoveCol = c;
            lastMessage = "Move placed at (" + r + "," + c + ") by player " + player + ". " + engineSignature;
            return true;
        }

        public void ClearBoard()
        {
            int i, j;
            for (i = 0; i < boardSize; i++)
                for (j = 0; j < boardSize; j++)
                    board[i, j] = 0;
            lastMoveRow = -1;
            lastMoveCol = -1;
            lastMessage = "Board cleared. " + engineSignature;
        }

        public int CheckWin()
        {
            if (lastMoveRow == -1) return 0;
            int player = board[lastMoveRow, lastMoveCol];
            if (player == 0) return 0;

            // directions: horizontal, vertical, diag1, diag2
            int[] dr = new int[] { 0, 1, 1, 1 };
            int[] dc = new int[] { 1, 0, 1, -1 };

            int dir;
            for (dir = 0; dir < 4; dir++)
            {
                int count = 1;
                int r, c;
                // forward
                r = lastMoveRow + dr[dir];
                c = lastMoveCol + dc[dir];
                while (r >= 0 && r < boardSize && c >= 0 && c < boardSize && board[r, c] == player)
                {
                    count++;
                    r += dr[dir];
                    c += dc[dir];
                }
                // backward
                r = lastMoveRow - dr[dir];
                c = lastMoveCol - dc[dir];
                while (r >= 0 && r < boardSize && c >= 0 && c < boardSize && board[r, c] == player)
                {
                    count++;
                    r -= dr[dir];
                    c -= dc[dir];
                }
                if (count >= 5) return player;
            }
            return 0;
        }

        // Very simple AI: choose best cell by scanning for immediate wins or blocking opponent
        // Returns Tuple<int row, int col> as out params
        public bool ComputeAIMove(out int row, out int col)
        {
            row = -1;
            col = -1;
            int aiPlayer = currentPlayer;
            int human = (aiPlayer == 1) ? 2 : 1;

            // 1) If AI can win in one move -> play it
            if (FindWinningMove(aiPlayer, out row, out col))
            {
                SetCell(row, col, aiPlayer);
                lastMessage = "AI (" + aiPlayer + ") plays winning move at (" + row + "," + col + "). " + engineSignature;
                return true;
            }

            // 2) If human can win next -> block
            if (FindWinningMove(human, out row, out col))
            {
                SetCell(row, col, aiPlayer);
                lastMessage = "AI (" + aiPlayer + ") blocks at (" + row + "," + col + "). " + engineSignature;
                return true;
            }

            // 3) Else pick center if empty
            int center = boardSize / 2;
            if (board[center, center] == 0)
            {
                SetCell(center, center, aiPlayer);
                lastMessage = "AI (" + aiPlayer + ") takes center (" + center + "," + center + "). " + engineSignature;
                return true;
            }

            // 4) Else heuristic: pick cell maximizing adjacency count
            int bestR = -1, bestC = -1;
            int bestScore = -1;
            int r, c;
            for (r = 0; r < boardSize; r++)
            {
                for (c = 0; c < boardSize; c++)
                {
                    if (board[r, c] != 0) continue;
                    int score = EvaluateCell(r, c, aiPlayer);
                    if (score > bestScore)
                    {
                        bestScore = score;
                        bestR = r; bestC = c;
                    }
                }
            }
            if (bestR != -1)
            {
                SetCell(bestR, bestC, aiPlayer);
                lastMessage = "AI (" + aiPlayer + ") heuristic move at (" + bestR + "," + bestC + "). " + engineSignature;
                return true;
            }
            // fallback: first empty
            for (r = 0; r < boardSize; r++)
            {
                for (c = 0; c < boardSize; c++)
                {
                    if (board[r, c] == 0)
                    {
                        SetCell(r, c, aiPlayer);
                        lastMessage = "AI (" + aiPlayer + ") fallback move at (" + r + "," + c + "). " + engineSignature;
                        return true;
                    }
                }
            }
            return false;
        }

        // Find winning move for player p
        private bool FindWinningMove(int p, out int row, out int col)
        {
            row = -1; col = -1;
            int r, c;
            for (r = 0; r < boardSize; r++)
            {
                for (c = 0; c < boardSize; c++)
                {
                    if (board[r, c] != 0) continue;
                    board[r, c] = p;
                    int savedLastR = lastMoveRow;
                    int savedLastC = lastMoveCol;
                    lastMoveRow = r; lastMoveCol = c;
                    int winner = CheckWin();
                    // restore
                    board[r, c] = 0;
                    lastMoveRow = savedLastR;
                    lastMoveCol = savedLastC;
                    if (winner == p)
                    {
                        row = r; col = c;
                        return true;
                    }
                }
            }
            return false;
        }

        // Evaluate cell simple adjacency (count same-player neighbors)
        private int EvaluateCell(int r0, int c0, int p)
        {
            int score = 0;
            int dr, dc, r, c, i;
            for (dr = -1; dr <= 1; dr++)
            {
                for (dc = -1; dc <= 1; dc++)
                {
                    if (dr == 0 && dc == 0) continue;
                    r = r0 + dr; c = c0 + dc;
                    for (i = 0; i < 3; i++) // look up to 3 steps
                    {
                        if (r >= 0 && r < boardSize && c >= 0 && c < boardSize)
                        {
                            if (board[r, c] == p) score += 2;
                            else if (board[r, c] != 0) score += 1; // adjacent enemy a little
                        }
                        r += dr; c += dc;
                    }
                }
            }
            return score;
        }

        // Helper: export board as simple string (rows separated by ;)
        public string ExportBoardString()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            int r, c;
            for (r = 0; r < boardSize; r++)
            {
                for (c = 0; c < boardSize; c++)
                {
                    sb.Append(board[r, c].ToString());
                    if (c < boardSize - 1) sb.Append(',');
                }
                if (r < boardSize - 1) sb.Append(';');
            }
            return sb.ToString();
        }

        // Helper: import board from exported string (used if external app wants to set entire board)
        public bool ImportBoardString(string s)
        {
            try
            {
                string[] rows = s.Split(new char[] { ';' });
                int rcount = rows.Length;
                if (rcount == 0) return false;
                // if size mismatch, reinitialize
                int cols = rows[0].Split(new char[] { ',' }).Length;
                if (rcount != boardSize || cols != boardSize)
                {
                    InitializeBoard(rcount);
                }
                int r, c;
                for (r = 0; r < boardSize; r++)
                {
                    string[] colsArr = rows[r].Split(new char[] { ',' });
                    for (c = 0; c < boardSize; c++)
                    {
                        int val = 0;
                        int.TryParse(colsArr[c], out val);
                        board[r, c] = val;
                    }
                }
                lastMessage = "Board imported. " + engineSignature;
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
