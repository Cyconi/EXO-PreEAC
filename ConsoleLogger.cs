using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleLogger
{
    internal class CLog
    {
        internal static void L(string MessageToLog, ConsoleColor NameColor = ConsoleColor.DarkRed, ConsoleColor TextColor = ConsoleColor.White, ConsoleColor MidColor = ConsoleColor.DarkRed)
        {
            System.Console.ForegroundColor = NameColor;
            System.Console.Write("[EXO]");
            System.Console.ForegroundColor = MidColor;
            System.Console.Write(" [~>] ");

            System.Console.ForegroundColor = TextColor;
            System.Console.WriteLine(MessageToLog);
            System.Console.ResetColor();
        }

        internal static void S(string MessageToLog, ConsoleColor NameColor = ConsoleColor.DarkRed, ConsoleColor TextColor = ConsoleColor.DarkRed, ConsoleColor MidColor = ConsoleColor.DarkRed)
        {
            System.Console.ForegroundColor = NameColor;
            System.Console.Write("[EXO]");
            System.Console.ForegroundColor = MidColor;
            System.Console.Write(" [~>] ");

            System.Console.ForegroundColor = TextColor;
            System.Console.WriteLine(MessageToLog);
            System.Console.ResetColor();
        }

        internal static void E(string MessageToLog, ConsoleColor NameColor = ConsoleColor.DarkBlue, ConsoleColor TextColor = ConsoleColor.Red, ConsoleColor MidColor = ConsoleColor.Red)
        {

            System.Console.ForegroundColor = NameColor;
            System.Console.Write("[EXO]");
            System.Console.ForegroundColor = MidColor;
            System.Console.Write(" [~>] ");
            System.Console.ForegroundColor = ConsoleColor.Red;
            System.Console.Write(" [ERROR] ");

            System.Console.ForegroundColor = TextColor;
            System.Console.WriteLine(MessageToLog);
            System.Console.ResetColor();
        }
        public static void E(Exception ex)
        {
            string stack = ex.StackTrace;
            string source = ex.Source;
            string message = ex.Message;
            string LF = Environment.NewLine;

            CLog.E($"\n============ERROR============ \nTIME: {DateTime.Now.ToString("HH:mm.fff", System.Globalization.CultureInfo.InvariantCulture)} \nERROR MESSAGE: {message} \nLAST INSTRUCTIONS: {stack} \nFULL ERROR: {ex.ToString()} \n=============END=============\n");
        }
    }
}

	
