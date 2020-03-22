using System.Collections.Immutable;

namespace Core.Models
{
    public class Context
    {
        public ImmutableStack<Instruction> Instructions { get; }
        
        public ImmutableDictionary<Register, int> Registers { get; }
        
        public ImmutableDictionary<int, object> Memory { get; }

        public Context(ImmutableStack<Instruction> instructions, ImmutableDictionary<Register, int> registers, ImmutableDictionary<int, object> memory)
        {
            Instructions = instructions;
            Registers = registers;
            Memory = memory;
        }
    }
}