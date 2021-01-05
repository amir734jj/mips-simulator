using System;
using System.IO;
using Core.Parser;
using FParsec.CSharp;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var projectDirectory = Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.FullName;

            var code = File.ReadAllText(Path.Join(projectDirectory, "code.s"));

            var result = new MipsParser().ProgramP.ParseString(code);

            Console.WriteLine("Hello World!");
        }
    }
}