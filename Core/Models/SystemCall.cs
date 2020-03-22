using System;
using System.Collections.Generic;

namespace Core.Models
{
    public class SystemCall : Instruction
    {
        public int Number { get; }
        
        public SystemCall(int number)
        {
            Number = number;
        }

        public static string Name { get; } = "syscall";

        public override Context Pipeline(Context context)
        {
            switch (Number)
            {
                case 5:
                    Console.WriteLine(context.Memory.GetValueOrDefault(context.Registers.GetValueOrDefault(Register.V0), ""));
                    break;
            }

            return context;
        }
    }
}