using Core.Interfaces;
using Core.Models;

namespace Core.Tokens
{
    public class BranchLessThanOrEqualsZero : IInstruction
    {
        public Register R1 { get; }

        public string Value { get; }

        public BranchLessThanOrEqualsZero(Register r1, string value)
        {
            R1 = r1;
            Value = value;
        }
        
        public override string ToString()
        {
            return $"blez {R1.Name()}, {Value}";
        }
    }
}