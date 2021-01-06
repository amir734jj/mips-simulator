using Core.Interfaces;
using Core.Models;

namespace Core.Tokens
{
    public class Add : IInstruction
    {
        public Register R2 { get; }
        public Register R3 { get; }

        public Register R1 { get; }

        public Add(Register r1, Register r2, Register r3)
        {
            R1 = r1;
            R2 = r2;
            R3 = r3;
        }
        
        public override string ToString()
        {
            return $"add {R1.Name()}, {R2.Name()}, {R3.Name()}";
        }
    }
}