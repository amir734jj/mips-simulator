using System;
using Core.Parser;
using Pidgin;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            const string code = "move $t0, $t0 \n" + 
                                "li $t0 555";

            var result = new MipsParser().ProgramP.Parse(code);

            Console.WriteLine("Hello World!");
        }
    }
}