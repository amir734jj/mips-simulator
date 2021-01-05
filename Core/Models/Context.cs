using System.Collections.Immutable;
using Core.Interfaces;

namespace Core.Models
{
    public class Context
    {
        public ImmutableStack<IInstruction> Instructions { get; }
        
        public ImmutableDictionary<Register, int> Registers { get; }
        
        public ImmutableDictionary<int, object> Memory { get; }

        public Context(ImmutableStack<IInstruction> instructions, ImmutableDictionary<Register, int> registers, ImmutableDictionary<int, object> memory)
        {
            Instructions = instructions;
            Registers = registers;
            Memory = memory;
        }
    }
}