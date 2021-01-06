using Core.Interfaces;
using Core.Models;

namespace Core.Tokens
{
    public class Move : IInstruction
    {
        public Register R2 { get; }

        public Register R1 { get; }

        public Move(Register r1, Register r2)
        {
            R1 = r1;
            R2 = r2;
        }
        
        public override string ToString()
        {
            return $"move {R1.Name()}, {R2.Name()}";
        }
    }
}