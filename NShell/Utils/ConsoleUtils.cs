using System;
using System.Text;
using System.IO;

namespace NShell.Utils
{
    public static class ConsoleUtils
    {
        public static void PrintWelcome()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("┌── Welcome to NShell ──┐");
            Console.ResetColor();
        }

        private static string GetVirtualPath(ShellContext context)
        {
            var rel = Path.GetRelativePath(context.RootDirectory, context.CurrentDirectory)
                          .Replace("\\", "/");

            if (rel == ".")
                rel = "";

            return string.IsNullOrEmpty(rel) ? "NShell/Root" : $"NShell/Root/{rel}";
        }

        public static void PrintPrompt(ShellContext context)
        {
            string virtualPath = GetVirtualPath(context);

            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write("$");
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.Write("NShell ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write($"[{virtualPath}] ");
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.Write(">> ");
            Console.ResetColor();
        }

        public static string ReadLineWithHistory(ShellContext context)
        {
            var input = new StringBuilder();
            int cursor = 0;

            while (true)
            {
                var key = Console.ReadKey(true);

                if (key.Key == ConsoleKey.Enter)
                {
                    Console.WriteLine();
                    string command = input.ToString();
                    if (!string.IsNullOrWhiteSpace(command))
                    {
                        context.History.Add(command);
                        context.HistoryIndex = context.History.Count; 
                    }
                    return command;
                }
                else if (key.Key == ConsoleKey.Backspace)
                {
                    if (cursor > 0)
                    {
                        input.Remove(cursor - 1, 1);
                        cursor--;
                        RedrawPromptLine(context, input.ToString(), cursor);
                    }
                }
                else if (key.Key == ConsoleKey.LeftArrow)
                {
                    if (cursor > 0)
                    {
                        cursor--;
                        Console.CursorLeft--;
                    }
                }
                else if (key.Key == ConsoleKey.RightArrow)
                {
                    if (cursor < input.Length)
                    {
                        cursor++;
                        Console.CursorLeft++;
                    }
                }
                else if (key.Key == ConsoleKey.UpArrow)
                {
                    if (context.History.Count == 0) continue;
                    if (context.HistoryIndex > 0) context.HistoryIndex--;
                    input.Clear().Append(context.History[context.HistoryIndex]);
                    cursor = input.Length;
                    RedrawPromptLine(context, input.ToString(), cursor);
                }
                else if (key.Key == ConsoleKey.DownArrow)
                {
                    if (context.History.Count == 0) continue;
                    if (context.HistoryIndex < context.History.Count - 1) context.HistoryIndex++;
                    else
                    {
                        context.HistoryIndex = context.History.Count;
                        input.Clear();
                        cursor = 0;
                        RedrawPromptLine(context, "", 0);
                        continue;
                    }

                    input.Clear().Append(context.History[context.HistoryIndex]);
                    cursor = input.Length;
                    RedrawPromptLine(context, input.ToString(), cursor);
                }
                else
                {
                    input.Insert(cursor, key.KeyChar);
                    cursor++;
                    RedrawPromptLine(context, input.ToString(), cursor);
                }
            }
        }

        private static void RedrawPromptLine(ShellContext context, string text, int cursor)
        {
            int top = Console.CursorTop;

            Console.SetCursorPosition(0, top);

            string virtualPath = GetVirtualPath(context);

            string prompt = $"$NShell [{virtualPath}] >> ";

            Console.Write(new string(' ', prompt.Length + Math.Max(text.Length, Console.WindowWidth - prompt.Length)));
            Console.SetCursorPosition(0, top);

            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write("$");
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.Write("NShell ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write($"[{virtualPath}] ");
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.Write(">> ");
            Console.ResetColor();

            Console.Write(text);

            Console.SetCursorPosition(prompt.Length + cursor, top);
        }
    }
}