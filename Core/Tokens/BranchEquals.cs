using Core.Interfaces;
using Core.Models;

namespace Core.Tokens
{
    public class BranchEquals : IInstruction
    {
        public Register R1 { get; }
        
        public Register R2 { get; }
        
        public string Value { get; }

        public BranchEquals(Register r1, Register r2, string value)
        {
            R1 = r1;
            R2 = r2;
            Value = value;
        }
        
        public override string ToString()
        {
            return $"beq {R1.Name()}, {R2.Name()}, {Value}";
        }
    }
}