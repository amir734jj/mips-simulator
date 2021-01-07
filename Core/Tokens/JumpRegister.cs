using Core.Interfaces;
using Core.Models;

namespace Core.Tokens
{
    public class JumpRegister : IInstruction
    {
        public Register R1 { get; }

        public JumpRegister(Register r1)
        {
            R1 = r1;
        }
        
        public override string ToString()
        {
            return $"jr {R1.Name()}";
        }
    }
}