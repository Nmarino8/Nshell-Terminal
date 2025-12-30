using System;
using System.Collections.Generic;
using System.IO;
using NShell;

namespace NShell.Commands
{
    public class NanoCommand : CommandBase
    {
        public override string Name => "nano";
        public override string Arguments => "<file>";
        public override string Description => "Simple text editor";

        public override void Execute(ShellContext context, string args)
        {
            if (string.IsNullOrWhiteSpace(args))
            {
                Console.WriteLine("Usage: nano <file>");
                return;
            }

            RunEditor(args);
        }

        private void RunEditor(string filePath)
        {
            List<string> lines = File.Exists(filePath)
                ? new List<string>(File.ReadAllLines(filePath))
                : new List<string> { "" };

            int cursorX = 0;
            int cursorY = 0;
            bool running = true;

            Console.CursorVisible = false;

            while (running)
            {
                Draw(lines, cursorX, cursorY);

                ConsoleKeyInfo key = Console.ReadKey(true);

                // CTRL shortcuts
                if (key.Modifiers == ConsoleModifiers.Control)
                {
                    if (key.Key == ConsoleKey.S)
                    {
                        File.WriteAllLines(filePath, lines);
                        continue;
                    }

                    if (key.Key == ConsoleKey.X)
                    {
                        running = false;
                        continue;
                    }
                }

                switch (key.Key)
                {
                    case ConsoleKey.LeftArrow:
                        if (cursorX > 0) cursorX--;
                        break;

                    case ConsoleKey.RightArrow:
                        if (cursorX < lines[cursorY].Length) cursorX++;
                        break;

                    case ConsoleKey.UpArrow:
                        if (cursorY > 0)
                        {
                            cursorY--;
                            cursorX = Math.Min(cursorX, lines[cursorY].Length);
                        }
                        break;

                    case ConsoleKey.DownArrow:
                        if (cursorY < lines.Count - 1)
                        {
                            cursorY++;
                            cursorX = Math.Min(cursorX, lines[cursorY].Length);
                        }
                        break;

                    case ConsoleKey.Enter:
                        string rest = lines[cursorY].Substring(cursorX);
                        lines[cursorY] = lines[cursorY].Substring(0, cursorX);
                        lines.Insert(cursorY + 1, rest);
                        cursorY++;
                        cursorX = 0;
                        break;

                    case ConsoleKey.Backspace:
                        if (cursorX > 0)
                        {
                            lines[cursorY] = lines[cursorY].Remove(cursorX - 1, 1);
                            cursorX--;
                        }
                        else if (cursorY > 0)
                        {
                            cursorX = lines[cursorY - 1].Length;
                            lines[cursorY - 1] += lines[cursorY];
                            lines.RemoveAt(cursorY);
                            cursorY--;
                        }
                        break;

                    default:
                        if (!char.IsControl(key.KeyChar))
                        {
                            lines[cursorY] =
                                lines[cursorY].Insert(cursorX, key.KeyChar.ToString());
                            cursorX++;
                        }
                        break;
                }
            }

            Console.CursorVisible = true;
            Console.Clear();
        }

        private void Draw(List<string> lines, int cursorX, int cursorY)
        {
            Console.Clear();

            foreach (var line in lines)
                Console.WriteLine(line);

            // status bar
            Console.SetCursorPosition(0, Console.WindowHeight - 1);
            Console.Write("Ctrl+S Save | Ctrl+X Exit");

            Console.SetCursorPosition(cursorX, cursorY);
        }
    }
}