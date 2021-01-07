using Core.Interfaces;
using Core.Models;

namespace Core.Tokens
{
    public class LoadWord : IInstruction
    {
        public Register R1 { get; }
        
        public int Offset { get; }

        public Register R2 { get; }

        public LoadWord(Register r1, int offset, Register r2)
        {
            R1 = r1;
            Offset = offset;
            R2 = r2;
        }
        
        public override string ToString()
        {
            return $"lw {R1.Name()}, {Offset}({R2.Name()})";
        }
    }
}