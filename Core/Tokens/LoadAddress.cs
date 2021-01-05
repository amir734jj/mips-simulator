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
    }
}