using Core.Interfaces;
using Core.Models;

namespace Core.Tokens
{
    public class ShiftLeftLogical : IInstruction
    {
        public Register R1 { get; }
        public Register R2 { get; }

        public int Value { get; }

        public ShiftLeftLogical(Register r1, Register r2, int value)
        {
            R1 = r1;
            R2 = r2;
            Value = value;
        }
        
        public override string ToString()
        {
            return $"sll {R1.Name()}, {R2.Name()}, {Value}";
        }
    }
}