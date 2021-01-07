using System;
using System.IO;
using System.Linq;
using Core.Logic;
using Core.Parser;
using FParsec;
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

            Console.WriteLine(result.Status == ReplyStatus.Ok);

            if (result.Status == ReplyStatus.Ok)
            {
                foreach (var instruction in result.Result)
                {
                     //Console.WriteLine(instruction);
                }
            }
            
            new MipsRuntime(result.Result.ToList()).Process();
        }
    }
}