// Program.cs
using System;
using HieuCaro;

namespace ConsoleCaro
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Console Caro using HieuCaro DLL ===");
            GameEngine engine = new GameEngine();
            engine.BoardSize = 11; // ví dụ
            engine.ClearBoard();
            Console.WriteLine("Board size: " + engine.BoardSize);

            while (true)
            {
                PrintBoard(engine);
                Console.WriteLine("Player {0}'s turn (you are X=1). Enter move as row,col or 'ai' or 'quit':", engine.CurrentPlayer);
                string line = Console.ReadLine();
                if (line == null) break;
                line = line.Trim().ToLower();
                if (line == "quit") break;
                if (line == "ai")
                {
                    int r, c;
                    bool ok = engine.ComputeAIMove(out r, out c);
                    Console.WriteLine(engine.LastMessage);
                    int w = engine.CheckWin();
                    if (w != 0)
                    {
                        PrintBoard(engine);
                        Console.WriteLine("Player {0} wins!", w);
                        break;
                    }
                    continue;
                }
                // parse row,col
                string[] parts = line.Split(new char[] { ',' });
                if (parts.Length != 2)
                {
                    Console.WriteLine("Invalid input. Use r,c (0-based).");
                    continue;
                }
                int rr, cc;
                if (!int.TryParse(parts[0], out rr) || !int.TryParse(parts[1], out cc))
                {
                    Console.WriteLine("Invalid numbers.");
                    continue;
                }
                bool setok = engine.SetCell(rr, cc, engine.CurrentPlayer);
                if (!setok)
                {
                    Console.WriteLine("Cannot set that cell.");
                    continue;
                }
                int winner = engine.CheckWin();
                if (winner != 0)
                {
                    PrintBoard(engine);
                    Console.WriteLine("Player {0} wins!", winner);
                    break;
                }
                // switch player
                engine.CurrentPlayer = (engine.CurrentPlayer == 1) ? 2 : 1;
            }

            Console.WriteLine("Exiting. Signature: " + engine.Signature);
        }

        static void PrintBoard(GameEngine engine)
        {
            int n = engine.BoardSize;
            int[,] b = engine.Board;
            Console.WriteLine("   " + IndexLine(n));
            for (int r = 0; r < n; r++)
            {
                Console.Write("{0,2} ", r);
                for (int c = 0; c < n; c++)
                {
                    char ch = '.';
                    if (b[r, c] == 1) ch = 'X';
                    else if (b[r, c] == 2) ch = 'O';
                    Console.Write(ch + " ");
                }
                Console.WriteLine();
            }
        }

        static string IndexLine(int n)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            for (int i = 0; i < n; i++)
            {
                sb.Append(i.ToString() + " ");
            }
            return sb.ToString();
        }
    }
}
