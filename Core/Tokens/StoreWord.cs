using Core.Interfaces;
using Core.Models;

namespace Core.Tokens
{
    public class StoreWord : IInstruction
    {
        public Register R1 { get; }
        
        public int Offset { get; }

        public Register R2 { get; }

        public StoreWord(Register r1, int offset, Register r2)
        {
            R1 = r1;
            Offset = offset;
            R2 = r2;
        }
        
        public override string ToString()
        {
            return $"sw {R1.Name()}, {Offset}({R2.Name()})";
        }
    }
}