using System;
using System.Runtime.InteropServices;

namespace HelloWorld
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("=================================================");
            Console.WriteLine("        PBKK Pertemuan 2 - .NET Framework        ");
            Console.WriteLine("               Latihan 1: Hello World            ");
            Console.WriteLine("=================================================");
            Console.WriteLine();
            Console.WriteLine("Hello, World! Selamat Datang di Ekosistem .NET!");
            Console.WriteLine();
            Console.WriteLine("--- Informasi Lingkungan Runtime .NET (CLR) ---");
            Console.WriteLine($"• .NET Version            : {Environment.Version}");
            Console.WriteLine($"• Framework Description   : {RuntimeInformation.FrameworkDescription}");
            Console.WriteLine($"• OS Description          : {RuntimeInformation.OSDescription}");
            Console.WriteLine($"• OS Architecture         : {RuntimeInformation.OSArchitecture}");
            Console.WriteLine($"• Process Architecture    : {RuntimeInformation.ProcessArchitecture}");
            Console.WriteLine($"• Current Managed Thread  : {Environment.CurrentManagedThreadId}");
            Console.WriteLine($"• Machine Name            : {Environment.MachineName}");
            Console.WriteLine($"• User Domain / Name      : {Environment.UserDomainName}\\{Environment.UserName}");
            Console.WriteLine();
            Console.WriteLine("=================================================");
            Console.WriteLine("Program berhasil dieksekusi dengan baik.");
        }
    }
}
