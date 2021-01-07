using Core.Interfaces;
using Core.Models;

namespace Core.Tokens
{
    public class BranchLessThanZero : IInstruction
    {
        public Register R1 { get; }

        public string Value { get; }

        public BranchLessThanZero(Register r1, string value)
        {
            R1 = r1;
            Value = value;
        }
        
        public override string ToString()
        {
            return $"bltz {R1.Name()}, {Value}";
        }
    }
}