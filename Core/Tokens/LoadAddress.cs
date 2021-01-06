using Core.Interfaces;
using Core.Models;

namespace Core.Tokens
{
    public class LoadAddress : IInstruction
    {
        public Register R1 { get; }
        
        public string Label { get; }

        public LoadAddress(Register r1, string label)
        {
            R1 = r1;
            Label = label;
        }
        
        public override string ToString()
        {
            return $"li {R1.Name()}, {Label}";
        }
    }
}