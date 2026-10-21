using System;

namespace SistemAkademik.Utils
{
    public static class ConsoleHelper
    {
        public static void PrintHeader(string title)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("================================================================================");
            Console.WriteLine($"   {title}");
            Console.WriteLine("================================================================================");
            Console.ResetColor();
        }

        public static void PrintSuccess(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"[✓] {message}");
            Console.ResetColor();
        }

        public static void PrintWarning(string message)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"[!] {message}");
            Console.ResetColor();
        }

        public static void PrintError(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"[✗] {message}");
            Console.ResetColor();
        }

        public static string ReadMandatoryString(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                var line = Console.ReadLine();
                var input = line?.Trim('\uFEFF', ' ', '\r', '\n', '\t') ?? string.Empty;
                if (!string.IsNullOrWhiteSpace(input))
                {
                    return input;
                }
                PrintError("Input tidak boleh kosong. Silakan ulangi.");
            }
        }

        public static string ReadOptionalString(string prompt, string defaultValue)
        {
            Console.Write($"{prompt} (default: {defaultValue}): ");
            var line = Console.ReadLine();
            var input = line?.Trim('\uFEFF', ' ', '\r', '\n', '\t');
            return string.IsNullOrWhiteSpace(input) ? defaultValue : input;
        }

        public static int ReadInt(string prompt, int min, int max)
        {
            while (true)
            {
                Console.Write(prompt);
                var line = Console.ReadLine();
                var input = line?.Trim('\uFEFF', ' ', '\r', '\n', '\t');
                if (int.TryParse(input, out var result) && result >= min && result <= max)
                {
                    return result;
                }
                PrintError($"Input harus berupa bilangan bulat antara {min} dan {max}.");
            }
        }

        public static void Pause()
        {
            if (Console.IsInputRedirected) return;
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write("Tekan [Enter] untuk melanjutkan...");
            Console.ResetColor();
            Console.ReadLine();
        }

        public static void Clear()
        {
            if (!Console.IsOutputRedirected)
            {
                Console.Clear();
            }
        }
    }
}
