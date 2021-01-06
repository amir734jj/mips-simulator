using Core.Interfaces;
using Core.Models;

namespace Core.Tokens
{
    public class LoadImmediate : IInstruction
    {
        public int Value { get; }

        public Register R1 { get; }

        public LoadImmediate(Register r1, int value)
        {
            R1 = r1;
            Value = value;
        }
        
        public override string ToString()
        {
            return $"li {R1.Name()}, {Value}";
        }
    }
}