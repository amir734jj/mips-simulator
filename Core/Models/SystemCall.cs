using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
                    var str = Encoding.UTF8.GetString(context.Memory.Skip(context.Registers.GetValueOrDefault(Register.V0)).TakeWhile(b => b != 0).ToArray());
                    Console.WriteLine(str);
                    break;
            }

            return context;
        }
    }
}